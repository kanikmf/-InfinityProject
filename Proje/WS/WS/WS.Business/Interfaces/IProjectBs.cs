using Infrastructure.Utilities.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Project;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IProjectBs
    {
        Task<ApiResponse<Project>> InsertAsync(ProjectPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(ProjectPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<ProjectGetDto>> GetByIdAsync(int projectId, params string[] includeList);
        Task<ApiResponse<List<ProjectGetDto>>> GetProjectsAsync(params string[] includeList);
        Task<ApiResponse<List<ProjectGetDto>>> GetProjectsByBudgetAsync(decimal min, decimal max, params string[] includeList);
    }
}
