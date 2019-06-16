using DAL.BindingModel;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAuthorService
    {
        List<AuthorViewModel> GetList();

        AuthorViewModel GetElement(int id);

        void AddElement(AuthorBindingModel model);

        void UpdElement(AuthorBindingModel model);

        void DelElement(int id);
    }
}
