using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class InputColSevenViewModel
    {

        public double txt_out;
        private string _txtbxInputValid;

        private string _finishedItemNumber;
        
        public InputColSevenViewModel()
        {
            Slider = new MenuSlider();
            Navigator = new MenuNavigator();
            RelayCommand_inputValid = new RelayCommand<object>(CheckLegalDataType);
            _txtbxInputValid = "";

        }

        
        /// <summary>
        /// Checks if this value are of the data type string, null/whitespace if yes, gives a warning with a red star in a textBlock behind the textbox 
        /// </summary>
        /// <param name="parameter"></param>
        public void CheckLegalDataType(object parameter)
        {
            RoutedEventArgs args = parameter as RoutedEventArgs;
            TextBox box = args?.OriginalSource as TextBox;

            switch (box?.Name)
            {
                case "txtbx_col1":
                    ChangeValueForLegalData(0);
                    break;
                case "txtbx_col2":
                    ChangeValueForLegalData(1);
                    break;
                case "txtbx_col3":
                    ChangeValueForLegalData(2);
                    break;
                case "txtbx_col4":
                    ChangeValueForLegalData(3);
                    break;
                case "txtbx_col5":
                    ChangeValueForLegalData(4);
                    break;
                case "txtbx_col6":
                    ChangeValueForLegalData(5);
                    break;
                case "txtbx_col7":
                    ChangeValueForLegalData(6);
                    break;
                case "txtbx_col8":
                    ChangeValueForLegalData(7);
                    break;
                case "txtbx_col9":
                    ChangeValueForLegalData(8);
                    break;
                case "txtbx_col10":
                    ChangeValueForLegalData(9);
                    break;
                case "txtbx_col11":
                    ChangeValueForLegalData(10);
                    break;
                case "txtbx_col12":
                    ChangeValueForLegalData(11);
                    break;
                case "txtbx_col13":
                    ChangeValueForLegalData(12);
                    break;
                case "txtbx_col14":
                    ChangeValueForLegalData(13);
                    break;
                case "txtbx_col15":
                    ChangeValueForLegalData(14);
                    break;
            }
        }

        private void ChangeValueForLegalData(int i) // Hjælpemetode til rød stjerne. Tjekker på det index, den tar som param. 
        {
            // Kan string parses eller er den tom, vises stjernen ikke
            if (double.TryParse(Save.TapOperatorMoments[i].Moment, out txt_out) || string.IsNullOrWhiteSpace(Save.TapOperatorMoments[i].Moment))
            {
                TxtBxInputValid[i] = "";

            }
            else
            {
                TxtBxInputValid[i] = "*";
            }
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

        /// <summary>
        /// Relay command som eventet "lostFocus" bruger når data typen i text feltet skal valideres
        /// </summary>
        public RelayCommand<object> RelayCommand_inputValid { get; set; }
        

        /// <summary>
        /// FinishedItems
        /// </summary>
        public IEnumerable<FinishedItems> EnumerableFinishItems { get; set; }

        /// <summary>
        /// String til færdigvarenummer i UI. Ved input søges om færdigvarenummer eksisterer i databasen.
        /// </summary>
        public string FinishedItemNumber
        {
            get
            {
                if (Save.CurrentFinishedItem.FinishedItemNumber==0) // Hvis færdigvarenummer er 0, skal stringen være tom.
                {
                    return "";
                }
                return Save.CurrentFinishedItem.FinishedItemNumber.ToString();
            }
            set
            {
                _finishedItemNumber = value;
                
                int i;
                if (!int.TryParse(value, out i)) i = 0; // TryParser string ellers 0
                Save.FinishNumber = i; // Sender færdigvarenummeret til objektet tapOp i save.
                EnumerableFinishItems =
                    Load.FinishedItemsList.Where(n => n.FinishedItemNumber.Equals(i)); // Søger på færdigvarenummeret
                if (EnumerableFinishItems.ToList().Count == 0)
                {
                    // Advar mod ikke eksiterende færdigvarenummer
                    ShowMsg showMsg = new ShowMsg();
                    showMsg.ShowMessage("Der er indtastet et ikke eksisterende færdigvarenummer");
                    Save.CurrentFinishedItem = new FinishedItems();
                }
                else
                {
                    Save.CurrentFinishedItem = EnumerableFinishItems.First(); // Gemmer objektet af finishedItem i SaveTapOperator
                }
            }
        }

        /// <summary>
        /// ObervableCollection der bliver brugt til at vise om der er brugt forkerte data type i input
        /// </summary>
        public ObservableCollection<string> TxtBxInputValid { get; set; } = new ObservableCollection<string>{"", "", "", "", "", "", "", "", "", "", "", "", "", "", ""};
    }
}
