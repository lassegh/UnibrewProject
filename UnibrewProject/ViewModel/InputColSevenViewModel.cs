using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnibrewProject.ViewModel.HelperClasses;

namespace UnibrewProject.ViewModel
{
    public class InputColSevenViewModel
    {
        public InputColSevenViewModel()
        {
            Slider = new MenuSlider();
            Navigator = new MenuNavigator();
            Save = new SaveButton();
        }

        public MenuSlider Slider { get; set; }
        public MenuNavigator Navigator { get; set; }
        public SaveButton Save { get; set; }
    }
}
