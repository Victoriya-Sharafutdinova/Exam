using DAL.BindingModel;
using DAL.Interfaces;
using DAL.ViewModel;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementsServiceList.Implementations
{
    public class AuthorServiceList : IAuthorService
    {
        private DataListSingleton source;

        public AuthorServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<AuthorViewModel> GetList()
        {
            //var tmp = source.Articles.FirstOrDefault(recA => recA.Id == source.Authors[1].ArticleId);

            List<AuthorViewModel> authors = source.Authors.Select(rec => new AuthorViewModel
            {
                Id = rec.Id,
                FullName = rec.FullName,
                Email = rec.Email,
                DateBirth = rec.DateBirth,
               
                ArticleId = rec.ArticleId,
                ArticleName = source.Articles.FirstOrDefault(recA => recA.Id == rec.ArticleId)?.Title,
                Job = rec.Job
            })
            .ToList();
            return authors;
        }

        public AuthorViewModel GetElement(int id)
        {
            Author author = source.Authors.FirstOrDefault(rec => rec.Id == id);
            if (author != null)
            {
                return new AuthorViewModel
                {
                    Id = author.Id,
                    FullName = author.FullName,
                    Email = author.Email,
                    DateBirth = author.DateBirth,
                    Job = author.Job,
                    ArticleId = author.ArticleId,
                    ArticleName = source.Articles.FirstOrDefault(recA => recA.Id == author.ArticleId)?.Title

                };
            }
            throw new Exception("Автор не найден");
        }

        public void AddElement(AuthorBindingModel authors)
        {
            Author element = source.Authors.FirstOrDefault(rec => rec.FullName == authors.FullName);
            if (element != null)
            {
                throw new Exception("Уже есть такой автор");
            }
            int maxId = source.Authors.Count > 0 ? source.Authors.Max(rec => rec.Id) : 0;
            source.Authors.Add(new Author
            {
                Id = maxId + 1,
                FullName = authors.FullName,
                DateBirth = authors.DateBirth,
                Email = authors.Email,
                Job = authors.Job,
                ArticleId = authors.Id + 1
            });
        }

        public void UpdElement(AuthorBindingModel authors)
        {
            Author element = source.Authors.FirstOrDefault(rec => rec.FullName == authors.FullName &&
            rec.Email == authors.Email && rec.DateBirth == authors.DateBirth && rec.Job == authors.Job
            && rec.ArticleId == authors.ArticleId);
            if (element != null)
            {
                throw new Exception("Уже есть такой автор");
            }
            element = source.Authors.FirstOrDefault(rec => rec.Id == authors.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.FullName = authors.FullName;
            element.Email = authors.Email;
            element.DateBirth = authors.DateBirth;
            element.Job = authors.Job;
            element.ArticleId = authors.ArticleId;
        }

        public void DelElement(int id)
        {
            Author author = source.Authors.FirstOrDefault(rec => rec.Id == id);
            if (author != null)
            {
                source.Authors.Remove(author);
            }
            else
            {
                throw new Exception("Элемент не найден");

            }
        }
    }
}
