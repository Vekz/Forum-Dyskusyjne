using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum_Dyskusyjne.Models
{
    public enum UserRole
    {
        Admin,
        Mod,
        User
    }

    [Table("User")]
    public class User
    {
        public User()
        {
            CreatedTime = DateTime.UtcNow;
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
                
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<Thread> Threads { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        
        public virtual ICollection<Message> MessagesSent { get; set; }
        public virtual ICollection<Message> MessagesReceived { get; set; }
    }
}