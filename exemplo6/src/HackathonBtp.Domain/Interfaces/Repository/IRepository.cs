using System;
using System.Threading.Tasks;

namespace HackatonBtp.Domain.Interfaces.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<int> Save(TEntity obj);

    }
}