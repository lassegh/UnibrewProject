using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnibrewProject.Model;

namespace UnibrewProject.ViewModel.HelperClasses
{
    /// <summary>
    /// Klasse, der sørger for at hente data til programmet
    /// </summary>
    public class Loader
    {
        private static Loader _load = null;
        private Loader()
        {
            FinishedItemsList = ComGeneric.GetAll<FinishedItems>();
            LiquidTanksList = ComGeneric.GetAll<LiquidTanks>();
            ProcessNumberList = ComGeneric.GetAll<ProcessingItems>();
            /*foreach (ProcessingItems items in ProcessNumberList)
            {
                items.TapOperator = (ICollection<TapOperator>) ComGeneric.GetAll<TapOperator>()
                    .Where(x => x.ProcessNumber.Equals(items.ProcessNumber));
            }*/
            Debug.WriteLine(ProcessNumberList);
        }

        public List<FinishedItems> FinishedItemsList { get; set; }
        public List<LiquidTanks> LiquidTanksList { get; set; }
        public List<ProcessingItems> ProcessNumberList { get; set; }

        public DbComGeneric ComGeneric { get; set; } = DbComGeneric.ComGeneric;

        public static Loader Load
        {
            get
            {
                if (_load == null)
                {
                    _load = new Loader();
                }
                return _load;
            }
        }
    }
}
