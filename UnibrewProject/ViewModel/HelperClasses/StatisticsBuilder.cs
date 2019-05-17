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
    public class StatisticsBuilder : INotifyPropertyChanged
    {
        private SeriesCollection _momentCollection;
        private List<string> _momentLabels;
        private Func<double, string> _yFormatter;

        public StatisticsBuilder(List<TapOperator> tapOperatorList, DateTime startDate, DateTime endDate)
        {
            RebiuldStats(tapOperatorList,startDate,endDate);
        }

        public void RebiuldStats(List<TapOperator> tapOperatorList, DateTime startDate, DateTime endDate)
        {
            List<double> bottleOne = new List<double>();    
            List<double> bottleTwo = new List<double>();
            List<double> bottleThree = new List<double>();
            MomentLabels = new List<string>();
            foreach (TapOperator tapOperator in tapOperatorList)
            {
                if (tapOperator.ClockDate > startDate && tapOperator.ClockDate < endDate)
                {
                    bottleOne.Add(tapOperator.Bottle1);
                    bottleTwo.Add(tapOperator.Bottle2);
                    bottleThree.Add(tapOperator.Bottle3);
                    MomentLabels.Add(tapOperator.ClockDate.ToString("dd-MM-yyyy HH:mm"));
                }
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
                }
            };
            YFormatter = value => value.ToString("N");
        }



        public SeriesCollection MomentCollection
        {
            get { return _momentCollection; }
            set
            {
                _momentCollection = value;
                OnPropertyChanged();
            }
        }

        public List<string> MomentLabels
        {
            get { return _momentLabels; }
            set
            {
                _momentLabels = value;
                OnPropertyChanged();
            }
        }

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
