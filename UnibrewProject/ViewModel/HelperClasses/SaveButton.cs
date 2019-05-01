using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using UnibrewProject.Model;

namespace UnibrewProject.ViewModel.HelperClasses
{
    /// <summary>
    /// Gem klasse.
    /// Til gem knap og automatisk gem
    /// </summary>
    public class SaveButton
    {
        private float _saveBlockOpacity = 0;

        public SaveButton()
        {
            SaveCommand = new RelayCommand(SaveMethod);
        }

        private void SaveMethod()
        {
            double[] bottleMoments = new double[15];
            for (int i = 0; i < BottleStrings.Length; i++)
            {
                BottleStrings[i] = BottleStrings[i].Replace(',', '.');
                if (double.TryParse(BottleStrings[i], out bottleMoments[i])) Debug.Write("String can be parsed");
                else bottleMoments[i] = 0; // TODO tilføj en warning til brugeren om fejl indtastning og om der skal fortsættes med huller i DB?
            }
            TESTmoment moments = new TESTmoment(bottleMoments[0], bottleMoments[1], bottleMoments[2], bottleMoments[3], bottleMoments[4], bottleMoments[5], bottleMoments[6], bottleMoments[7], bottleMoments[8], bottleMoments[9], bottleMoments[10], bottleMoments[11], bottleMoments[12], bottleMoments[13], bottleMoments[14]);
            DbCommunication.Post(moments);
        }

        public RelayCommand SaveCommand { get; set; }
        public string[] BottleStrings { get; set; } = new string[15];
    }
}
