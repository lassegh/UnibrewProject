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
            TapOp = new TapOperator();
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
            TapOp = new TapOperator();

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
            
            TapOp.Bottle1 = bottleMoments[0];
            TapOp.Bottle2 = bottleMoments[1];
            TapOp.Bottle3 = bottleMoments[2];
            TapOp.Bottle4 = bottleMoments[3];
            TapOp.Bottle5 = bottleMoments[4];
            TapOp.Bottle6 = bottleMoments[5];
            TapOp.Bottle7 = bottleMoments[6];
            TapOp.Bottle8 = bottleMoments[7];
            TapOp.Bottle9 = bottleMoments[8];
            TapOp.Bottle10 = bottleMoments[9];
            TapOp.Bottle11 = bottleMoments[10];
            TapOp.Bottle12 = bottleMoments[11];
            TapOp.Bottle13 = bottleMoments[12];
            TapOp.Bottle14 = bottleMoments[13];
            TapOp.Bottle15 = bottleMoments[14];

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
            TapOp.ClockDate = DateTime.Now;
            if (DbCommunication.Post(TapOp))
            {
                TapOp.ID = DbCommunication.MomentID;
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
            DbCommunication.Put(TapOp, TapOp.ID);
        }


        public TapOperator TapOp { get; set; }
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
