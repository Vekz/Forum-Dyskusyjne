using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum_Dyskusyjne.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        [ForeignKey("ParentCategory")]
        public int ParentId { get; set; } 
        public virtual Category ParentCategory { get; set; }
        
        public virtual ICollection<Category> SubCategories { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }
    }
}