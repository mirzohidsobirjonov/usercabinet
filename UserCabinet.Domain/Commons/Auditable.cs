using System;

namespace UserCabinet.Domain.Commons
{
    public class Auditable<T>
    {
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}