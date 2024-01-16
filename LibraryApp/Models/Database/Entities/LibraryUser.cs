﻿using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Database.Entities
{
    public class LibraryUser
    {
        public int LibraryUserId { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly Birthday { get; set; }
        public DateOnly CreationDate { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }

        //relations
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}