
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnibrewProject.ViewModel;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Intet objekt af InputColSevenViewModel
            InputColSevenViewModel vm = null;
            Assert.IsTrue(vm == null);

            // Objekt initialiseret
            vm = new InputColSevenViewModel();
            Assert.IsInstanceOfType(vm,typeof(InputColSevenViewModel));
        }

        [TestMethod]
        public void TestMethod2()
        {
            // Tjekker på FinishedItem og EnumerableFinishedItems

            // Tjekker på korrekt færdigvarenummer
            InputColSevenViewModel vm = new InputColSevenViewModel();
            vm.FinishedItemNumber = "7894";
            Assert.IsTrue(vm.EnumerableFinishItems.First().FinishedItemNumber==7894);

            // Tjekker på ikke eksisterende færdigvarenummer
            vm.FinishedItemNumber = "9400";
            Assert.IsTrue(vm.EnumerableFinishItems.Count()==0);

        }
    }
}
