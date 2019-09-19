using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taskk.Models;

namespace Taskk.Controllers
{
    public class HomeController : Controller
    {
        BankEntities db = new BankEntities();

        public ActionResult Index()
        {
            List<ClientViewModel> clientList = db.Clients.Select(x => new ClientViewModel
            {
                Id = x.Id,
                Name = x.Name,
                BirthDate = x.BirthDate,
                Address = x.Address,
                PhoneNumber = x.PhoneNumber,
                SocialNumber = x.SocialNumber
            }).ToList();

            return View(clientList);
        }

        public ActionResult SelectedClient(string socialId)
        {
            var selectedClient = db.Clients.Select(x => new ClientViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                BirthDate = x.BirthDate,
                PhoneNumber = x.PhoneNumber,
                SocialNumber = x.SocialNumber
            }).Where(x => x.SocialNumber == socialId).FirstOrDefault();
            if (selectedClient == null)
            {
                return View("ObjectNotFound");
            }
            return View(selectedClient);
        }

        public ActionResult ExportToExcel(string socialId)
        {
            var selectedClient = db.Clients.Select(x => new ClientViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                BirthDate = x.BirthDate,
                PhoneNumber = x.PhoneNumber,
                SocialNumber = x.SocialNumber
            }).Where(x => x.SocialNumber == socialId).FirstOrDefault();

            //Путь к шаблон-файлу
            var newPath = "C:\\Template";

            //Путь к результирующиему-файлу
            var resultPath = "C:\\Result";

            //Имя и путь к шаблон-файлу
            var newtempFile = $"{newPath}\\example.xlsx";
            DirectoryInfo tempDirectoryInfo = new DirectoryInfo(newPath);

            //Если нет данной директории, создаем путь
            if (!tempDirectoryInfo.Exists)
            {
                //создание пути
                tempDirectoryInfo.Create();

                //создание файла-шаблона  
                CreateNewExcelFile(newPath);
            }

            //создаем путь к результирующими-файлу, если не существует
            DirectoryInfo resultDirectoryInfo = new DirectoryInfo(resultPath);
            if (!resultDirectoryInfo.Exists)
            {
                resultDirectoryInfo.Create();
            }

            var resultFilePath = FillingTempFile(newtempFile, resultPath, selectedClient);

            ViewBag.Path = resultFilePath;

            return View(selectedClient);
        }

        private void CreateNewExcelFile(string newPath)
        {
            using (ExcelPackage newExcelPackage = new ExcelPackage())
            {
                var newTemplateFile = $"{newPath}\\example.xlsx";
                FileInfo newFile = new FileInfo(newTemplateFile);

                ExcelWorksheet ws = newExcelPackage.Workbook.Worksheets.Add("Report");

                string[] autoFillLeft = new string[8] { "Дата заполнения", " ", "ID", "Фамилия имя:", "Дата рождения:", "Номер телефона:", "Адрес:", "ИНН" };
                string[] autoFillRight = new string[6] { "ID", "Фамилия имя:", "Дата рождения:", "Номер телефона:", "Адрес:", "ИНН" };
                string[] alhabet = new string[6] { "D", "E", "F", "G", "H", "I" };
                int vertRowStart = 1;
                int horiRowStart = 0;
                foreach (var item in autoFillLeft)
                {
                    ws.Cells[string.Format("A{0}", vertRowStart)].Value = item;
                    vertRowStart++;
                }

                foreach (var item in autoFillRight)
                {
                    ws.Cells[$"{alhabet[horiRowStart]}3"].Value = item;

                    horiRowStart++;
                }

                ws.Cells["A8:B8"].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                ws.Cells["B1:B8"].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                ws.Cells["A3:B3"].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                ws.Cells["A3:B3"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells["A4:B4"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells["A5:B5"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells["A6:B6"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells["A7:B7"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ws.Cells["D3:I3"].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                ws.Cells["D3:D4"].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                ws.Cells["D4:I4"].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                ws.Cells["I3:I4"].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                ws.Cells["D3:I3"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells["D3:D4"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells["E3:E4"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells["F3:F4"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells["G3:G4"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells["H3:H4"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells["A3:A8"].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                ws.Cells["B1:B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["B1:B1"].Style.Fill.BackgroundColor.SetColor(Color.YellowGreen);
                ws.Cells["B3:B8"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["B3:B8"].Style.Fill.BackgroundColor.SetColor(Color.YellowGreen);

                ws.Cells["D4:I4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["D4:I4"].Style.Fill.BackgroundColor.SetColor(Color.YellowGreen);

                ws.Cells["A:AZ"].AutoFitColumns();

                newExcelPackage.SaveAs(newFile);
            }
        }

        private string FillingTempFile(string newtempFile, string resultPath, ClientViewModel selectedClient)
        {
            ExcelPackage pck = new ExcelPackage();

            FileInfo templateFile = new FileInfo(newtempFile);

            var resultFilePath = $"{resultPath}\\{DateTime.Now.ToString("ddMMyyyy")}-{selectedClient.SocialNumber.ToString()}.xlsx";

            using (ExcelPackage excelPackage = new ExcelPackage(templateFile))
            {

                ExcelWorksheet ws = excelPackage.Workbook.Worksheets[1];



                ws.Cells["B1"].Value = DateTime.Now.ToString("dd.MM.yyyy");
                ws.Cells["B3"].Value = selectedClient.Id;
                ws.Cells["B4"].Value = selectedClient.Name;
                ws.Cells["B5"].Value = selectedClient.BirthDate.ToString("dd.MM.yyyy");
                ws.Cells["B6"].Value = selectedClient.PhoneNumber;
                ws.Cells["B7"].Value = selectedClient.Address;
                ws.Cells["B8"].Value = selectedClient.SocialNumber;

                ws.Cells["D4"].Value = selectedClient.Id;
                ws.Cells["E4"].Value = selectedClient.Name;
                ws.Cells["F4"].Value = selectedClient.BirthDate.ToString("dd.MM.yyyy");
                ws.Cells["G4"].Value = selectedClient.PhoneNumber;
                ws.Cells["H4"].Value = selectedClient.Address;
                ws.Cells["I4"].Value = selectedClient.SocialNumber;

                ws.Cells["A:AZ"].AutoFitColumns();

                FileInfo resultFile = new FileInfo(resultFilePath);
                excelPackage.SaveAs(resultFile);

            }
            return resultFilePath;
        }
    }
}