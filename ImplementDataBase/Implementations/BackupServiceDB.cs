using DAL.BindingModel;
using DAL.Interfaces;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDataBase.Implementations
{
    public class BackupServiceDB : BackupService

    {
        private AbstractDbContext context;

        private ArticleServiceDB articleService;

        private AuthorServiceDB authorService;

        public BackupServiceDB(ArticleServiceDB articleService, AuthorServiceDB authorService)
        {
            this.articleService = articleService;

            this.authorService = authorService;
        }
        public void SaveAuthors(string fileName)
        {
            DataContractJsonSerializer jsonFormatterAuthor = new DataContractJsonSerializer(typeof(List<AuthorViewModel>));
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                jsonFormatterAuthor.WriteObject(writer.BaseStream, authorService.GetList().ToArray());
            }
        }

        public void SaveArticles(string fileName)
        {
            DataContractJsonSerializer jsonFormatterArticle = new DataContractJsonSerializer(typeof(List<ArticleViewModel>));
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                jsonFormatterArticle.WriteObject(writer.BaseStream, articleService.GetList().ToArray());
            }
        }

        public void LoadArticles(string fileName)
        {
            DataContractJsonSerializer jsonFormatterArticle = new DataContractJsonSerializer(typeof(List<ArticleViewModel>));
            using (StreamReader reader = new StreamReader(fileName))
            {
                var list = (List<ArticleViewModel>)jsonFormatterArticle.ReadObject(reader.BaseStream);
                var bindingList = list.Select(x => new ArticleBindingModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Subject = x.Subject,
                    DateCreate = x.DateCreate                    
                });
                foreach (var model in bindingList)
                {
                    articleService.UpdElement(model);
                }
            }
        }

        public void LoadAuthors(string fileName)
        {
            List<AuthorViewModel> result = context.Authors.Select(rec => new AuthorViewModel
            {
                Id = rec.Id,
                FullName = rec.FullName,
                Email = rec.Email,
                DateBirth = rec.DateBirth,
                ArticleId = rec.ArticleId,
                ArticleName = context.Articles.FirstOrDefault(recA => recA.Id == rec.ArticleId).Title,
                Job = rec.Job
            }).ToList();

            foreach (var el in result)
            {
                authorService.DelElement(el.Id);
                context.SaveChanges();
            }
            
            DataContractJsonSerializer jsonFormatterArticle = new DataContractJsonSerializer(typeof(List<AuthorViewModel>));
            using (StreamReader reader = new StreamReader(fileName))
            {
                var list = (List<AuthorViewModel>)jsonFormatterArticle.ReadObject(reader.BaseStream);
                var bindingList = list.Select(x => new AuthorBindingModel()
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    Job = x.Job,
                    DateBirth = x.DateBirth,
                    ArticleId = x.ArticleId
                });
                foreach (var model in bindingList)
                {
                    authorService.UpdElement(model);
                }
            }
        }
    }
}
