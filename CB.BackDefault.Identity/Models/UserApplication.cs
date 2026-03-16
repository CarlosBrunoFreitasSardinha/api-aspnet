using Microsoft.AspNetCore.Identity;

namespace CB.BackDefault.Identity.Models
{
    public class UserApplication : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UrlProfile { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
