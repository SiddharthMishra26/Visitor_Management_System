using Microsoft.Azure.Cosmos;
using Visitor_Management_System.Entities;
using Visitor_Management_System.Common;
using Visitor_Management_System.Models;

namespace Visitor_Management_System.Cosmos
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly CosmosClient _client;
        private readonly Container _container;

        public CosmosDbService()
        {
            _client = new CosmosClient(Credential.cosmosUrl, Credential.authkey);
            _container = _client.GetContainer(Credential.database, Credential.container);
        }


        public async Task<T> AddItemAsync<T>(T item)
        {
            try
            {
                var response = await _container.CreateItemAsync(item);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ReplaceAsync(dynamic item)
        {
            var response = await _container.ReplaceItemAsync(item, item.Id);
        }

        public async Task<List<VisitorEntity>> GetAllVisitor()
        {
            try
            {
                var response = _container.GetItemLinqQueryable<VisitorEntity>(true).Where(i => i.dType == Credential.visitorDocumentType && i.Active == true && i.Archived == false).AsEnumerable().ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PassEntity>> GetAllPass()
        {
            try
            {
                var response = _container.GetItemLinqQueryable<PassEntity>(true).Where(i => i.dType == Credential.passDocumentType && i.Active == true && i.Archived == false).AsEnumerable().ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ManagerEntity>> GetAllManager()
        {
            try
            {
                var response = _container.GetItemLinqQueryable<ManagerEntity>(true).Where(i => i.dType == Credential.managerDocumentType && i.Active == true && i.Archived == false).AsEnumerable().ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OfficeEntity>> GetAllOffice()
        {
            try
            {
                var response = _container.GetItemLinqQueryable<OfficeEntity>(true).Where(i => i.dType == Credential.officeDocumentType && i.Active == true && i.Archived == false).AsEnumerable().ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<LoginEntity>> GetAllLogin()
        {
            try
            {
                var response = _container.GetItemLinqQueryable<LoginEntity>(true).Where(i => i.dType == Credential.loginDocumentType && i.Active == true && i.Archived == false).AsEnumerable().ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SecurityEntity>> GetAllSecurity()
        {
            try
            {
                var response = _container.GetItemLinqQueryable<SecurityEntity>(true).Where(i => i.dType == Credential.securityDocumentType && i.Active == true && i.Archived == false).AsEnumerable().ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<VisitorEntity> GetVisitorByUId(string UId)
        {
            try
            {
                var response = _container.GetItemLinqQueryable<VisitorEntity>(true).Where(i => i.UId == UId && i.dType == Credential.visitorDocumentType && i.Active == true && i.Archived == false).AsEnumerable().FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<OfficeEntity> GetOfficeByUId(string UId)
        {
            try
            {
                var response = _container.GetItemLinqQueryable<OfficeEntity>(true).Where(i => i.UId == UId && i.dType == Credential.officeDocumentType && i.Active == true && i.Archived == false).AsEnumerable().FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<SecurityEntity> GetSecurityByUId(string UId)
        {
            try
            {
                var response = _container.GetItemLinqQueryable<SecurityEntity>(true).Where(i => i.UId == UId && i.dType == Credential.securityDocumentType && i.Active == true && i.Archived == false).AsEnumerable().FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ManagerEntity> GetManagerByUId(string UId)
        {
            try
            {
                var response = _container.GetItemLinqQueryable<ManagerEntity>(true).Where(i => i.UId == UId && i.dType == Credential.managerDocumentType && i.Active == true && i.Archived == false).AsEnumerable().FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PassEntity> GetPassByUId(string UId)
        {
            try
            {
                var response =  _container.GetItemLinqQueryable<PassEntity>(true).Where(i => i.UId == UId && i.dType == Credential.passDocumentType && i.Active == true && i.Archived == false).AsEnumerable().FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PassEntity>> GetPassByStatus(string Status)
        {
            try
            {
                var response = _container.GetItemLinqQueryable<PassEntity>(true).Where(i => i.Status == Status && i.dType == Credential.passDocumentType && i.Active == true && i.Archived == false).AsEnumerable().ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> Login(LoginEntity login)
        {
            var email = login.Email;
            var password = login.Password;

            var security = _container.GetItemLinqQueryable<SecurityEntity>(true).Where(i => i.Email ==  email && i.Password == password && i.dType == Credential.securityDocumentType && i.Active == true && i.Archived == false).AsEnumerable().FirstOrDefault();
            var Manager = _container.GetItemLinqQueryable<ManagerEntity>(true).Where(i => i.Email == email && i.Password == password && i.dType == Credential.managerDocumentType && i.Active == true && i.Archived == false).AsEnumerable().FirstOrDefault();
            var Office = _container.GetItemLinqQueryable<OfficeEntity>(true).Where(i => i.Email == email && i.Password == password && i.dType == Credential.officeDocumentType && i.Active == true && i.Archived == false).AsEnumerable().FirstOrDefault();

            if (security != null || Manager != null || Office != null)
            {
                return "Login Succesfull";
            }
            else
            {
                return "Login Failed";
            }
        }

        public async Task<bool> CheckExistMail(string Email)
        {
            var existMail = _container.GetItemLinqQueryable<VisitorEntity>(true).Where(i => i.Email == Email && i.Active == true && i.Archived == false && i.dType == Credential.visitorDocumentType).AsEnumerable().FirstOrDefault();
            if (existMail != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
