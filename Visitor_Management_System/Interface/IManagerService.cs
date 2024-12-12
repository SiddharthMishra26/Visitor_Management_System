using Visitor_Management_System.Entities;
using Visitor_Management_System.Models;

namespace Visitor_Management_System.Interface
{
    public interface IManagerService
    {
        Task<ManagerModel> CreateManager(ManagerModel managerModel);
        Task<List<ManagerModel>> GetAllManager();
        Task<ManagerModel> ReadManagerByUId(string UId);
        Task<ManagerModel> UpdateManagerByUId(ManagerModel managerModel);
        Task<string> DeleteManagerByUId(string UId);
    }
}
