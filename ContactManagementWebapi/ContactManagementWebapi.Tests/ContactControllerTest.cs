using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ContactManagementWebapi.Controllers;
using ContactManagementWebapi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;

namespace ContactManagementWebapi.Tests
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange

            Mock<IContact> mockContactRepo = new Mock<IContact>();
            var items = new List<Contact>()
              {
                new Contact
                {
                   FirstName="A",
                   LastName="B",
                   Email="a@a.com",
                   Id=12345,
                   Status="Active"
                },
                new Contact
                {
                   FirstName="z",
                   LastName="xerd",
                   Email="a@a.com",
                   Id=12345,
                   Status="Inactive"
                },

                new Contact
                {
                   FirstName="zxcv",
                   LastName="xerder",
                   Email="a@a.com",
                   Id=12345,
                   Status="Inactive"
                }
              };
            mockContactRepo.Setup(m => m.GetContacts()).Returns(items.AsEnumerable());
            ContactController controller = new ContactController(mockContactRepo.Object);
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            //// Act
            //IEnumerable<Contact> result = controller.Get();
            IEnumerable<Contact> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            Mock<IContact> mockContactRepo = new Mock<IContact>();
            var item = new Contact();
            item.FirstName = "A";
            item.LastName = "B";
            item.Email = "a@a.com";
            item.Id = 1456;
            item.Status = "Active";
            mockContactRepo.Setup(m => m.GetContact(item.Id)).Returns(item);
            ContactController controller = new ContactController(mockContactRepo.Object);
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var result = controller.GetContactById(1456);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void post()
        {
            // Arrange

            Mock<IContact> mockContactRepo = new Mock<IContact>();
            var item = new Contact();
            item.FirstName = "A";
            item.LastName = "B";
            item.Email = "a@a.com";
            item.Id = 5;
            item.Status = "Active";
            mockContactRepo.Setup(m => m.AddContact(item)).Returns(item);
            ContactController controller = new ContactController(mockContactRepo.Object);
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act
            var result = controller.Post(item);

            // Assert
            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        [TestMethod]
        public void Put()
        {
            // Arrange

            Mock<IContact> mockContactRepo = new Mock<IContact>();
            var item = new Contact();
            item.FirstName = "A";
            item.LastName = "B";
            item.Email = "a@a.com";
            item.Id = 5;
            item.Status = "Active";
            mockContactRepo.Setup(m => m.Update(item)).Returns(true);
            ContactController controller = new ContactController(mockContactRepo.Object);
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var result=controller.Put(item);

            // Assert
            Assert.IsTrue(result.IsSuccessStatusCode);

        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            Mock<IContact> mockContactRepo = new Mock<IContact>();
            var item = new Contact();
            item.FirstName = "A";
            item.LastName = "B";
            item.Email = "a@a.com";
            item.Id = 1456;
            item.Status = "Active";
            mockContactRepo.Setup(m => m.GetContact(item.Id)).Returns(item);
            ContactController controller = new ContactController(mockContactRepo.Object);
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var result = controller.Delete(1456);

            // Assert
            Assert.IsTrue(result.IsSuccessStatusCode);

        }
    }
}
