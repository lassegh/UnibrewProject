using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using GalaSoft.MvvmLight.Command;
using UnibrewProject.Annotations;

namespace UnibrewProject.ViewModel.HelperClasses
{
    public class MenuSlider : INotifyPropertyChanged
    {
        private delegate void CallBack(Object state);

        private CallBack callBack;

        private int _menuWidth = 50;
        private bool _menuTextVisibility = false;

        public MenuSlider()
        {
            MenuHideButton = new RelayCommand(MenuHideMethod);
            callBack = MenuShowCallback;

        }

        private void MenuHideMethod()
        {
            MenuMoveTimer = new Timer(new TimerCallback(callBack), null, 1, Timeout.Infinite);

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
