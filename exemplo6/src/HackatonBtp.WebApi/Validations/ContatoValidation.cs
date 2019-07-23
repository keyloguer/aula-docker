using FluentValidation;
using HackathonBtp.Domain.Models.DTOs;

namespace HackatonBtp.WebApi.Validations
{
    public class ContatoValidation : AbstractValidator<ContatoDTO>
    {
        public ContatoValidation()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Nome).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Mensagem).NotEmpty().MaximumLength(500);
        }
    }
}
