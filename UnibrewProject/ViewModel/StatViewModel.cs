    using System;
using System.Collections.Generic;
using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight.Command;
using UnibrewProject.Annotations;
    using UnibrewProject.Model;
    using UnibrewProject.ViewModel.HelperClasses;

namespace UnibrewProject.ViewModel
{
    class StatViewModel
    {
        private List<TapOperator> _tapOperators = Loader.Load.GetTapOperators();
        private DateTime _fromDateTime;
        private DateTime _toDateTime;

        public StatViewModel()
        {
            Slider = new MenuSlider();
            Navigator = new MenuNavigator();
            StatBuilder = new StatisticsBuilder(_tapOperators,FromDateTime,ToDateTime);
            FromDateTime = new DateTime();
            CalendarCommand = new RelayCommand<object>(CalendarCommandMethod);
            CalendarToDateCommand = new RelayCommand<object>(CalendarToDateCommandMethod);
        }

        private void CalendarCommandMethod(object obj)
        {
            CalendarDatePickerDateChangedEventArgs args = obj as CalendarDatePickerDateChangedEventArgs;
            if (args?.NewDate != null)
            {
                FromDateTime = (DateTime) args.NewDate.Value.DateTime;
            }
        }

        private void CalendarToDateCommandMethod(object obj)
        {
            CalendarDatePickerDateChangedEventArgs args = obj as CalendarDatePickerDateChangedEventArgs;
            if (args?.NewDate != null)
            {
                ToDateTime = (DateTime)args.NewDate.Value.DateTime;
            }
        }

        public MenuSlider Slider { get; set; }
        public MenuNavigator Navigator { get; set; }
        public StatisticsBuilder StatBuilder { get; set; }
        public Loader Load { get; set; } = Loader.Load;

        public RelayCommand<object> CalendarCommand { get; set; }
        public RelayCommand<object> CalendarToDateCommand { get; set; }

        public DateTime FromDateTime
        {
            get { return _fromDateTime; }
            set
            {
                _fromDateTime = value;
                StatBuilder.RebiuldStats(_tapOperators,FromDateTime,ToDateTime);
            }
        }

        public DateTime ToDateTime
        {
            get { return _toDateTime; }
            set
            {
                _toDateTime = value;
                StatBuilder.RebiuldStats(_tapOperators, FromDateTime, ToDateTime);
            }
        }
    }
}
