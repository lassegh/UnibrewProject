﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using UnibrewProject.Annotations;

namespace UnibrewProject.ViewModel.HelperClasses.SaveClasses
{
    /// <summary>
    /// Klasse til vægtkontrols indtastninger.
    /// Klassen indeholder kun en property; Weight 
    /// </summary>
    public class FluidWeightControl : INotifyPropertyChanged
    {
        private string _weight;
        private SaveTapOperator _save;

        public FluidWeightControl(SaveTapOperator save)
        {
            _save = save;
        }

        /// <summary>
        /// Holder indtastet vægt
        /// </summary>
        public string Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () =>
                    {
                        // Your UI update code goes here!
                        OnPropertyChanged();
                        _save.CalculateAverageWeight();
                    }
                );
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
