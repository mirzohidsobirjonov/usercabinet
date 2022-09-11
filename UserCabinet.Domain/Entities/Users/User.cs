
using System.ComponentModel.DataAnnotations.Schema;
using UserCabinet.Domain.Commons;
using UserCabinet.Domain.Entities.Attachments;

namespace UserCabinet.Domain.Entities.Users
{
    public class User : Auditable<long>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Adress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public long? AttechmentId { get; set; }
        [ForeignKey(nameof(AttechmentId))]
        public Attechment Attechment { get; set; }
    }
}
