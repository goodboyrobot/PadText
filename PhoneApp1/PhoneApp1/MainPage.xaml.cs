using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp1.Resources;

using PadText.Library;
namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        Nfc nfc = new Nfc();
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            nfc.Listen();
            nfc.OnMessage += (string msg) =>
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        helloBox.Text = msg.Length.ToString();
                    });
                };
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ISmsSender test = new SmsSender();
            test.Message = helloBox.Text;
            test.To.PhoneNumber = toBox.Text;
            test.Send();
        }

        private void nfcBut_Click(object sender, RoutedEventArgs e)
        {
            nfc.Send(helloBox.Text);
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}