using Common;
using DataAccess.DBServices.DTO;
using DataAccess.DBServices.Entities;
using Domain;
using Domain.Models;
using Presentation.Helpers;
using Presentation.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.ChildForms
{
    public partial class FormClientProductMaintenance : BaseForms.BaseFixedForm
    {
        #region -> Campos
        private ClientProductModel _clientProductModel;
        private ClientProductEntity _clientProductEntity;
        private bool _clientProductModify;
        private int _clientProductId;
        #endregion

        #region -> Constructores
        public FormClientProductMaintenance()
        {
            InitializeComponent();
            lblCaption.Text = "Associar novo produto ao Cliente";
            _clientProductModel = new ClientProductModel();
            _clientProductEntity = new ClientProductEntity();
            _clientProductModify = false;

            GetStatusCombo();
            GetClientCombo();
            GetProductCombo();

            CboCliente.SelectedIndex = -1;
            CboProduto.SelectedIndex = -1;
            CboStatus.SelectedIndex = -1;
        }

        public FormClientProductMaintenance(ClientProductModel clientProductModel)
        {
            InitializeComponent();
            this.TitleBarColor = Color.MediumSeaGreen;
            btnSave.BackColor = Color.MediumSeaGreen;

            _clientProductModel = clientProductModel;
            _clientProductModify = true;
            FillFields();
            lblCaption.Text = "Desativar o produto do Cliente";
        }
        #endregion

        #region -> Métodos
        private void FillFields()
        {
            _clientProductId = _clientProductModel.Id;

            //TxtEmailGestorComercial.Text = _clientProductModel.EmailGestorComercial;
        }

        private void GetClientCombo()
        {
            ClientModel clientObj = new ClientModel();
            CboCliente.DataSource = clientObj.GetAllClients();
            CboCliente.DisplayMember = "RazaoSocial";
            CboCliente.ValueMember = "ID";
        }

        private void GetProductCombo()
        {
            ProductModel productObj = new ProductModel();
            CboProduto.DataSource = productObj.GetAllProducts();
            CboProduto.DisplayMember = "NomeProduto";
            CboProduto.ValueMember = "ID";
        }

        private void GetStatusCombo()
        {
            CboStatus.Items.Clear();
            CboStatus.Items.Add("Ativo");
            CboStatus.Items.Add("Inativo");
        }

        private void FillClientProductEntity()
        {
            _clientProductEntity.Id = _clientProductId;
            _clientProductEntity.ClientID = Convert.ToInt32(CboCliente.SelectedValue);
            _clientProductEntity.ProdutoID = Convert.ToInt32(CboProduto.SelectedValue);
            _clientProductEntity.EmailEnvioCobranca = TxtEmail.Text;
            _clientProductEntity.StatusClienteProduto = CboStatus.Text;
        }

        private void Save()
        {
            int result = -1;
            try
            {
                FillClientProductEntity();
                var validateData = new DataValidation(_clientProductModel);
                // Personal Validation
                string erro = "";
                bool estaOK = true;
                if (CboCliente.SelectedIndex == -1)
                {
                    erro += "Selecione o Cliente \n";
                    estaOK = false;
                }
                if (CboProduto.SelectedIndex == -1)
                {
                    erro += "Selecione o Produto \n";
                    estaOK = false;
                }
                if (TxtEmail.Text == string.Empty)
                {
                    erro += "Informe o email \n";
                    estaOK = false;
                }
                if (CboStatus.SelectedIndex == -1)
                {
                    erro += "Selecione o Status \n";
                    estaOK = false;
                }

                if (estaOK == false)
                {
                    MessageBox.Show(erro, "Erros encontrados");
                }


                if (estaOK == true)
                {
                    if (_clientProductModify == true)
                    {
                        // Editar o produto no cliente
                        result = _clientProductModel.ModifyClientProduct(_clientProductEntity);
                        result = 1;
                        if (result > 0)
                        {
                            MessageBox.Show("Dados do produto atualizado com sucesso", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            //this.Close();
                            lblCaption.Text = "Associar novo produto ao Cliente";

                            CboProduto.SelectedIndex = -1;
                            TxtEmail.Text = string.Empty;
                            CboStatus.SelectedIndex = -1;

                            CboCliente.Enabled = true;
                            CboProduto.Enabled = true;
                            _clientProductModify = false;
                            ListClientProducts();
                        }
                        else
                        {
                            MessageBox.Show("Não foi realizada operação alguma, tente novamente", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        // Associar o produto ao cliente
                        result = _clientProductModel.CreateClientProduct(_clientProductEntity);

                        result = 1;

                        if (result > 0)
                        {
                            MessageBox.Show("Produto incluido com sucesso", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            //this.Close();
                            CboProduto.SelectedIndex = -1;
                            TxtEmail.Text = string.Empty;
                            CboStatus.SelectedIndex = -1;
                            ListClientProducts();
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
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        private void CboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (CboCliente.SelectedValue == null || CboCliente.SelectedIndex > 0)
            {
                dataGridView1.DataSource = _clientProductModel.GetAllClientProducts(Convert.ToInt32(CboCliente.SelectedValue));
            }
        }

        private void CboCliente_SelectedValueChanged(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = _clientProductModel.GetAllClientProducts(6);

        }

        private void ListClientProducts()
        {
            if (CboCliente.SelectedValue == null || CboCliente.SelectedIndex > 0)
            {
                dataGridView1.DataSource = _clientProductModel.GetAllClientProducts(Convert.ToInt32(CboCliente.SelectedValue));
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                lblCaption.Text = "Associar novo produto ao Cliente";

                CboProduto.SelectedIndex = -1;
                TxtEmail.Text = string.Empty;
                CboStatus.SelectedIndex = -1;

                CboCliente.Enabled = true;
                CboProduto.Enabled = true;
                _clientProductModify = false;
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                lblCaption.Text = "Editação do produto do Cliente";

                if (dataGridView1.SelectedCells.Count > 1)
                {
                    _clientProductId= (int)dataGridView1.CurrentRow.Cells[0].Value;
                    CboProduto.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    TxtEmail.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    CboStatus.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    CboCliente.Enabled = false;
                    CboProduto.Enabled = false;
                    _clientProductModify = true;
                }
            }
        }
    }
}
