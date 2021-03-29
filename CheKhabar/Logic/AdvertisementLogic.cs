using CheKhabar.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CheKhabar.Logic
{
    public class AdvertisementLogic
    {
        public async static Task<List<Advertisement>> GetAdvertisements(double latitude, double longitude)
        {
            List<Advertisement> advertisements = new List<Advertisement>();

            var url = AdvertisementRoot.GenerateURL(latitude, longitude);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var advList = JsonConvert.DeserializeObject<Advertisement[]>(json);

                advertisements = advList.ToList<Advertisement>();
                //advertisements.AddRange(advList);
            }

            return advertisements;
        }
    }
}
