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
    public class UserLogic
    {
        public async static Task<Users> GetUserProfile(string mobile)
        {
            Users user = null;

            var url = UserRoot.GenerateUserProfileURL(mobile);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                user = JsonConvert.DeserializeObject<Users>(json);
            }

            return user;
        }
    }
}
