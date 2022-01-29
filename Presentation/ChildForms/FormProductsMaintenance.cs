using Common;
using Domain;
using Presentation.Helpers;
using Presentation.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.ChildForms
{
    public partial class FormProductsMaintenance : BaseForms.BaseFixedForm
    {
        #region -> Campos
        private ProductModel _productModel;
        private bool _productModify;
        private int _productId;
        #endregion

        #region -> Constructores
        public FormProductsMaintenance()
        {
            InitializeComponent();
            lblCaption.Text = "Incluir novo Produto";
            _productModel = new ProductModel();
            _productModify = false;
        }
        public FormProductsMaintenance(ProductModel productModel)
        {

            InitializeComponent();
            this.TitleBarColor = Color.MediumSeaGreen;
            btnSave.BackColor = Color.MediumSeaGreen;

            _productModel = productModel;
            _productModify = true;
            FillFields();
            lblCaption.Text = "Modificar o Produto";
        }
        #endregion

        #region -> Métodos
        private void FillFields()
        {//Cargar los datos del modelo  en los campos del formulario.
            _productId = _productModel.Id;
            TxtCodigoProduto.Text = _productModel.CodigoProduto;
            TxtNomeProduto.Text = _productModel.NomeProduto;
            TxtOperadorManutencao.Text = _productModel.OperadorManutencao;
            TxtDataManutencao.Text = _productModel.DataManutencao;
        }
        private void FillProductModel()
        {
            _productModel.Id = _productId;
            _productModel.CodigoProduto = TxtCodigoProduto.Text;
            _productModel.NomeProduto = TxtNomeProduto.Text;
            _productModel.OperadorManutencao = TxtOperadorManutencao.Text;
            _productModel.DataManutencao = TxtDataManutencao.Text;
        }
        private void Save()
        { 
            int result = -1;
            try
            {
                FillProductModel();
                var validateData = new DataValidation(_productModel);

                if (validateData.Result == true)
                {
                    // Editar Produto
                    if (_productModify == true)
                    {
                        result = _productModel.ModifyProduct();
                        if (result > 0)
                        {
                            MessageBox.Show("Produto atualizado com sucesso", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        // Incluir Produto
                        result = _productModel.CreateProduct(); 

                        if (result > 0)
                        {
                            MessageBox.Show("Produto incluido com sucesso", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
