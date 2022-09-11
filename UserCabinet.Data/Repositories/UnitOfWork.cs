
using System;
using System.Threading.Tasks;
using UserCabinet.Data.DbContexts;
using UserCabinet.Data.IRepositories;
using UserCabinet.Domain.Entities.Attachments;
using UserCabinet.Domain.Entities.Users;

namespace UserCabinet.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<User> Users { get; }
        public IGenericRepository<Attechment> Attechments { get; }
        public UserCabinetDbContext _dbContext { get; set; }

        public UnitOfWork(UserCabinetDbContext dbContext)
        {
            _dbContext = dbContext;
            Users = new GenericRepository<User>(dbContext);
            Attechments = new GenericRepository<Attechment>(dbContext);
        }

        public void Dispose()
            => GC.SuppressFinalize(this);

        public async Task SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();
    }
}
