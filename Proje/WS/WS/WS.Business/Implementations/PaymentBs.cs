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
using WS.Model.Dtos.Payment;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class PaymentBs : IPaymentBs
    {
        private readonly IPaymentRepository _repo;
        private readonly IMapper _mapper;

        public PaymentBs(IPaymentRepository repo, IMapper mapper)
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

        public async Task<ApiResponse<PaymentGetDto>> GetByIdAsync(int paymentId, params string[] includeList)
        {
            if (paymentId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük olmalıdır");

            var payment = await _repo.GetByIdAsync(paymentId, includeList);
            if (payment == null)
                throw new NotFoundException("bu id li ödeme seçeneği bulunamadı");
            var dto = _mapper.Map<PaymentGetDto>(payment);

            return ApiResponse<PaymentGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<PaymentGetDto>>> GetPaymentsAsync(params string[] includeList)
        {
            var payments = await _repo.GetAllAsync(p => p.IsActive == true, includeList: includeList);
            if (payments.Count > 0)
            {

                var returnList = _mapper.Map<List<PaymentGetDto>>(payments);

                return ApiResponse<List<PaymentGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }
            throw new NotFoundException("kaynak bulunamadı");
        }

        public async Task<ApiResponse<List<PaymentGetDto>>> GetPaymentsByAmountAsync(decimal min, decimal max, params string[] includeList)
        {
            if (min > max)
                throw new BadRequestException("min değer maxdan büyük olamaz");

            if (min < 0 || max < 0)
                throw new BadRequestException("Ödenecek tutar pozitif değer olmalıdır");

            var payments = await _repo.GetAmountAsync(min, max, includeList);

            if (payments.Count > 0)
            {
                var returnList = _mapper.Map<List<PaymentGetDto>>(payments);
                return ApiResponse<List<PaymentGetDto>>.Success(200, returnList);
            }
            throw new NotFoundException("kaynak bulunamadı");
        }

        public async Task<ApiResponse<Payment>> InsertAsync(PaymentPostDto dto)
        {
            if (dto.Amount <= 0)
                throw new BadRequestException("Ödenecek tutar pozitif bir değer olmalıdır");

            var entity = _mapper.Map<Payment>(dto);
            entity.IsActive = true;

            var insertedPayment = await _repo.InsertAsync(entity);

            return ApiResponse<Payment>.Success(StatusCodes.Status201Created, insertedPayment);

        }

        public async Task<ApiResponse<NoData>> UpdateAsync(PaymentPutDto dto)
        {
            if (dto.PaymentID < 0)
            {
                throw new BadRequestException("Id pozitif bir değer olmalıdır");
            }

            if (dto.Amount <= 0)
            {
                throw new BadRequestException("Ödenecek tutar pozitif bir değer olmalıdır");
            }
            var entity = _mapper.Map<Payment>(dto);
            entity.IsActive = true;

            await _repo.UpdateAsync(entity);
            entity.IsActive = true;

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
