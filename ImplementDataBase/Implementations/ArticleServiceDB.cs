using DAL.Interfaces;
using DAL.BindingModel;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ImplementDataBase.Implementations
{
    public class ArticleServiceDB : IArticleService
    {
        private AbstractDbContext context;

        public ArticleServiceDB(AbstractDbContext context) { this.context = context; }

        public List<ArticleViewModel> GetList()
        {
            List<ArticleViewModel> result = context.Articles.Select(rec => new ArticleViewModel
            {
                Id = rec.Id,
                Title = rec.Title,
                Subject = rec.Subject,
                DateCreate = rec.DateCreate
            }).ToList();
            return result; }

        public ArticleViewModel GetElement(int id)
        {
            Article element = context.Articles.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ArticleViewModel
                {
                    Id = element.Id,
                    Title = element.Title,
                    Subject = element.Subject,
                    DateCreate = element.DateCreate
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ArticleBindingModel model)
        {
            Article element = context.Articles.FirstOrDefault(rec => rec.Title == model.Title);
            if (element != null)
            {
                throw new Exception("Уже есть такая статья");
            }
            context.Articles.Add(new Article
            {
                Title = model.Title,
                Subject = model.Subject,
                DateCreate = model.DateCreate
            });
            context.SaveChanges(); }

        public void UpdElement(ArticleBindingModel model)
        {
            Article element = context.Articles.FirstOrDefault(rec => rec.Title == model.Title
            && rec.Subject == model.Subject && rec.DateCreate == model.DateCreate && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть такая статья");
            }
            element = context.Articles.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Title = model.Title;
            element.Subject = model.Subject;
            element.DateCreate = model.DateCreate;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Article element = context.Articles.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Articles.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
