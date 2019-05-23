using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnibrewREST.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using UnibrewRESTTests;

namespace UnibrewREST.Controllers.Tests
{
    [TestClass()]
    public class TapOperatorsControllerTests
    {
        [TestMethod()]
        public void GetTapOperatorTest()
        {
            //Arrange
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<TapOperator>())
                .Returns(new FakeDbSet<TapOperator>
                {
                    new TapOperator() { ID = 1 }
                });

            // Dette må være controlleren // UserService userService = new UserService(mock.Object);

            // Act
            //var allUsers = userService.GetAllUsers();

            // Assert
            //Assert.AreEqual(1, allUsers.Count());

        }
    }
}