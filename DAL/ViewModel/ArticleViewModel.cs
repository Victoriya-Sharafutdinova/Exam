using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public DateTime DateCreate { get; set; }

    }
}
