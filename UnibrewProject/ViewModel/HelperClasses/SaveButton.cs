using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using GalaSoft.MvvmLight.Command;
using UnibrewProject.Annotations;
using UnibrewProject.Model;

namespace UnibrewProject.ViewModel.HelperClasses
{
    /// <summary>
    /// Gem klasse.
    /// Til gem knap og automatisk gem
    /// </summary>
    public class SaveButton : INotifyPropertyChanged
    {
        private delegate void FadingTextTimerCallBack(Object state);

        private delegate void SaveToDbMethod();

        private FadingTextTimerCallBack fadingTextCallBack;
        private SaveToDbMethod saveToDbMethod;

        private float _saveBlockOpacity = 0;

        public SaveButton()
        {
            fadingTextCallBack = FadingText;
            saveToDbMethod = PostSaveMethod;
            SaveCommand = new RelayCommand(SaveCommandPush);
        }

        private void SaveCommandPush()
        {
            //TODO run saveDelegate
            //TODO Return saveDelegate to startMethod
        }

        private TESTmoment PrepareSave()
        {
            double[] bottleMoments = new double[15];
            for (int i = 0; i < BottleStrings.Length; i++)
            {
                BottleStrings[i] = BottleStrings[i].Replace(',', '.');
                if (double.TryParse(BottleStrings[i], out bottleMoments[i])) Debug.Write("String can be parsed");
                else bottleMoments[i] = 0; // TODO tilføj en warning til brugeren om fejl indtastning og om der skal fortsættes med huller i DB?
            }
            return new TESTmoment(bottleMoments[0], bottleMoments[1], bottleMoments[2], bottleMoments[3], bottleMoments[4], bottleMoments[5], bottleMoments[6], bottleMoments[7], bottleMoments[8], bottleMoments[9], bottleMoments[10], bottleMoments[11], bottleMoments[12], bottleMoments[13], bottleMoments[14]);
        }

        private void PostSaveMethod()
        {
            DbCommunication.Post(PrepareSave());
            saveToDbMethod = PutSaveMethod;
        }

        private void PutSaveMethod()
        {

        }

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
        public RelayCommand SaveCommand { get; set; }
        public string[] BottleStrings { get; set; } = new string[15];
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
