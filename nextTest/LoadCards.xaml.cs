using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace nextTest
{
    public partial class LoadCards : PhoneApplicationPage
    {
        public LoadCards()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(LoadCards_Loaded);
        }

        private void LoadCards_Loaded(object sender, RoutedEventArgs e)
        {
            //Controller.updateFileList();
            List<String> files = Controller.getFileList();
            fileListing.ItemsSource = files;
        }
        private void listItem_Click(object sender, RoutedEventArgs e)
        {
            //Access sender.text 
            Button b = (Button)e.OriginalSource;
            if ((bool)delCheckBox.IsChecked)
            {
                Controller.removeFile(((TextBlock)(b.Content)).Text);
                List<String> files = Controller.getFileList();
                fileListing.ItemsSource = files;
            }
            else
            {
                Controller.loadOld(((TextBlock)(b.Content)).Text);
                Controller.setIndex(0);
                NavigationService.Navigate(new Uri("/ViewCards.xaml", UriKind.Relative));
            }
        }
        private void clearFiles_Click(object sender, RoutedEventArgs e)
        {
            Controller.removeAllFiles();
            List<String> files = Controller.getFileList();
            fileListing.ItemsSource = files;
        }

        private void delCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (delCheckBox.Content.Equals("Delete?"))
            {
                Color c = new Color();
                c.A = 150;
                c.R = 255;
                c.G = 0;
                c.B = 0;
                fileListing.Background = new SolidColorBrush(c);
                delCheckBox.Content = "Tap files to delete.";
            }
            else
            {
                Color c = new Color();
                c.A = 0;
                c.R = 255;
                c.G = 0;
                c.B = 0;
                fileListing.Background = new SolidColorBrush(c);
                delCheckBox.Content = "Delete?";
            }
        }
    }
}