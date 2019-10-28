using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace INFT2051_QuickApp.Services
{
    public static class ApplicationSettings
    {
        public static long UserID { get; set; }

        public static bool IsSignIn { get; set; }

        public static DateTime ExpirationDate { get; set; }

        public static string AccessToken { get; set; }

        public static string CompanyName { get; set; }

        public static string Password { get; set; }

        public static bool Paid { get; set; }
    }
}
