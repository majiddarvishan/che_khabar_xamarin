using CheKhabar.Logic;
using CheKhabar.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheKhabar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Users>();
                var user = conn.Table<Users>().Where(usr => usr.mobile == App.LoginUserNumber);

                if (user.ToList().Count > 0)
                {
                    DisplayUser(user.First());
                }
                else
                {
                    var userProfile = await UserLogic.GetUserProfile(App.LoginUserNumber);
                    DisplayUser(userProfile);
                }
            }
        }

        private void DisplayUser(Users user)
        {
            firstNameEntry.Text = user.first_name;
            lastNameEntry.Text = user.last_name;
            emailEntry.Text = user.email;
            mobileEntry.Text = user.mobile;
            activeSwitch.IsToggled = user.active;
            visibleSwitch.IsToggled = user.visible;

            scoreLabel.Text = $"Score: {user.score.ToString()}";

            userTypeSwitch.IsToggled = user.user_mode == UserMode.Advertiser ? true : false;
            string stateName = user.user_mode == UserMode.Advertiser ? "Advertiser" : "Common";
            userTypeLabel.Text = $"The user type is {stateName}";

            radiusSlider.Value = user.distance;
            radiusLabel.Text = String.Format("Radius is {0}", user.distance);
        }

        private void userTypeSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            string stateName = e.Value ? "Advertiser" : "Common";
            userTypeLabel.Text = $"The user type is {stateName}";
        }

        private void visibleSwitch_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void activeSwitch_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void radiusSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var newStep = Math.Round(e.NewValue);
            radiusSlider.Value = newStep;

            radiusLabel.Text = String.Format("Radius is {0}", e.NewValue);
        }

        private void updateButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}