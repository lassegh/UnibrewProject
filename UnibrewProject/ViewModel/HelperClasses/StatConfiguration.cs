using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using UnibrewProject.Annotations;
using UnibrewProject.Model;

namespace UnibrewProject.ViewModel.HelperClasses
{
    /// <summary>
    /// Holder configuration af managerdelen i programmet - så lang tid programmet kører.
    /// </summary>
    public class StatConfiguration : INotifyPropertyChanged
    {
        private static StatConfiguration _statConfig = null;
        private FinishedItems _finishedItemForWeightGraph;


        private StatConfiguration()
        {
            FromDateTime = new DateTime(2019, 1, 1);
            ToDateTime = DateTime.Today;
            ShowingBottles = new bool[15];
            for (int i = 0; i < ShowingBottles.Length; i++)
            {
                ShowingBottles[i] = true;
            }
        }

        /// <summary>
        /// Ændrer bools i array, afhængig af checkbokse
        /// </summary>
        /// <param name="name"></param>
        public void ToggleGraphs(string name)
        {
            switch (name)
            {
                case "B1":
                    ShowingBottles[0] = !ShowingBottles[0];
                        break;

                case "B2":
                    ShowingBottles[1] = !ShowingBottles[1];
                    break;

                case "B3":
                    ShowingBottles[2] = !ShowingBottles[2];
                    break;

                case "B4":
                    ShowingBottles[3] = !ShowingBottles[3];
                    break;

                case "B5":
                    ShowingBottles[4] = !ShowingBottles[4];
                    break;

                case "B6":
                    ShowingBottles[5] = !ShowingBottles[5];
                    break;

                case "B7":
                    ShowingBottles[6] = !ShowingBottles[6];
                    break;

                case "B8":
                    ShowingBottles[7] = !ShowingBottles[7];
                    break;

                case "B9":
                    ShowingBottles[8] = !ShowingBottles[8];
                    break;

                case "B10":
                    ShowingBottles[9] = !ShowingBottles[9];
                    break;

                case "B11":
                    ShowingBottles[10] = !ShowingBottles[10];
                    break;

                case "B12":
                    ShowingBottles[11] = !ShowingBottles[11];
                    break;

                case "B13":
                    ShowingBottles[12] = !ShowingBottles[12];
                    break;

                case "B14":
                    ShowingBottles[13] = !ShowingBottles[13];
                    break;

                case "B15":
                    ShowingBottles[14] = !ShowingBottles[14];
                    break;
            }
        }

        /// <summary>
        /// Holder liste af tapOperators, der indgår i vægtkontrolsgraf
        /// </summary>
        public List<TapOperator> TapOperatorListForWeightGraph { get; set; } = new List<TapOperator>();
        
        /// <summary>
        /// Gemmer indtastede dato i memory
        /// </summary>
        public DateTime FromDateTime { get; set; }

        /// <summary>
        /// Gemmer indtastede dato i memory
        /// </summary>
        public DateTime ToDateTime { get; set; }

        /// <summary>
        /// Indgang til denne singleton - StatConfiguration
        /// </summary>
        public static StatConfiguration StatConfig
        {
            get
            {
                if (_statConfig == null)
                {
                    _statConfig = new StatConfiguration();
                }
                return _statConfig;
            }
        }

        /// <summary>
        /// Array af bools, der holder synligheden af den enkelte momentGraf
        /// </summary>
        public bool[] ShowingBottles { get; set; }

        /// <summary>
        /// Holder færdigvareoplysninger for vægtkontrolsgraf
        /// </summary>
        public FinishedItems FinishedItemForWeightGraph
        {
            get
            {
                if (_finishedItemForWeightGraph == null)_finishedItemForWeightGraph = new FinishedItems() { Name = "Vælg færdigvare"}; // Hvis der ikke holdes et objekt, vil combobox vise "vælg færdigvare"
                return _finishedItemForWeightGraph;
            }
            set { _finishedItemForWeightGraph = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
