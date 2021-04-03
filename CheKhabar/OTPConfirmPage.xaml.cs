using CheKhabar.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheKhabar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OTPConfirmPage : ContentPage
    {
        public string MobileNumber = "";
        public OTPConfirmPage(string mobileNumber)
        {
            InitializeComponent();

            MobileNumber = mobileNumber;
        }

        private async void enterButton_Clicked(object sender, EventArgs e)
        {
            var url = string.Format(Constants.USER_EXISTING, MobileNumber);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    App.LoginUserNumber = MobileNumber;
                    _ = Navigation.PushAsync(new HomePage());
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _ = Navigation.PushAsync(new RegisterUserPage(MobileNumber));
                }
                else
                {
                    _ = DisplayAlert("Error", "Problem in connecting with server, please try later", "OK");
                }
            }
        }
    }
}