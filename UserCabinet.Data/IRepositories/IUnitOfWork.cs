
using System;
using System.Threading.Tasks;
using UserCabinet.Domain.Entities.Attachments;
using UserCabinet.Domain.Entities.Users;

namespace UserCabinet.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Attechment> Attechments { get; }

        Task SaveChangesAsync();
    }
}
