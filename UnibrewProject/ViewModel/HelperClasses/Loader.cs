﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Media3D;
using LiveCharts;
using LiveCharts.Uwp;
using UnibrewProject.Model;

namespace UnibrewProject.ViewModel.HelperClasses
{
    /// <summary>
    /// Klasse, der sørger for at hente data til programmet
    /// </summary>
    public class Loader
    {
        private static Loader _load = null;
        private Loader()
        {
            // Liste af færdigvarer og væsketanke hentes ved programstart
            FinishedItemsList = ComGeneric.GetAll<FinishedItems>();
            LiquidTanksList = ComGeneric.GetAll<LiquidTanks>();
        }

        /// <summary>
        /// Henter liste af TapOperators
        /// </summary>
        /// <returns>List af TapOperators</returns>
        public List<TapOperator> GetTapOperators()
        {
            return ComGeneric.GetAll<TapOperator>();
        }

        /// <summary>
        /// Henter liste af ProcessingItems
        /// </summary>
        /// <returns>List af ProcessingItems</returns>
        public List<ProcessingItems> GetProcessingItems()
        {
            return ComGeneric.GetAll<ProcessingItems>();
        }

        /// <summary>
        /// Liste af færdigvarer
        /// </summary>
        public List<FinishedItems> FinishedItemsList { get; set; }

        /// <summary>
        /// Liste af LiquidTanks
        /// </summary>
        public List<LiquidTanks> LiquidTanksList { get; set; }

        /// <summary>
        /// Indgang til persistensklassen
        /// </summary>
        public DbComGeneric ComGeneric { get; set; } = DbComGeneric.ComGeneric;

        /// <summary>
        /// Indgang til Loader - singleton
        /// </summary>
        public static Loader Load
        {
            get
            {
                if (_load == null)
                {
                    _load = new Loader();
                }
                return _load;
            }
        }
    }
}
