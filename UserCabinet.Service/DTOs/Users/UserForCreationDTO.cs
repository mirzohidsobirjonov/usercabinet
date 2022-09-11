
using Microsoft.AspNetCore.Http;

namespace UserCabinet.Service.DTOs.Users
{
    public class UserForCreationDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public IFormFile File { get; set; }
    }
}