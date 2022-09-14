using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCabinet.Domain.Entities.Users;

namespace UserCabinet.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateTokenAsync(string username, string password);
    }
}
