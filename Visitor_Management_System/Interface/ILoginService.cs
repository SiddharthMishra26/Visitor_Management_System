using Visitor_Management_System.Entities;
using Visitor_Management_System.Models;

namespace Visitor_Management_System.Interface
{
    public interface ILoginService
    {
        Task<string> Login(LoginModel loginModel);
        Task<List<LoginModel>> GetAllLogin();
    }
}
