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
            CalendarCommand = new RelayCommand<object>(CalendarCommandMethod);
            CalendarToDateCommand = new RelayCommand<object>(CalendarToDateCommandMethod);
            CheckBoxCommand = new RelayCommand<string>(CheckBoxCommandMethod);
            FromDateTime = StatConfig.FromDateTime;
            ToDateTime = StatConfig.ToDateTime;
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

        private void CheckBoxCommandMethod(string name)
        {
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

        /// <summary>
        /// Slider til menuen
        /// </summary>
        public MenuSlider Slider { get; set; }

        /// <summary>
        /// Knapperne i menuen
        /// </summary>
        public MenuNavigator Navigator { get; set; }

        /// <summary>
        /// Bygger grafen
        /// </summary>
        public MomentStatBuilder StatBuilder { get; set; }

        /// <summary>
        /// Henter data fra persistens klassen
        /// </summary>
        public Loader Load { get; set; } = Loader.Load;

        /// <summary>
        /// Konfigurerer graferne
        /// </summary>
        public StatConfiguration StatConfig { get; set; } = StatConfiguration.StatConfig;

        /// <summary>
        /// Skifter "fra" dato
        /// </summary>
        public RelayCommand<object> CalendarCommand { get; set; }

        /// <summary>
        /// Skifter "til" dato
        /// </summary>
        public RelayCommand<object> CalendarToDateCommand { get; set; }

        /// <summary>
        /// Slår given graf til/fra
        /// </summary>
        public RelayCommand<string> CheckBoxCommand { get; set; }

        /// <summary>
        /// Fra dato
        /// </summary>
        public DateTime FromDateTime
        {
            get => StatConfig.FromDateTime;
            set 
            {
                StatConfig.FromDateTime = value;
                RegenerateGraph();
            }
        }

        /// <summary>
        /// Til dato
        /// </summary>
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
