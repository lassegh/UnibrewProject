namespace UnibrewProject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TapOperator
    {
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

        public bool HeuftLid { get; set; }

        public bool HeuftFillingHeight { get; set; }

        public bool ProductTasted { get; set; }

        public bool SukkerStickTest { get; set; }

        public bool DropTest { get; set; }

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
    }
}
