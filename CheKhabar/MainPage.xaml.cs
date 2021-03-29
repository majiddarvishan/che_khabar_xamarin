using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CheKhabar
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            iconImage.Source = ImageSource.FromResource("CheKhabar.Assets.Images.chekhabar.webp", assembly);
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            var mobileNumber = mobileEntry.Text;

            if(!IsNumberValid(mobileNumber))
            {
                DisplayAlert("Error", "Please input a valid mobile number", "OK");
            }
            else
            {
                Navigation.PushAsync(new OTPConfirmPage(mobileNumber));
            }
        }

        public static bool IsNumberValid(string strNumber)
        {
            if (string.IsNullOrEmpty(strNumber))
            {
                return false;
            }
            else if (!strNumber.StartsWith("09") || strNumber.Length != 11)
            {
                return false;
            }
            else if(!strNumber.All(char.IsDigit))
            {
                return false;
            }

            return true;
        }
    }
}
