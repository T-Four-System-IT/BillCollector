using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Presentation.ChildForms
{
    public partial class FormClients : Form
    {
        private ClientModel _clientModel = new ClientModel();
        private List<ClientModel> _clientList;

        public FormClients()
        {
            InitializeComponent();
            ListClients();
        }

        private void ListClients()
        {
            _clientList = _clientModel.GetAllClients().ToList();
            dataGridView1.DataSource = _clientList;
        }

        private void FormClients_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var clientForm = new FormClientsMaintenance();
            DialogResult result = clientForm.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                ListClients();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (TxtSearch.Text != string.Empty)
            {
                FindClient(TxtSearch.Text);
            }
            else
            {
                ListClients();
            }

        }
        private void FindClient(string value)
        {
            ClientModel clientModel = new ClientModel();

            var clientList = clientModel.GetByValue(value);
            dataGridView1.DataSource = clientList;
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
                var clientDM = _clientModel.GetClientById((int)dataGridView1.CurrentRow.Cells[0].Value);
                if (clientDM != null)
                {
                    var productForm = new FormClientsMaintenance(clientDM);
                    DialogResult result = productForm.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        ListClients();
                    }
                }
                else MessageBox.Show("Não foi encontrado o Cliente", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
                MessageBox.Show("Por favor selecione um cliente", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }
    }
}
