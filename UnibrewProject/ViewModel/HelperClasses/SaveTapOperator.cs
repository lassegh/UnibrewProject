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

        /// <summary>
        /// Delegate for enten post eller put method
        /// </summary>
        /// <param name="caller">Defineres som button eller timer</param>
        /// <returns>bool - kan der gemmes?</returns>
        public delegate bool SaveToDbMethod(string caller);

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

        private FinishedItems _currentFinishedItem;

        private SaveTapOperator()
        {
            for (int i = 0; i < TapOperatorMoments.Length; i++) // initialiserer objekter i listen TapOperatorMoments
            {
                TapOperatorMoments[i] = new TapOperatorMoment();
            }

            for (int i = 0; i < FluidWeightControls.Length; i++) // initialiserer objekter i listen FluidWeightControls
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
            CurrentFinishedItem = new FinishedItems();
        }

        private void GenerateObjectsToBeSaved() // Opretter ny TapOperator
        {
            TapOp = new TapOperator();
        }

        private void SaveCommandPush()
        {
            // Stopper timer
            AutoSaveTimer.StopTimer();

            //Run saveDelegate
            if (SAveToDbMethod("button"))
            {
                //Return saveDelegate to startMethod
                _saveToDbMethod = PostSaveMethod;

                //Nulstil objeckt af TapOperator
                GenerateObjectsToBeSaved();

                // Slet indtastninger i momenter
                foreach (TapOperatorMoment moment in TapOperatorMoments)
                {
                    moment.Moment = "";
                }

                // Sletter indtastninger vægtkontrol
                foreach (FluidWeightControl weight in FluidWeightControls)
                {
                    weight.Weight = "";
                }

                // Fjerner kryds i checkbokse
                IsCheckedHeuftLid = false;
                IsCheckedFillHeight = false;
                IsCheckedProductTasted = false;
                IsCheckedSugarTest = false;
                IsCheckedDropTest = false;

                // Sletter kommentar
                Comment = "";
            }
        }

        private bool ProcessItemExists(string caller) // Tjekker om processnummer eksisterer i forvejen. Om det passer med færdigvarenummer. 
        {
            bool exists = true;

            if (FinishNumber==0 || Processnumber.Equals("")) // Er der indtastet nul eller ingenting
            {
                //Warn about missing fields if caller is button
                if(caller.Equals("button"))ShowMsg.ShowMessage("Indtast venligst processordrenummer og færdigvarenummer");
                exists = false;
            }
            else if (ProItem.FinishedItemNumber != FinishNumber || ProItem.ProcessNumber != Processnumber) // Er der bare én af indtastningerne, der ikke passer med lagret processItem
            {
                // Opretter et processingItem at sammenligne med.   Et processingItem forsøges hentet fra databasen med det indtastede processNummer som ID
                ProcessingItems comparableProcessingItemFromDb = ComGeneric.GetOne<ProcessingItems, string>(Processnumber);

                ProItem = new ProcessingItems // Opretter nyt processingItem til lagret processingItem
                {
                    FinishedItemNumber = FinishNumber, // Oprettes med indtastede oplysninger
                    ProcessNumber = Processnumber
                };
                if (comparableProcessingItemFromDb == null) // Er processingItem - hentet fra databasen - null?
                {
                    if (!ComGeneric.Post(ProItem))// Gemmer nyt processingItem i DB
                    {
                        //Warn about connection problem to DB
                        ShowMsg.ShowMessage("Der er ikke forbindelse til serveren");
                        exists = false;
                    }
                }
                else if (!comparableProcessingItemFromDb.FinishedItemNumber.Equals(ProItem.FinishedItemNumber)) // Er indtastet færdigvarenummer og processingitems færdigvarenummer (fra databasen) ens?
                {
                    // Warn about conflicting ProcessItem
                    ShowMsg.ShowMessage("Procesordrenummeret eksisterer i forvejen, men med et andet færdigvarenummer");
                    exists = false;
                }
            }

            return exists;
        }

        private bool PrepareSave(string caller)
        {
            if (!ProcessItemExists(caller))
            {
                
                return false;
            }

            // Parser strings til doubles for momenter
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

            // overfører doubles til objekt af TapOperator
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

            // Parser strings til doubles for vægtkontrol
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

            // Overfører doubles til objekt af TapOperator
            TapOp.Weight1 = bottleWeight[0];
            TapOp.Weight2 = bottleWeight[1];
            TapOp.Weight3 = bottleWeight[2];
            TapOp.Weight4 = bottleWeight[3];
            TapOp.Weight5 = bottleWeight[4];
            TapOp.Weight6 = bottleWeight[5];

            // Overfører resterende data til objekt af TapOperator
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


        private bool PostSaveMethod(string caller) // Metode til delegaten SaveToDbMethod. Denne køres første gang der gemmes om det er med knap eller timer
        {
            if (PrepareSave(caller))
            {
                TapOp.ClockDate = DateTime.Now; // Tidsstempel for NU tilføjes til objekt
                if (ComGeneric.Post<TapOperator>(TapOp)) // Der gemmes til db
                {
                    TapOp.ID = ComGeneric.TapOperatorId; // Id'et fra db hentes
                }
                else
                {
                    // meld fejl om kommunikaiton til server
                    ShowMsg.ShowMessage("Der er ikke forbindelse til serveren");
                    return false;
                }

                _saveToDbMethod = PutSaveMethod; // Delegaten ændres til PutSaveMethod
                return true;
            }

            return false;
        }

        private bool PutSaveMethod(string caller) // Metode til delegaten SaveToDbMethod. Denne køres indtil knappen aktiveres
        {
            if (PrepareSave(caller))
            {
                if (!ComGeneric.Put(TapOp.ID, TapOp)) // Der gemmes til db
                {
                    // meld fejl om kommunikaiton til server
                    ShowMsg.ShowMessage("Der er ikke forbindelse til serveren");
                    return false;
                }

                return true;
            }

            return false;
        }

        private void LiquidTankCommandMethod(object obj) // Metode for eventet i combobox for Væsketanke
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
