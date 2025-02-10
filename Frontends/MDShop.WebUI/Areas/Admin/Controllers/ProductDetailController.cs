using MDShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/ProductDetail")]
    public class ProductDetailController : Controller {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductDetailController(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        [Route("UpdateProductDetail/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateProductDetail(string id) {
            ProductDetailViewbagList("Ürün Bilgisi Güncelleme");
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/ProductDetails/GetByProductIdProductDetail?id=" + id);
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDetailDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [Route("UpdateProductDetail/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto) {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDetailDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PutAsync("https://localhost:7070/api/ProductDetails/", stringContent);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
            }
            return View();
        }

        void ProductDetailViewbagList(string v4) {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün İşlemleri";
            ViewBag.v4 = v4;

        }
    }
}
