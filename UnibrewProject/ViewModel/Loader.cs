using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnibrewProject.Model;
using UnibrewProject.ViewModel.HelperClasses.DbCommunication;

namespace UnibrewProject.ViewModel
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
        }

        public List<FinishedItems> FinishedItemsList { get; set; }
        public IList<LiquidTanks> LiquidTanksList { get; set; }

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
