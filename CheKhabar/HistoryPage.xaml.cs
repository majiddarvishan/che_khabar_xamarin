using CheKhabar.Logic;
using CheKhabar.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
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

    //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/data-binding/converters
    public class GregorianToSolarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //http://www.w3-farsi.com/convert-gregorian-date-to-solar-csharp/
            if (((String)value).Length != 0)
            {
                DateTime dateTime = ConvertStrToDate((String)value);

                PersianCalendar persianCalendar = new PersianCalendar();
                return string.Format(@"{0}/{1}/{2}",
                     persianCalendar.GetYear(dateTime),
                     persianCalendar.GetMonth(dateTime),
                     persianCalendar.GetDayOfMonth(dateTime));
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }

        private DateTime ConvertStrToDate(String inputDate)
        {
            string[] dateFormats = new[] { "yyyy-MM-dd"};
            CultureInfo provider = new CultureInfo("en-US");
            DateTime outDate = DateTime.ParseExact(inputDate, dateFormats, provider,DateTimeStyles.AdjustToUniversal);

            return outDate;
        }
    }
}