using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BindingModel
{
    public class ArticleBindingModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public DateTime DateCreate { get; set; }

        public List<AuthorBindingModel> Authors { get; set; }
    }
}
