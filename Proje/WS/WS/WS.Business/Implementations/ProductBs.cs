using AutoMapper;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Business.CustomExceptions;
using WS.Business.Interfaces;
using WS.DataAccess.Interfaces;
using WS.Model.Dtos.Product;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class ProductBs : IProductBs
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductBs(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("id pozitif bir değer olmalıdır");
            var entity = await _repo.GetByIdAsync(id);
            entity.IsActive = false;
            await _repo.UpdateAsync(entity);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<ProductGetDto>> GetByIdAsync(int productId, params string[] includeList)
        {

            if (productId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük olmalıdır");

            var product = await _repo.GetByIdAsync(productId, includeList);
            if (product == null)
                throw new NotFoundException("bu id li ürün bulunamadı");
            var dto = _mapper.Map<ProductGetDto>(product);

            return ApiResponse<ProductGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<ProductGetDto>>> GetProductsAsync(params string[] includeList)
        {
            var products = await _repo.GetAllAsync(p => p.IsActive == true, includeList: includeList);
            if (products.Count > 0)
            {

                var returnList = _mapper.Map<List<ProductGetDto>>(products);

                return ApiResponse<List<ProductGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }
            throw new NotFoundException("kaynak bulunamadı");
        }

        public async Task<ApiResponse<List<ProductGetDto>>> GetProductsByUnitPriceAsync(decimal min, decimal max, params string[] includeList)
        {
            if (min > max)
                throw new BadRequestException("min değer maxdan büyük olamaz");

            if (min < 0 || max < 0)
                throw new BadRequestException("Birim fiyatı pozitif değer olmalıdır");

            var products = await _repo.GetByUnitPriceAsync(min, max, includeList);

            if (products.Count > 0)
            {
                var returnList = _mapper.Map<List<ProductGetDto>>(products);
                return ApiResponse<List<ProductGetDto>>.Success(200, returnList);
            }
            throw new NotFoundException("kaynak bulunamadı");
        }

        public async Task<ApiResponse<Product>> InsertAsync(ProductPostDto dto)
        {
            

            var entity = _mapper.Map<Product>(dto);
            entity.IsActive = true;

            var insertedProduct = await _repo.InsertAsync(entity);

            return ApiResponse<Product>.Success(StatusCodes.Status201Created, insertedProduct);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(ProductPutDto dto)
        {
            if (dto.ProductID < 0)
            {
                throw new BadRequestException("Id pozitif bir değer olmalıdır");
            }

            if (dto.UnitPrice <= 0)
            {
                throw new BadRequestException("Birim fiyat pozitif bir değer olmalıdır");
            }
            var entity = _mapper.Map<Product>(dto);
            entity.IsActive = true;

            await _repo.UpdateAsync(entity);
            entity.IsActive = true;

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
