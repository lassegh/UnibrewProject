using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using UnibrewProject.Annotations;
using UnibrewProject.View;

namespace UnibrewProject.ViewModel.HelperClasses
{
    /// <summary>
    /// Indeholder menuen og knappernes relayCommands
    /// </summary>
    public class MenuNavigator : INotifyPropertyChanged
    {

        public MenuNavigator()
        {
            InputColSevenCommand = new RelayCommand(InputColSevenMethod);
            StatCommand = new RelayCommand(StatMethod);
        }

        private void InputColSevenMethod()// Åbner en inputColSeven page
        {
            ((Frame)Window.Current.Content).Navigate(typeof(InputColSevenPage));
        }

        private void StatMethod() // Åbner en StatPage
        {
            ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
        }

        /// <summary>
        /// RelayCommand til InputColSevenPage
        /// </summary>
        public RelayCommand InputColSevenCommand { get; set; }

        /// <summary>
        /// RelayCommand til StatPage
        /// </summary>
        public RelayCommand StatCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
