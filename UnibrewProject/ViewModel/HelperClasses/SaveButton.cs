using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using UnibrewProject.Model;

namespace UnibrewProject.ViewModel.HelperClasses
{
    public class SaveButton
    {
        public SaveButton()
        {
            SaveCommand = new RelayCommand(SaveMethod);
        }

        public void SaveMethod()
        {
            Console.WriteLine("SaveMethod kaldt");
            TESTmoment moments = new TESTmoment(Double.Parse(Bottle01), Double.Parse(Bottle02), Double.Parse(Bottle03), Double.Parse(Bottle04), Double.Parse(Bottle05), Double.Parse(Bottle06), Double.Parse(Bottle07), Double.Parse(Bottle08), Double.Parse(Bottle09), Double.Parse(Bottle10), Double.Parse(Bottle11), Double.Parse(Bottle12), Double.Parse(Bottle13), Double.Parse(Bottle14), Double.Parse(Bottle15));
            DbCommunication.Post(moments);
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
