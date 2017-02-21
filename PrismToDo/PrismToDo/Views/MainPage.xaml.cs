using System;
using Xamarin.Forms;

namespace PrismToDo.Views
{
    public partial class MainPage : ContentPage
    {
        bool originalStyle = true;

        public MainPage()
        {
            InitializeComponent();    
            Resources["labeColorlStyle"] = Resources["blueColor"];            
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            if (originalStyle)
            {
                Resources["labeColorlStyle"] = Resources["redColor"];
                originalStyle = false;
            }
            else
            {
                Resources["labeColorlStyle"] = Resources["blueColor"];
                originalStyle = true;
            }
        }
    }
}
