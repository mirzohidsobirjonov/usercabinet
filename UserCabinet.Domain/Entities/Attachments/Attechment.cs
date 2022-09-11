
using UserCabinet.Domain.Commons;

namespace UserCabinet.Domain.Entities.Attachments
{
    public class Attechment : Auditable<long>
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
