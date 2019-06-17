using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    [DataContract]
    public class ArticleViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public DateTime DateCreate { get; set; }

    }
}
