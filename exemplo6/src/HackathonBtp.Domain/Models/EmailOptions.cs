namespace HackatonBtp.Domain.Models
{
    public class EmailOptions
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string Subject { get; set; }
    }
}