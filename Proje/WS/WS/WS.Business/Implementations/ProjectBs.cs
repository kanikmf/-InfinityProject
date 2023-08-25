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
using WS.Model.Dtos.Project;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class ProjectBs : IProjectBs
    {
        private readonly IProjectRepository _repo;
        private readonly IMapper _mapper;

        public ProjectBs(IProjectRepository repo, IMapper mapper)
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

        public async Task<ApiResponse<ProjectGetDto>> GetByIdAsync(int projectId, params string[] includeList)
        {
            if (projectId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük olmalıdır");

            var project = await _repo.GetByIdAsync(projectId, includeList);
            if (project == null)
                throw new NotFoundException("bu id li proje bulunamadı");
            var dto = _mapper.Map<ProjectGetDto>(project);

            return ApiResponse<ProjectGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<ProjectGetDto>>> GetProjectsAsync(params string[] includeList)
        {
            var projects = await _repo.GetAllAsync(p => p.IsActive == true, includeList: includeList);
            if (projects.Count > 0)
            {

                var returnList = _mapper.Map<List<ProjectGetDto>>(projects);

                return ApiResponse<List<ProjectGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }
            throw new NotFoundException("kaynak bulunamadı");
        }

        public async Task<ApiResponse<List<ProjectGetDto>>> GetProjectsByBudgetAsync(decimal min, decimal max, params string[] includeList)
        {
            if (min > max)
                throw new BadRequestException("min değer maxdan büyük olamaz");

            if (min < 0 || max < 0)
                throw new BadRequestException("Bütçe pozitif değer olmalıdır");

            var projects = await _repo.GetByBudgetAsync(min, max, includeList);

            if (projects.Count > 0)
            {
                var returnList = _mapper.Map<List<ProjectGetDto>>(projects);
                return ApiResponse<List<ProjectGetDto>>.Success(200, returnList);
            }
            throw new NotFoundException("kaynak bulunamadı");
        }

        public async Task<ApiResponse<Project>> InsertAsync(ProjectPostDto dto)
        {
            if (dto.Budget <= 0)
                throw new BadRequestException("ürün fiyatı pozitif bir değer olmalıdır");

            var entity = _mapper.Map<Project>(dto);
            entity.IsActive = true;

            var insertedProject = await _repo.InsertAsync(entity);

            return ApiResponse<Project>.Success(StatusCodes.Status201Created, insertedProject);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(ProjectPutDto dto)
        {

            if (dto.ProjectID < 0)
            {
                throw new BadRequestException("Id pozitif bir değer olmalıdır");
            }

            if (dto.Budget <= 0)
            {
                throw new BadRequestException("Bütçe pozitif bir değer olmalıdır");
            }
            var entity = _mapper.Map<Project>(dto);
            entity.IsActive = true;

            await _repo.UpdateAsync(entity);
            entity.IsActive = true;

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
