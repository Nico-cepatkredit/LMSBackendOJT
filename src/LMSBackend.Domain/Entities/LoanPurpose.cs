using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSBackend.Domain.Entities;

[Table("LoanPurpose", Schema = "REF")]
public class LoanPurpose
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "varchar(150)")]
    public string Name { get; set; } = null!;
    [Required]
    public bool Status { get; set; }
}