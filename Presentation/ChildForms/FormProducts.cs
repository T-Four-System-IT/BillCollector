using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Presentation.ChildForms
{
    public partial class FormProducts : Form
    {
        private ProductModel _productModel = new ProductModel();
        private List<ProductModel> _productList;

        public FormProducts()
        {
            InitializeComponent();
            ListProducts();
        }

        private void ListProducts()
        {
            _productList = _productModel.GetAllProducts().ToList();
            dataGridView1.DataSource = _productList;
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            var productForm = new FormProductsMaintenance();
            DialogResult result = productForm.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                ListProducts();
            }
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.RowCount <= 0)
            {
                MessageBox.Show("Selecione algum registro", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView1.SelectedCells.Count > 1)
            {
                var productDM = _productModel.GetProductById((int)dataGridView1.CurrentRow.Cells[0].Value);
                if (productDM != null)
                {
                    var productForm = new FormProductsMaintenance(productDM);
                    DialogResult result = productForm.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        ListProducts();
                    }
                }
                else MessageBox.Show("Não foi encontrado o Produto", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
                MessageBox.Show("Por favor selecione um produto", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnRemove_Click(object sender, System.EventArgs e)
        {
            // Eliminar Produto.
            if (dataGridView1.RowCount <= 0)
            {
                MessageBox.Show("Não há dados para selecionar", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView1.SelectedCells.Count > 1)
            {
                var result = _productModel.RemoveProduct((int)dataGridView1.CurrentRow.Cells[0].Value);

                if (result > 0)
                {
                    MessageBox.Show("Produto excluido com sucesso.");
                    ListProducts();
                }
                else MessageBox.Show("Não foi realizada operação alguma, tente novamente");
            }
            else
                MessageBox.Show("Por favor selecione um produto", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            if (TxtSearch.Text != string.Empty)
            {
                FindProduct(TxtSearch.Text);
            }
            else
            {
                ListProducts();
            }

        }
        private void FindProduct(string value)
        {
            ProductModel productModel = new ProductModel();

            var productList = productModel.GetByValue(value); 
            dataGridView1.DataSource = productList; 
        }

       


    }
}
