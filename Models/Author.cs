using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Author
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        
        public DateTime DateBirth { get; set; }

        public string Job { get; set; }

        public int ArticleId { get; set; }
    }
}
