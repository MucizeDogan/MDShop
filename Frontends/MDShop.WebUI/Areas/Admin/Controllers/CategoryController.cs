using MDShop.DtoLayer.CatalogDtos.CategoryDto;
using MDShop.WebUI.Services.CatalogServices.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("Admin/Category")]
    public class CategoryController : Controller {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICategoryService _categoryService;

        public CategoryController(IHttpClientFactory httpClientFactory, ICategoryService categoryService) {
            _httpClientFactory = httpClientFactory;
            _categoryService = categoryService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index() {
            try {
                CategoryViewbagList("Kategori Listesi");
                var values = await _categoryService.GetAllCategoryAsync();
                return View(values);
            } catch (Exception ex) {
                TempData["Error"] = "Kategoriler yüklenirken bir hata oluştu!";
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
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
            //DÖNÜŞÜMDEN ÖNCEKİ HALİ
            /*
             * ar client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PostAsync("https://localhost:7070/api/Categories", stringContent);
            // Parametre olarak gönderilen değeri (createCategoryDto) aldık json a dönüştürdük. Sonra bu değeri content olarak atadık. Neyin content olarak atandığı -> jsonData, bu atanan değerin dil desteği -> Encoding.UTF8, bunun mediatr ü ne olduğu -> application/json
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
            */

            //DÖNÜŞÜMDEN SONRA
            try {
                await _categoryService.CreateCategoryAsync(createCategoryDto);
                TempData["Success"] = "Kategori başarıyla eklendi!";
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            } catch (Exception ex) {
                TempData["Error"] = "Kategori eklenirken bir hata oluştu!";
                return View(createCategoryDto);
            }
        }

        [Route("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(string id) {
            //ESKİ KOD(Dönüşümden önce)
            /*
            var client = _httpClientFactory.CreateClient();
            var res = await client.DeleteAsync("https://localhost:7070/api/Categories?id=" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
            */

            //Yeni KOD
            try {
                await _categoryService.DeleteCategoryAsync(id);
                TempData["Success"] = "Kategori başarıyla silindi!";
            } catch (Exception ex) {
                TempData["Error"] = "Kategori silinirken bir hata oluştu!";
            }
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }

        [Route("UpdateCategory/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id) {
            //DÖNÜŞÜMDEN ÖNCE
            /*
            CategoryViewbagList("Kategori Güncelleme");
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/Categories/" + id);
            if(res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);
                return View(values);
            }
            return View();
            */

            //DÖNÜŞÜMDEN SONRA
            try {
                CategoryViewbagList("Kategori Güncelleme");
                var value = await _categoryService.GetByIdCategoryAsync(id);
                return View(value);
            } catch (Exception ex) {
                TempData["Error"] = "Kategori yüklenirken bir hata oluştu!";
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
        }

        [Route("UpdateCategory/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto) {
            //DÖNÜŞÜMDEN ÖNCE
            /*
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PutAsync("https://localhost:7070/api/Categories/", stringContent);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
            */

            //DÖNÜŞÜMDEN SONRA
            try {
                await _categoryService.UpdateCategoryAsync(updateCategoryDto);
                TempData["Success"] = "Kategori başarıyla güncellendi!";
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            } catch (Exception ex) {
                TempData["Error"] = "Kategori güncellenirken bir hata oluştu!";
                return View(updateCategoryDto);
            }
        }

        void CategoryViewbagList(string v4) {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori İşlemleri";
            ViewBag.v4 = v4;
            
        }
    }
}
