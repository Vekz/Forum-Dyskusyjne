using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum_Dyskusyjne.Models
{
    public enum UserRole
    {
        Admin,
        Mod,
        User
    }

    public class User
    {
        public int User_ID { get; set; }
        public string User_Name { get; set; }

        public byte[] Avatar { get; set; }

        public string PassWordHash {get; set;}

        public UserRole Role { get; set; }

        public float Timeout { get; set; }

        public string eMail { get; set; }
                
    }
}