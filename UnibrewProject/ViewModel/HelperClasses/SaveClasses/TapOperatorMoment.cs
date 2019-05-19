using System;
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
    /// Klasse til momentkontrols indtastninger.
    /// Klassen indeholder kun en property; Moment 
    /// </summary>
    public class TapOperatorMoment : INotifyPropertyChanged
    {
        private string _moment;
        
        /// <summary>
        /// Holder indtastet moment
        /// </summary>
        public string Moment
        {
            get { return _moment; }
            set
            {
                _moment = value;
                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () =>
                    {
                        // Your UI update code goes here!
                        OnPropertyChanged();
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
