using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models.Database.Entities
{
    public class Reader
    {
        public string LibraryUserId { get; set; }
        public LibraryUser LibraryUser { get; set; }
        public bool IsActive { get; set; }
    }
}
