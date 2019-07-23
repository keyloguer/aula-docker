namespace HackatonBtp.Application.Email
{
    public interface IEmailSender
    {
        bool SendEmail(EmailDTO email);
    }
}