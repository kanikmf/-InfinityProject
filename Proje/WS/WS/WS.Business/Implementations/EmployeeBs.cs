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
using WS.Model.Dtos.Employee;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class EmployeeBs : IEmployeeBs
    {
        private readonly IEmployeeRepository _repo;
        private readonly IMapper _mapper;

        public EmployeeBs (IEmployeeRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id)
        {
           if (id <= 0)
               throw new BadRequestException("id pozitif bir değer olmalıdır");
           var entity =await _repo.GetByIdAsync(id);
           entity.IsActive = false;
           await _repo.UpdateAsync(entity);

           return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            
        }

        public async Task<ApiResponse<EmployeeGetDto>> GetByIdAsync(int employeeId, params string[] includeList)
        {

            if (employeeId <= 0)
                throw new BadRequestException("Çalışan id değeri 0 dan büyük olmalıdır");

                var employee = await _repo.GetByIdAsync(employeeId, includeList);
            if (employee == null)
                throw new NotFoundException("Bu id li çalışan bulunamadı");
                var dto = _mapper.Map<EmployeeGetDto>(employee);

                return ApiResponse<EmployeeGetDto>.Success(StatusCodes.Status200OK, dto);
            

        }

        public async Task<ApiResponse<List<EmployeeGetDto>>> GetEmployeesAsync(params string[] includeList)
        {
          var employees = await _repo.GetAllAsync(p => p.IsActive == true,includeList: includeList);
          if (employees.Count > 0)
          {

              var returnList = _mapper.Map<List<EmployeeGetDto>>(employees);

              return ApiResponse<List<EmployeeGetDto>>.Success(StatusCodes.Status200OK, returnList);
          }
          throw new NotFoundException("kaynak bulunamadı");
        }

        public async Task<ApiResponse<List<EmployeeGetDto>>> GetEmployeesBySalaryAsync(decimal min, decimal max, params string[] includeList)
        {
            if (min > max)
                throw new BadRequestException("min değer maxdan büyük olamaz");

            if (min < 0 || max < 0)
                throw new BadRequestException("fiyatlar pozitif değer olmalıdır");

            var employees = await _repo.GetBySalaryAsync(min, max, includeList);

            if (employees.Count > 0)
            {
                var returnList = _mapper.Map<List<EmployeeGetDto>>(employees);
                return ApiResponse<List<EmployeeGetDto>>.Success(200, returnList);
            }
            throw new NotFoundException("kaynak bulunamadı");
        }

        public async Task<ApiResponse<Employee>> InsertAsync(EmployeePostDto dto)
        {

            

            var entity = _mapper.Map<Employee>(dto);
            entity.IsActive = true;

            var insertedEmployee =await _repo.InsertAsync(entity);

            return ApiResponse<Employee>.Success(StatusCodes.Status201Created, insertedEmployee);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(EmployeePutDto dto)
        {
                if (dto.DepartmanID < 0)
                {
                    throw new BadRequestException("Id pozitif bir değer olmalıdır");
                }
                
                if (dto.Salary <= 0)
                {
                    throw new BadRequestException("Maaş pozitif bir değer olmalıdır");
                }
                var entity = _mapper.Map<Employee>(dto);
            entity.IsActive = true;

            await _repo.UpdateAsync(entity);
                        entity.IsActive = true;

                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
