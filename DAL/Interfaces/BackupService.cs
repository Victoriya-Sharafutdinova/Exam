using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface BackupService
    {
        void SaveAuthors(string fileName);

        void SaveArticles(string fileName);

        void LoadArticles(string fileName);

        void LoadAuthors(string fileName);
    }
}
