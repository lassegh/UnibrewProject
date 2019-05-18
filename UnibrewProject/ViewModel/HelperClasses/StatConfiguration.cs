using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnibrewProject.Annotations;

namespace UnibrewProject.ViewModel.HelperClasses
{
    /// <summary>
    /// Holder configuration af managerdelen i programmet - så lang tid programmet kører.
    /// </summary>
    public class StatConfiguration : INotifyPropertyChanged
    {
        private static StatConfiguration _statConfig = null;

        private StatConfiguration()
        {
            FromDateTime = new DateTime(2019, 1, 1);
            ToDateTime = DateTime.Today;
            ShowingBottles = new bool[16];
            for (int i = 0; i < ShowingBottles.Length; i++)
            {
                ShowingBottles[i] = true;
            }
        }

        public void ToggleGraphs(string name)
        {
            switch (name)
            {
                case "All":
                    if (ShowingBottles[0])
                    {
                        for (int i = 0; i < ShowingBottles.Length; i++)
                        {
                            ShowingBottles[i] = false;
                            
                        }
                    }
                    else for (int i = 0; i < ShowingBottles.Length; i++)
                    {
                        ShowingBottles[i] = true;
                    }

                    for (int i = 0; i < ShowingBottles.Length; i++)
                    {
                        OnPropertyChanged(ShowingBottles[i].ToString());
                    }
                    break;

                case "B1":
                    break;

                case "B2":
                    break;

                case "B3":
                    break;

                case "B4":
                    break;

                case "B5":
                    break;

                case "B6":
                    break;

                case "B7":
                    break;

                case "B8":
                    break;

                case "B9":
                    break;

                case "B10":
                    break;

                case "B11":
                    break;

                case "B12":
                    break;

                case "B13":
                    break;

                case "B14":
                    break;

                case "B15":
                    break;
            }
        }



        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }

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

        public bool[] ShowingBottles { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
