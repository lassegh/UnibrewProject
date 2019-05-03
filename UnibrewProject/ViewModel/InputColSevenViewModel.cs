using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using UnibrewProject.Annotations;
using UnibrewProject.ViewModel.HelperClasses;

namespace UnibrewProject.ViewModel
{
    public class InputColSevenViewModel : INotifyPropertyChanged
    {
        private string _txtbxInputValid;
        public MenuSlider Slider { get; set; }
        public MenuNavigator Navigator { get; set; }
        public SaveButton Save { get; set; } = SaveButton.Save;

        public RelayCommand<object> RelayCommand_inputValid { get; set; }

        public double txt_out;
        public string txtbx_inputValid
        {
            get => _txtbxInputValid;
            set { _txtbxInputValid = value; OnPropertyChanged();}
        }

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
            
            if (double.TryParse(Save.BottleStrings[1], out txt_out))
            {
                txtbx_inputValid = "";

            }
            else
            {
                txtbx_inputValid = "*";
            }

            _txtbxInputValid = box?.Text;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
