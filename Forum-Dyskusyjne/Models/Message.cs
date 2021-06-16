using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum_Dyskusyjne.Models
{
    [Table("Message")]
    public class Message
    {
        public Message()
        {
            SendDate = DateTime.UtcNow;
        }
        
        [Key]
        public int MessageId { get; set; }
        
        
        [ForeignKey("Sender"), Column(Order = 1)]
        public String SenderId { get; set; }
        public virtual User Sender { get; set; }
        
        [ForeignKey("Receiver"), Column(Order = 2)]
        public String ReceiverId { get; set; }
        public virtual User Receiver { get; set; }

        public String OrginalSender { get; set; }

        public String OrginalReciver { get; set; }

        
        [Required]
        public string Text { get; set; }

        [Required]
        [MaxLength(120)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime SendDate { get; set; }

        public bool Seen { get; set; } = false ;

    }
}