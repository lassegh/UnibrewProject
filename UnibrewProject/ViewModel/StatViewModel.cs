    using System;
using System.Collections.Generic;
using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight.Command;
using UnibrewProject.Annotations;
    using UnibrewProject.Model;
    using UnibrewProject.ViewModel.HelperClasses;

namespace UnibrewProject.ViewModel
{
    /// <summary>
    /// ViewModel for StatPage
    /// </summary>
    public class StatViewModel
    {
        private List<TapOperator> _tapOperators = Loader.Load.GetTapOperators();

        public StatViewModel()
        {
            Slider = new MenuSlider();
            Navigator = new MenuNavigator();
            StatBuilder = new MomentStatBuilder();
            RegenerateGraph();
            FromDateTime = new DateTime();
            CalendarCommand = new RelayCommand<object>(CalendarCommandMethod);
            CalendarToDateCommand = new RelayCommand<object>(CalendarToDateCommandMethod);
            CheckBoxCommand = new RelayCommand<object>(CheckBoxCommandMethod);
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

        private void CheckBoxCommandMethod(object obj)
        {
            TappedRoutedEventArgs args = obj as TappedRoutedEventArgs;
            CheckBox box = args?.OriginalSource as CheckBox;
            string name = box?.Name;
            StatConfig.ToggleGraphs(name);
            RegenerateGraph();
        }

        /// <summary>
        /// Gentegner graf af tilspændingsMomenter
        /// </summary>
        public void RegenerateGraph()
        {
            StatBuilder.RebiuldStats(_tapOperators, FromDateTime, ToDateTime, StatConfig.ShowingBottles);
        }

        public MenuSlider Slider { get; set; }
        public MenuNavigator Navigator { get; set; }
        public MomentStatBuilder StatBuilder { get; set; }
        public Loader Load { get; set; } = Loader.Load;
        public StatConfiguration StatConfig { get; set; } = StatConfiguration.StatConfig;


        public RelayCommand<object> CalendarCommand { get; set; }
        public RelayCommand<object> CalendarToDateCommand { get; set; }
        public RelayCommand<object> CheckBoxCommand { get; set; }

        public DateTime FromDateTime
        {
            get => StatConfig.FromDateTime;
            set
            {
                StatConfig.FromDateTime = value;
                RegenerateGraph();
            }
        }

        public DateTime ToDateTime
        {
            get => StatConfig.ToDateTime;
            set
            {
                StatConfig.ToDateTime = value;
                RegenerateGraph();
            }
        }
    }
}
