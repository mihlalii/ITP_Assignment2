﻿namespace ITP_Assignment.Models.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;

        // Navigation
        public ICollection<User>? Users { get; set; }
    }
}
