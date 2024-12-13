using AutoMapper;
using Visitor_Management_System.Cosmos;
using Visitor_Management_System.Entities;
using Visitor_Management_System.Interface;
using Visitor_Management_System.Models;
using Visitor_Management_System.Common;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Visitor_Management_System.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly ICosmosDbService _dbService;
        private readonly IMapper _mapper;
        private readonly IPassService _passService;
        private readonly ExcelService _excelService;
        public VisitorService(ICosmosDbService dbService, IMapper mapper, IPassService passService, ExcelService excelService)
        {
            _dbService = dbService;
            _mapper = mapper;
            _passService = passService;
            _excelService = excelService;
        }


        public async Task<VisitorModel> CreateVisitor(VisitorModel visitorModel)
        {

            var visitor = _mapper.Map<VisitorEntity>(visitorModel);
            visitor.Initialize(true, Credential.visitorDocumentType, "Siddharth", "Mishra");

            // Check if the email already exists
            var existMail = await _dbService.CheckExistMail(visitor.Email);
            var acceptStatus = await _dbService.GetVisitorByUId(visitor.UId);

            if (existMail)
            {
                // Email already exists, send an email notification
                //string fromEmail = "tidkeshubham10@gmail.com";
                //string subject = "Email Already Exists";
                //string toEmail = visitor.Email;
                //string username = visitor.Name;
                //string message = "The email you provided is already registered in our system.";

                //await _emailSender.SendEmail(fromEmail, subject, toEmail, username, message);

                //// Check if the visitor's request is accepted
                //var existingVisitor = await _dbService.GetVisitorByEmail(visitor.Email);
                //if (existingVisitor?.Status == "Accepted")
                //{
                //    string fromEmailNew2 = "tidkeshubham10@gmail.com";
                //    string subjectNew2 = "Request Accepted";
                //    string toEmailNew2 = visitor.Email;
                //    string usernameNew2 = visitor.Name;
                //    string messageNew2 = $"Dear {visitor.Name}, we have confirmed your entry pass. Pass Status: Accepted.";

                //    await _emailSender.SendEmail(fromEmailNew2, subjectNew2, toEmailNew2, usernameNew2, messageNew2);
                //}

                return visitorModel;
            }
            else
            {
                // Add the new visitor to the database
                var response = await _dbService.AddItemAsync(visitor);
                var model = _mapper.Map<VisitorModel>(response);

                _passService.CreatePass(model);
                _excelService.AddVisitorToExcel(model);
                return model;
            }
        }

        public async Task<VisitorModel> ReadVisitorByUId(string UId)
        {
            var visitor = await _dbService.GetVisitorByUId(UId);
            var response = _mapper.Map<VisitorModel>(visitor);
            return response;
        }

        public async Task<List<VisitorModel>> GetAllVisitor()
        {
            var visitorList = await _dbService.GetAllVisitor();
            var response = _mapper.Map<List<VisitorModel>>(visitorList);
            return response;
        }

        public async Task<VisitorModel> UpdateVisitorByUId(VisitorModel visitorModel)
        {
            var existingVisitor = new VisitorEntity();
            existingVisitor = await _dbService.GetVisitorByUId(visitorModel.UId);

            existingVisitor.Active = false;
            existingVisitor.Archived = true;
            await _dbService.ReplaceAsync(existingVisitor);

            _mapper.Map(visitorModel, existingVisitor);

            existingVisitor.Initialize(false, Credential.visitorDocumentType, "Mukesh", "Ambani");
            var response = await _dbService.AddItemAsync(existingVisitor);
            var model = _mapper.Map<VisitorModel>(response);
            return model;
        }
        //public async Task<VisitorModel> UpdateVisitorByUId(VisitorModel visitorModel)
        //{
        //    var existingVisitor = await _dbService.GetVisitorByUId(visitorModel.UId);
        //    var updatedVisitor = _mapper.Map(visitorModel, existingVisitor);
        //    updatedVisitor.Active = false;
        //    updatedVisitor.Archived = true;
        //    await _dbService.ReplaceAsync(existingVisitor);

        //    existingVisitor.Initialize(false, Credential.visitorDocumentType, "Mukesh", "Ambani");
        //    var response = await _dbService.AddItemAsync(existingVisitor);
        //    var model = _mapper.Map<VisitorModel>(response);
        //    _excelService.AddVisitorToExcel(model);
        //    return model;
        //}

        public async Task<string> DeleteVisitorByUId(string UId)
        {
            var visitor = await _dbService.GetVisitorByUId(UId);
            visitor.Active = false;
            visitor.Archived = true;
            await _dbService.ReplaceAsync(visitor);
            visitor.Initialize(false, Credential.visitorDocumentType, "Mukesh", "Ambani");
            visitor.Active = false;
            visitor.Archived = true;

            await _dbService.AddItemAsync(visitor);
            return "Visitor deleted successfully";
        }
        //public async Task<string> DeleteVisitorByUId(string UId)
        //{
        //    var visitor = await _dbService.GetVisitorByUId(UId);
        //    visitor.Active = false;
        //    visitor.Archived = true;
        //    await _dbService.ReplaceAsync(visitor);
        //    return "Visitor Deleted";
        //}



    }
}
