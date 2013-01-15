using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Input;

namespace nextTest
{
    public partial class ViewCardsLandscape : PhoneApplicationPage
    {
        bool isFrontFacing = true;
        bool incOK = false;
        bool decOK = false;
        //bool empty = true;

        private TranslateTransform translation;
        private TransformGroup transformGroup;

        public ViewCardsLandscape()
        {
            InitializeComponent();

            this.transformGroup = new TransformGroup();
            this.translation = new TranslateTransform();

            this.transformGroup.Children.Add(this.translation);
            this.cardBox.RenderTransform = this.transformGroup;

            this.ManipulationCompleted +=
                this.ViewCardsLandscape_ManipulationCompleted;
            this.Tap +=
                this.card_Tap;
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
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            checkIndex();
            if (Controller.getIndex() >= 0)
            {
                this.cardBox.Text = Controller.getFront();
            }
        }
        void ViewCardsLandscape_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            if (e.TotalManipulation.Translation.X < -5)
            {
                //Goes to the right
                if (incOK)
                {
                    Controller.incIndex();
                    this.cardBox.Text = Controller.getFront();
                    checkIndex();
                }
                else
                {
                    Controller.setIndex(0);
                    this.cardBox.Text = Controller.getFront();
                    checkIndex();
                }
            }
            else if (e.TotalManipulation.Translation.X > 5 && decOK)
            {
                //Goes to the left.
                Controller.decIndex();
                this.cardBox.Text = Controller.getFront();
                checkIndex();
            }
        }
        public void checkIndex()
        {
            if (Controller.getIndex() + 1 < Controller.getLength())
                this.incOK = true;
            else
                this.incOK = false;

            if (Controller.getIndex() > 0)
                this.decOK = true;
            else
                this.decOK = false;

            //if (Controller.getIndex() < 0)
            //    this.empty = false;
            //else
            //    this.empty = true;
        }

    }
}