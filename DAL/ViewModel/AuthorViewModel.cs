using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    [DataContract]
    public class AuthorViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public DateTime DateBirth { get; set; }

        [DataMember]
        public string Job { get; set; }

        [DataMember]
        public int ArticleId { get; set; }

        public string ArticleName { get; set; }

    }
}
