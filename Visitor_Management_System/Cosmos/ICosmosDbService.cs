using Visitor_Management_System.Entities;

namespace Visitor_Management_System.Cosmos
{
    public interface ICosmosDbService
    {
        Task<T> AddItemAsync<T>(T item);
        Task ReplaceAsync(dynamic item);

        //Visitor
        Task<List<VisitorEntity>> GetAllVisitor();
        Task<VisitorEntity> GetVisitorByUId(string UId);

        //Office
        Task<List<OfficeEntity>> GetAllOffice();
        Task<OfficeEntity> GetOfficeByUId(string UId);

        //Security
        Task<List<SecurityEntity>> GetAllSecurity();
        Task<SecurityEntity> GetSecurityByUId(string UId);

        //Manager
        Task<List<ManagerEntity>> GetAllManager();
        Task<ManagerEntity> GetManagerByUId(string UId);

        //Pass
        Task<List<PassEntity>> GetAllPass();
        Task<PassEntity> GetPassByUId(string UId);
        Task<List<PassEntity>> GetPassByStatus(string Status);

        //Login
        Task<List<LoginEntity>> GetAllLogin();
        Task<string> Login(LoginEntity login);
        Task<bool> CheckExistMail(string Email);
    }
}
