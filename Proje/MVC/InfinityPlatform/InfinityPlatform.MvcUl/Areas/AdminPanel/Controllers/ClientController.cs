using InfinityPlatform.MvcUI.ApiServices;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Filters;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.ApiTypes;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Client;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class ClientController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHost;
        public ClientController(IHttpApiService apiService, IWebHostEnvironment webHost)
        {
            _apiService = apiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {
            //servise bağlan m  üşterileri çek view e model olarak gönder

            var response =
              await _apiService.GetData<ResponseBody<List<ClientItem>>>("/clients");

            return View(response.Data);

        }

        [HttpPost]
        public async Task<IActionResult> Save(NewClientDto dto, IFormFile clientImage)
        {
            

            if (clientImage == null)
                return Json(new { IsSuccess = false, Message = "Kategori resmi seçilmelidir" });

            if (!clientImage.ContentType.StartsWith("image/"))
                return Json(new { IsSuccess = false, Message = "Sadece resim dosyası seçilmelidir" });

            if (clientImage.Length > 1024 * 250)
                return Json(new { IsSuccess = false, Message = "Dosya büyüklüğü en fazla 250 KB olabilir" });




            var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(clientImage.FileName)}";
            var uploadPath = $@"{_webHost.WebRootPath}/adminPanel/clientImages/{randomFileName}";

            using (var fs = new FileStream(uploadPath, FileMode.Create))
            {
                await clientImage.CopyToAsync(fs);
            }



            var postData = new
            {

                Address = dto.Address,
                ClientName = dto.ClientName,
                PhotoPath = $@"/adminPanel/clientImages/{randomFileName}"
               
            };

            var response =
              await _apiService.PostData<ResponseBody<ClientItem>>("/clients", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Müşteri Başarıyla Kaydedildi" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Müşteri Başarıyla Kaydedildi" });


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
              await _apiService.DeleteData($"/clients/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Müşteri Silindi" });

            return Json(new { IsSuccess = false, Message = "Müşteri Silinemedi" });
        }

        [HttpPost]
        public async Task<IActionResult> GetClient(int id)
        {
            var response =
              await _apiService.GetData<ResponseBody<ClientItem>>($"/clients/{id}");

            return Json(new
            {
                Address = response.Data.Address,
                ClientName = response.Data.ClientName,
                PhotoPath = response.Data.PhotoPath,
                ClientID = response.Data.ClientID 
            });
             
        }
        
    }

}
