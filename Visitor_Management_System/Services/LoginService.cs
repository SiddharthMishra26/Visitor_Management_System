using Visitor_Management_System.Cosmos;
using Visitor_Management_System.Entities;
using Visitor_Management_System.Models;
using Visitor_Management_System.Interface;
using Visitor_Management_System.Common;
using AutoMapper;
namespace Visitor_Management_System.Services
{
    public class LoginService : ILoginService
    {
        private readonly ICosmosDbService _dbService;
        private readonly IMapper _mapper;
        public LoginService(ICosmosDbService cosmosDBService, IMapper mapper)
        {
            _dbService = cosmosDBService;
            _mapper = mapper;
        }

        public async Task<List<LoginModel>> GetAllLogin()
        {
            var loginList = await _dbService.GetAllLogin();
            var response = _mapper.Map<List<LoginModel>>(loginList);
            return response;
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            var login = _mapper.Map<LoginEntity>(loginModel);
            login.Initialize(true, Credential.loginDocumentType, "Siddharth", "Mishra");
            var response = await _dbService.Login(login);
            return response;
        }
    }
}
