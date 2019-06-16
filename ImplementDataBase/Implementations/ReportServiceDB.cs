using DAL.ViewModel;
using DAL.BindingModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace ImplementDataBase.Implementations
{
    public class ReportServiceDB : IReportService
    {
        private AbstractDbContext context;
        private static BaseFont baseFont;

        public List<ReportViewModel> GetAuthors(ReportBindingModel model)
        {
            return context.Authors.Include(x => x.Article)
                .Where(x => x.Article.DateCreate >= model.DateFrom
                                          && x.Article.DateCreate <= model.DateTo)
                .Select(x => new ReportViewModel()
                {                  
                    Title = x.Article.Title,
                    DateCreate = x.Article.DateCreate,
                    FullName = x.FullName,
                    DateBirth = x.DateBirth,
                    Job = x.Job
                })
                .ToList();
        }

        public ReportServiceDB(AbstractDbContext context)
        {
            this.context = context;

            if (!File.Exists("FiraSans.ttf"))
            {
                File.WriteAllBytes("FiraSans.ttf", Properties.Resources.FiraSans);
            }

            baseFont = BaseFont.CreateFont("FiraSans.ttf",
                BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        }

        public void SaveAuthorArticles(ReportBindingModel model)
        {
            var document = new iTextSharp.text.Document();
            using (var writer = PdfWriter.GetInstance(document, new FileStream(model.FileName, FileMode.Create)))
            {
                document.Open();

                PrintDates(model, document);
                PrintAuthors(document);

                document.Close();
                writer.Close();
            }
        }

        private void PrintAuthors(Document document)
        {
            PdfPTable table = new PdfPTable(5);

            PdfPCell cell = new PdfPCell(new Phrase("Статьи авторов",
                new Font(baseFont, 12, Font.BOLD)));

            cell.Colspan = 5;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            var list = context.Authors.Select(x => new ReportViewModel()
            {
                Title = x.Article.Title,
                DateCreate = x.Article.DateCreate,
                FullName = x.FullName,
                DateBirth = x.DateBirth,
                Job = x.Job
            }).ToList();

            foreach (var model in list)
            {
                table.AddCell(model.Title);
                table.AddCell(model.DateCreate.ToString());
                table.AddCell(model.FullName);
                table.AddCell(model.DateBirth.ToString());
                table.AddCell(model.Job.ToString());
            }
        }

        private void PrintDates(ReportBindingModel model, Document document)
        {
            var dateFrom = model.DateFrom?.ToString("dd-MM-yyyy");
            var dateTo = model.DateTo?.ToString("dd-MM-yyyy");

            var phraseTitle = new Phrase("С " + dateFrom + " по " + dateTo,
                new Font(baseFont, 16, Font.BOLD));

            Paragraph paragraph = new Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };

            document.Add(paragraph);
        }
    }
}
