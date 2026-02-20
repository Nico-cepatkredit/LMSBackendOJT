
namespace LMSBackend.Domain.Entities
{
    public class UserProcessType
    {
        public int Id { get; set; }
        public required string User { get; set; }
        public required string Process { get; set; }
        public required string Type { get; set; }
        public required string RecBy { get; set; }
        public DateTime RecDate { get; set; }
        public string? ModBy { get; set; }
        public DateTime? ModDate { get; set; }
    }
}