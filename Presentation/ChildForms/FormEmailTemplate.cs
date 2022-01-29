using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.ChildForms
{
    public partial class FormEmailTemplate : Form
    {
        private EmailTemplateModel _emailTemplateModel = new EmailTemplateModel();
        private List<EmailTemplateModel> _emailTemplateList;


        public FormEmailTemplate()
        {
            InitializeComponent();
            ListEmailTemplate();
        }

        private void ListEmailTemplate()
        {
            dataGridView1.DataSource = _emailTemplateModel.GetAllEmailTemplates().ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var emailTemplateForm = new FormEmailTemplateMaintenance();
            DialogResult result = emailTemplateForm.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                ListEmailTemplate();
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount <= 0)
            {
                MessageBox.Show("Selecione algum registro", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView1.SelectedCells.Count > 1)
            {
                var emailTemplateDM = _emailTemplateModel.GetEmailTemplateById((int)dataGridView1.CurrentRow.Cells[0].Value);
                if (emailTemplateDM != null)
                {
                    var emailTemplateForm = new FormEmailTemplateMaintenance(emailTemplateDM);
                    DialogResult result = emailTemplateForm.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        ListEmailTemplate();
                    }
                }
                else MessageBox.Show("Não foi encontrado o Template", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
                MessageBox.Show("Por favor selecione um template", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
