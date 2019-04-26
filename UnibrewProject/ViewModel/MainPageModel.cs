using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight.Command;
using UnibrewProject.Annotations;
using LiveCharts;
using LiveCharts.Uwp;

namespace UnibrewProject.ViewModel
{
    class MainPageModel : INotifyPropertyChanged
    {

        private delegate void CallBack(Object state);

        private CallBack callBack;

        private int _menuWidth = 50;
        private bool _menuTextVisibility = false;

        public MainPageModel()
        {
            
            MenuHideButton = new RelayCommand(MenuHideMethod);
            callBack = MenuShowCallback;

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
                },
                /*new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Series 3",
                    Values = new ChartValues<double> { 4,2,7,2,7 },
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15
                }*/
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");

            //modifying the series collection will animate and update the chart
            /*SeriesCollection.Add(new LineSeries
            {
                Title = "Series 4",
                Values = new ChartValues<double> { 5, 3, 2, 4 },
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                PointGeometry = new PointGeometry("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
                PointGeometrySize = 50,
                PointForeround = new SolidColorBrush(Windows.UI.Colors.Gray)
            });

            //modifying any series values will also animate and update the chart
            SeriesCollection[3].Values.Add(5d);*/
        }

        private void MenuHideMethod()
        {
            MenuMoveTimer = new Timer(new TimerCallback(callBack),null,1,Timeout.Infinite);
           
        }

        private void MenuShowCallback(Object state)
        {
            if (MenuWidth < 200)
            {
                
                MenuWidth = MenuWidth + 5;
                MenuMoveTimer.Change(1, Timeout.Infinite);
            }
            else
            {
                MenuMoveTimer.Dispose();
                callBack = MenuHideCallback;
                MenuTextVisibility = true;
            }
        }

        private void MenuHideCallback(Object state)
        {
            if (MenuWidth > 50)
            {
                MenuTextVisibility = false;
                MenuWidth = MenuWidth - 5;
                MenuMoveTimer.Change(1, Timeout.Infinite);
            }
            else
            {
                MenuMoveTimer.Dispose();
                callBack = MenuShowCallback;
                
            }
            
        }

        public Timer MenuMoveTimer { get; set; }
        public RelayCommand MenuHideButton { get; set; }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }



        public int MenuWidth
        {
            get { return _menuWidth; }
            set
            {
                _menuWidth = value;
                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () =>
                    {
                        // Your UI update code goes here!
                        OnPropertyChanged();
                    }
                );
            }
        }

        public bool MenuTextVisibility
        {
            get { return _menuTextVisibility; }
            set
            {
                _menuTextVisibility = value;
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
