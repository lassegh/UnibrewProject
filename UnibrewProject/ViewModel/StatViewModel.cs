    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight.Command;
using UnibrewProject.Annotations;
using LiveCharts;
using LiveCharts.Uwp;
using UnibrewProject.ViewModel.HelperClasses;

namespace UnibrewProject.ViewModel
{
    class StatViewModel
    {

        public StatViewModel()
        {
            Slider = new MenuSlider();
            Navigator = new MenuNavigator();
            Load.GetTapOperators();
            
        }

        public MenuSlider Slider { get; set; }
        public MenuNavigator Navigator { get; set; }
        public Loader Load { get; set; } = Loader.Load;
    }
}
