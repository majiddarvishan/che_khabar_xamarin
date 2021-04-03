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
    public partial class PostDetailPage : ContentPage
    {
        Advertisement selectedAdvertisement;
        public PostDetailPage(Advertisement selectedAdv)
        {
            InitializeComponent();

            this.selectedAdvertisement = selectedAdv;
            descriptionEntry.Text = selectedAdvertisement.description;
            startTimeEntry.Text = selectedAdvertisement.start_time;
            stopTimeEntry.Text = selectedAdvertisement.end_time;
        }

        private void updateButton_Clicked(object sender, EventArgs e)
        {
            selectedAdvertisement.description = descriptionEntry.Text;
            selectedAdvertisement.start_time = startTimeEntry.Text;
            selectedAdvertisement.end_time = stopTimeEntry.Text;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Advertisement>();
                int rows = conn.Update(selectedAdvertisement);

                if (rows > 0)
                {
                    DisplayAlert("Success", "Experience successfully updated", "OK");
                }
                else
                {
                    DisplayAlert("Failure", "Experience failed to be updated", "OK");
                }

                //TODO:Majid Darvishan: must be update in web server too
            }
        }

        private void deleteButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Advertisement>();
                int rows = conn.Delete(selectedAdvertisement);

                if (rows > 0)
                {
                    DisplayAlert("Success", "Advertisement successfully deleted", "OK");
                }
                else
                {
                    DisplayAlert("Failure", "Advertisement failed to be deleted", "OK");
                }
            }
        }
    }
}