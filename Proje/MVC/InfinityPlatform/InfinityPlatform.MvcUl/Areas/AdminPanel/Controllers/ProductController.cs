using InfinityPlatform.MvcUI.ApiServices;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Filters;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.ApiTypes;
using InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Product;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class ProductController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHost;
        public ProductController(IHttpApiService apiService, IWebHostEnvironment webHost)
        {
            _apiService = apiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {

            var response =
              await _apiService.GetData<ResponseBody<List<ProductItem>>>("/products");

            return View(response.Data);

        }

        [HttpPost]
        public async Task<IActionResult> Save(NewProductDto dto, IFormFile productImage)
        {


            if (productImage == null)
                return Json(new { IsSuccess = false, Message = "Kategori resmi seçilmelidir" });

            if (!productImage.ContentType.StartsWith("image/"))
                return Json(new { IsSuccess = false, Message = "Sadece resim dosyası seçilmelidir" });

            if (productImage.Length > 1024 * 250)
                return Json(new { IsSuccess = false, Message = "Dosya büyüklüğü en fazla 250 KB olabilir" });




            var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(productImage.FileName)}";
            var uploadPath = $@"{_webHost.WebRootPath}/adminPanel/productImages/{randomFileName}";

            using (var fs = new FileStream(uploadPath, FileMode.Create))
            {
                await productImage.CopyToAsync(fs);
            }



            var postData = new
            {

                ProductName = dto.ProductName,
                Description = dto.Description,
                PhotoPath = $@"/adminPanel/productImages/{randomFileName}"

            };

            var response =
              await _apiService.PostData<ResponseBody<ProductItem>>("/products", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Ürün Başarıyla Kaydedildi" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Ürün Başarıyla Kaydedildi" });


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
              await _apiService.DeleteData($"/products/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Ürün Silindi" });

            return Json(new { IsSuccess = false, Message = "Ürün Silinemedi" });
        }

        [HttpPost]
        public async Task<IActionResult> GetProduct(int id)
        {
            var response =
              await _apiService.GetData<ResponseBody<ProductItem>>($"/products/{id}");

            return Json(new
            {
                ProductName = response.Data.ProductName,
                Description = response.Data.Description,
                PhotoPath = response.Data.PhotoPath,
                ProductID = response.Data.ProductID
            });

        }
    }
}
