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
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(padSlider!=null)
                PadSizeTextBox.Text = Math.Round(padSlider.Value, 0).ToString();
           
        }

        private void PadSizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            //padSlider.Value
            int tmp;
            Int32.TryParse(PadSizeTextBox.Text, out tmp);
            padSlider.Value = tmp;
        }

    }
}