using Microsoft.AspNetCore.Identity;
using POS_System.Auth.Models;
using System.ComponentModel.DataAnnotations;

public class ApplicationUser : IdentityUser<Guid>
{
    // Add custom properties here
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public ICollection<Role> Role { get; set; } = new List<Role>();
    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
