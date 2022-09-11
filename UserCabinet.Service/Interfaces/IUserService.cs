using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserCabinet.Domain.Configuration;
using UserCabinet.Domain.Entities.Users;
using UserCabinet.Service.DTOs.Users;

namespace UserCabinet.Service.Interfaces
{
    public interface IUserService
    {
        Task<User> AddAsync(UserForCreationDTO user);
        Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        Task<User> GetAsync(Expression<Func<User, bool>> expression);
        Task<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null);
        Task<User> UpdateAsync(long id, UserForCreationDTO dto);
        Task<User> LogIn(string email, string password);
    }
}
