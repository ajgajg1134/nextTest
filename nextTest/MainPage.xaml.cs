using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;



namespace nextTest
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }
        private void mkCardBtn_Click(object sender, RoutedEventArgs e)
        {
            Controller.add(this.frontBox.Text, this.backBox.Text);
            this.frontBox.Text = "";
            this.backBox.Text = "";
            Controller.setIndex(0);
            //currentIndex++;
            //isFrontFacing = true;
            //this.cardTitle.Text = "Card Number " + (currentIndex + 1);
            //this.cardBox.Text = fronts.ElementAt(currentIndex);
        }

        private void viewCardsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ViewCards.xaml", UriKind.Relative));
        }

        private void loadCardsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LoadCards.xaml", UriKind.Relative));
        }

        private void frontBox_Tap(object sender, RoutedEventArgs e)
        {
            if (this.frontBox.Text.Equals("Front"))
            {
                this.frontBox.Text = "";
            }
        }
        private void backBox_Tap(object sender, RoutedEventArgs e)
        {
            if (this.backBox.Text.Equals("Back"))
            {
                this.backBox.Text = "";
            }
        }

        private void Button_Tap_1(object sender, GestureEventArgs e)
        {

        }   
    }
}