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
        public string Bottle01 { get; set; }
        public string Bottle02 { get; set; }
        public string Bottle03 { get; set; }
        public string Bottle04 { get; set; }
        public string Bottle05 { get; set; }
        public string Bottle06 { get; set; }
        public string Bottle07 { get; set; }
        public string Bottle08 { get; set; }
        public string Bottle09 { get; set; }
        public string Bottle10 { get; set; }
        public string Bottle11 { get; set; }
        public string Bottle12 { get; set; }
        public string Bottle13 { get; set; }
        public string Bottle14 { get; set; }
        public string Bottle15 { get; set; }
    }
}
