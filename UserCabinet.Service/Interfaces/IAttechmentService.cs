
using System;
using System.IO;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Threading.Tasks;
using UserCabinet.Domain.Entities.Attachments;
using UserCabinet.Domain.Entities.Users;

namespace UserCabinet.Service.Interfaces
{
    public interface IAttechmentService
    {
        Task<Attechment> UploadAsync(Stream stream, string FileName);
    }
}