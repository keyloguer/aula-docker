namespace HackathonBtp.Application.Email
{
    public interface IContantoEmailSender
    {
        bool EnviarEmail(string nome, string email, string mensagem);
    }
}
