namespace LMSBackend.Domain.Entities
{
    public class StatusList
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public bool Status { get; set; }
    }
}