using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnibrewProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InputColSevenPage : Page
    {
        public InputColSevenPage()
        {
            ViewModel = new InputColSevenViewModel();
            this.InitializeComponent();
        }

        private void FormName_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            ApplicationView.GetForCurrentView().TryResizeView(new Size(1280, 720));
        }

        private void KeyDownEventHandler(object sender, KeyRoutedEventArgs e)
        {
            ViewModel.Save.AutoSaveTimer.StartTimer();
        }

        public InputColSevenViewModel ViewModel { get; set; }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
