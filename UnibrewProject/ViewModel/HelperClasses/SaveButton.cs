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
    }
}
