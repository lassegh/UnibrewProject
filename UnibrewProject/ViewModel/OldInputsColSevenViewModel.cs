using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using Microsoft.Toolkit.Uwp.UI.Controls;
using UnibrewProject.Model;
using UnibrewProject.ViewModel.HelperClasses;

namespace UnibrewProject.ViewModel
{
    public class OldInputsColSevenViewModel
    {
        private ProcessingItems _lastProcessingItem;
        private FinishedItems _lastFinishedItem;

        public OldInputsColSevenViewModel()
        {
            Slider = new MenuSlider();
            Navigator = new MenuNavigator();
            ChangeLiquidTankCommand = new RelayCommand<object>(ChangeLiquidTankCommandMethod);
        }

        private void ChangeLiquidTankCommandMethod(object obj)
        {
            SelectionChangedEventArgs args = obj as SelectionChangedEventArgs;
            LiquidTanks liquidTank = args?.AddedItems[0] as LiquidTanks;
            LastInputTapOperator.LiquidTank = liquidTank?.Name;
        }

        public RelayCommand<object> ChangeLiquidTankCommand { get; set; }

        public TapOperator LastInputTapOperator { get; set; } = Loader.Load.GetTapOperators().Last();
        public MenuSlider Slider { get; set; }
        public MenuNavigator Navigator { get; set; }
        public Loader Load { get; set; } = Loader.Load;

        public ProcessingItems LastProcessingItem
        {
            get
            {
                _lastProcessingItem = Loader.Load
                        .GetProcessingItems().First(p => p.ProcessNumber.Equals(LastProcessingItem.ProcessNumber));
                return _lastProcessingItem;
            }
            set { _lastProcessingItem = value; }
        }

        public FinishedItems LastFinishedItem
        {
            get
            {
                _lastFinishedItem =
                    Loader.Load.FinishedItemsList.First(f =>
                        f.FinishedItemNumber == LastProcessingItem.FinishedItemNumber);
                return _lastFinishedItem;
            }
            set { _lastFinishedItem = value; }
        }
    }
}
