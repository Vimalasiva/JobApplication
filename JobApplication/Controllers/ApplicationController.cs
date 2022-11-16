
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using ExcelDataReader;
using JobApplication.Core.IService;
using JobApplication.Core.Model;
using JobApplication.Entity.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Net;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;


namespace JobApplication.Controllers
{
    public class ApplicationController : Controller
    {
        #region Declaration
        private readonly IJobService jobService;
        private int i;
        #endregion

        #region Constructor
        public ApplicationController(IJobService jobService)
        {
            this.jobService = jobService;

        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(JobModel jobModel, IFormFile formFile)
        {

            if (formFile == null || formFile.Length == 0)
                return Content("file not selected");
            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\File",
                        formFile.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyToAsync(stream);
            }
            jobModel.Resume = path;
            var i = 0;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read()) //Each row of the file
                    {
                        jobModel.FullName = (reader.GetValue(0) != null) ? reader.GetValue(0).ToString() : " ";
                        jobModel.Xth_Mark = (reader.GetValue(1) != null) ? reader.GetValue(1).ToString() : " ";
                        jobModel.XIIth_Mark = (reader.GetValue(2) != null) ? reader.GetValue(2).ToString() : " ";
                        jobModel.CGPA = (reader.GetValue(3) != null) ? reader.GetValue(3).ToString() : " ";
                        jobModel.Interest = (reader.GetValue(4) != null) ? reader.GetValue(4).ToString() : " ";
                        jobModel.Skills = (reader.GetValue(5) != null) ? reader.GetValue(5).ToString() : " ";
                    }
                    if (jobModel.FullName != " ")
                    {
                        if (!IsLetter(jobModel.FullName))
                        {
                            i++;
                            ViewBag.Message1 = "Invalid Name in excel ";
                        }
                    }
                    else
                    {
                        i++;
                        ViewBag.Message1 = " Field Name is empty";
                    }
                    if (jobModel.Xth_Mark != " ")
                    {
                        if (!IsNumber(jobModel.Xth_Mark))
                        {
                            i++;
                            ViewBag.Message2 = "Invalid Xth Mark in excel";
                        }
                    }
                    else
                    {
                        i++;
                        ViewBag.Message2 = " Field Xth is empty";
                    }
                    if (jobModel.XIIth_Mark != " ")
                    {
                        if (!IsNumber(jobModel.XIIth_Mark))
                        {
                            i++;
                            ViewBag.Message3 = "Invalid XIIth Mark in excel";
                        }
                    }
                    else
                    {
                        i++;
                        ViewBag.Message3 = " Field XIIth is empty";
                    }
                    if (jobModel.CGPA != " ")
                    { 
                        if (!IsNumber(jobModel.CGPA))
                        {
                            i++;
                            ViewBag.Message5 = "Invalid CGPA in excel";
                        }
                    }
                    else
                    {
                        i++;
                        ViewBag.Message4 = " Field CGPA is empty";
                    }
                    if (jobModel.Interest != " ")
                    {
                        if (!IsLetter(jobModel.Interest))
                        {
                            i++;
                            ViewBag.Message5 = "Invalid Interest in excel ";

                        }
                    }
                    else
                    {
                        i++;
                        ViewBag.Message5 = " Field Interest is empty";
                    }
                    if (jobModel.Skills == " ")
                    {
                       
                        ViewBag.Message6 = " Field Skills is empty";
                    }
                    
                    if(i==0)
                    {
                        jobService.CreateForm(jobModel);
                        return RedirectToAction("List");
                    }

                }
            }
                
            return View();
        }

        private bool IsNumber(string value)
        {
            return value.All(char.IsDigit);
        }

        private bool IsLetter(string value)
        {
            Regex regexLetter = new Regex("^[a-zA-Z ]+$");
            return regexLetter.IsMatch(value);
        }

        #endregion

        #region List
        [HttpGet]
        public IActionResult List()
        {
            var list = jobService.ListDetail();
            return View(list);
        }
        #endregion

        #region Viewing details using Partial view
        [HttpGet]
        public IActionResult _Detail(int id)
        {
            var partial = jobService.Save(id);
            return PartialView(partial);
        }
        #endregion

        #region Downloading the details in excel
        public IActionResult Download(String filename)
        {
            if (filename == null)
                return Content("filename not present");
            //File path
            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot\\File", filename);
            var memory = new MemoryStream();
            //To store the uploaded file in wwwroot\\File
            using (var stream = new FileStream(path, FileMode.Open))
            {
                 stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
             {
                 {".xls", "application/vnd.ms-excel"},
                 {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
             };
        }
        #endregion

       

    }
}
