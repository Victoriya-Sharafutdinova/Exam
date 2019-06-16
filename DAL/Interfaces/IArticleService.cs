﻿using DAL.BindingModel;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IArticleService
    {
        List<ArticleViewModel> GetList();

        ArticleViewModel GetElement(int id);

        void AddElement(ArticleBindingModel model);

        void UpdElement(ArticleBindingModel model);

        void DelElement(int id);
    }
}
