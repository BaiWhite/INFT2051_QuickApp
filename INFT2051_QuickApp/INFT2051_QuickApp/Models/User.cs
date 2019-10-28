using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051_QuickApp.Models
{
    public class User
    {
        public long UserID { get; set; }

        public string CompanyName { get; set; }

        public string Password { get; set; }

        public bool paid { get; set; }
    }

    public class RegisterBindingModel {
        public string CompanyName { get; set; }

        public string Password { get; set; }
    }

    public class MyPage
    {
        public string UserID { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }
    }
}
