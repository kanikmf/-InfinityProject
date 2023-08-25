using InfinityPlatform.MvcUI.ApiServices;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Filters;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.ApiTypes;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Departman;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Project;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class ProjectController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHost;
        public ProjectController(IHttpApiService apiService, IWebHostEnvironment webHost)
        {
            _apiService = apiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {
            //servise bağlan kategorileri çek view e model olarak gönder

            var response =
              await _apiService.GetData<ResponseBody<List<ProjectItem>>>("/projects");

            return View(response.Data);

        }

        [HttpPost]
        public async Task<IActionResult> Save(NewProjectDto dto)
        {


            var postData = new
            {
                ProjectName = dto.ProjectName,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,


            };

            var response =
              await _apiService.PostData<ResponseBody<ProjectItem>>("/projects", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Projeler Başarıyla Kaydedildi" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Projeler Başarıyla Kaydedildi" });


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
              await _apiService.DeleteData($"/projects/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Projeler Silindi" });

            return Json(new { IsSuccess = false, Message = "Projeler Silinemedi" });
        }
        [HttpPost]
        public async Task<IActionResult> GetProject(int id)
        {
            var response =
              await _apiService.GetData<ResponseBody<ProjectItem>>($"/projects/{id}");

            return Json(new
            {
                ProjectName = response.Data.ProjectName,
                StartDate = response.Data.StartDate,
                Description = response.Data.Description,
                ProjectID = response.Data.ProjectID,
                EndDate = response.Data.EndDate
            });

        }
    }
}