using OfficeOpenXml;
using System.IO;
using Visitor_Management_System.Models;
namespace Visitor_Management_System.Services
{
    public class ExcelService
    {
        private readonly string _filePath = "Visitors.xlsx";

        public ExcelService()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Required for non-commercial use
        }

        // Add a visitor to the Excel file
        public void AddVisitorToExcel(VisitorModel visitor)
        {
            FileInfo file = new FileInfo(_filePath);

            using (var package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet;

                // Check if the worksheet exists, otherwise create a new one
                if (package.Workbook.Worksheets.Count == 0)
                {
                    worksheet = package.Workbook.Worksheets.Add("Visitors");

                    // Add headers if it's a new sheet
                    worksheet.Cells[1, 1].Value = "UId";
                    worksheet.Cells[1, 2].Value = "Name";
                    worksheet.Cells[1, 3].Value = "Email";
                    worksheet.Cells[1, 4].Value = "PhoneNumber";
                    worksheet.Cells[1, 5].Value = "Address";
                    worksheet.Cells[1, 6].Value = "VisitingTo";
                    worksheet.Cells[1, 7].Value = "Purpose";
                    worksheet.Cells[1, 8].Value = "EntryTime";
                    worksheet.Cells[1, 9].Value = "ExitTime";
                }
                else
                {
                    worksheet = package.Workbook.Worksheets["Visitors"];
                }

                // Find the next empty row
                int nextRow = worksheet.Dimension?.Rows + 1 ?? 2;

                // Add visitor data
                worksheet.Cells[nextRow, 1].Value = visitor.UId;
                worksheet.Cells[nextRow, 2].Value = visitor.Name;
                worksheet.Cells[nextRow, 3].Value = visitor.Email;
                worksheet.Cells[nextRow, 4].Value = visitor.PhoneNumber;
                worksheet.Cells[nextRow, 5].Value = visitor.Address;
                worksheet.Cells[nextRow, 6].Value = visitor.VisitingTo;
                worksheet.Cells[nextRow, 7].Value = visitor.Purpose;
                worksheet.Cells[nextRow, 8].Value = visitor.EntryTime;
                worksheet.Cells[nextRow, 9].Value = visitor.ExitTime;

                // Save the Excel file
                package.Save();
            }
        }

        // Retrieve the Excel file path
        public string GetExcelFilePath()
        {
            return Path.GetFullPath(_filePath);
        }
    }
}
