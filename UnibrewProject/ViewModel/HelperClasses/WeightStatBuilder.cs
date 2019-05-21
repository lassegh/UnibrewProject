using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Uwp;
using UnibrewProject.Annotations;
using UnibrewProject.Model;

namespace UnibrewProject.ViewModel.HelperClasses
{
    public class WeightStatBuilder : INotifyPropertyChanged
    {
        private SeriesCollection _weightCollection;
        private List<string> _weightLabels;
        private Func<double, string> _yFormatter;

        /// <summary>
        /// Opretter de lister, der skal bruges til de ønskede grafer
        /// </summary>
        /// <param name="tapOperatorList">Liste af TapOperators</param>
        /// <param name="startDate">Startdato for grafer</param>
        /// <param name="endDate">Slutdato for grafer</param>
        /// <param name="thisItem">Den færdigvare, der kigges på</param>
        public void RebiuldStats(List<TapOperator> tapOperatorList, DateTime startDate, DateTime endDate, FinishedItems thisItem)
        {
            List<double> weightMin = new List<double>();
            List<double> weightMax = new List<double>();
            List<double> weightActual = new List<double>();

            WeightLabels = new List<string>();
            foreach (TapOperator tapOperator in tapOperatorList)
            {
                if (tapOperator.ClockDate > startDate && tapOperator.ClockDate < endDate)
                {
                    double weight = 0;
                    if (tapOperator.Weight1 > 0) weight = weight + tapOperator.Weight1;
                    if (tapOperator.Weight2 > 0) weight = weight + tapOperator.Weight2;
                    if (tapOperator.Weight3 > 0) weight = weight + tapOperator.Weight3;
                    if (tapOperator.Weight4 > 0) weight = weight + tapOperator.Weight4;
                    if (tapOperator.Weight5 > 0) weight = weight + tapOperator.Weight5;
                    if (tapOperator.Weight6 > 0) weight = weight + tapOperator.Weight6;

                    if (weight > 0)
                    {
                        weightActual.Add(weight/6);
                        weightMin.Add(thisItem.WeightMin);
                        weightMax.Add(thisItem.WeightMax);
                        WeightLabels.Add(tapOperator.ClockDate.ToString("dd-MM-yyyy HH:mm"));
                    }
                }
            }


            WeightCollection = new SeriesCollection();

            WeightCollection.Add(new LineSeries
            {
                Title = "Maksimum vægt",
                Values = new ChartValues<double>(weightMax) { }
            });

            WeightCollection.Add(new LineSeries
            {
                Title = "Faktisk vægt",
                Values = new ChartValues<double>(weightActual) { }
            });

            WeightCollection.Add(new LineSeries
            {
                Title = "Minimum vægt",
                Values = new ChartValues<double>(weightMin) { }
            });

            YFormatter = value => value.ToString("N");
        }

        /// <summary>
        /// Serie af grafer
        /// </summary>
        public SeriesCollection WeightCollection
        {
            get { return _weightCollection; }
            set
            {
                _weightCollection = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Labels på de datoer, grafen indeholder - x-aksen
        /// </summary>
        public List<string> WeightLabels
        {
            get { return _weightLabels; }
            set
            {
                _weightLabels = value;
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
