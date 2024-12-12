using System.Net.Mail;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Visitor_Management_System.Common;
using Visitor_Management_System.Cosmos;
using Visitor_Management_System.Entities;
using Visitor_Management_System.Interface;
using Visitor_Management_System.Models;
using IronPdf;
using System.IO;

namespace Visitor_Management_System.Services
{
    public class PassService : IPassService
    {
        private readonly ICosmosDbService _dbService;
        private readonly IMapper _mapper;
        public PassService(IMapper mapper, ICosmosDbService dbService)
        {
            _mapper = mapper;
            _dbService = dbService;
        }
        public async Task<PassModel> CreatePass(PassModel passModel)
        {
            var pass = _mapper.Map<PassEntity>(passModel);
            pass.Initialize(true, Credential.passDocumentType, "Siddharth", "Mishra");
            var response = await _dbService.AddItemAsync(pass);
            var model = _mapper.Map<PassModel>(response);
            return model;
        }

        public async Task<List<PassModel>> GetAllPass()
        {
            var passList = await _dbService.GetAllPass();
            var response = _mapper.Map<List<PassModel>>(passList);
            return response;
        }

        public async Task<List<PassModel>> GetPassByStatus(string status)
        {
            var pass = await _dbService.GetPassByStatus(status);
            var response = _mapper.Map<List<PassModel>>(pass);
            return response;
        }

        public async Task<PassModel> ReadPassByUId(string UId)
        {
            var pass = await _dbService.GetPassByUId(UId);
            var response = _mapper.Map<PassModel>(pass);
            return response;
        }

        public async Task<PassModel> UpdatePassByUId(PassModel passModel)
        {
            var pass = await _dbService.GetPassByUId(passModel.UId);
            var updatedPass = _mapper.Map(passModel, pass);
            pass.Active = false;
            pass.Archived = true;
            await _dbService.ReplaceAsync(pass);

            
            pass.Initialize(false, Credential.passDocumentType, "Mukesh", "Ambani");
            var response = await _dbService.AddItemAsync(pass);
            var model = _mapper.Map<PassModel>(response);
            return model;
        }

        public async Task<string> GeneratePassPdf(string UId)
        {
            // Fetch the pass details from the database
            var pass = await _dbService.GetPassByUId(UId);
            if (pass == null) return "Pass not found";

            // Create the HTML content for the pass
            string htmlContent = $@"
            <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; }}
                        .header {{ font-size: 24px; font-weight: bold; }}
                        .content {{ margin-top: 20px; }}
                    </style>
                </head>
                <body>
                    <div class='header'>Visitor Pass</div>
                    <div class='content'>
                        <p><strong>Name:</strong> {pass.VisitorName}</p>
                        <p><strong>UId:</strong> {pass.UId}</p>
                        <p><strong>Status:</strong> {pass.Status}</p>
                    </div>
                </body>
            </html>";

            // Use IronPDF to generate the PDF from the HTML content
            var renderer = new HtmlToPdf();  // Correct instantiation
            var pdf = renderer.RenderHtmlAsPdf(htmlContent); // Correct method usage

            // Save the PDF to a file
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedPasses", $"{pass.UId}_Pass.pdf");

            // Create the directory if it doesn't exist
            var directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Save the PDF to the specified path
            pdf.SaveAs(filePath);

            return filePath; // Returning the file path where the PDF was saved
        }


        public async Task<string> GeneratePassesByStatusPdf(string status)
        {
            // Fetch all passes with the given status
            var passes = await _dbService.GetPassByStatus(status);
            if (passes == null || passes.Count == 0) return "No passes found";

            // Create the HTML content for all passes
            var htmlContent = $@"
            <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; }}
                        .header {{ font-size: 24px; font-weight: bold; text-align: center; }}
                        .content {{ margin-top: 20px; }}
                        table {{ width: 100%; border-collapse: collapse; margin-top: 20px; }}
                        th, td {{ border: 1px solid black; padding: 8px; text-align: left; }}
                        th {{ background-color: #f2f2f2; }}
                    </style>
                </head>
                <body>
                    <div class='header'>Visitor Passes - Status: {status}</div>
                    <table>
                        <tr>
                            <th>Sr. No</th>
                            <th>Name</th>
                            <th>Mail Id</th>
                            <th>Status</th>
                        </tr>";

                    int passNumber = 1;
                    // Add each pass as a row in the HTML table
                    foreach (var pass in passes)
                    { 
                        htmlContent += $@"
                        <tr>
                            <td>{passNumber++}
                            <td>{pass.VisitorName}</td>
                            <td>{pass.Email}</td>
                            <td>{pass.Status}</td>
                        </tr>";
                    }

                    htmlContent += @"
                    </table>
                </body>
            </html>";

            // Generate the PDF using IronPDF
            var renderer = new HtmlToPdf();
            var pdf = renderer.RenderHtmlAsPdf(htmlContent);

            // Save the PDF to a file
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedPasses", $"{status}_Passes.pdf");

            // Create the directory if it doesn't exist
            var directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            pdf.SaveAs(filePath);

            return filePath; // Return the path of the generated PDF
        }



        //public async Task<bool> SendPassToVisitor(string UId, string visitorEmail)
        //{
        //    try
        //    {
        //        // Generate the pass PDF
        //        var pdfFilePath = await GeneratePassPdf(UId);
        //        if (string.IsNullOrEmpty(pdfFilePath) || !System.IO.File.Exists(pdfFilePath))
        //            return false; // If PDF generation fails, exit

        //        // Email configuration
        //        string fromAddress = "xyz@gmail.com"; // Your email
        //        string fromPassword = "xyz";         // Your email password
        //        string smtpServer = "smtp.gmail.com";        // Your SMTP server
        //        int smtpPort = 587;                            // SMTP port (e.g., 587 for TLS)

        //        // Email message
        //        var mail = new MailMessage
        //        {
        //            From = new MailAddress(fromAddress),
        //            Subject = "Your Visitor Pass",
        //            Body = "Dear Visitor,\n\nPlease find attached your visitor pass.\n\nThank you.",
        //            IsBodyHtml = false // Plain text email
        //        };
        //        mail.To.Add(visitorEmail);

        //        // Attach the PDF
        //        var attachment = new Attachment(pdfFilePath);
        //        mail.Attachments.Add(attachment);

        //        // SMTP client
        //        using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
        //        {
        //            smtpClient.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
        //            smtpClient.EnableSsl = true; // Enable SSL for secure email
        //            await smtpClient.SendMailAsync(mail);
        //        }

        //        return true; // Email sent successfully
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error (you can replace this with actual logging)
        //        Console.WriteLine($"Error sending email: {ex.Message}");
        //        return false;
        //    }
        //}

    }
}
