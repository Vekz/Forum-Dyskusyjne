using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum_Dyskusyjne.Models
{
    public enum UserRole
    {
        Admin,
        Mod,
        User
    }

    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        public string UserName { get; set; }

        public byte[] Avatar { get; set; }

        [Required]
        public string PasswordHash {get; set;}

        [Required]
        public UserRole Role { get; set; }

        [Required] 
        public float Timeout { get; set; } = 1800.0f; // 30min = 1800s

        [Required]
        public string EMail { get; set; }
                
        public ICollection<Thread> Threads { get; set; }
        public ICollection<Post> Posts { get; set; }
        
        public ICollection<Message> Messages { get; set; }
    }
}