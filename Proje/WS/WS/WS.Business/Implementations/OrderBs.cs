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
using WS.Model.Dtos.Order;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class OrderBs : IOrderBs
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;

        public OrderBs(IOrderRepository repo, IMapper mapper)
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

        public async Task<ApiResponse<OrderGetDto>> GetByIdAsync(int orderId, params string[] includeList)
        {
            if (orderId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük olmalıdır");

            var order = await _repo.GetByIdAsync(orderId, includeList);
            if (order == null)
                throw new NotFoundException("Bu id li sipariş bulunamadı");
            var dto = _mapper.Map<OrderGetDto>(order);

            return ApiResponse<OrderGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<OrderGetDto>>> GetOrdersAsync(params string[] includeList)
        {
            var orders = await _repo.GetAllAsync(p => p.IsActive == true, includeList: includeList);
            if (orders.Count > 0)
            {

                var returnList = _mapper.Map<List<OrderGetDto>>(orders);

                return ApiResponse<List<OrderGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }
            throw new NotFoundException("kaynak bulunamadı");
        }

        public async Task<ApiResponse<List<OrderGetDto>>> GetOrdersByTotalAmountAsync(decimal min, decimal max, params string[] includeList)
        {
            if (min > max)
                throw new BadRequestException("min değer maxdan büyük olamaz");

            if (min < 0 || max < 0)
                throw new BadRequestException("fiyatlar pozitif değer olmalıdır");

            var orders = await _repo.GetTotalAmountAsync(min, max, includeList);

            if (orders.Count > 0)
            {
                var returnList = _mapper.Map<List<OrderGetDto>>(orders);
                return ApiResponse<List<OrderGetDto>>.Success(200, returnList);
            }
            throw new NotFoundException("kaynak bulunamadı");
        }

        public async Task<ApiResponse<Order>> InsertAsync(OrderPostDto dto)
        {
            if (dto.TotalAmount <= 0)
                throw new BadRequestException("ürün fiyatı pozitif bir değer olmalıdır");

            var entity = _mapper.Map<Order>(dto);
            entity.IsActive = true;

            var insertedOrder = await _repo.InsertAsync(entity);

            return ApiResponse<Order>.Success(StatusCodes.Status201Created, insertedOrder);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(OrderPutDto dto)
        {

            if (dto.OrderID < 0)
            {
                throw new BadRequestException("Id pozitif bir değer olmalıdır");
            }

            if (dto.TotalAmount <= 0)
            {
                throw new BadRequestException("Toplam tutar pozitif bir değer olmalıdır");
            }
            var entity = _mapper.Map<Order>(dto);
            entity.IsActive = true;

            await _repo.UpdateAsync(entity);
            entity.IsActive = true;

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
