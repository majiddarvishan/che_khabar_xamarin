using CheKhabar.Helpers;
using CheKhabar.Model;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace CheKhabar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAdvertisementPage : ContentPage
    {
        Position newPosition;
        public AddAdvertisementPage(Position position)
        {
            InitializeComponent();

            newPosition = position;
        }

        private async  void addAdvertisementButton_Clicked(object sender, EventArgs e)
        {
            var description = descriptionEntry.Text;
            if (string.IsNullOrEmpty(description))
            {
                _ = DisplayAlert("Error", "You must enter description", "OK");
            }
            else
            {
                var startTime = startTimeEntry.Text;
                if (string.IsNullOrEmpty(startTime))
                    startTime = DateTime.Now.ToString("yyyy-MM-dd");

                var stopTime = stopTimeEntry.Text;
                if (string.IsNullOrEmpty(stopTime))
                    stopTime = "";

                Advertisement adv = new Advertisement()
                {
                    mobile = App.LoginUserNumber,
                    description = descriptionEntry.Text,
                    start_time = startTime,
                    end_time = stopTime,
                    latitude = newPosition.Latitude,
                    longitude = newPosition.Longitude,
                    tags = ""
                };

                var url = Constants.ADD_ADVERTISEMENT;

                using (HttpClient client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(adv);
                    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, stringContent);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SaveToDatabase(adv);
                        _ = Navigation.PushAsync(new HomePage());
                    }
                    else
                    {
                        _ = DisplayAlert("Error", "Problem in connecting with server, please try later", "OK");
                    }
                }
            }
        }

        private void cancelAdvertisementButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void SaveToDatabase(Advertisement adv)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Advertisement>();
                int rows = conn.Insert(adv);

                //if (rows > 0)
                //{
                //    DisplayAlert("Success", "Advertisement successfully inserted", "OK");
                //}
                //else
                //{
                //    DisplayAlert("Failure", "Advertisement failed to be inserted", "OK");
                //}
            }
        }
    }
}