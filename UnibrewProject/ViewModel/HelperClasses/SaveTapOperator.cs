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
    /// Gemmer indtastninger fra inputColSevenPage
    /// </summary>
    public class SaveTapOperator : INotifyPropertyChanged
    {
        private static SaveTapOperator _save = null;
        public delegate bool SaveToDbMethod();
        private SaveToDbMethod _saveToDbMethod;

        private string _processnumber;
        private bool _isCheckedHeuftLid;
        private bool _isCheckedFillHeight;
        private bool _isCheckedProductTasted;
        private bool _isCheckedSugarTest;
        private bool _isCheckedDropTest;

        private string _comment;

        private string _lidNumber;
        private string _preformMaterialNumber;

        private string _averageWeight;

        private SaveTapOperator()
        {
            for (int i = 0; i < TapOperatorMoments.Length; i++)
            {
                TapOperatorMoments[i] = new TapOperatorMoment();
            }

            for (int i = 0; i < FluidWeightControls.Length; i++)
            {
                FluidWeightControls[i] = new FluidWeightControl(this);
            }

            GenerateObjectsToBeSaved();
            _saveToDbMethod = PostSaveMethod;
            SaveCommand = new RelayCommand(SaveCommandPush);
            AutoSaveTimer = new AutoSaveTimer(this);
            LiquidTankCommand = new RelayCommand<object>(LiquidTankCommandMethod);
            ProItem = new ProcessingItems();
            ShowMsg = new ShowMsg();
            CalculateAverageWeight();
        }

        private void GenerateObjectsToBeSaved()
        {
            TapOp = new TapOperator();
        }

        private void SaveCommandPush()
        {
            // Stopper timer
            AutoSaveTimer.StopTimer();

            //Run saveDelegate
            if (SAveToDbMethod())
            {
                //Return saveDelegate to startMethod
                _saveToDbMethod = PostSaveMethod;

                //Nulstil objeckt af TapOperator
                GenerateObjectsToBeSaved();

                // Slet indtastninger i view
                foreach (TapOperatorMoment moment in TapOperatorMoments)
                {
                    moment.Moment = "";
                }

                foreach (FluidWeightControl weight in FluidWeightControls)
                {
                    weight.Weight = "";
                }

                IsCheckedHeuftLid = false;
                IsCheckedFillHeight = false;
                IsCheckedProductTasted = false;
                IsCheckedSugarTest = false;
                IsCheckedDropTest = false;

                Comment = "";
            }
        }

        private bool ProcessItemExists()
        {
            bool exists = true;

            if (FinishNumber==0 || Processnumber.Equals(""))
            {
                //Warn about missing fields
                ShowMsg.ShowMessage("Indtast venligst processordrenummer og færdigvarenummer");
                exists = false;
            }
            else if (ProItem.FinishedItemNumber != FinishNumber || ProItem.ProcessNumber != Processnumber)
            {
                ProcessingItems comparableProcessingItemFromDb = ComGeneric.GetOne<ProcessingItems, string>(Processnumber);
                ProItem = new ProcessingItems
                {
                    FinishedItemNumber = FinishNumber,
                    ProcessNumber = Processnumber
                };
                if (comparableProcessingItemFromDb == null)
                {
                    if (!ComGeneric.Post(ProItem))// Gemmer nyt processingItem i DB
                    {
                        //Warn about connection problem to DB
                        ShowMsg.ShowMessage("Der er ikke forbindelse til serveren");
                        exists = false;
                    }
                }
                else if (!comparableProcessingItemFromDb.FinishedItemNumber.Equals(ProItem.FinishedItemNumber))
                {
                    // Warn about conflicting ProcessItem
                    ShowMsg.ShowMessage("Procesordrenummeret eksisterer i forvejen, men med et andet færdigvarenummer");
                    exists = false;
                }
            }

            return exists;
        }

        private bool PrepareSave()
        {
            if (!ProcessItemExists())
            {
                
                return false;
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

            TapOp.LidMaterialNo = LidNumber;
            TapOp.PreformMaterialNo = PreformMaterialNumber;
            return true;

        }


        private bool PostSaveMethod()
        {
            if (PrepareSave())
            {
                TapOp.ClockDate = DateTime.Now;
                if (ComGeneric.Post<TapOperator>(TapOp))
                {
                    TapOp.ID = ComGeneric.TapOperatorId;
                }
                else
                {
                    // meld fejl om kommunikaiton til server
                    ShowMsg.ShowMessage("Der er ikke forbindelse til serveren");
                    return false;
                }

                _saveToDbMethod = PutSaveMethod;
                return true;
            }

            return false;
        }

        private bool PutSaveMethod()
        {
            if (PrepareSave())
            {
                if (!ComGeneric.Put(TapOp.ID, TapOp))
                {
                    // meld fejl om kommunikaiton til server
                    ShowMsg.ShowMessage("Der er ikke forbindelse til serveren");
                    return false;
                }

                return true;
            }

            return false;
        }

        private void LiquidTankCommandMethod(object obj)
        {
            SelectionChangedEventArgs args = obj as SelectionChangedEventArgs;
            LiquidTanks liquidTank = args?.AddedItems[0] as LiquidTanks;
            TapOp.LiquidTank = liquidTank?.Name;
        }

        /// <summary>
        /// Udregner gennemsnitsværdien af indtastede kontrolvejninger
        /// </summary>
        public void CalculateAverageWeight()
        {
            double averageWeight = 0;
            double tempWeight = 0;
            int i = 0;
            foreach (FluidWeightControl weightControl in FluidWeightControls)
            {
                double.TryParse(weightControl.Weight, out tempWeight);
                if (tempWeight > 0)
                {
                    averageWeight = averageWeight + tempWeight;
                    tempWeight = 0;
                    i++;
                }
            }

            AverageWeight = (averageWeight/i).ToString("N");

            // TODO tilpas AverageWeightBorderColor afhængig af AverageWeight
        }

        /// <summary>
        /// Farve på AverageWeight's kant
        /// </summary>
        public string AverageWeightBorderColor { get; set; } = "Black";

        /// <summary>
        /// Gennemsnitsvægt for kontrolvejning
        /// </summary>
        public string AverageWeight
        {
            get { return _averageWeight; }
            set
            {
                _averageWeight = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Objekt af showMsg, der viser pop op beskeder
        /// </summary>
        public ShowMsg ShowMsg { get; set; }

        /// <summary>
        /// Objekt af tapOperator, der kan gemmes til
        /// </summary>
        public TapOperator TapOp { get; set; }

        /// <summary>
        /// RelayCommand til gemknap
        /// </summary>
        public RelayCommand SaveCommand { get; set; }

        /// <summary>
        /// Objekt af autosavetimer, der sørger for automatisk gemning
        /// </summary>
        public AutoSaveTimer AutoSaveTimer { get; set; }

        /// <summary>
        /// Array af tapOperatorMoments, der bruges til momentindtastninger
        /// </summary>
        public TapOperatorMoment[] TapOperatorMoments { get; set; } = new TapOperatorMoment[15];

        /// <summary>
        /// Array af FluidWeightControls, der bruges til vægtindtastninger
        /// </summary>
        public FluidWeightControl[] FluidWeightControls { get; set; } = new FluidWeightControl[6];

        /// <summary>
        /// RelayCommand til valg af Liquidtanks, der sørger for den valgte tank gemmes
        /// </summary>
        public RelayCommand<object> LiquidTankCommand { get; set; }

        /// <summary>
        /// int til færdigvarenummer
        /// </summary>
        public int FinishNumber { get; set; }

        /// <summary>
        /// Henvisning til persistensklassen
        /// </summary>
        public DbComGeneric ComGeneric { get; set; } = DbComGeneric.ComGeneric;

        /// <summary>
        /// Delegate, der skifter mellem post gemning og put gemning
        /// </summary>
        public SaveToDbMethod SAveToDbMethod
        {
            get { return _saveToDbMethod; }
        }

        /// <summary>
        /// Indgang til denne singleton - SaveTapOperator
        /// </summary>
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

        /// <summary>
        /// Objekt af processingItem, der kan gemmes til
        /// </summary>
        public ProcessingItems ProItem { get; set; }

        /// <summary>
        /// bool til HeuftLid
        /// </summary>
        public bool IsCheckedHeuftLid
        {
            get { return _isCheckedHeuftLid; }
            set
            {
                _isCheckedHeuftLid = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// bool til FillingHeight
        /// </summary>
        public bool IsCheckedFillHeight
        {
            get { return _isCheckedFillHeight; }
            set
            {
                _isCheckedFillHeight = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// bool til Smagstest
        /// </summary>
        public bool IsCheckedProductTasted
        {
            get { return _isCheckedProductTasted; }
            set
            {
                _isCheckedProductTasted = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// bool til sukkerTest
        /// </summary>
        public bool IsCheckedSugarTest
        {
            get { return _isCheckedSugarTest; }
            set
            {
                _isCheckedSugarTest = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// bool til dropTest
        /// </summary>
        public bool IsCheckedDropTest
        {
            get { return _isCheckedDropTest; }
            set
            {
                _isCheckedDropTest = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// String til kommentarer
        /// </summary>
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// String til lidNumber
        /// </summary>
        public string LidNumber
        {
            get { return _lidNumber; }
            set
            {
                _lidNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// String til preformNummer
        /// </summary>
        public string PreformMaterialNumber
        {
            get { return _preformMaterialNumber; }
            set
            {
                _preformMaterialNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// String til Process nummer
        /// </summary>
        public string Processnumber
        {
            get
            {
                if (_processnumber == null) return "";
                return _processnumber;
            }
            set { _processnumber = value.PadRight(10); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
