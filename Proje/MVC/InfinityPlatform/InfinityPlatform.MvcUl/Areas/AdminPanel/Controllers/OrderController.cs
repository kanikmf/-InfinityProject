using InfinityPlatform.MvcUI.ApiServices;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Filters;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.ApiTypes;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Departman;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Order;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class OrderController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHost;
        public OrderController(IHttpApiService apiService, IWebHostEnvironment webHost)
        {
            _apiService = apiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {

            var response =
              await _apiService.GetData<ResponseBody<List<OrderItem>>>("/orders");

            return View(response.Data);

        }

        [HttpPost]
        public async Task<IActionResult> Save(NewOrderDto dto)
        {


            var postData = new
            {
                ClientID = dto.ClientID,
                OrderDate = dto.OrderDate,
                TotalAmount = dto.TotalAmount,
                Status = dto.Status,


            };

            var response =
              await _apiService.PostData<ResponseBody<OrderItem>>("/orders", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Sipariş Başarıyla Kaydedildi" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Sipariş Başarıyla Kaydedildi" });


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
              await _apiService.DeleteData($"/orders/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Siparişler Silindi" });

            return Json(new { IsSuccess = false, Message = "Siparişler Silinemedi" });
        }
        [HttpPost]
        public async Task<IActionResult> GetOrder(int id)
        {
            var response =
              await _apiService.GetData<ResponseBody<OrderItem>>($"/orders/{id}");

            return Json(new
            {
                ClientID = response.Data.ClientID,
                OrderDate = response.Data.OrderDate,
                TotalAmount = response.Data.TotalAmount,
                Status = response.Data.Status,
                OrderID = response.Data.OrderID
            });

        }
    }
}
