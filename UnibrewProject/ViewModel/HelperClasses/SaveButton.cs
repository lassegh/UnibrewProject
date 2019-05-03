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
        private static SaveButton _save = null;
        private string[] _bottleStrings;
        public delegate void SaveToDbMethod();
        private SaveToDbMethod _saveToDbMethod;
        private int _id;
        

        private SaveButton()
        {
            BottleStrings = new string[15];
            Tmoment = new TESTmoment();
            _saveToDbMethod = PostSaveMethod;
            SaveCommand = new RelayCommand(SaveCommandPush);
            AutoSaveTimer = new AutoSaveTimer(this);
        }

        private void SaveCommandPush()
        {
            //Run saveDelegate
            SAveToDbMethod();
            //Return saveDelegate to startMethod
            _saveToDbMethod = PostSaveMethod;
            //Nulstil objeckt af TESTmoment
            Tmoment = new TESTmoment();
            for (int i = 0; i < BottleStrings.Length; i++)
            {
                BottleStrings[i] = "";
            }
        }

        private void PrepareSave()
        {
            double[] bottleMoments = new double[15];
            for (int i = 0; i < BottleStrings.Length; i++)
            {
                if (BottleStrings[i]==null)
                {
                    BottleStrings[i] = "0";
                }
                BottleStrings[i] = BottleStrings[i].Replace(',', '.');
                if (double.TryParse(BottleStrings[i], out bottleMoments[i])) Debug.Write("String can be parsed");
                else bottleMoments[i] = 0; // TODO tilføj en warning til brugeren om fejl indtastning og om der skal fortsættes med huller i DB?
            }

            Tmoment.Bottle01 = bottleMoments[0];
            Tmoment.Bottle02 = bottleMoments[1];
            Tmoment.Bottle03 = bottleMoments[2];
            Tmoment.Bottle04 = bottleMoments[3];
            Tmoment.Bottle05 = bottleMoments[4];
            Tmoment.Bottle06 = bottleMoments[5];
            Tmoment.Bottle07 = bottleMoments[6];
            Tmoment.Bottle08 = bottleMoments[7];
            Tmoment.Bottle09 = bottleMoments[8];
            Tmoment.Bottle10 = bottleMoments[9];
            Tmoment.Bottle11 = bottleMoments[10];
            Tmoment.Bottle12 = bottleMoments[11];
            Tmoment.Bottle13 = bottleMoments[12];
            Tmoment.Bottle14 = bottleMoments[13];
            Tmoment.Bottle15 = bottleMoments[14];
        }

        private void PostSaveMethod()
        {
            PrepareSave();
            if (DbCommunication.Post(Tmoment))
            {
                Tmoment.Id = DbCommunication.MomentID;
            }
            else
            {
                // TODO meld fejl om kommunikaiton til server
            }
            
            _saveToDbMethod = PutSaveMethod;
        }

        private void PutSaveMethod()
        {
            PrepareSave();
            DbCommunication.Put(Tmoment, Tmoment.Id);
        }


        public TESTmoment Tmoment { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public AutoSaveTimer AutoSaveTimer { get; set; }

        public SaveToDbMethod SAveToDbMethod
        {
            get { return _saveToDbMethod; }
        }

        public static SaveButton Save
        {
            get
            {
                if (_save == null)
                {
                    _save = new SaveButton();
                }
                return _save;
            }
        }

        public string[] BottleStrings
        {
            get { return _bottleStrings; }
            set
            {
                _bottleStrings = value;
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
