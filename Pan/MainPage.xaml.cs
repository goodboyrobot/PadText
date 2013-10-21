using PadText.Library;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace PanApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        PhoneNumberChooserTask phoneNumberChooserTask;
        PhoneNumberResult decodeNumber = null;
        public bool hasNumber = false;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewMessagePage.xaml", UriKind.Relative));
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            phoneNumberChooserTask = new PhoneNumberChooserTask();
            phoneNumberChooserTask.Completed += new EventHandler<PhoneNumberResult>(phoneNumberChooserTask_Completed);
            phoneNumberChooserTask.Show();

        }
        private void phoneNumberChooserTask_Completed(object sender, PhoneNumberResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                PadText.Library.Central.GetInstance().ContactPads.Where(p => p.user == e.PhoneNumber.Replace("(555) 555-5555", "5555555555")).FirstOrDefault().SendMessage(EncodingTextBox.Text) ;
                
            }
            else
            {
                MessageBox.Show("Error sending message");
            }
        }

        private void DecodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (decodeNumber == null) {
                MessageBox.Show("Error: Must have a contact to decode");
                return;
            }

            MessageBox.Show(PadText.Library.Central.GetInstance().ContactPads.Where(p=> p.user == decodeNumber.PhoneNumber).FirstOrDefault().Decode(DecodingTextBox.Text));

                

        }

        private void ContactSelectButton_Click(object sender, RoutedEventArgs e)
        {
            phoneNumberChooserTask = new PhoneNumberChooserTask();
            phoneNumberChooserTask.Completed += new EventHandler<PhoneNumberResult>(contactSelectTask_Completed);
            phoneNumberChooserTask.Show();
        }

        private void contactSelectTask_Completed(object sender, PhoneNumberResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                MessageBox.Show("The phone number for " + e.DisplayName + " is " + e.PhoneNumber);
                decodeNumber = e;
            }
        }
    }
}