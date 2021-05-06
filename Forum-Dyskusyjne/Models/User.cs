using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Forum_Dyskusyjne.Models
{
    [Table("User")]
    public class User : IdentityUser
    {
        public User()
        {
            CreatedTime = DateTime.UtcNow; // Set creation date to now
            Timeout = 1800.0f; // Set timeout to: 30min = 1800s
        }
        public byte[] Avatar { get; set; }

        [Required] 
        public float Timeout { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<Thread> Threads { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        
        public virtual ICollection<Message> MessagesSent { get; set; }
        public virtual ICollection<Message> MessagesReceived { get; set; }
        
        // Methods
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}