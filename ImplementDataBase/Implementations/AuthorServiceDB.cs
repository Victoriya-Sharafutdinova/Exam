using DAL.BindingModel;
using DAL.Interfaces;
using DAL.ViewModel;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDataBase.Implementations
{
    public class AuthorServiceDB : IAuthorService
    {
        private AbstractDbContext context;

        public AuthorServiceDB(AbstractDbContext context) { this.context = context; }

        public List<AuthorViewModel> GetList()
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
            return result;
        }

        public AuthorViewModel GetElement(int id)
        {
            Author author = context.Authors.FirstOrDefault(rec => rec.Id == id);
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
                    ArticleName = context.Articles.FirstOrDefault(recA => recA.Id == author.ArticleId)?.Title
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(AuthorBindingModel model)
        {
            Author authors = context.Authors.FirstOrDefault(rec => rec.FullName == model.FullName);
            if (authors != null)
            {
                throw new Exception("Уже есть такой автор");
            }
            context.Authors.Add(new Author
            {
                FullName = model.FullName,
                DateBirth = model.DateBirth,
                Email = model.Email,
                Job = model.Job,
                ArticleId = model.Id +1 
            });
            context.SaveChanges();
        }

        public void UpdElement(AuthorBindingModel authors)
        {
            Author element = context.Authors.FirstOrDefault(rec => rec.FullName == authors.FullName &&
            rec.Email == authors.Email && rec.DateBirth == authors.DateBirth && rec.Job == authors.Job
            && rec.ArticleId == authors.ArticleId && rec.Id != authors.Id);
            if (element != null)
            {
                throw new Exception("Уже есть такой автор");
            }
            element = context.Authors.FirstOrDefault(rec => rec.Id == authors.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.FullName = authors.FullName;
            element.Email = authors.Email;
            element.DateBirth = authors.DateBirth;
            element.Job = authors.Job;
            element.ArticleId = authors.ArticleId;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Author element = context.Authors.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Authors.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
