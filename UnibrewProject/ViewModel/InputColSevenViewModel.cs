using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using UnibrewProject.Annotations;
using UnibrewProject.Model;
using UnibrewProject.ViewModel.HelperClasses;

namespace UnibrewProject.ViewModel
{
    /// <summary>
    /// ViewModel for InputColSevenPage
    /// </summary>
    public class InputColSevenViewModel : INotifyPropertyChanged
    {

        public double txt_out;
        private string _txtbxInputValid;
        private string _finishedItemNumber;
        private FinishedItems _currentFinishedItem;

        public InputColSevenViewModel()
        {
            Slider = new MenuSlider();
            Navigator = new MenuNavigator();
            RelayCommand_inputValid = new RelayCommand<object>(Execute);
            _txtbxInputValid = "";
        }

        

        public void Execute(object parameter)
        {
            RoutedEventArgs args = parameter as RoutedEventArgs;
            TextBlock box = args?.OriginalSource as TextBlock;

            if (double.TryParse(Save.TapOperatorMoments[0].Moment, out txt_out))
            {
                txtbx_inputValid = "";

            }
            else
            {
                txtbx_inputValid = "*";
            }


            _txtbxInputValid = box?.Text;
        }

        /// <summary>
        /// Sørger for at menuen kan slide ind og ud
        /// </summary>
        public MenuSlider Slider { get; set; }

        /// <summary>
        /// Giver menuens knapper RelayCommands og metoder
        /// </summary>
        public MenuNavigator Navigator { get; set; }

        /// <summary>
        /// Sørger for indtastninger bliver gemt
        /// </summary>
        public SaveTapOperator Save { get; set; } = SaveTapOperator.Save;

        /// <summary>
        /// Henter data af LiquidTanks og FinishedItems
        /// </summary>
        public Loader Load { get; set; } = Loader.Load;

        public RelayCommand<object> RelayCommand_inputValid { get; set; }
        

        public string txtbx_inputValid
        {
            get => _txtbxInputValid;
            set { _txtbxInputValid = value; OnPropertyChanged(); }
        }
        
        /// <summary>
        /// FinishedItems
        /// </summary>
        public IEnumerable<FinishedItems> EnumerableFinishItems { get; set; }

        /// <summary>
        /// Finder tilsvarende data til færdigvarenummer
        /// </summary>
        public string FinishedItemNumber
        {
            get { return _finishedItemNumber; }
            set
            {
                _finishedItemNumber = value;
                
                int i;
                if (!int.TryParse(value, out i)) i = 0;
                Save.FinishNumber = i; // Sender færdigvarenummeret til objektet tapOp i save.
                EnumerableFinishItems =
                    Load.FinishedItemsList.Where(n => n.FinishedItemNumber.Equals(i));
                if (EnumerableFinishItems.ToList().Count == 0)
                {
                    // TODO Advar mod ikke eksiterende færdigvarenummer
                    CurrentFinishedItem = new FinishedItems();
                }
                else
                {
                    CurrentFinishedItem = EnumerableFinishItems.First();
                }
            }
        }

        /// <summary>
        /// Den aktuelle instans af færdigvarenummer, der bruges i view
        /// </summary>
        public FinishedItems CurrentFinishedItem
        {
            get { return _currentFinishedItem; }
            set
            {
                _currentFinishedItem = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
