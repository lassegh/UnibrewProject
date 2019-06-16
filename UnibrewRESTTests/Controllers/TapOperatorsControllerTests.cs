using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnibrewREST.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Castle.Components.DictionaryAdapter.Xml;
using Moq;
using Newtonsoft.Json;
using UnibrewREST;
using UnibrewRESTTests;

namespace UnibrewRESTTests.Controllers
{
    [TestClass()]
    public class TapOperatorsControllerTests
    {
        

        [TestMethod()]
        public void GetTapOperatorTest()
        {
            // Arrange
            TapOperatorsController tC = new TapOperatorsController();
            var tapOperators = tC.GetTapOperator();

            // Assert
            Assert.IsTrue(tapOperators.Any());
        }

        [TestMethod()]
        public void GetTapOperatorTest1()
        {
            // Arrange
            TapOperatorsController tC = new TapOperatorsController();
            IHttpActionResult actionResult = tC.GetTapOperator(1);
            var contentResult = actionResult as OkNegotiatedContentResult<TapOperator>;

            // Act
            int id = 1;

            // Assert
            Assert.AreEqual(id, contentResult?.Content.ID);
        }

        [TestMethod()]
        public void PostTapOperatorTest()
        {
            // Arrange
            TapOperatorsController tC = new TapOperatorsController();
            int id = 0;

            IHttpActionResult actionResult = tC.PostTapOperator(new TapOperator(){Bottle1 = 15, Bottle2 = 16, Bottle3 = 17, Bottle4 = 18, Bottle5 = 19, Bottle6 = 14, Bottle7 = 15,Bottle8 = 16, Bottle9 = 17, Bottle10 = 18, Bottle11 = 19, Bottle12 = 14, Bottle13 = 15, Bottle14 = 16, Bottle15 = 17, ClockDate = DateTime.Now, Comments = "Dette er en test", DropTest = true, HeuftFillingHeight = true, HeuftLid = true, LidMaterialNo = "70001", LiquidTank = "B001", Operator = "LG", PreformMaterialNo = "80001", ProcessNumber = "1         ", ProductTasted = true, SukkerStickTest = true, Weight1 = 550, Weight2 = 555, Weight3 = 551, Weight4 = 552, Weight5 = 553, Weight6 = 554});
            var contentResult = actionResult as CreatedAtRouteNegotiatedContentResult<TapOperator>;

            // Act
            if (contentResult != null)
            {
                id = contentResult.Content.ID;
            }
            
            // Assert
            Assert.IsTrue(id.Equals(contentResult?.Content.ID));
            
        }

        [TestMethod()]
        public void PutTapOperatorTest()
        {
            // Dette tester successfuldt put

            //Arrange
            TapOperatorsController tC = new TapOperatorsController();
            int IdOfPostetTapOperator = 1;
            var actionResult = tC.PutTapOperator(IdOfPostetTapOperator,
                new TapOperator()
                {ID = IdOfPostetTapOperator, Bottle1 = 15, Bottle2 = 16, Bottle3 = 17, Bottle4 = 18, Bottle5 = 19, Bottle6 = 14, Bottle7 = 15,
                    Bottle8 = 16, Bottle9 = 17, Bottle10 = 18, Bottle11 = 19, Bottle12 = 14, Bottle13 = 15,
                    Bottle14 = 16, Bottle15 = 17, ClockDate = DateTime.Now, Comments = "Dette er en test - I have been changed",
                    DropTest = true, HeuftFillingHeight = true, HeuftLid = true, LidMaterialNo = "70001",
                    LiquidTank = "B001", Operator = "LG", PreformMaterialNo = "80001", ProcessNumber = "1         ",
                    ProductTasted = true, SukkerStickTest = true, Weight1 = 550, Weight2 = 555, Weight3 = 551,
                    Weight4 = 552, Weight5 = 553, Weight6 = 554
                });
            var contentResult = actionResult as StatusCodeResult;

            // Assert
            Assert.IsTrue(contentResult?.StatusCode == HttpStatusCode.NoContent);

            // Dette tester bad request, hvor ID parameter og ID i objekt ikke stemmer overens

            // Arrange
            var actionResultTwo = tC.PutTapOperator(1,
                new TapOperator()
                {
                    ID = 98,
                    Bottle1 = 15,
                    Bottle2 = 16,
                    Bottle3 = 17,
                    Bottle4 = 18,
                    Bottle5 = 19,
                    Bottle6 = 14,
                    Bottle7 = 15,
                    Bottle8 = 16,
                    Bottle9 = 17,
                    Bottle10 = 18,
                    Bottle11 = 19,
                    Bottle12 = 14,
                    Bottle13 = 15,
                    Bottle14 = 16,
                    Bottle15 = 17,
                    ClockDate = DateTime.Now,
                    Comments = "Dette er en test - I have been changed",
                    DropTest = true,
                    HeuftFillingHeight = true,
                    HeuftLid = true,
                    LidMaterialNo = "70001",
                    LiquidTank = "B001",
                    Operator = "LG",
                    PreformMaterialNo = "80001",
                    ProcessNumber = "1         ",
                    ProductTasted = true,
                    SukkerStickTest = true,
                    Weight1 = 550,
                    Weight2 = 555,
                    Weight3 = 551,
                    Weight4 = 552,
                    Weight5 = 553,
                    Weight6 = 554
                });
            var contentTwo = actionResultTwo as BadRequestResult;

            // Assert
            Assert.IsTrue(contentTwo.ToString().Equals("System.Web.Http.Results.BadRequestResult"));


            // Dette tester not found, hvor ID parameter og ID ikke findes i tabellen

            // Arrange
            var actionResultThree = tC.PutTapOperator(-1,
                new TapOperator()
                {
                    ID = -1,
                    Bottle1 = 15,
                    Bottle2 = 16,
                    Bottle3 = 17,
                    Bottle4 = 18,
                    Bottle5 = 19,
                    Bottle6 = 14,
                    Bottle7 = 15,
                    Bottle8 = 16,
                    Bottle9 = 17,
                    Bottle10 = 18,
                    Bottle11 = 19,
                    Bottle12 = 14,
                    Bottle13 = 15,
                    Bottle14 = 16,
                    Bottle15 = 17,
                    ClockDate = DateTime.Now,
                    Comments = "Dette er en test - I have been changed",
                    DropTest = true,
                    HeuftFillingHeight = true,
                    HeuftLid = true,
                    LidMaterialNo = "70001",
                    LiquidTank = "B001",
                    Operator = "LG",
                    PreformMaterialNo = "80001",
                    ProcessNumber = "1         ",
                    ProductTasted = true,
                    SukkerStickTest = true,
                    Weight1 = 550,
                    Weight2 = 555,
                    Weight3 = 551,
                    Weight4 = 552,
                    Weight5 = 553,
                    Weight6 = 554
                });
            var contentThree = actionResultTwo as BadRequestResult;

            // Assert
            Assert.IsTrue(contentTwo.ToString().Equals("System.Web.Http.Results.BadRequestResult"));

        }

        [TestMethod()]
        public void DeleteTapOperatorTest()
        {
            // For id, der ikke eksisterer

            // Arrange
            TapOperatorsController tC = new TapOperatorsController();
            int idToBeRemoved = 60;
            tC.DeleteTapOperator(idToBeRemoved);
            IHttpActionResult actionResult = tC.GetTapOperator(idToBeRemoved);
            var contentResult = actionResult as OkNegotiatedContentResult<TapOperator>;

            // Assert
            Assert.AreEqual(null, contentResult?.Content.ID);

            // For id, der eksisterer - id skal ændres for hver test for at teste denne del af metoden

            // Arrange
            idToBeRemoved = 98;
            tC.DeleteTapOperator(idToBeRemoved);
            actionResult = tC.GetTapOperator(idToBeRemoved);
            contentResult = actionResult as OkNegotiatedContentResult<TapOperator>;

            // Assert
            Assert.AreEqual(null, contentResult?.Content.ID);
        }
    }
}