using Visitor_Management_System.Entities;
using Visitor_Management_System.Models;

namespace Visitor_Management_System.Interface
{
    public interface IOfficeService
    {
        Task<OfficeModel> CreateOffice(OfficeModel officeModel);
        Task<List<OfficeModel>> GetAllOffice();
        Task<OfficeModel> ReadOfficeByUId(string UId);
        Task<OfficeModel> UpdateOfficeByUId(OfficeModel officeModel);
        Task<string> DeleteOfficeByUId(string UId);
    }
}
