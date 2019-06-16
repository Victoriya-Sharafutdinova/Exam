using DAL.BindingModel;
using DAL.Interfaces;
using DAL.ViewModel;
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
    public partial class FormAuthor : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IAuthorService service;

        private readonly IArticleService articleService;

        private int? id;

        public FormAuthor(IAuthorService service, IArticleService articleService)
        {
            InitializeComponent();
            this.service = service;
            this.articleService = articleService;
        }

        private void FormAuthor_Load(object sender, EventArgs e)
        {
            List<ArticleViewModel> list = articleService.GetList();
            if (list != null)
            {
                comboBox1.DisplayMember = "Title";
                comboBox1.ValueMember = "Id";
                comboBox1.DataSource = list;
            }
            if (id.HasValue)
            {
                try
                {

                    var view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxFullName.Text = view.FullName;
                        textBoxEmail.Text = view.Email;
                        dateTimePicker1.Value = view.DateBirth;
                        textBoxJob.Text = view.Job;

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
            if (string.IsNullOrEmpty(textBoxFullName.Text))
            {
                MessageBox.Show("Введите ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Выберите статью", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new AuthorBindingModel()
                    {
                        Id = id.Value,
                        FullName = textBoxFullName.Text,
                        Email = textBoxEmail.Text,
                        Job = textBoxJob.Text,
                        DateBirth = dateTimePicker1.Value,
                        ArticleId = Convert.ToInt32(comboBox1.SelectedValue)
                    });
                }
                else
                {
                    service.AddElement(new AuthorBindingModel()
                    {
                        FullName = textBoxFullName.Text,
                        Email = textBoxEmail.Text,
                        Job = textBoxJob.Text,
                        DateBirth = dateTimePicker1.Value,
                        ArticleId = Convert.ToInt32(comboBox1.SelectedValue)
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
