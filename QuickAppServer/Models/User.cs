namespace QuickAppServer.Models
{
    public class User
    {
        public long UserID { get; set; }

        public string CompanyName { get; set; }

        public string Password { get; set; }

        public bool Paid { get; set; }
    }

    public class MyPage
    {
        public int UserID { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }
    }
}
