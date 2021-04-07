using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum_Dyskusyjne.Models
{
    [Table("Message")]
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        
        [ForeignKey("Sender"), Column(Order = 1)]
        public int SenderId { get; set; }
        public virtual User Sender { get; set; }
        
        [ForeignKey("Receiver"), Column(Order = 2)]
        public int ReceiverId { get; set; }
        public virtual User Receiver { get; set; }
        
        [Required]
        public string Text { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime SendDate { get; set; }

        public bool Seen { get; set; } = false ;

    }
}