using Microsoft.AspNetCore.Mvc;
using Visitor_Management_System.Entities;
using Visitor_Management_System.Models;

namespace Visitor_Management_System.Interface
{
    public interface IPassService
    {
        Task<PassModel> CreatePass(PassModel passModel);
        Task<List<PassModel>> GetAllPass();
        Task<PassModel> ReadPassByUId(string UId);
        Task<PassModel> UpdatePassByUId(PassModel passModel);
        Task<List<PassModel>> GetPassByStatus(string status);
        Task<string> GeneratePassPdf(string UId);
        Task<string> GeneratePassesByStatusPdf(string status);
        //Task<bool> SendPassToVisitor(string UId, string visitorEmail);
    }
}
