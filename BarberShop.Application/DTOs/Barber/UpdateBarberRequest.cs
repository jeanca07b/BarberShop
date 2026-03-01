using System.ComponentModel.DataAnnotations;

public class UpdateBarberRequest
{
    [MaxLength(100)]
    public required string FullName { get; set; }

    [Phone]
    public string? Phone { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }
}