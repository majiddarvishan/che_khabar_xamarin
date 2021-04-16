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
            distanceEntry.Text = user.distance.ToString();
        }

        private void userTypeSwitch_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            userTypeLabel.Text = sender.ToString();
        }
    }
}