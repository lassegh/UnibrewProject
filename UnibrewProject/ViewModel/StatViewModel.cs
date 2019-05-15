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
using UnibrewProject.ViewModel.HelperClasses;

namespace UnibrewProject.ViewModel
{
    class StatViewModel
    {
        private DateTime _fromDateTime = DateTime.Today;
        private DateTime _toDateTime;

        public StatViewModel()
        {
            Slider = new MenuSlider();
            Navigator = new MenuNavigator();
            Load.GetTapOperators();
            
        }

        public MenuSlider Slider { get; set; }
        public MenuNavigator Navigator { get; set; }
        public Loader Load { get; set; } = Loader.Load;

        public DateTime FromDateTime
        {
            get { return _fromDateTime; }
            set { _fromDateTime = value; }
        }

        public DateTime ToDateTime
        {
            get { return _toDateTime; }
            set { _toDateTime = value; }
        }
    }
}
