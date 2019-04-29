using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnibrewProject.ViewModel.HelperClasses;

namespace UnibrewProject.ViewModel
{
    class InputColSevenViewModel
    {
        public InputColSevenViewModel()
        {
            Slider = new MenuSlider();
        }

        public MenuSlider Slider { get; set; }
    }
}
