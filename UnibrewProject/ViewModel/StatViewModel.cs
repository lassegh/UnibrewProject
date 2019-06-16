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
            RegenerateWeightGraph();
            ChooseFinishedItemCommand = new RelayCommand<object>(ChooseFinishedItemCommandMethod);
            ChooseProcessingItemCommand = new RelayCommand<object>(ChooseProcessingItemCommandMethod);
            CalendarCommand = new RelayCommand<object>(CalendarCommandMethod);
            CalendarToDateCommand = new RelayCommand<object>(CalendarToDateCommandMethod);
            CheckBoxCommand = new RelayCommand<string>(CheckBoxCommandMethod);
            
        }

        private void ChooseProcessingItemCommandMethod(object obj) // Metode der køres når der vælges i combobox for ProcessingItems
        {
            SelectionChangedEventArgs args = obj as SelectionChangedEventArgs; // Prøver at parse objekt til argument
            StatConfig.ProcessItemForOldData = args?.AddedItems[0] as ProcessingItems; // Henter valgt processingItem
            RegenerateOldData(StatConfig.ProcessItemForOldData?.ProcessNumber); // Genererer gammeldata - kører metoden RegenerateOldData()
        }

        private void RegenerateOldData(string processNumber) // Kalder PopulateTapOperator i statConfig
        {
            StatConfig.PopulateTapOperatorCollectionForOldData(processNumber);
        }

        private void ChooseFinishedItemCommandMethod(object obj) // Metode der køres når der vælges i combobox for FinishedItems
        {
            SelectionChangedEventArgs args = obj as SelectionChangedEventArgs; // Prøver at parse objekt til argument
            StatConfig.FinishedItemForWeightGraph = args?.AddedItems[0] as FinishedItems; // Henter valgt FinishedItem

            // Filtrerer processingItems på valgt færdigvareNummer || 
            IEnumerable<ProcessingItems> processingItemsList = Load.GetProcessingItems().Where(p => p.FinishedItemNumber == StatConfig.FinishedItemForWeightGraph?.FinishedItemNumber);

            // Gemmer listen af processnumre i StatConfiguration som observableCollection - denne bruges i ComboBoxen for gammel data
            StatConfig.PopulateObservableCollectionForComboBox(processingItemsList.ToList()); 

            StatConfig.TapOperatorListForWeightGraph.Clear(); // Tømmer listen, der bruges til vægtgrafen

            // Filtrerer listen af Tappeoperatører på processnumre, der passer med ønskede færdigvareNummer. (Der hentes ny liste af tappeOperatører hver gang StatView åbnes)
            foreach (ProcessingItems processingItem in processingItemsList)
            {
                StatConfig.TapOperatorListForWeightGraph.AddRange(_tapOperators.Where(p => p.ProcessNumber == processingItem.ProcessNumber).ToList());
            }

            // Sorterer listen til vægtgrafen efter dato/tid
            StatConfig.TapOperatorListForWeightGraph = StatConfig.TapOperatorListForWeightGraph.OrderBy(d => d.ClockDate).ToList();

            RegenerateWeightGraph(); // Tegner graf
        }

        private void CalendarCommandMethod(object obj) // Metode til event af ændret dato i "Fra dato"
        {
            CalendarDatePickerDateChangedEventArgs args = obj as CalendarDatePickerDateChangedEventArgs;
            if (args?.NewDate != null)
            {
                FromDateTime = (DateTime) args.NewDate.Value.DateTime; // Gemmer ønsket dato
            }
        }

        private void CalendarToDateCommandMethod(object obj) // Metode til event af ændret dato i "til dato"
        {
            CalendarDatePickerDateChangedEventArgs args = obj as CalendarDatePickerDateChangedEventArgs;
            if (args?.NewDate != null)
            {
                ToDateTime = (DateTime)args.NewDate.Value.DateTime; // Gemmer ønsket dato
            }
        }

        private void RegenerateWeightGraph() // Tegner grafen. Kalder StatBuilderWeight med datoer, liste og ønsket færdigVare
        {
            StatBuilderWeight.RebuildStats(StatConfig.TapOperatorListForWeightGraph, FromDateTime, ToDateTime, StatConfig.FinishedItemForWeightGraph);
        }

        /// <summary>
        /// Gentegner graf af tilspændingsMomenter
        /// </summary>
        public void RegenerateMomentGraph()
        {
            StatBuilderMoment.RebuildStats(_tapOperators, FromDateTime, ToDateTime, StatConfig.ShowingBottles);
        }

        private void CheckBoxCommandMethod(string name) // Når en tjekBox markeres til eller fra kaldes denne metode
        {
            StatConfig.ToggleGraphs(name); // Den nye markering gemmes
            RegenerateMomentGraph(); // Graf tegnes igen
        }

        /// <summary>
        /// Command til combobox - valg af færdigvarenummer
        /// </summary>
        public RelayCommand<object> ChooseFinishedItemCommand { get; set; }

        /// <summary>
        /// Command til combobox - valg af processnummer
        /// </summary>
        public RelayCommand<object> ChooseProcessingItemCommand { get; set; }

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
                RegenerateOldData(StatConfig.ProcessItemForOldData?.ProcessNumber);
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
                RegenerateOldData(StatConfig.ProcessItemForOldData?.ProcessNumber);
            }
        }
    }
}
