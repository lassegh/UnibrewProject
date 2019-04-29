using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnibrewProject.Model
{
    public class TESTmoment
    {
        public TESTmoment(double bottle01, double bottle02, double bottle03, double bottle04, double bottle05, double bottle06, double bottle07, double bottle08, double bottle09, double bottle10, double bottle11, double bottle12, double bottle13, double bottle14, double bottle15)
        {
            Bottle01 = bottle01;
            Bottle02 = bottle02;
            Bottle03 = bottle03;
            Bottle04 = bottle04;
            Bottle05 = bottle05;
            Bottle06 = bottle06;
            Bottle07 = bottle07;
            Bottle08 = bottle08;
            Bottle09 = bottle09;
            Bottle10 = bottle10;
            Bottle11 = bottle11;
            Bottle12 = bottle12;
            Bottle13 = bottle13;
            Bottle14 = bottle14;
            Bottle15 = bottle15;
            DateTime = DateTime.Now;
        }

        public int Id { get; set; }

        public double Bottle01 { get; set; }

        public double Bottle02 { get; set; }

        public double Bottle03 { get; set; }

        public double Bottle04 { get; set; }

        public double Bottle05 { get; set; }

        public double Bottle06 { get; set; }

        public double Bottle07 { get; set; }

        public double Bottle08 { get; set; }

        public double Bottle09 { get; set; }

        public double Bottle10 { get; set; }

        public double Bottle11 { get; set; }

        public double Bottle12 { get; set; }

        public double Bottle13 { get; set; }

        public double Bottle14 { get; set; }

        public double Bottle15 { get; set; }

        public DateTime DateTime { get; set; }
    }
}
