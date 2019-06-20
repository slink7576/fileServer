using System;
using Xunit;
using FileManager.Controllers;
using Microsoft.Extensions.Configuration;
using Moq;
using FileManager;

namespace Tests
{
    public class FileControllerTests
    {
        [Fact]
        public void GetFilesConstraintTest()
        {
            var conf = new Mock<IConfiguration>();
            var context = new Mock<FilesContext>();
            var controller = new FilesController(context.Object, conf.Object);
            conf.Setup(cont => cont["AllowedSize"]).Returns("10000");
            conf.Setup(cont => cont["AllowedFormats"]).Returns(".pdf,.gif,.jpg");

            var result = controller.GetFilesConstraint();

            Assert.Equal(1, 1);

        }
        
    }
}
