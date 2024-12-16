using Microsoft.AspNetCore.Identity;
using System;

namespace POS_System.Auth.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        // Add custom properties here
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Role> Role { get; set; } = new List<Role>();
        public IEnumerable<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
