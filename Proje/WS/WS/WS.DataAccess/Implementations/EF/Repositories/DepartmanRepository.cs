using Infrastructure.DataAccess.EF;
using WS.DataAccess.Implementations.EF.Contex;
using WS.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.Implementations.EF.Repositories
{
    public class DepartmanRepository : BaseRepository<Departman, InfinityContext>, IDepartmanRepository
    {
        public async Task<Departman> GetByIdAsync(int departmanId, params string[] includeList)
        {
            return await GetAsync(prd => prd.DepartmanId == departmanId, includeList);
        }
        public async Task<List<Departman>> GetByDescriptionAsync(string description, params string[] includeList)
        {
            return await GetAllAsync(emp => emp.Description == description);
        }
        public async Task<List<Departman>> GetByDepartmanNameAsync(string departmanName, params string[] includeList)
        {
            return await GetAllAsync(emp => emp.DepartmanName == departmanName);
        }
        public async Task<List<Departman>> GetByContactInformationAsync(string contackInformation, params string[] includeList)
        {
            return await GetAllAsync(emp => emp.ContactInformation == contackInformation);
        }
    }
}
