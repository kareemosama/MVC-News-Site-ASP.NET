using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public String FullName { get; set; }
    }
}
