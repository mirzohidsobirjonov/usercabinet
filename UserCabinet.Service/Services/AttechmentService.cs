
using System;
using System.IO;
using System.Threading.Tasks;
using UserCabinet.Data.IRepositories;
using UserCabinet.Domain.Entities.Attachments;
using UserCabinet.Service.Helpers;
using UserCabinet.Service.Interfaces;

namespace UserCabinet.Service.Services
{
    public class AttechmentService : IAttechmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttechmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Attechment> UploadAsync(Stream stream, string fileName)
        {
            //Uploading to wwwroot
            fileName = Guid.NewGuid().ToString("N") + "-" + fileName;
            string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);

            FileStream fileStream = File.Create(filePath);
            await stream.CopyToAsync(fileStream);

            await fileStream.FlushAsync();
            fileStream.Close();

            // save to database
            var attachment = await _unitOfWork.Attechments.AddAsync(new Attechment()
            {
                Name = Path.GetFileName(filePath),
                Path = Path.Combine(EnvironmentHelper.FilePath, Path.GetFileName(filePath)),
                CreatedAt = DateTime.UtcNow
            });

            await _unitOfWork.SaveChangesAsync();

            return attachment;
        }
    }
}
