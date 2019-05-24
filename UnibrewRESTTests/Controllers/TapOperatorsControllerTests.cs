using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnibrewREST.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Moq;
using Newtonsoft.Json;
using UnibrewREST;
using UnibrewRESTTests;

namespace UnibrewRESTTests.Controllers
{
    [TestClass()]
    public class TapOperatorsControllerTests
    {
        TapOperatorsController tC = new TapOperatorsController();
        private int numberOfPostetTapOperator;

        [TestMethod()]
        public void GetTapOperatorTest()
        {
            var tapOperators = tC.GetTapOperator();
            numberOfPostetTapOperator = tapOperators.Count();
            Assert.IsTrue(tapOperators.Any());
        }

        [TestMethod()]
        public void GetTapOperatorTest1()
        {
            IHttpActionResult actionResult = tC.GetTapOperator(1);
            var contentResult = actionResult as OkNegotiatedContentResult<TapOperator>;
            Assert.AreEqual(1, contentResult?.Content.ID);
        }

        [TestMethod()]
        public void PostTapOperatorTest()
        {
            IHttpActionResult actionResult = tC.PostTapOperator(new TapOperator(){Bottle1 = 15, Bottle2 = 16, Bottle3 = 17, Bottle4 = 18, Bottle5 = 19, Bottle6 = 14, Bottle7 = 15,Bottle8 = 16, Bottle9 = 17, Bottle10 = 18, Bottle11 = 19, Bottle12 = 14, Bottle13 = 15, Bottle14 = 16, Bottle15 = 17, ClockDate = DateTime.Now, Comments = "Dette er en test", DropTest = true, HeuftFillingHeight = true, HeuftLid = true, LidMaterialNo = "70001", LiquidTank = "B001", Operator = "LG", PreformMaterialNo = "80001", ProcessNumber = "1         ", ProductTasted = true, SukkerStickTest = true, Weight1 = 550, Weight2 = 555, Weight3 = 551, Weight4 = 552, Weight5 = 553, Weight6 = 554});
            
            Assert.IsTrue(tC.GetTapOperator().Count()>numberOfPostetTapOperator);
        }

        [TestMethod()]
        public void PutTapOperatorTest()
        {
            tC.PutTapOperator(52,
                new TapOperator()
                {ID = 52,
                    Bottle1 = 15, Bottle2 = 16, Bottle3 = 17, Bottle4 = 18, Bottle5 = 19, Bottle6 = 14, Bottle7 = 15,
                    Bottle8 = 16, Bottle9 = 17, Bottle10 = 18, Bottle11 = 19, Bottle12 = 14, Bottle13 = 15,
                    Bottle14 = 16, Bottle15 = 17, ClockDate = DateTime.Now, Comments = "Dette er en test - I have been changed",
                    DropTest = true, HeuftFillingHeight = true, HeuftLid = true, LidMaterialNo = "70001",
                    LiquidTank = "B001", Operator = "LG", PreformMaterialNo = "80001", ProcessNumber = "1         ",
                    ProductTasted = true, SukkerStickTest = true, Weight1 = 550, Weight2 = 555, Weight3 = 551,
                    Weight4 = 552, Weight5 = 553, Weight6 = 554
                });
            IHttpActionResult actionResult = tC.GetTapOperator(52);
            var contentResult = actionResult as OkNegotiatedContentResult<TapOperator>;
            Assert.AreEqual("Dette er en test - I have been changed", contentResult?.Content.Comments);
        }

        [TestMethod()]
        public void DeleteTapOperatorTest()
        {
            int idToBeRemoved = 60;
            tC.DeleteTapOperator(idToBeRemoved);
            IHttpActionResult actionResult = tC.GetTapOperator(idToBeRemoved);
            var contentResult = actionResult as OkNegotiatedContentResult<TapOperator>;
            Assert.AreEqual(null, contentResult?.Content.ID);

        }
    }
}