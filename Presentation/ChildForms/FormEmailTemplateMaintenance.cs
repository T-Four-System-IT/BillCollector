using Common;
using Domain;
using Presentation.Helpers;
using Presentation.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.ChildForms
{
    public partial class FormEmailTemplateMaintenance : BaseForms.BaseFixedForm
    {
        #region -> Campos
        private EmailTemplateModel _emailTemplateModel;
        private bool _emailTemplateModify;
        private int _emailTemplateId;
        #endregion

        #region -> Constructores
        public FormEmailTemplateMaintenance()
        {
            InitializeComponent();
            lblCaption.Text = "Incluir novo Template";
            _emailTemplateModel = new EmailTemplateModel();
            _emailTemplateModify = false;
        }
        public FormEmailTemplateMaintenance(EmailTemplateModel emailTemplateModel)
        {
            InitializeComponent();
            this.TitleBarColor = Color.MediumSeaGreen;
            btnSave.BackColor = Color.MediumSeaGreen;

            _emailTemplateModel = emailTemplateModel;
            _emailTemplateModify = true;
            FillFields();
            lblCaption.Text = "Modificar o Template do Email";
        }

        public FormEmailTemplateMaintenance(EmailTemplateModel emailTemplateModel, bool emailTemplateDetail)
        {
            InitializeComponent();
            this.TitleBarColor = Color.MediumSlateBlue;
            btnSave.BackColor = Color.MediumSlateBlue;

            _emailTemplateModel = emailTemplateModel;
            _emailTemplateModify = true;
            FillFields();
            lblCaption.Text = "Visualizar informações sobre Template do Email";
            TxtDescricaoTemplate.ReadOnly = true;
            TxtAssunto.ReadOnly = true;
            TxtParagrafo1.ReadOnly = true;
            TxtParagrafo2.ReadOnly = true;
            TxtParagrafo3.ReadOnly = true;
            btnSave.Enabled = false;
        }
        #endregion

        #region -> Métodos
        private void FillFields()
        {
            _emailTemplateId = _emailTemplateModel.Id;
            TxtDescricaoTemplate.Text = _emailTemplateModel.Descricao;
            TxtAssunto.Text = _emailTemplateModel.Assunto;
            TxtParagrafo1.Text = _emailTemplateModel.Paragrafo1;
            TxtParagrafo2.Text = _emailTemplateModel.Paragrafo2;
            TxtParagrafo3.Text = _emailTemplateModel.Paragrafo2;
        }
        private void FillEmailTemplateModel()
        {
            _emailTemplateModel.Id = _emailTemplateId;
            _emailTemplateModel.Descricao = TxtDescricaoTemplate.Text;
            _emailTemplateModel.Assunto = TxtAssunto.Text;
            _emailTemplateModel.Paragrafo1 = TxtParagrafo1.Text;
            _emailTemplateModel.Paragrafo2 = TxtParagrafo2.Text;
            _emailTemplateModel.Paragrafo3 = TxtParagrafo3.Text;
        }
        private void Save()
        { 
            int result = -1;
            try
            {
                FillEmailTemplateModel();
                var validateData = new DataValidation(_emailTemplateModel);

                if (validateData.Result == true)
                {
                    // Editar Template do email
                    if (_emailTemplateModify == true)
                    {
                        result = _emailTemplateModel.ModifyEmailTemplate();
                        if (result > 0)
                        {
                            MessageBox.Show("Template atualizado com sucesso", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No se realizó la operación, intente nuevamente", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        // Incluir Template do emaul
                        result = _emailTemplateModel.CreateEmailTemplate(); 

                        if (result > 0)
                        {
                            MessageBox.Show("Template incluido com sucesso", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = System.Windows.Forms.DialogResult.OK; 
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Não foi realiza alguma operação, tente novamente.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else 
                {
                    if (validateData.Result == false)
                        MessageBox.Show(validateData.ErrorMessage, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                var message = ExceptionManager.GetMessage(ex);
                MessageBox.Show(message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region -> Metodos de evento
        private void FormUserMaintenance_Load(object sender, EventArgs e)
        {

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
