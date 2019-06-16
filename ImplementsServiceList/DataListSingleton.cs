using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementsServiceList
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Article> Articles { get; set; }

        public List<Author> Authors { get; set; }

        public static DataListSingleton GetInstance()
        {
            instance = new DataListSingleton();
            return instance; 
        }

        private DataListSingleton()
        {
            Articles = new List<Article>();
            Authors = new List<Author>();
        }
    }
}
