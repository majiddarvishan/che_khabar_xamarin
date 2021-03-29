using System;
using System.Collections.Generic;
using System.Text;

namespace CheKhabar.Model

{
    public enum UserMode
    {
        Common = 1,
        Advertiser = 2
    }

    public class Users
    {
        public long id { get; set; }

        public string email { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string nickname { get; set; }

        public string mobile { get; set; }

        public DateTime created_at { get; set; }

        public bool active { get; set; }

        public int score { get; set; }

        public bool visible { get; set; }

        public int distance { get; set; }

        public UserMode user_mode { get; set; }

        public string bio { get; set; }
    }
}
