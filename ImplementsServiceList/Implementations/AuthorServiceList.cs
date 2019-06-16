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
            List<AuthorViewModel> authors = new List<AuthorViewModel>();
            for (int i = 0; i < source.Authors.Count; i++)
            {
                authors.Add(new AuthorViewModel
                {
                    Id = source.Authors[i].Id,
                    FullName = source.Authors[i].FullName,
                    Email = source.Authors[i].Email,
                    DateBirth = source.Authors[i].DateBirth,
                    Job = source.Authors[i].Job,
                    ArticleId = source.Authors[i].ArticleId
                });
            }
            return authors;
        }

        public AuthorViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Authors.Count; ++i)
            {              
                if (id == source.Authors[i].Id)
                {
                    return new AuthorViewModel
                    {
                        Id = source.Authors[i].Id,
                        FullName = source.Authors[i].FullName,
                        Email = source.Authors[i].Email,
                        DateBirth = source.Authors[i].DateBirth,
                        Job = source.Authors[i].Job,
                        ArticleId = source.Authors[i].ArticleId
                    };
                }
            }
            throw new Exception("Статья не найдена");
        }

        public void AddElement(AuthorBindingModel authors)
        {
            int maxId = 0;
            for (int i = 0; i < source.Authors.Count; i++)
            {
                if (source.Authors[i].Id > maxId)
                {
                    maxId = source.Authors[i].Id;
                }
                if (source.Authors[i].FullName == authors.FullName)
                {
                    throw new Exception("Уже есть такой автор");
                }
            }
            source.Authors.Add(new Author
            {
                Id = maxId + 1,
                FullName = authors.FullName,
                DateBirth = authors.DateBirth,
                Email = authors.Email,
                Job = authors.Job,
                ArticleId = authors.Id
            });
        }

        public void UpdElement(AuthorBindingModel authors)
        {
            int index = -1;
            for (int i = 0; i < source.Authors.Count; i++)
            {
                if (source.Authors[i].Id == authors.Id)
                {
                    index = i;
                }
                if (source.Authors[i].FullName == authors.FullName &&
                    source.Authors[i].Email == authors.Email &&
                    source.Authors[i].DateBirth == authors.DateBirth &&
                    source.Authors[i].Job == authors.Job &&
                    source.Authors[i].ArticleId == authors.ArticleId &&
                    source.Authors[i].Id == authors.Id)
                {
                    throw new Exception("Уже есть такой автор");
                }
            }

            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }

            source.Authors[index].FullName = authors.FullName;
            source.Authors[index].Email = authors.Email;
            source.Authors[index].DateBirth = authors.DateBirth;
            source.Authors[index].Job = authors.Job;
            source.Authors[index].ArticleId = authors.ArticleId;
            
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Authors.Count; i++)
            {
                if (source.Authors[i].Id == id)
                {
                    source.Authors.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("элемнет не найден");
        }



    }
}
