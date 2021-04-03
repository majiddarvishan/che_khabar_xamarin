using CheKhabar.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheKhabar.Model
{
    public class Advertisement
    {
        public int Id { get; set; }

        public string mobile { get; set; }

        public string description { get; set; }

        public int distance { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }

        public string start_time { get; set; }

        public string end_time { get; set; }

        public string tags { get; set; }
    }

    public class AdvertisementRoot
    {
        public IList<Advertisement> advertisements { get; set; }

        public static string GenerateURL(double latitude, double longitude)
        {
            return string.Format(Constants.ADVERTISEMENT_SEARCH,
                                        latitude,
                                        longitude,
                                        1000 /*distance*/);
        }

        public static string GenerateUserURL(string mobile)
        {
            return string.Format(Constants.ADVERTISEMENT_SEARCH_BY_USER, mobile);
        }
    }
}
