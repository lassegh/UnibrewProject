using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace UnibrewProject.ViewModel.HelperClasses
{
    /// <summary>
    /// Opretter og viser en pop op besked
    /// </summary>
    public class ShowMsg
    {
        /// <summary>
        /// Opretter og viser en pop op besked
        /// </summary>
        /// <param name="message">Beskeden, der skal vises</param>
        public void ShowMessage(string message)
        {
            var msg = new Windows.UI.Popups.MessageDialog(message) {DefaultCommandIndex = 1};
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    // Your UI update code goes here!
                    msg.ShowAsync();
                }
            );
            
        }
    }
}
