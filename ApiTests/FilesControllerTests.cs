using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FileManager;
using FileManager.Controllers;
using FileManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTests
{
    [TestClass]
    public class FilesControllerTests
    {
        public Mock<IConfiguration> _configuration { get; set; }
        public Mock<IRepository<FileData>> _repository { get; set; }
        public FilesController _controller { get; set; }
        void Arrange()
        {
            _configuration = new Mock<IConfiguration>();
            _repository = new Mock<IRepository<FileData>>();
            _controller = new FilesController(_repository.Object, _configuration.Object);
        }

        [TestMethod]
        public void GetFilesConstraintTest()
        {
            Arrange();

            _configuration.Setup(cont => cont["AllowedSize"]).Returns("10000");
            _configuration.Setup(cont => cont["AllowedFormats"]).Returns(".pdf,.gif,.jpg");

            var result = _controller.GetFilesConstraint() as OkObjectResult;
            var expectedValue = new FileConstraints()
            {
                Formats = ".pdf,.gif,.jpg",
                Size = 10000
            };

            Assert.AreEqual((result.Value as FileConstraints).Size, expectedValue.Size);
            Assert.AreEqual((result.Value as FileConstraints).Formats, expectedValue.Formats);
        }

        [TestMethod]
        public void GetFilesTest()
        {
            Arrange();

            var date = DateTime.Now;
            var data = new List<FileData>() { new FileData() {
                 Author = "test",
                  FileName = "text.txt",
                   Id = 1,
                    Size = 1000,
                     UploadDate = date
            } };

            _repository.Setup(rep => rep.ListAll()).Returns(data);

            var result = _controller.GetFiles() as OkObjectResult;

            Assert.AreEqual(result.Value, data);
        }
    }
}
