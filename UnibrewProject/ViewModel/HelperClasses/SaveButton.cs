using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;

namespace UnibrewProject.ViewModel.HelperClasses
{
    public class SaveButton
    {
        public SaveButton()
        {
            SaveCommand = new RelayCommand(SaveMethod);
        }

        private void SaveMethod()
        {

        }

        public RelayCommand SaveCommand { get; set; }
        public double Bottle01 { get; set; }
        public double Bottle02 { get; set; }
        public double Bottle03 { get; set; }
        public double Bottle04 { get; set; }
        public double Bottle05 { get; set; }
        public double Bottle06 { get; set; }
        public double Bottle07 { get; set; }
        public double Bottle08 { get; set; }
        public double Bottle09 { get; set; }
        public double Bottle10 { get; set; }
        public double Bottle11 { get; set; }
        public double Bottle12 { get; set; }
        public double Bottle13 { get; set; }
        public double Bottle14 { get; set; }
        public double Bottle15 { get; set; }
    }
}
