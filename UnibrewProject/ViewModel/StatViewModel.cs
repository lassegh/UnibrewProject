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
            StatBuilderMoment = new MomentStatBuilder();
            StatBuilderWeight = new WeightStatBuilder();
            RegenerateMomentGraph();
            ChooseFinishedItemCommand = new RelayCommand<object>(ChooseFinishedItemCommandMethod);
            CalendarCommand = new RelayCommand<object>(CalendarCommandMethod);
            CalendarToDateCommand = new RelayCommand<object>(CalendarToDateCommandMethod);
            CheckBoxCommand = new RelayCommand<string>(CheckBoxCommandMethod);
            FromDateTime = StatConfig.FromDateTime;
            ToDateTime = StatConfig.ToDateTime;
        }

        private void ChooseFinishedItemCommandMethod(object obj)
        {
            SelectionChangedEventArgs args = obj as SelectionChangedEventArgs;
            StatConfig.FinishedItemForWeightGraph = args?.AddedItems[0] as FinishedItems;
            IEnumerable<ProcessingItems> processingItemsList = Load.GetProcessingItems().Where(p => p.FinishedItemNumber == StatConfig.FinishedItemForWeightGraph?.FinishedItemNumber);
            StatConfig.TapOperatorListForWeightGraph.Clear();
            foreach (ProcessingItems processingItem in processingItemsList)
            {
                StatConfig.TapOperatorListForWeightGraph.AddRange(_tapOperators.Where(p => p.ProcessNumber == processingItem.ProcessNumber).ToList());
            }

            StatConfig.TapOperatorListForWeightGraph = StatConfig.TapOperatorListForWeightGraph.OrderBy(d => d.ClockDate).ToList();
            RegenerateWeightGraph();
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

        private void RegenerateWeightGraph()
        {
            StatBuilderWeight.RebiuldStats(StatConfig.TapOperatorListForWeightGraph, FromDateTime, ToDateTime, StatConfig.FinishedItemForWeightGraph);
        }

        /// <summary>
        /// Gentegner graf af tilspændingsMomenter
        /// </summary>
        public void RegenerateMomentGraph()
        {
            StatBuilderMoment.RebiuldStats(_tapOperators, FromDateTime, ToDateTime, StatConfig.ShowingBottles);
        }

        private void CheckBoxCommandMethod(string name)
        {
            StatConfig.ToggleGraphs(name);
            RegenerateMomentGraph();
        }

        /// <summary>
        /// Command til combobox - valg af færdigvarenummer
        /// </summary>
        public RelayCommand<object> ChooseFinishedItemCommand { get; set; }

        /// <summary>
        /// Slider til menuen
        /// </summary>
        public MenuSlider Slider { get; set; }

        /// <summary>
        /// Knapperne i menuen
        /// </summary>
        public MenuNavigator Navigator { get; set; }

        /// <summary>
        /// Bygger grafen af momenter
        /// </summary>
        public MomentStatBuilder StatBuilderMoment { get; set; }

        /// <summary>
        /// Bygger grafen af vægt
        /// </summary>
        public WeightStatBuilder StatBuilderWeight { get; set; }

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
                RegenerateMomentGraph();
                RegenerateWeightGraph();
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
                RegenerateMomentGraph();
                RegenerateWeightGraph();
            }
        }
    }
}
