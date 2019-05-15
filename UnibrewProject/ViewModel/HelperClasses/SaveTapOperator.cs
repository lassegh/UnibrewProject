using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using UnibrewProject.Annotations;
using UnibrewProject.Model;
using UnibrewProject.ViewModel.HelperClasses.SaveClasses;

namespace UnibrewProject.ViewModel.HelperClasses
{
    /// <summary>
    /// Gem klasse.
    /// Til gem knap og automatisk gem
    /// </summary>
    public class SaveTapOperator : INotifyPropertyChanged
    {
        private static SaveTapOperator _save = null;
        public delegate void SaveToDbMethod();
        private SaveToDbMethod _saveToDbMethod;
        private ProcessingItems _proItem;

        private bool _isCheckedHeuftLid;
        private bool _isCheckedFillHeight;
        private bool _isCheckedProductTasted;
        private bool _isCheckedSugarTest;
        private bool _isCheckedDropTest;

        private string _comment;

        private SaveTapOperator()
        {
            for (int i = 0; i < TapOperatorMoments.Length; i++)
            {
                TapOperatorMoments[i] = new TapOperatorMoment();
            }

            for (int i = 0; i < FluidWeightControls.Length; i++)
            {
                FluidWeightControls[i] = new FluidWeightControl();
            }

            GenerateObjectsToBeSaved();
            _saveToDbMethod = PostSaveMethod;
            SaveCommand = new RelayCommand(SaveCommandPush);
            AutoSaveTimer = new AutoSaveTimer(this);
            LiquidTankCommand = new RelayCommand<object>(LiquidTankCommandMethod);
            ProItem = new ProcessingItems();
        }

        private void GenerateObjectsToBeSaved()
        {
            TapOp = new TapOperator();
        }

        private void SaveCommandPush()
        {
            //Run saveDelegate
            SAveToDbMethod();

            //Return saveDelegate to startMethod
            _saveToDbMethod = PostSaveMethod;
            
            //Nulstil objeckt af TESTmoment
            GenerateObjectsToBeSaved();

            // Slet indtastninger i view
            foreach (TapOperatorMoment moment in TapOperatorMoments)
            {
                moment.Moment = "";
            }

            foreach (FluidWeightControl weight  in FluidWeightControls)
            {
                weight.Weight = "";
            }

            IsCheckedHeuftLid = false;

            Comment = "";
            

            // Stopper timer
            if (AutoSaveTimer.TimeSinceLastKeyDownTimer != null)
            {
                AutoSaveTimer.TimeSinceLastKeyDownTimer.Dispose();
            }
        }

        private bool ProcessItemExists()
        {
            bool exists = false;

            if (ProItem.FinishedItemNumber != FinishNumber || ProItem.ProcessNumber != Processnumber)
            {
                ProcessingItems comparableProcessingItemFromDb = ComGeneric.GetOne<ProcessingItems, string>(Processnumber);
                ProItem = new ProcessingItems
                {
                    FinishedItemNumber = FinishNumber,
                    ProcessNumber = Processnumber
                };
                if (comparableProcessingItemFromDb == null)
                {
                    if (!ComGeneric.Post(ProItem))
                    {
                        // TODO Warn about connection problem to DB
                    }
                }
                else if (comparableProcessingItemFromDb.Equals(ProItem))
                {
                    exists = true;
                }
            }

            return exists;
        }

        private void PrepareSave()
        {
            if (!ProcessItemExists())
            {
                // TODO Warn about conflicting ProcessItem
                Debug.WriteLine("ProcessItem conflict");
            }

            double[] bottleMoments = new double[15];

            for (int i = 0; i < TapOperatorMoments.Length; i++)
            {
                if (TapOperatorMoments[i].Moment == null) bottleMoments[i] = 0;
                else
                {
                    TapOperatorMoments[i].Moment = TapOperatorMoments[i].Moment.Replace(',', '.');
                    if (!double.TryParse(TapOperatorMoments[i].Moment, out bottleMoments[i])) bottleMoments[i] = 0;
                }
            }
            
            TapOp.Bottle1 = bottleMoments[0];
            TapOp.Bottle2 = bottleMoments[1];
            TapOp.Bottle3 = bottleMoments[2];
            TapOp.Bottle4 = bottleMoments[3];
            TapOp.Bottle5 = bottleMoments[4];
            TapOp.Bottle6 = bottleMoments[5];
            TapOp.Bottle7 = bottleMoments[6];
            TapOp.Bottle8 = bottleMoments[7];
            TapOp.Bottle9 = bottleMoments[8];
            TapOp.Bottle10 = bottleMoments[9];
            TapOp.Bottle11 = bottleMoments[10];
            TapOp.Bottle12 = bottleMoments[11];
            TapOp.Bottle13 = bottleMoments[12];
            TapOp.Bottle14 = bottleMoments[13];
            TapOp.Bottle15 = bottleMoments[14];
            

            double[] bottleWeight = new double[6];

            for (int i = 0; i < FluidWeightControls.Length; i++)
            {
                if (FluidWeightControls[i].Weight == null) bottleWeight[i] = 0;
                else
                {
                    FluidWeightControls[i].Weight = FluidWeightControls[i].Weight.Replace(',', '.');
                    if (!double.TryParse(FluidWeightControls[i].Weight, out bottleWeight[i])) bottleWeight[i] = 0;
                }
        
            }

            TapOp.Weight1 = bottleWeight[0];
            TapOp.Weight2 = bottleWeight[1];
            TapOp.Weight3 = bottleWeight[2];
            TapOp.Weight4 = bottleWeight[3];
            TapOp.Weight5 = bottleWeight[4];
            TapOp.Weight6 = bottleWeight[5];

            TapOp.ProcessNumber = Processnumber;

            TapOp.HeuftLid = IsCheckedHeuftLid;
            TapOp.HeuftFillingHeight = IsCheckedFillHeight;
            TapOp.ProductTasted = IsCheckedProductTasted;
            TapOp.SukkerStickTest = IsCheckedSugarTest;
            TapOp.DropTest = IsCheckedDropTest;

            TapOp.Comments = Comment;
        }
        

        private void PostSaveMethod()
        {
            PrepareSave();
            TapOp.ClockDate = DateTime.Now;
            if (ComGeneric.Post(TapOp))
            {
                TapOp.ID = ComGeneric.TapOperatorId;
            }
            else
            {
                // TODO meld fejl om kommunikaiton til server
            }
            
            _saveToDbMethod = PutSaveMethod;
        }

        private void PutSaveMethod()
        {
            PrepareSave();
            if (!ComGeneric.Put(TapOp.ID, TapOp))
            {
                // TODO meld fejl om kommunikaiton til server
            }


        }

        private void LiquidTankCommandMethod(object obj)
        {
            SelectionChangedEventArgs args = obj as SelectionChangedEventArgs;
            LiquidTanks liquidTank = args?.AddedItems[0] as LiquidTanks;
            Debug.WriteLine(liquidTank?.Name);
            TapOp.LiquidTank = liquidTank?.Name;
        }


        
        public TapOperator TapOp { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public AutoSaveTimer AutoSaveTimer { get; set; }
        public TapOperatorMoment[] TapOperatorMoments { get; set; } = new TapOperatorMoment[15];
        public FluidWeightControl[] FluidWeightControls { get; set; } = new FluidWeightControl[6];
        public RelayCommand<object> LiquidTankCommand { get; set; }
        public int FinishNumber { get; set; }
        public string Processnumber { get; set; }

        public DbComGeneric ComGeneric { get; set; } = DbComGeneric.ComGeneric;

        public SaveToDbMethod SAveToDbMethod
        {
            get { return _saveToDbMethod; }
        }

        public static SaveTapOperator Save
        {
            get
            {
                if (_save == null)
                {
                    _save = new SaveTapOperator();
                }
                return _save;
            }
        }

        public ProcessingItems ProItem
        {
            get { return _proItem; }
            set
            {
                _proItem = value;

            }
        }

        public bool IsCheckedHeuftLid
        {
            get { return _isCheckedHeuftLid; }
            set
            {
                _isCheckedHeuftLid = value;
                OnPropertyChanged();
            }
        }

        public bool IsCheckedFillHeight
        {
            get { return _isCheckedFillHeight; }
            set
            {
                _isCheckedFillHeight = value;
                OnPropertyChanged();
            }
        }

        public bool IsCheckedProductTasted
        {
            get { return _isCheckedProductTasted; }
            set
            {
                _isCheckedProductTasted = value;
                OnPropertyChanged();
            }
        }

        public bool IsCheckedSugarTest
        {
            get { return _isCheckedSugarTest; }
            set
            {
                _isCheckedSugarTest = value;
                OnPropertyChanged();
            }
        }

        public bool IsCheckedDropTest
        {
            get { return _isCheckedDropTest; }
            set
            {
                _isCheckedDropTest = value;
                OnPropertyChanged();
            }
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
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
