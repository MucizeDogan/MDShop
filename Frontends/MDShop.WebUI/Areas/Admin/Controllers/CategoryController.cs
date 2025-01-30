using MDShop.DtoLayer.CatalogDtos.CategoryDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/Category")]
    public class CategoryController : Controller {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index() {
            CategoryViewbagList("Kategori Listesi");

            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/Categories"); //isteğin yapılacağı adres (port-catalog servisine gidecek)
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateCategory")]
        public IActionResult CreateCategory() {
            CategoryViewbagList("Yeni Kategori Ekleme");
            return View();
        }

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto) {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PostAsync("https://localhost:7070/api/Categories", stringContent);
            // Parametre olarak gönderilen değeri (createCategoryDto) aldık json a dönüştürdük. Sonra bu değeri content olarak atadık. Neyin content olarak atandığı -> jsonData, bu atanan değerin dil desteği -> Encoding.UTF8, bunun mediatr ü ne olduğu -> application/json
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Category", new {area="Admin"});
            }
            return View();
        }

        [Route("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(string id) {
            var client = _httpClientFactory.CreateClient();
            var res = await client.DeleteAsync("https://localhost:7070/api/Categories?id=" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateCategory/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id) {
            CategoryViewbagList("Kategori Güncelleme");
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/Categories/" + id);
            if(res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [Route("UpdateCategory/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto) {
            CategoryViewbagList("Kategori Güncelleme");
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PutAsync("https://localhost:7070/api/Categories/", stringContent);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
        }

        void CategoryViewbagList(string v4) {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori İşlemleri";
            ViewBag.v4 = v4;
            
        }
    }
}
