using AutoMapper;
using Visitor_Management_System.Cosmos;
using Visitor_Management_System.Entities;
using Visitor_Management_System.Interface;
using Visitor_Management_System.Models;
using Visitor_Management_System.Common;

namespace Visitor_Management_System.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ICosmosDbService _dbService;
        private readonly IMapper _mapper;
        public SecurityService (ICosmosDbService cosmosDbService, IMapper mapper)
        {
            _mapper = mapper;
            _dbService = cosmosDbService;
        }

        public async Task<SecurityModel> CreateSecurity(SecurityModel securityModel)
        {
            var security = _mapper.Map<SecurityEntity>(securityModel);
            security.Initialize(true, Credential.securityDocumentType, "Siddharth", "Mishra");
            var response = await _dbService.AddItemAsync(security);
            var model = _mapper.Map<SecurityModel>(response);
            return model;
        }

        public async Task<List<SecurityModel>> GetAllSecurity()
        {
            var securityList = await _dbService.GetAllSecurity();
            var response = _mapper.Map<List<SecurityModel>>(securityList);
            return response;
        }

        public async Task<SecurityModel> ReadSecurityByUId(string UId)
        {
            var security = await _dbService.GetSecurityByUId(UId);
            var model = _mapper.Map<SecurityModel>(security);
            return model;
        }


        public async Task<SecurityModel> UpdateSecurityByUId(SecurityModel securityModel)
        {
            var existingSecurity = await _dbService.GetSecurityByUId(securityModel.UId);
            var updatedSecurity = _mapper.Map(securityModel, existingSecurity);
            updatedSecurity.Active = false;
            updatedSecurity.Archived = true;
            await _dbService.ReplaceAsync(existingSecurity);

            existingSecurity.Initialize(false, Credential.securityDocumentType, "Mukesh", "Ambani");
            var response = await _dbService.AddItemAsync(existingSecurity);
            var model = _mapper.Map<SecurityModel>(response);
            return model;
        }

        public async Task<string> DeleteSecurityByUId(string UId)
        {
            var security = await _dbService.GetSecurityByUId(UId);
            //candidate.Active = false;
            security.Archived = true;
            await _dbService.ReplaceAsync(security);
            security.Initialize(false, Credential.securityDocumentType, "Mukesh", "Ambani");
            security.Active = false;
            security.Archived = true;

            await _dbService.AddItemAsync(security);
            return "Security deleted successfully";
        }
        //public async Task<SecurityModel> UpdateSecurityByUId(SecurityModel securityModel)
        //{
        //    var security = await _dbService.GetSecurityByUId(securityModel.UId);
        //    security.Active = false;
        //    security.Archived = true;
        //    await _dbService.ReplaceAsync(security);

        //    var updatedSecurity = _mapper.Map<SecurityEntity>(securityModel);
        //    updatedSecurity.Initialize(false, Credential.securityDocumentType, "Siddharth", "Mishra");
        //    var response = await _dbService.AddItemAsync(updatedSecurity);
        //    var model = _mapper.Map<SecurityModel>(response);
        //    return model;
        //}

        //public async Task<string> DeleteSecurityByUId(string UId)
        //{
        //    var security = await _dbService.GetSecurityByUId(UId);
        //    security.Active = false;
        //    security.Archived = true;
        //    await _dbService.ReplaceAsync(security);
        //    return "Security Deleted";
        //}
    }
}
