using Visitor_Management_System.Entities;
using Visitor_Management_System.Models;

namespace Visitor_Management_System.Interface
{
    public interface IVisitorService
    {
        Task<VisitorModel> CreateVisitor(VisitorModel visitorModel);
        Task<List<VisitorModel>> GetAllVisitor();
        Task<VisitorModel> ReadVisitorByUId(string UId);
        Task<VisitorModel> UpdateVisitorByUId(VisitorModel visitorModel);
        Task<string> DeleteVisitorByUId(string UId);
    }
}
