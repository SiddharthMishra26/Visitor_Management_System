using Visitor_Management_System.Entities;
using Visitor_Management_System.Models;

namespace Visitor_Management_System.Interface
{
    public interface ISecurityService
    {
        Task<SecurityModel> CreateSecurity(SecurityModel securityModel);
        Task<List<SecurityModel>> GetAllSecurity();
        Task<SecurityModel> ReadSecurityByUId(string UId);
        Task<SecurityModel> UpdateSecurityByUId(SecurityModel securityModel);
        Task<string> DeleteSecurityByUId(string UId);
    }
}
