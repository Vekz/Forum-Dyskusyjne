using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum_Dyskusyjne.Models
{
    public class Message
    {
        //[Key] // If we want to have index for message without addressing users that are sender and receiver.
        //public int Message_ID { get; set; }
        
        [Key, Column(Order = 1)]
        [ForeignKey("Sender")]
        public int SenderId { get; set; }
        public User Sender { get; set; }
        
        [Key, Column(Order = 2)]
        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }
        
        [Required]
        public string Text { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime SendDate { get; set; }

        public bool Seen { get; set; } = false ;

    }
}