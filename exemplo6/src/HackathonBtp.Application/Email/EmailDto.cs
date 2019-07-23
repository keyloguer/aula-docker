using HackatonBtp.Domain.Models;

namespace HackatonBtp.Application.Email
{
    public class EmailDTO
    {
        public Time Time { get; set;}
        public string UrlEnvioEmail { get; set; }
        public string EmailTemplate { get; set; }
    }
}