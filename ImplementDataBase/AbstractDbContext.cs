using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDataBase
{
    public class AbstractDbContext : DbContext
    {
        public AbstractDbContext() : base("ExamDB")
        {                 
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        } 


        public virtual DbSet<Article> Articles { get; set; }

        public virtual DbSet<Author> Authors { get; set; }
    }
}
