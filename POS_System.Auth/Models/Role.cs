using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace POS_System.Auth.Models
{
    public class Role : IdentityRole<Guid>
    {
        public string? Description { get; set; }
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
