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
using UnibrewProject.ViewModel.HelperClasses.SaveClasses;

namespace UnibrewProject.ViewModel.HelperClasses
{
    /// <summary>
    /// Gem klasse.
    /// Til gem knap og automatisk gem
    /// </summary>
    public class SaveButton
    {
        private static SaveButton _save = null;
        public delegate void SaveToDbMethod();
        private SaveToDbMethod _saveToDbMethod;
        

        private SaveButton()
        {
            for (int i = 0; i < TapOperatorMoments.Length; i++)
            {
                TapOperatorMoments[i] = new TapOperatorMoment();
            }
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

            // Slet indtastninger i view
            foreach (TapOperatorMoment moment in TapOperatorMoments)
            {
                moment.Moment = "";
            }

            // Stopper timer
            AutoSaveTimer.TimeSinceLastKeyDownTimer.Dispose();
        }

        private void PrepareSave()
        {
            double[] bottleMoments = new double[15];

            for (int i = 0; i < TapOperatorMoments.Length; i++)
            {
                if (TapOperatorMoments[i].Moment == null) bottleMoments[i] = 0;
                else
                {
                    TapOperatorMoments[i].Moment = TapOperatorMoments[i].Moment.Replace(',', '.');
                    if (!double.TryParse(TapOperatorMoments[i].Moment, out bottleMoments[i])) bottleMoments[i] = 0;
                }
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

            double[] bottleWeight = new double[6];

            for (int i = 0; i < FluidWeightControls.Length; i++)
            {
                if (FluidWeightControls[i].Vejning == null) 
                {
                    
                }
        
            }
        }

        private void PostSaveMethod()
        {
            PrepareSave();
            Tmoment.DateTime = DateTime.Now;
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
        public TapOperatorMoment[] TapOperatorMoments { get; set; } = new TapOperatorMoment[15];
        public FluidWeightControl[] FluidWeightControls { get; set; } = new FluidWeightControl[6];

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
    }
}
