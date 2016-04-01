using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifLk.Models
{
    public class UserModel
    {
        public long ID { get; set; }
        public long SteamID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

    }
}