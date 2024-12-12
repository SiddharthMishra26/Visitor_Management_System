using AutoMapper;
using Visitor_Management_System.Common;
using Visitor_Management_System.Cosmos;
using Visitor_Management_System.Entities;
using Visitor_Management_System.Interface;
using Visitor_Management_System.Models;

namespace Visitor_Management_System.Services
{
    public class ManagerService : IManagerService
    {
        private readonly ICosmosDbService _dbService;
        private readonly IMapper _mapper;
        public ManagerService(ICosmosDbService cosmosDbService, IMapper mapper)
        {
            _mapper = mapper;
            _dbService = cosmosDbService;
        }

        public async Task<ManagerModel> CreateManager(ManagerModel managerModel)
        {
            var manager = _mapper.Map<ManagerEntity>(managerModel);
            manager.Initialize(true, Credential.managerDocumentType, "Siddharth", "Mishra");
            var response = await _dbService.AddItemAsync(manager);
            var model = _mapper.Map<ManagerModel>(response);
            return model;
        }

        public async Task<ManagerModel> ReadManagerByUId(string UId)
        {
            var manager = await _dbService.GetManagerByUId(UId);
            var model = _mapper.Map<ManagerModel>(manager);
            return model;
        }

        public async Task<List<ManagerModel>> GetAllManager()
        {
            var managerList = await _dbService.GetAllManager();
            var response = _mapper.Map<List<ManagerModel>>(managerList);
            return response;
        }

        public async Task<ManagerModel> UpdateManagerByUId(ManagerModel managerModel)
        {
            var manager = await _dbService.GetManagerByUId(managerModel.UId);
            var updatedManager = _mapper.Map(managerModel, manager);
            updatedManager.Active = false;
            updatedManager.Archived = true;
            await _dbService.ReplaceAsync(manager);

            manager.Initialize(false, Credential.managerDocumentType, "Siddharth", "Mishra");
            var response = await _dbService.AddItemAsync(manager);
            var model = _mapper.Map<ManagerModel>(response);
            return model;
        }

        public async Task<string> DeleteManagerByUId(string UId)
        {
            var manager = await _dbService.GetManagerByUId(UId);
            //manager.Active = false;
            manager.Archived = true;
            await _dbService.ReplaceAsync(manager);
            manager.Initialize(false, Credential.managerDocumentType, "Mukesh", "Ambani");
            manager.Active = false;
            manager.Archived = true;
            await _dbService.AddItemAsync(manager);
            return "Manager Deleted";
        }
    }
}
