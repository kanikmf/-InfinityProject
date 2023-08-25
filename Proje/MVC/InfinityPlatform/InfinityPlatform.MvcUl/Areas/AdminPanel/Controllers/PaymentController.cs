using InfinityPlatform.MvcUI.ApiServices;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Filters;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.ApiTypes;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Payment;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class PaymentController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHost;
        public PaymentController(IHttpApiService apiService, IWebHostEnvironment webHost)
        {
            _apiService = apiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {

            var response =
              await _apiService.GetData<ResponseBody<List<PaymentItem>>>("/payments");

            return View(response.Data);

        }

        [HttpPost]
        public async Task<IActionResult> Save(NewPaymentDto dto)
        {


            var postData = new
            {
                OrderID = dto.OrderID,
                PaymentDate = dto.PaymentDate,
                Amount = dto.Amount,
                PaymentMethod = dto.PaymentMethod,


            };

            var response =
              await _apiService.PostData<ResponseBody<PaymentItem>>("/payments", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Ödemeler Başarıyla Kaydedildi" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Ödemeler Başarıyla Kaydedildi" });


            var errorMessages = string.Empty;

            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br />";
            }

            return Json(new
            {
                IsSuccess = false,
                Message = $"Ödemeler Kaydedilemedi <br /> {errorMessages}"
            });


        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response =
              await _apiService.DeleteData($"/payments/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Ödemeler Silindi" });

            return Json(new { IsSuccess = false, Message = "Ödemeler Silinemedi" });
        }
        [HttpPost]
        public async Task<IActionResult> GetPayment(int id)
        {
            var response =
              await _apiService.GetData<ResponseBody<PaymentItem>>($"/payments/{id}");

            return Json(new
            {
                Amount = response.Data.Amount,
                PaymentDate = response.Data.PaymentDate,
                OrderID = response.Data.OrderID,
                PaymentMethod = response.Data.PaymentMethod,
                PaymentID = response.Data.PaymentID
            });

        }
    }
}