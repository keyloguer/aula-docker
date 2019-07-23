using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackatonBtp.Domain.Models;

namespace HackatonBtp.Domain.Interfaces.Repository
{
    public interface ITimeRepository : IRepository<Time>
    {
        Task<Time> BuscarPorNome(string nomeTime);
        Task<Time> BuscarPorEmail(string emailTime);
        Task<Time> ObterTime(Guid timeId);
        Task<int> ConfirmarEmail(Guid timeId);
        Task<int> ConfirmarEnvioEmail(Guid timeId);
        Task<IEnumerable<Time>> ObterTimesSemEnvioEmail();
    }
}