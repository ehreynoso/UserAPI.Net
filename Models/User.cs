using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
}
