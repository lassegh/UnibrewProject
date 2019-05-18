using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UnibrewProject.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UnibrewProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            ViewModel = new StatViewModel();
            this.InitializeComponent();
        }

        private void FormName_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            //ApplicationView.GetForCurrentView().TryResizeView(new Size(1280, 720));
        }

        private void OnTapped(object sender, TappedRoutedEventArgs e)
        {
            string name = ((CheckBox)sender).Name;
            ViewModel.StatConfig.ToggleGraphs(name);
        }

        public StatViewModel ViewModel { get; set; }
    }
}
