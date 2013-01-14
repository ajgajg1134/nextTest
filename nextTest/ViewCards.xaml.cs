using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
namespace nextTest
{
    public partial class ViewCards : PhoneApplicationPage
    {
        bool isFrontFacing = true;
        Popup p = new Popup();
        TextBox fileNameInput = new TextBox();
        public ViewCards()
        {
            InitializeComponent();
            makePopup();
        }
        public void checkIndex()
        {
            if (Controller.getIndex() + 1 < Controller.getLength())
                this.nextBtn.IsEnabled = true;
            else
                this.nextBtn.IsEnabled = false;

            if (Controller.getIndex() > 0)
                this.prevBtn.IsEnabled = true;
            else
                this.prevBtn.IsEnabled = false;

            if (Controller.getIndex() < 0)
            {
                this.removeBtn.IsEnabled = false;
                this.flipBtn.IsEnabled = false;
                this.saveBtn.IsEnabled = false;
                this.shuffleBtn.IsEnabled = false;
            }
            else
            {
                this.removeBtn.IsEnabled = true;
                this.flipBtn.IsEnabled = true;
                this.saveBtn.IsEnabled = true;
                this.shuffleBtn.IsEnabled = true;
            }
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            checkIndex();
            if (Controller.getIndex() >= 0)
            {
                this.cardBox.Text = Controller.getFront();
            }
        }
        private void flipCardBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isFrontFacing)
            {
                this.cardBox.Text = Controller.getBack();
                isFrontFacing = false;
            }
            else
            {
                this.cardBox.Text = Controller.getFront();
                isFrontFacing = true;
            }
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            Controller.incIndex();
            if (isFrontFacing)
                this.cardBox.Text = Controller.getFront();
            else
                this.cardBox.Text = Controller.getBack();

            checkIndex();
            this.cardTitle.Text = "Card Number " + (Controller.getIndex() + 1);
        }

        private void prevBtn_Click(object sender, RoutedEventArgs e)
        {
            Controller.decIndex();
            if (isFrontFacing)
                this.cardBox.Text = Controller.getFront();
            else
                this.cardBox.Text = Controller.getBack();

            checkIndex();
            this.cardTitle.Text = "Card Number " + (Controller.getIndex() + 1);
        }

        private void removeBtn_Click(object sender, RoutedEventArgs e)
        {
            Controller.removeAt();

            if (this.nextBtn.IsEnabled)
            {
                Controller.decIndex(); //This is to offset the fact that nextBtn_Click() changes current Index
                nextBtn_Click(sender, e);
            }
            else if (this.prevBtn.IsEnabled)
            {
                prevBtn_Click(sender, e);
            }
            else
            {
                this.cardBox.Text = "";
                Controller.decIndex();
            }
            checkIndex();
        }

        private void addMoreBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
        private void makePopup()
        {
            Border border = new Border();
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            border.BorderThickness = new Thickness(5.0);
            border.Width = 300;

            StackPanel panel1 = new StackPanel();
            panel1.Background = new SolidColorBrush(Colors.LightGray);

            Button button1 = new Button();
            button1.Content = "Close";
            button1.Margin = new Thickness(5.0);
            button1.Click += new RoutedEventHandler(button1_Click);

            Button button2 = new Button();
            button2.Content = "Save";
            button2.Margin = new Thickness(5.0);
            button2.Click += new RoutedEventHandler(button2_Click);


            fileNameInput.AcceptsReturn = false;
            fileNameInput.Text = "Title";
            fileNameInput.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(fileName_Tap);

            panel1.Children.Add(fileNameInput);
            panel1.Children.Add(button1);
            panel1.Children.Add(button2);

            border.Child = panel1;

            // Set the Child property of Popup to the border 
            // which contains a stackpanel, textblock and button.
            p.Child = border;

            // Set where the popup will show up on the screen.
            p.VerticalOffset = 150;
            p.HorizontalOffset = 50;
            
        }
        private void card_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (Controller.getIndex() >= 0)
            {
                if (isFrontFacing)
                {
                    this.cardBox.Text = Controller.getBack();
                    isFrontFacing = false;
                }
                else
                {
                    this.cardBox.Text = Controller.getFront();
                    isFrontFacing = true;
                }
            }
        }
        private void fileName_Tap(object sender, EventArgs e)
        {
            fileNameInput.Text = "";
        }
        private void saveCardsBtn_Click(object sender, RoutedEventArgs e)
        {
            p.IsOpen = true;
        }
        void button1_Click(object sender, RoutedEventArgs e)
        {
            // Close the popup.
            p.IsOpen = false;
        }
        void button2_Click(object sender, RoutedEventArgs e)
        {
            Controller.saveCurrent(fileNameInput.Text);
            p.IsOpen = false;
        }
        private void loadCardsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LoadCards.xaml", UriKind.Relative));
        }

        private void studyModeBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ViewCardsLandscape.xaml", UriKind.Relative));
        }

        private void shuffleBtn_Click(object sender, RoutedEventArgs e)
        {
            Controller.shuffleCards();
            checkIndex();
            if (Controller.getIndex() >= 0)
            {
                this.cardBox.Text = Controller.getFront();
            }
            
        }
    }
}