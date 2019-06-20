using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FileManager.ViewModels;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileManager.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FilesController : Controller
    {
        private readonly IRepository<FileData> _fileDataRepository;
        private readonly IConfiguration _configuration;

        public FilesController(IRepository<FileData> repo, IConfiguration configuration)
        {
            _fileDataRepository = repo;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetFilesConstraint()
        {
            return Ok(new FileConstraints()
            {
                Size = Int32.Parse(_configuration["AllowedSize"]),
                Formats = _configuration["AllowedFormats"],
            });
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult GetFiles()
        {
            return Ok(_fileDataRepository.ListAll());
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (!_configuration["AllowedFormats"].Contains(
                    (file.FileName.Split('.')[file.FileName.Split('.').Length - 1]).ToLower()))
                {
                    return Json("Wrong file format. Acceptable formats are: " + _configuration["AllowedFormats"]);
                }
                if (file.Length > Int32.Parse(_configuration["AllowedSize"]))
                {
                    return Json("File is too big. Max size is: " + Int32.Parse(_configuration["AllowedSize"]));
                }
                if (!Directory.Exists(_configuration["Savepath"]))
                {
                    Directory.CreateDirectory(_configuration["Savepath"]);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(_configuration["Savepath"], fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                _fileDataRepository.Add(new FileData()
                {
                    Author = Request.Form.Keys.FirstOrDefault(),
                    FileName = file.FileName,
                    Size = file.Length,
                    UploadDate = DateTime.Now
                });
                return Json("Upload Successful.");
            }
            catch (Exception ex)
            {
                return Json("Upload Failed: " + ex.Message);
            }
        }

    }
}
