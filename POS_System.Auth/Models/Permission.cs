using System;

namespace POS_System.Auth.Models
{
    public class Permission
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
