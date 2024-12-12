using AutoMapper;
using Visitor_Management_System.Common;
using Visitor_Management_System.Cosmos;
using Visitor_Management_System.Entities;
using Visitor_Management_System.Interface;
using Visitor_Management_System.Models;

namespace Visitor_Management_System.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly ICosmosDbService _dbService;
        private readonly IMapper _mapper;
        public OfficeService(ICosmosDbService cismosDbService, IMapper mapper)
        {
            _dbService = cismosDbService;
            _mapper = mapper;
        }

        public async Task<OfficeModel> CreateOffice(OfficeModel officeModel)
        {
            var office = _mapper.Map<OfficeEntity>(officeModel);
            office.Initialize(true, Credential.officeDocumentType, "Siddharth", "Mishra");
            var response = await _dbService.AddItemAsync(office);
            var model = _mapper.Map<OfficeModel>(response);
            return model;
        }


        public async Task<List<OfficeModel>> GetAllOffice()
        {
            var officeList = await _dbService.GetAllOffice();
            var response = _mapper.Map<List<OfficeModel>>(officeList);
            return response;
        }

        public async Task<OfficeModel> ReadOfficeByUId(string UId)
        {
            var office = await _dbService.GetOfficeByUId(UId);
            var reponse = _mapper.Map<OfficeModel>(office);
            return reponse;
        }

        public async Task<OfficeModel> UpdateOfficeByUId(OfficeModel officeModel)
        {
            var office = await _dbService.GetOfficeByUId(officeModel.UId);
            var updatedOffice = _mapper.Map(officeModel, office);
            office.Active = false;
            office.Archived = true;
            await _dbService.ReplaceAsync(office);

            office.Initialize(false, Credential.officeDocumentType, "Mukesh0", "Ambani");
            var response = await _dbService.AddItemAsync(office);
            var model = _mapper.Map<OfficeModel>(response);
            return model;
        }

        public async Task<string> DeleteOfficeByUId(string UId)
        {
            var office = await _dbService.GetOfficeByUId(UId);
            //office.Active = false;
            office.Archived = true;
            await _dbService.ReplaceAsync(office);
            office.Initialize(false, Credential.officeDocumentType, "Mukesh", "Ambani");
            office.Active = false;
            office.Archived = true;
            await _dbService.AddItemAsync(office);
            return "Office Deleted";
        }
    }
}
