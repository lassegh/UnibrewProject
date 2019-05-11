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
using UnibrewProject.ViewModel.HelperClasses.DbCommunication;

namespace UnibrewProject.ViewModel
{
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

        public MenuSlider Slider { get; set; }
        public MenuNavigator Navigator { get; set; }
        public SaveTapOperator Save { get; set; } = SaveTapOperator.Save;
        public DbComGeneric ComGeneric { get; set; } = DbComGeneric.ComGeneric;

        public RelayCommand<object> RelayCommand_inputValid { get; set; }
        

        public string txtbx_inputValid
        {
            get => _txtbxInputValid;
            set { _txtbxInputValid = value; OnPropertyChanged(); }
        }
        
        public IEnumerable<FinishedItems> EnumerableFinishItems { get; set; }

        public string FinishedItemNumber
        {
            get { return _finishedItemNumber; }
            set
            {
                _finishedItemNumber = value;
                int i;
                if (!int.TryParse(value, out i)) i = 0;
                EnumerableFinishItems =
                    ComGeneric.FinishedItemsList.Where(n => n.FinishedItemNumber.Equals(i));
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
