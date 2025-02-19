using MDShop.DtoLayer.CommentDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Controllers {
    public class ProductListController : Controller {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductListController(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index(string id) {
            ViewBag.i = id; // id değerini taşımak için

            ViewBag.path1 = "Ana Sayfa";
            ViewBag.path3 = "Ürünler";
            ViewBag.path3 = "Ürün Listesi";

            return View();
        }

        public IActionResult ProductDetail(string id) {
            ViewBag.x = id;

            ViewBag.path1 = "Ana Sayfa";
            ViewBag.path3 = "Ürün Listesi";
            ViewBag.path3 = "Ürün Detayları";

            return View();
        }

        [HttpGet]
        public async Task<PartialViewResult> AddComment(string id) {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7155/api/Comments/CommentListByProductId/" + id);
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
                return PartialView(values);
            }
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto) {

            createCommentDto.ImageUrl = "https://yt3.ggpht.com/a/AATXAJyk2VmL7NqghohEuPMG3VqdQrP66-UTq98FIQ=s900-c-k-c0xffffffff-no-rj-mo";
            createCommentDto.Rating = 1;
            createCommentDto.CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            createCommentDto.Status = false;
            createCommentDto.ProductId = "678110f2362d598962f803a7";
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCommentDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7155/api/Comments", stringContent);
            if (responseMessage.IsSuccessStatusCode) {
                return RedirectToAction("ProductDetail", "ProductList", new { id = createCommentDto.ProductId });
            }
            return View();
        }
    }
}
