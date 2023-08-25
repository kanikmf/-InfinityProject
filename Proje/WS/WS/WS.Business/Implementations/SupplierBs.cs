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
using WS.DataAccess.Implementations.EF.Repositories;
using WS.DataAccess.Interfaces;
using WS.Model.Dtos.Employee;
using WS.Model.Dtos.Supplier;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class SupplierBs : ISupplierBs
    {
        private readonly ISupplierRepository _repo;
        private readonly IMapper _mapper;

        public SupplierBs(ISupplierRepository repo, IMapper mapper)
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

        public async Task<ApiResponse<SupplierGetDto>> GetByIdAsync(int supplierId, params string[] includeList)
        {
            if (supplierId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük olmalıdır");

            var supplier = await _repo.GetByIdAsync(supplierId, includeList);
            if (supplier == null)
                throw new NotFoundException("bu id li tedarikçi bulunamadı");
            var dto = _mapper.Map<SupplierGetDto>(supplier);

            return ApiResponse<SupplierGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<SupplierGetDto>>> GetSuppliersAsync(params string[] includeList)
        {
            var suppliers = await _repo.GetAllAsync(p => p.IsActive == true, includeList: includeList);
            if (suppliers.Count > 0)
            {

                var returnList = _mapper.Map<List<SupplierGetDto>>(suppliers);

                return ApiResponse<List<SupplierGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }
            throw new NotFoundException("kaynak bulunamadı");
        }

        public async Task<ApiResponse<Supplier>> InsertAsync(SupplierPostDto dto)
        {
           

            var entity = _mapper.Map<Supplier>(dto);
            entity.IsActive = true;

            var insertedSupplier = await _repo.InsertAsync(entity);

            return ApiResponse<Supplier>.Success(StatusCodes.Status201Created, insertedSupplier);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(SupplierPutDto dto)
        {
            if (dto.SupplierID < 0)
            {
                throw new BadRequestException("Id pozitif bir değer olmalıdır");
            }

            
            var entity = _mapper.Map<Supplier>(dto);
            entity.IsActive = true;

            await _repo.UpdateAsync(entity);
            entity.IsActive = true;

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
