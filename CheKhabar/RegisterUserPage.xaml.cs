using CheKhabar.Helpers;
using CheKhabar.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CheKhabar
{
    public partial class RegisterUserPage : ContentPage
    {
        public string MobileNumber = "";
        public RegisterUserPage(string mobileNumber)
        {
            InitializeComponent();

            MobileNumber = mobileNumber;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            mobileEntry.Text = MobileNumber;
        }

        private async void registerButton_Clicked(object sender, EventArgs e)
        {
            Users user = new Users()
            {
                email = emailEntry.Text,
                first_name = firstNameEntry.Text,
                last_name = lastNameEntry.Text,
                nickname = "",
                mobile = mobileEntry.Text,
                created_at = DateTime.Now,
                active = true,
                score = 0,
                visible = true,
                distance = 1000,
                user_mode = UserMode.Common,
                bio = ""
            };

            var url = Constants.ADD_USER;

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(user);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); 

                var response = await client.PostAsync(url, stringContent);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _ = Navigation.PushAsync(new HomePage());
                }
                else
                {
                    _ = DisplayAlert("Error", "Problem in connecting with server, please try later", "OK");
                }
            }
        }
    }
}