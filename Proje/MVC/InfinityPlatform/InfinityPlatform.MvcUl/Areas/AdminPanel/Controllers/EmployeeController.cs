using InfinityPlatform.MvcUI.ApiServices;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Filters;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.ApiTypes;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Client;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Employee;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class EmployeeController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHost;
        public EmployeeController(IHttpApiService apiService, IWebHostEnvironment webHost)
        {
            _apiService = apiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {
            //servise bağlan m  üşterileri çek view e model olarak gönder

            var response =
              await _apiService.GetData<ResponseBody<List<EmployeeItem>>>("/employees");

            return View(response.Data);

        }

        [HttpPost]
        public async Task<IActionResult> Save(NewEmployeeDto dto, IFormFile employeeImage)
        {


            if (employeeImage == null)
                return Json(new { IsSuccess = false, Message = "Kategori resmi seçilmelidir" });

            if (!employeeImage.ContentType.StartsWith("image/"))
                return Json(new { IsSuccess = false, Message = "Sadece resim dosyası seçilmelidir" });

            if (employeeImage.Length > 1024 * 250)
                return Json(new { IsSuccess = false, Message = "Dosya büyüklüğü en fazla 250 KB olabilir" });




            var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(employeeImage.FileName)}";
            var uploadPath = $@"{_webHost.WebRootPath}/adminPanel/employeeImages/{randomFileName}";

            using (var fs = new FileStream(uploadPath, FileMode.Create))
            {
                await employeeImage.CopyToAsync(fs);
            }



            var postData = new
            {

                LastName = dto.LastName,
                FirstName = dto.FirstName,
                PhotoPath = $@"/adminPanel/employeeImages/{randomFileName}"

            };

            var response =
              await _apiService.PostData<ResponseBody<EmployeeItem>>("/employees", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Çalışan Başarıyla Kaydedildi" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Çalışan Başarıyla Kaydedildi" });


            var errorMessages = string.Empty;

            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br />";
            }

            return Json(new
            {
                IsSuccess = false,
                Message = $"Müşteri Kaydedilemedi <br /> {errorMessages}"
            });


        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response =
              await _apiService.DeleteData($"/employees/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Çalışan Silindi" });

            return Json(new { IsSuccess = false, Message = "Çalışan Silinemedi" });
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var response =
              await _apiService.GetData<ResponseBody<EmployeeItem>>($"/employees/{id}");

            return Json(new
            {
                LastName = response.Data.LastName,
                FirstName = response.Data.FirstName,
                PhotoPath = response.Data.PhotoPath,
                EmployeeID = response.Data.EmployeeID
            });

        }
    }
}
