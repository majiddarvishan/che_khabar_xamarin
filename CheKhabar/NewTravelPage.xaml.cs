using CheKhabar.Helpers;
using CheKhabar.Logic;
using CheKhabar.Model;
using Newtonsoft.Json;
using Plugin.Geolocator;
using SQLite;
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
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVenue = venueListView.SelectedItem as Venue;

                SaveVenue(selectedVenue);

                //var firstCategory = selectedVenue.categories.FirstOrDefault();

                //Post post = new Post()
                //{
                //    Experience = experienceEntry.Text,
                //    CategoryId = firstCategory.id,
                //    CategoryName = firstCategory.name,
                //    Address = selectedVenue.location.address,
                //    Distance = selectedVenue.location.distance,
                //    Latitude = selectedVenue.location.lat,
                //    Longitude = selectedVenue.location.lng,
                //    VenueName = selectedVenue.name
                //};

                //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                //{
                //    conn.CreateTable<Post>();
                //    int rows = conn.Insert(post);

                //    if (rows > 0)
                //    {
                //        DisplayAlert("Success", "Experience successfully inserted", "OK");
                //    }
                //    else
                //    {
                //        DisplayAlert("Failure", "Experience failed to be inserted", "OK");
                //    }
                //}
            }
            catch(NullReferenceException ex)
            {

            }
            catch(Exception ex)
            {

            }
        }

        private async void SaveVenue(Venue venue)
        {
            //var description = experienceEntry.Text;
            //if (string.IsNullOrEmpty(description))
            //{
            //    _ = DisplayAlert("Error", "You must enter description", "OK");
            //}
            //else
            {
                Advertisement adv = new Advertisement()
                {
                    mobile = App.LoginUserNumber,
                    description = venue.name,
                    start_time = DateTime.Now.ToString("yyyy-MM-dd"),
                    end_time = "",
                    latitude = venue.location.lat,
                    longitude = venue.location.lng,
                    tags = venue.categories.FirstOrDefault().name
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