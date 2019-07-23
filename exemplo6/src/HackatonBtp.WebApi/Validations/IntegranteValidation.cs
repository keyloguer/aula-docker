using HackatonBtp.Domain.Models.DTOs;
using FluentValidation;

namespace HackatonBtp.WebApi.Validations
{
    public class IntegranteValidation : AbstractValidator<IntegranteDTO>
    {
        public IntegranteValidation()
        {
            RuleFor(x => x.Nome).NotEmpty().Must(x => x.Length >= 3 && x.Length <= 150);
            RuleFor(x => x.DataNascimento).NotEmpty().Must(x => System.DateTime.Now > x);
            RuleFor(x => x.Telefone).NotEmpty().Must(x => x.Length <= 20);
            RuleFor(x => x.RG).NotEmpty();
            RuleFor(x => x.Universidade).NotEmpty().When(x => !string.IsNullOrEmpty(x.Curso));
            RuleFor(x => x.Curso).NotEmpty().When(x => !string.IsNullOrEmpty(x.Universidade));
            RuleFor(x => x.DescricaoDeficiencia).NotEmpty().When(x => x.PossuiDeficiencia);
            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Linkedin).MaximumLength(150);
            RuleFor(x => x.Git).MaximumLength(150);
            RuleFor(x => x.Experiencia).MaximumLength(200);
            RuleFor(x => x.Categoria).Must(x => x == "Dev" || x == "Biz Dev" || x == "Design");
            RuleFor(x => x.ComunidadeDev).MaximumLength(150);
        }
    }
}