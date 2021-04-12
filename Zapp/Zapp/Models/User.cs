using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace Zapp.Models
{
    public class User
    {
        [PrimaryKey]
        public string _id { get; set; }
        public string user { get; set; }
    }

    public class AuthUser
    {
        public string user { get; set; }
        public string password { get; set; }
    }
}
