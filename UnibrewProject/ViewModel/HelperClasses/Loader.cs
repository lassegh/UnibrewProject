using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Media3D;
using LiveCharts;
using LiveCharts.Uwp;
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
        }

        /// <summary>
        /// Henter liste af TapOperators
        /// </summary>
        /// <returns>List af TapOperators</returns>
        public List<TapOperator> GetTapOperators()
        {
            return ComGeneric.GetAll<TapOperator>();
        }

        public List<FinishedItems> FinishedItemsList { get; set; }
        public List<LiquidTanks> LiquidTanksList { get; set; }

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
