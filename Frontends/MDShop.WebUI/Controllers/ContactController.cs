using MDShop.DtoLayer.CatalogDtos.ContactDtos;
using MDShop.WebUI.Services.CatalogServices.ContactServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Controllers {
    public class ContactController : Controller {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService) {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult Index() {
            ViewBag.path1 = "Ana Sayfa";
            ViewBag.path3 = "";
            ViewBag.path3 = "İletişim";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateContactDto createContactDto) {
            //createContactDto.isRead = false;
            //createContactDto.SendDate = DateTime.Now;
            //var client = _httpClientFactory.CreateClient();
            //var jsonData = JsonConvert.SerializeObject(createContactDto);
            //StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //var res = await client.PostAsync("https://localhost:7070/api/Contacts", stringContent);

            //if (res.IsSuccessStatusCode) {
            //    return RedirectToAction("Index", "Contact");
            //}
            //return View();
            createContactDto.isRead = false;
            createContactDto.SendDate = DateTime.Now;
            await _contactService.CreateContactAsync(createContactDto);
            return RedirectToAction("Index", "Default");
        }
    }
}
