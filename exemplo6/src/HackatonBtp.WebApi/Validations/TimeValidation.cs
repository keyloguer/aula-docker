using FluentValidation;
using HackatonBtp.Domain.Models.DTOs;
using HackatonBtp.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace HackatonBtp.WebApi.Validations
{
    public class TimeValidation : AbstractValidator<TimeDTO>
    {
        private readonly ITimeRepository _timeRepository;
        public TimeValidation(ITimeRepository timeRepository)
        {
            _timeRepository = timeRepository;

            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome do time não pode ser vazio");
            
            RuleFor(x => x.Nome).Must(x => ObterTimePorNome(x).Result).WithMessage("Nome de time já está em uso");
            RuleFor(x => x.Email).EmailAddress().WithMessage("O email não está em um formato válido");
            RuleFor(x => x.Email).Must(x => ObterTimePorEmail(x).Result).WithMessage("O email para o time já está em uso");
            RuleFor(x => x.Integrantes).Must(x => x.Count >= 1 && x.Count <= 4).WithMessage("O time só pode ter de 1 até 4 participantes");
            RuleForEach(x => x.Integrantes).SetValidator(new IntegranteValidation());
        }

        private async Task<bool> ObterTimePorNome(string nomeTime)
        {
            using (var timeRepository = _timeRepository)
            {
                var time = await timeRepository.BuscarPorNome(nomeTime);

                return time == null;
            }
        }

        private async Task<bool> ObterTimePorEmail(string emailTime)
        {
            using (var timeRepository = _timeRepository)
            {
                var time = await timeRepository.BuscarPorEmail(emailTime);

                return time == null;
            }
        }
    }
}