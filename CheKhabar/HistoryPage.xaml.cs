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
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Advertisement>();
                var advertisements = conn.Table<Advertisement>().ToList();
                if (advertisements.Count > 0)
                {
                    postListView.ItemsSource = advertisements;
                }
                else
                {
                    advertisements = await AdvertisementLogic.GetAdvertisements(App.LoginUserNumber);
                    postListView.ItemsSource = advertisements;
                }
            }
        }

        private void postListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedAdv = postListView.SelectedItem as Advertisement;

            if(selectedAdv != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedAdv));
            }
        }
    }
}