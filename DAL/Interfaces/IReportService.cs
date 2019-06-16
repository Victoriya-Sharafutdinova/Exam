using DAL.ViewModel;
using DAL.BindingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IReportService
    {
        void SaveAuthorArticles(ReportBindingModel model);

        List<ReportViewModel> GetAuthors(ReportBindingModel model);
    }
}
