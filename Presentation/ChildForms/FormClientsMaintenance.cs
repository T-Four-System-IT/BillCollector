using Common;
using Domain;
using Presentation.Helpers;
using Presentation.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.ChildForms
{
    public partial class FormClientsMaintenance : BaseForms.BaseFixedForm
    {
        #region -> Campos
        private ClientModel _clientModel;
        private bool _clientModify;
        private int _clientId;
        #endregion

        #region -> Constructores
        public FormClientsMaintenance()
        {
            InitializeComponent();
            lblCaption.Text = "Incluir novo Cliente";
            _clientModel = new ClientModel();
            _clientModify = false;
            CmbStatusCliente.SelectedIndex = -1;
            CmbTipoCliente.SelectedIndex = -1;
        }
        public FormClientsMaintenance(ClientModel clientModel)
        {
            InitializeComponent();
            this.TitleBarColor = Color.MediumSeaGreen;
            btnSave.BackColor = Color.MediumSeaGreen;

            _clientModel = clientModel;
            _clientModify = true;
            FillFields();
            lblCaption.Text = "Modificar o Cliente";
        }
        #endregion

        #region -> Métodos
        private void FillFields()
        {
            _clientId = _clientModel.Id;
            TxtCNPJ.Text = _clientModel.CNPJ;
            TxtRazaoSocial.Text = _clientModel.RazaoSocial;
            TxtNomeFantasia.Text = _clientModel.NomeFantasia;
            TxtCodeERP.Text = _clientModel.CodeERP;

            switch (_clientModel.TipoCliente)
            {
                case "P":
                    CmbTipoCliente.Text = "Pequeno";
                    break;

                case "M":
                    CmbTipoCliente.Text = "Médio";
                    break;

                case "G":
                    CmbTipoCliente.Text = "Grande";
                    break;

                default:
                    CmbTipoCliente.Text = "Pequeno";
                    break;
            }

            TxtEmailGestorComercial.Text = _clientModel.EmailGestorComercial;
            TxtEmnailkDiretorComercial.Text = _clientModel.EmailDiretorComercial;
            CmbStatusCliente.Text = _clientModel.StatusCliente;
            TxtOperadorManutencao.Text = _clientModel.OperadorManutencao;
            TxtDataManutencao.Text = _clientModel.DataManutencao.ToString();
        }

        private void FillClientModel()
        {
            _clientModel.Id = _clientId;
            _clientModel.CNPJ = TxtCNPJ.Text;
            _clientModel.RazaoSocial = TxtRazaoSocial.Text;
            _clientModel.NomeFantasia = TxtNomeFantasia.Text;
            _clientModel.CodeERP = TxtCodeERP.Text;

            switch (CmbTipoCliente.Text)
            {
                case "Pequeno":
                    _clientModel.TipoCliente = "P";
                    break;

                case "Médio":
                    _clientModel.TipoCliente = "M";
                    break;

                case "Grande":
                    _clientModel.TipoCliente = "G";
                    break;

                default:
                    _clientModel.TipoCliente = "P";
                    break;
            }

            _clientModel.EmailGestorComercial = TxtEmailGestorComercial.Text;
            _clientModel.EmailDiretorComercial = TxtEmnailkDiretorComercial.Text;

             _clientModel.StatusCliente = CmbStatusCliente.Text;

            //_clientModel.StatusCliente = TxtStatusCliente.Text;
            _clientModel.OperadorManutencao = TxtOperadorManutencao.Text;
            _clientModel.DataManutencao = DateTime.Now;
        }

        private void Save()
        {
            int result = -1;
            try
            {
                FillClientModel();
                var validateData = new DataValidation(_clientModel);

                if (validateData.Result == true)
                {
                    // Editar Cliente
                    if (_clientModify == true)
                    {
                        result = _clientModel.ModifyClient();
                        if (result > 0)
                        {
                            MessageBox.Show("Cliente atualizado com sucesso", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Não foi realizada operação alguma, tente novamente", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        // Incluir Cliente
                        result = _clientModel.CreateClient();

                        if (result > 0)
                        {
                            MessageBox.Show("Cliente incluido com sucesso", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            CmbTipoCliente.Items.Add("Pequeno");
            CmbTipoCliente.Items.Add("Médio");
            CmbTipoCliente.Items.Add("Grande");

            CmbStatusCliente.Items.Add("Ativo");
            CmbStatusCliente.Items.Add("Inativo");
            CmbStatusCliente.Items.Add("Cancelado");
            CmbStatusCliente.Items.Add("Bloqueado");
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
