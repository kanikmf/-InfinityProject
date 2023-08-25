using Infrastructure.DataAccess;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IDepartmanRepository : IBaseRepository<Departman>
    {
        Task<Departman> GetByIdAsync(int departmanId, params string[] includeList);
        Task<List<Departman>> GetByDescriptionAsync(string description, params string[] includeList);
        Task<List<Departman>> GetByDepartmanNameAsync(string departmanname, params string[] includeList);
        Task<List<Departman>> GetByContactInformationAsync(string contackInformation, params string[] includeList);
    }
}
