using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }
        
        public DateTime DateBirth { get; set; }

        [Required]
        public string Job { get; set; }

        public int ArticleId { get; set; }

        public virtual Article Article { get; set; } 
    }
}
