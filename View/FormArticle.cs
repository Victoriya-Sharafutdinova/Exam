using DAL.Interfaces;
using DAL.ViewModel;
using DAL.BindingModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace View
{
    public partial class FormArticle : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IArticleService service;

        private int? id;

        public FormArticle(IArticleService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormArticle_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxTitle.Text = view.Title;
                        textBoxSubject.Text = view.Subject;
                        dateTimePicker1.Value = view.DateCreate;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTitle.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxSubject.Text))
            {
                MessageBox.Show("Заполните тематику", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new ArticleBindingModel()
                    {
                        Id = id.Value,
                        Title = textBoxTitle.Text,
                        Subject = textBoxSubject.Text,
                        DateCreate = dateTimePicker1.Value
                    });
                }
                else
                {
                    service.AddElement(new ArticleBindingModel()
                    {
                        Title = textBoxTitle.Text,
                        Subject = textBoxSubject.Text,
                        DateCreate = dateTimePicker1.Value
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
