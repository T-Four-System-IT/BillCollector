using Domain;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Presentation.ChildForms
{
    public partial class FormBillingRule : Form
    {
        private ClientProductModel _clientProductModel = new ClientProductModel();

        public FormBillingRule()
        {
            InitializeComponent();
            ListClientProduct();
        }

        private void ListClientProduct()
        {
            dataGridView1.DataSource = _clientProductModel.GetAllClientProducts().ToList();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            var clientProductForm = new FormClientProductMaintenance();
            DialogResult result = clientProductForm.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                ListClientProduct();
            }
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            if (TxtSearch.Text != string.Empty)
            {
                FindClientProduct(TxtSearch.Text);
            }
            else
            {
                ListClientProduct();
            }

        }

        private void FindClientProduct(string value)
        {
            ClientProductModel clientProductModel = new ClientProductModel();

            var productList = clientProductModel.GetClientProductByValue(value); 
            dataGridView1.DataSource = productList; 
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {

        }
    }
}
