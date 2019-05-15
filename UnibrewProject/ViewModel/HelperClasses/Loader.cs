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

        public void GetTapOperators()
        {
            TapOperatorList = ComGeneric.GetAll<TapOperator>();
            List<double> bottleOne = new List<double>();
            List<double> bottleTwo = new List<double>();
            List<double> bottleThree = new List<double>();
            foreach (TapOperator tapOperator in TapOperatorList)
            {
                bottleOne.Add(tapOperator.Bottle1);
                bottleTwo.Add(tapOperator.Bottle2);
                bottleThree.Add(tapOperator.Bottle3);
            }


            MomentCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Flaske 1",
                    Values = new ChartValues<double>(bottleOne) {}
                },
                new LineSeries
                {
                    Title = "Flaske 2",
                    Values = new ChartValues<double>(bottleTwo) {},
                },
                new LineSeries
                {
                    Title = "Flaske 3",
                    Values = new ChartValues<double>(bottleThree) {},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    Transform3D = new CompositeTransform3D()
                }
            };
            MomentLabels = new List<string>();
            foreach (TapOperator tapOperator in TapOperatorList)
            {
                MomentLabels.Add(tapOperator.ClockDate.ToString("dd-MM-yyyy HH:mm"));
            }
            YFormatter = value => value.ToString("N");
        }

        public SeriesCollection MomentCollection { get; set; } // En samlet collection af al data, der skal vises i graf
        public List<string> MomentLabels { get; set; } // De labels, der skal vises på x-aksen
        public Func<double, string> YFormatter { get; set; } // De værdier, der vises på y-aksen

        public List<FinishedItems> FinishedItemsList { get; set; }
        public List<LiquidTanks> LiquidTanksList { get; set; }
        public List<TapOperator> TapOperatorList { get; set; }

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
