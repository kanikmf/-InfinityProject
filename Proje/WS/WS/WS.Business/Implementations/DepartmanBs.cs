using AutoMapper;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;
using WS.Business.CustomExceptions;
using WS.Business.Interfaces;
using WS.DataAccess.Interfaces;
using WS.Model.Dtos.Departman;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class DepartmanBs : IDepartmanBs
    {
        private readonly IDepartmanRepository _repo;
        private readonly IMapper _mapper;
        public DepartmanBs (IDepartmanRepository repo , IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id)
        {
            {
                if (id <= 0)
                throw new BadRequestException("id pozitif bir değer olmalıdır");
                var entity =await _repo.GetByIdAsync(id);
                entity.IsActive = false;
              await  _repo.UpdateAsync(entity);

                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
        }

        public async Task<ApiResponse<DepartmanGetDto>> GetByIdAsync(int departmanId, params string[] includeList)
        {
           
                if (departmanId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük bir değer olmalıdır.");

                var departman =await _repo.GetByIdAsync(departmanId, includeList);

            if (departman == null)
                throw new NotFoundException("Bu id'ye ait bir departman bulunamadı");

                var dto = _mapper.Map<DepartmanGetDto>(departman);

                return ApiResponse<DepartmanGetDto>.Success(StatusCodes.Status200OK, dto);
            
            
        }

        public async Task<ApiResponse<List<DepartmanGetDto>>> GetDepartmansAsync(params string[] includeList)
        {
            
                var departmans = await _repo.GetAllAsync(p => p.IsActive == true, includeList: includeList);

                if (departmans.Count > 0)
                {
                    var returnList = _mapper.Map<List<DepartmanGetDto>>(departmans);

                    return ApiResponse<List<DepartmanGetDto>>.Success(StatusCodes.Status200OK, returnList);
                }

                return ApiResponse<List<DepartmanGetDto>>.Fail(StatusCodes.Status404NotFound, "kaynak bulunamadı");
            
            
        }

        public async Task<ApiResponse<Departman>> InsertAsync(DepartmanPostDto dto)
        {
            if (dto.DepartmanName.Length < 1)
                throw new BadRequestException("Departman ismi en az 1 karakterden oluşmalıdır");

            var entity = _mapper.Map<Departman>(dto);
            entity.IsActive = true;

            var insertedDepartman = await _repo.InsertAsync(entity);
            
            return ApiResponse<Departman>.Success(StatusCodes.Status201Created, insertedDepartman);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(DepartmanPutDto dto)
        {
            
                if (dto.DepartmanId <= 0)
                
                    throw new BadRequestException("id pozitif bir değer olmalıdır");
                

                if (dto.DepartmanName.Length < 1)
                
                    throw new BadRequestException("Departman adı en az 1 karakterden oluşmalıdır");
                
                var entity = _mapper.Map<Departman>(dto);
            entity.IsActive = true;

            await _repo.UpdateAsync(entity);

                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            
        }
    }
}
