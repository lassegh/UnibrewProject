using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnibrewProject.Annotations;

namespace UnibrewProject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TapOperator : INotifyPropertyChanged
    {

        private bool _heuftLid;
        private bool _heuftFillingHeight;
        private bool _productTasted;
        private bool _sukkerStickTest;
        private bool _dropTest;

        public TapOperator()
        {
            ClockDate = DateTime.Now;
        }

        public int ID { get; set; }

        public DateTime ClockDate { get; set; }

        public string LidMaterialNo { get; set; }

        public string PreformMaterialNo { get; set; }

        public string LiquidTank { get; set; }

        public double Bottle1 { get; set; }

        public double Bottle2 { get; set; }

        public double Bottle3 { get; set; }

        public double Bottle4 { get; set; }

        public double Bottle5 { get; set; }

        public double Bottle6 { get; set; }

        public double Bottle7 { get; set; }

        public double Bottle8 { get; set; }

        public double Bottle9 { get; set; }

        public double? Bottle10 { get; set; }

        public double Bottle11 { get; set; }

        public double Bottle12 { get; set; }

        public double Bottle13 { get; set; }

        public double Bottle14 { get; set; }

        public double Bottle15 { get; set; }

        public bool HeuftLid
        {
            get { return _heuftLid; }
            set
            {
                _heuftLid = value;
                OnPropertyChanged();
            }
        }

        public bool HeuftFillingHeight
        {
            get { return _heuftFillingHeight; }
            set
            {
                _heuftFillingHeight = value;
                OnPropertyChanged();
            }
        }

        public bool ProductTasted
        {
            get { return _productTasted; }
            set
            {
                _productTasted = value;
                OnPropertyChanged();
            }
        }

        public bool SukkerStickTest
        {
            get { return _sukkerStickTest; }
            set
            {
                _sukkerStickTest = value;
                OnPropertyChanged();
            }
        }

        public bool DropTest
        {
            get { return _dropTest; }
            set
            {
                _dropTest = value;
                OnPropertyChanged();
            }
        }

        public double Weight1 { get; set; }

        public double Weight2 { get; set; }

        public double Weight3 { get; set; }

        public double Weight4 { get; set; }

        public double Weight5 { get; set; }

        public double Weight6 { get; set; }

        public string Comments { get; set; }

        public string Operator { get; set; }

        public string ProcessNumber { get; set; }

        public virtual LiquidTanks LiquidTanks { get; set; }

        public virtual ProcessingItems ProcessingItems { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
