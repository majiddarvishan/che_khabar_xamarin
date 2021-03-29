using System;
using System.Collections.Generic;
using System.Text;

namespace CheKhabar.Helpers
{
    class Constants
    {
        public const string VENUE_SEARCH = "https://api.foursquare.com/v2/venues/search?ll={0},{1}&client_id={2}&client_secret={3}&v={4}";
        public const string CLIENT_ID = "NK2FI4P5YIEEXAPXSLATSSO03IU40VQVJ0EEWS15JPTPLSKH";
        public const string CLIENT_SECRET = "OQXXY1T2PRDNRBX3B3DP0CR2TPU2HJ5QTO3SX5CT2AOW1IU0";

        public const string ADVERTISEMENT_SEARCH = "http://192.168.1.172:5000/advertisements?&lat={0}&lng={1}&distance={2}";

        public const string ADD_USER = "http://192.168.1.172:5000/users";
        public const string USER_EXISTING = "http://192.168.1.172:5000/users/{0}";
    }
}
