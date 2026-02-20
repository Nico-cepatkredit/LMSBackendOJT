namespace LMSBackend.Domain.Entities
{
    public class BranchList
    {
        public int Code {get; set; }
        public required string Name {get; set; }
        public required string Address {get; set;}
        public required string Description {get; set;}
        public int Status { get; set; }
        public required Guid RecUser { get; set; }
        public required DateTime RecDate {get; set; }
        public Guid? ModUser {get; set;}
        public DateTime? ModDate {get; set;}
    }
}