using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using UnibrewProject.Annotations;

namespace UnibrewProject.ViewModel.HelperClasses.SaveClasses
{
    /// <summary>
    /// Timer til automatisk gemfunktion.
    /// </summary>
    public class AutoSaveTimer : INotifyPropertyChanged
    {
        private delegate void FadingTextTimerCallBack(Object state);
        private SaveTapOperator _save;
        private FadingTextTimerCallBack fadingTextCallBack;
        private float _saveBlockOpacity = 0;

        public AutoSaveTimer(SaveTapOperator save)
        {
            fadingTextCallBack = FadingText;
            _save = save;
        }

        /// <summary>
        /// Stopper timer
        /// </summary>
        public void StopTimer()
        {
            TimeSinceLastKeyDownTimer?.Dispose();
        }

        /// <summary>
        /// Starter eller genstarter timer
        /// </summary>
        public void StartTimer()
        {
            if (TimeSinceLastKeyDownTimer != null)
            {
                TimeSinceLastKeyDownTimer.Dispose();
                TimeSinceLastKeyDownTimer = new Timer(Callback, null, 10000, Timeout.Infinite);
            }
            else TimeSinceLastKeyDownTimer = new Timer(Callback, null, 10000, Timeout.Infinite);
        }

        private void Callback(Object state)
        {
            _save.SAveToDbMethod();
            FadingTextTimer = new Timer(new TimerCallback(fadingTextCallBack), null, 1, Timeout.Infinite);
        }

        private void FadingText(Object state)
        {
            SaveBlockOpacity = SaveBlockOpacity + 0.1f;
            if (SaveBlockOpacity < 1)
            {
                FadingTextTimer.Change(100, Timeout.Infinite);
            }
            else
            {
                fadingTextCallBack = FadingTextOut;
                FadingTextTimer.Change(100, Timeout.Infinite);
            }
        }

        private void FadingTextOut(Object state)
        {
            SaveBlockOpacity = SaveBlockOpacity - 0.1f;
            if (SaveBlockOpacity > 0)
            {
                FadingTextTimer.Change(100, Timeout.Infinite);
            }
            else
            {
                fadingTextCallBack = FadingText;
                FadingTextTimer.Dispose();
            }
        }

        public Timer FadingTextTimer { get; set; }
        public Timer TimeSinceLastKeyDownTimer { get; set; }

        public float SaveBlockOpacity
        {
            get { return _saveBlockOpacity; }
            set
            {
                _saveBlockOpacity = value;
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
