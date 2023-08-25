using InfinityPlatform.MvcUI.ApiServices;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Filters;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.ApiTypes;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Departman;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Expense;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class ExpenseController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHost;
        public ExpenseController(IHttpApiService apiService, IWebHostEnvironment webHost)
        {
            _apiService = apiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {

            var response =
              await _apiService.GetData<ResponseBody<List<ExpenseItem>>>("/expenses");

            return View(response.Data);

        }

        [HttpPost]
        public async Task<IActionResult> Save(NewExpenseDto dto)
        {


            var postData = new
            {
                ExpenseDate = dto.ExpenseDate,
                Amount = dto.Amount,
                Description = dto.Description,
                Category = dto.Category,


            };

            var response =
              await _apiService.PostData<ResponseBody<ExpenseItem>>("/expenses", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Satışlar Başarıyla Kaydedildi" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Satışlar Başarıyla Kaydedildi" });


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
              await _apiService.DeleteData($"/expenses/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Gider Sİlindi" });

            return Json(new { IsSuccess = false, Message = "Gider Silinemedi" });
        }
        [HttpPost]
        public async Task<IActionResult> GetExpense(int id)
        {
            var response =
              await _apiService.GetData<ResponseBody<ExpenseItem>>($"/expenses/{id}");

            return Json(new
            {
                ExpenseDate = response.Data.ExpenseDate,
                Amount = response.Data.Amount,
                Description = response.Data.Description,
                Category = response.Data.Category,
                ExpenseID = response.Data.ExpenseID,
            });

        }
    }
}