using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum_Dyskusyjne.Models
{
    public class Message
    {
        //[Key] // If we want to have index for message without addressing users that are sender and receiver.
        //public int Message_ID { get; set; }
        
        [Key]
        [Column(Order = 1)]
        public int SenderId { get; set; }
        
        [Key]
        [Column(Order = 2)]
        public int ReceiverId { get; set; }
        
        [Required]
        public string Text { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime SendDate { get; set; }

        public bool Seen { get; set; } = false ;

    }
}