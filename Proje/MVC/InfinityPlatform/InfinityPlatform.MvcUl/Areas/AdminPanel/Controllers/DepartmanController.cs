using Humanizer;
using InfinityPlatform.MvcUI.ApiServices;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Filters;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.ApiTypes;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Departman;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Text.Json;

namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class DepartmanController : Controller
    { 
       private readonly IHttpApiService _apiService;
       private readonly IWebHostEnvironment _webHost;
       public DepartmanController(IHttpApiService apiService, IWebHostEnvironment webHost)
       {
           _apiService = apiService;
           _webHost = webHost;
       }
       public async Task<IActionResult> Index()
       {

           var response =
             await _apiService.GetData<ResponseBody<List<DepartmanItem>>>("/departmans");

           return View(response.Data);

       }

       [HttpPost]
       public async Task<IActionResult> Save(NewDepartmanDto dto)
       {


           var postData = new
           {
               DepartmanName = dto.DepartmanName,
               Description = dto.Description,
               Budget = dto.Budget,
               EmployeeCount = dto.EmployeeCount,


           };

           var response =
             await _apiService.PostData<ResponseBody<DepartmanItem>>("/departmans", JsonSerializer.Serialize(postData));

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
              await _apiService.DeleteData($"/departmans/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Departman Silindi" });

            return Json(new { IsSuccess = false, Message = "Departman Silinemedi" });
        }
        [HttpPost]
        public async Task<IActionResult> GetDepartman(int id)
        {
            var response =
              await _apiService.GetData<ResponseBody<DepartmanItem>>($"/departmans/{id}");

            return Json(new
            {
                DepartmanName = response.Data.DepartmanName,
                Budget = response.Data.Budget,
                Description = response.Data.Description,
                DepartmanId = response.Data.DepartmanId,
                EmployeeCount = response.Data.EmployeeCount
            });

        }
    }
}
