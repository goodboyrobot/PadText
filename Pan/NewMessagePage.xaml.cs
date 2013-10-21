using Microsoft.Phone.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PanApp
{
    public partial class NewMessagePage : PhoneApplicationPage
    {
        public NewMessagePage()
        {
            InitializeComponent();
        }

        void Contacts_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            //Do something with the results.
            
            ContactResultsData.DataContext = e.Results;
            
        }

        private void ContactTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            ContactResultsData.DataContext = null;
            

            Contacts cons = new Contacts();


            //Identify the method that runs after the asynchronous search completes.
            cons.SearchCompleted += new EventHandler<ContactsSearchEventArgs>(Contacts_SearchCompleted);

            //Start the asynchronous search.
            cons.SearchAsync(ContactTextBlock.Text, FilterKind.DisplayName, "Contacts Search");

        }

    }
}