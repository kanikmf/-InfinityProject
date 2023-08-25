using InfinityPlatform.MvcUI.ApiServices;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Filters;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.ApiTypes;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Departman;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Supplier;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Controllers
{

    [Area("AdminPanel")]
    [SessionControlAspect]
    public class SupplierController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHost;
        public SupplierController(IHttpApiService apiService, IWebHostEnvironment webHost)
        {
            _apiService = apiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {
            //servise bağlan kategorileri çek view e model olarak gönder

            var response =
              await _apiService.GetData<ResponseBody<List<SupplierItem>>>("/suppliers");

            return View(response.Data);

        }

        [HttpPost]
        public async Task<IActionResult> Save(NewSupplierDto dto)
        {


            var postData = new
            {
                SupplierName = dto.SupplierName,
                ContactPerson = dto.ContactPerson,
                ContactEmail = dto.ContactEmail,
                ContactPhone = dto.ContactPhone,


            };

            var response =
              await _apiService.PostData<ResponseBody<SupplierItem>>("/suppliers", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Tedarikçi Başarıyla Kaydedildi" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Tedarikçi Başarıyla Kaydedildi" });


            var errorMessages = string.Empty;

            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br />";
            }

            return Json(new
            {
                IsSuccess = false,
                Message = $"Satışlar Kaydedilemedi <br /> {errorMessages}"
            });


        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response =
              await _apiService.DeleteData($"/suppliers/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Tedarikçi Silindi" });

            return Json(new { IsSuccess = false, Message = "Tedarikçi Silinemedi" });
        }
        [HttpPost]
        public async Task<IActionResult> GetSupplier(int id)
        {
            var response =
              await _apiService.GetData<ResponseBody<SupplierItem>>($"/suppliers/{id}");

            return Json(new
            {
                SupplierName = response.Data.SupplierName,
                ContactPerson = response.Data.ContactPerson,
                ContactEmail = response.Data.ContactEmail,
                SupplierID = response.Data.SupplierID,
                ContactPhone = response.Data.ContactPhone
            });

        }
    }
}