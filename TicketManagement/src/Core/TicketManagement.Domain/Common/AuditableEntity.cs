using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Domain.Common;

public class AuditableEntity
{
    [Required]
    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    [Required]
    public string? LastModifiedBy { get; set; }

    public DateTime? LastModifiedDate { get; set; }
}
