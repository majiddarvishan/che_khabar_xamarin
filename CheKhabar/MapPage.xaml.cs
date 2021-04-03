using CheKhabar.Logic;
using CheKhabar.Model;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheKhabar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private bool hasLocationPermission = false;

        Position currentPosition;
        public MapPage()
        {
            InitializeComponent();

            GetPermissions();
        }

        private async void GetPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.LocationWhenInUse);

                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.LocationWhenInUse))
                    {
                        await DisplayAlert("Need your location", "We need to access your location", "OK");
                    }

                    var result = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.LocationWhenInUse);
                    if (result.ContainsKey(Plugin.Permissions.Abstractions.Permission.LocationWhenInUse))
                    {
                        status = result[Plugin.Permissions.Abstractions.Permission.LocationWhenInUse];
                    }
                }

                if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    hasLocationPermission = true;
                    locationMap.IsShowingUser = true;

                    GetLocation();
                }
                else
                {
                    await DisplayAlert("Location denied", "You didn't give us permission to access location. so we can't show your location", "OK");
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;

                locator.PositionChanged += Locator_PositionChanged;
                await locator.StartListeningAsync(TimeSpan.Zero, 100);
            }

            GetLocation();
        }

        private void DisplayInMap(List<Advertisement> advertisements)
        {
            foreach (var adv in advertisements)
            {
                try
                {
                    var position = new Xamarin.Forms.Maps.Position(adv.latitude, adv.longitude);

                    var pin = new Xamarin.Forms.Maps.Pin()
                    {
                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                        Position = position,
                        Label = adv.description,
                        Address = adv.distance.ToString() + " m"

                    };

                    locationMap.Pins.Add(pin);
                }
                catch (NullReferenceException ex)
                {

                }
                catch (Exception ex)
                {

                }
            }
            
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            await CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= Locator_PositionChanged;
        }

        private async void Locator_PositionChanged(object sender, PositionEventArgs e)
        {
            MoveMap(e.Position);
            var advertisements = await AdvertisementLogic.GetAdvertisements(currentPosition.Latitude, currentPosition.Longitude);
            DisplayInMap(advertisements);
        }

        private async void GetLocation()
        {
            try
            {
                //var hasPermission = await Utils.CheckPermissions(Permission.Location);
                if (!hasLocationPermission)
                    return;

                var locator = CrossGeolocator.Current;
                //locator.DesiredAccuracy = DesiredAccuracy.Value;

                //var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(Timeout.Value), null, IncludeHeading.IsToggled);
                currentPosition = await locator.GetPositionAsync();

                if (currentPosition == null)
                {
                    await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured for analysis! Thanks.", "OK");
                    return;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured for analysis! Thanks.", "OK");
                return;
            }
            //finally
            //{
            //    ButtonGetGPS.IsEnabled = true;
            //}

            //if (hasLocationPermission)
            //{
            //    var locator = CrossGeolocator.Current;
            //    var position = await locator.GetPositionAsync();

            //    currentPosition = position;

                MoveMap(currentPosition ); 
            //}
        }

        private void MoveMap(Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
            locationMap.MoveToRegion(span);
        }

        private void OnMapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            Navigation.PushAsync(new AddAdvertisementPage(e.Position));
        }
    }
}