using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Media3D;
using LiveCharts;
using LiveCharts.Uwp;
using UnibrewProject.Annotations;
using UnibrewProject.Model;

namespace UnibrewProject.ViewModel.HelperClasses
{
    /// <summary>
    /// Tegner grafer for tilspændingsmomenter
    /// </summary>
    public class MomentStatBuilder : INotifyPropertyChanged
    {
        private SeriesCollection _momentCollection;
        private List<string> _momentLabels;
        private Func<double, string> _yFormatter;

        public MomentStatBuilder()
        {

        }

        /// <summary>
        /// Opretter de lister, der skal bruges til de ønskede grafer
        /// </summary>
        /// <param name="tapOperatorList">Liste af TapOperators</param>
        /// <param name="startDate">Startdato for grafer</param>
        /// <param name="endDate">Slutdato for grafer</param>
        /// <param name="bottleShow">Array af bools, der fortæller hvilke grafer, der skal tegnes</param>
        public void RebuildStats(List<TapOperator> tapOperatorList, DateTime startDate, DateTime endDate, bool[] bottleShow)
        {
            List<double> bottleOne = new List<double>();
            List<double> bottleTwo = new List<double>();
            List<double> bottleThree = new List<double>();
            List<double> bottleFour = new List<double>();
            List<double> bottleFive = new List<double>();
            List<double> bottleSix = new List<double>();
            List<double> bottleSeven = new List<double>();
            List<double> bottleEight = new List<double>();
            List<double> bottleNine = new List<double>();
            List<double> bottleTen = new List<double>();
            List<double> bottleEleven = new List<double>();
            List<double> bottleTwelve = new List<double>();
            List<double> bottleThirteen = new List<double>();
            List<double> bottleFourteen = new List<double>();
            List<double> bottleFifteen = new List<double>();
            MomentLabels = new List<string>();
            foreach (TapOperator tapOperator in tapOperatorList)
            {
                if (tapOperator.ClockDate > startDate && tapOperator.ClockDate < endDate)
                {
                    bottleOne.Add(tapOperator.Bottle1);
                    bottleTwo.Add(tapOperator.Bottle2);
                    bottleThree.Add(tapOperator.Bottle3);
                    bottleFour.Add(tapOperator.Bottle4);
                    bottleFive.Add(tapOperator.Bottle5);
                    bottleSix.Add(tapOperator.Bottle6);
                    bottleSeven.Add(tapOperator.Bottle7);
                    bottleEight.Add(tapOperator.Bottle8);
                    bottleNine.Add(tapOperator.Bottle9);
                    bottleTen.Add(tapOperator.Bottle10);
                    bottleEleven.Add(tapOperator.Bottle11);
                    bottleTwelve.Add(tapOperator.Bottle12);
                    bottleThirteen.Add(tapOperator.Bottle13);
                    bottleFourteen.Add(tapOperator.Bottle14);
                    bottleFifteen.Add(tapOperator.Bottle15);
                    MomentLabels.Add(tapOperator.ClockDate.ToString("dd-MM-yyyy HH:mm"));
                }
            }


            MomentCollection = new SeriesCollection();

            // TODO tilføj Lineseries på min(8) max(18) og optimal(10)

            if (bottleShow[0])MomentCollection.Add(new LineSeries
            {
                Title = "Flaske 1",
                Values = new ChartValues<double>(bottleOne) { }
            });

            if (bottleShow[1]) MomentCollection.Add(new LineSeries
            {
                Title = "Flaske 2",
                Values = new ChartValues<double>(bottleTwo) { }
            });

            if (bottleShow[2]) MomentCollection.Add(new LineSeries
            {
                Title = "Flaske 3",
                Values = new ChartValues<double>(bottleThree) { }
            });

            if (bottleShow[3]) MomentCollection.Add(new LineSeries
            {
                Title = "Flaske 4",
                Values = new ChartValues<double>(bottleFour) { }
            });

            if (bottleShow[4]) MomentCollection.Add(new LineSeries
            {
                Title = "Flaske 5",
                Values = new ChartValues<double>(bottleFive) { }
            });

            if (bottleShow[5]) MomentCollection.Add(new LineSeries
            {
                Title = "Flaske 6",
                Values = new ChartValues<double>(bottleSix) { }
            });

            if (bottleShow[6]) MomentCollection.Add(new LineSeries
            {
                Title = "Flaske 7",
                Values = new ChartValues<double>(bottleSeven) { }
            });

            if (bottleShow[7]) MomentCollection.Add(new LineSeries
            {
                Title = "Flaske 8",
                Values = new ChartValues<double>(bottleEight) { }
            });

            if (bottleShow[8]) MomentCollection.Add(new LineSeries
            {
                Title = "Flaske 9",
                Values = new ChartValues<double>(bottleNine) { }
            });

            if (bottleShow[9]) MomentCollection.Add(new LineSeries
            {
                Title = "Flaske 10",
                Values = new ChartValues<double>(bottleTen) { }
            });

            if (bottleShow[10]) MomentCollection.Add(new LineSeries
            {
                Title = "Flaske 11",
                Values = new ChartValues<double>(bottleEleven) { }
            });

            if (bottleShow[11]) MomentCollection.Add(new LineSeries
            {
                Title = "Flaske 12",
                Values = new ChartValues<double>(bottleTwelve) { }
            });

            if (bottleShow[12]) MomentCollection.Add(new LineSeries
            {
                Title = "Flaske 13",
                Values = new ChartValues<double>(bottleThirteen) { }
            });

            if (bottleShow[13]) MomentCollection.Add(new LineSeries
            {
                Title = "Flaske 14",
                Values = new ChartValues<double>(bottleFourteen) { }
            });

            if (bottleShow[14]) MomentCollection.Add(new LineSeries
            {
                Title = "Flaske 15",
                Values = new ChartValues<double>(bottleFifteen) { }
            });

            YFormatter = value => value.ToString("N");
        }

        /// <summary>
        /// Serie af grafer
        /// </summary>
        public SeriesCollection MomentCollection
        {
            get { return _momentCollection; }
            set
            {
                _momentCollection = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Labels på de datoer, grafen indeholder - x-aksen
        /// </summary>
        public List<string> MomentLabels
        {
            get { return _momentLabels; }
            set
            {
                _momentLabels = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Labels på de momenter, grafen indeholder - y-aksen
        /// </summary>
        public Func<double, string> YFormatter
        {
            get { return _yFormatter; }
            set
            {
                _yFormatter = value;
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
