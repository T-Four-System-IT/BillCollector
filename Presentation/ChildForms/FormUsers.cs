using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Presentation.ChildForms
{
    public partial class FormUsers : Form
    {
        private UserModel _userModel = new UserModel();
        private List<UserModel> _userList;

        public FormUsers()
        {
            InitializeComponent();
            ListUsers();
        }

        private void ListUsers()
        {
            _userList = _userModel.GetAllUsers().ToList();
            dataGridView1.DataSource = _userList;
        }

        private void FormUsers_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var userForm = new FormUserMaintenance();
            DialogResult result = userForm.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                ListUsers();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount <= 0)
            {
                MessageBox.Show("Não existem dados para selecionar", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView1.SelectedCells.Count > 1)
            {
                var userDM = _userModel.GetUserById((int)dataGridView1.CurrentRow.Cells[0].Value);
                if (userDM != null)
                {
                    var userForm = new FormUserMaintenance(userDM, false);
                    DialogResult result = userForm.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        ListUsers();
                    }
                }
                else MessageBox.Show("No se ha encontrado usuario", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
                MessageBox.Show("Por favor seleccione una fila", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //Eliminar usuario.
            if (dataGridView1.RowCount <= 0)
            {
                MessageBox.Show("No hay datos para seleccionar", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView1.SelectedCells.Count > 1)
            {
                var result = _userModel.RemoveUser((int)dataGridView1.CurrentRow.Cells[0].Value);//Obtener ID del usuario e invocar el metodo eliminar usuario del modelo.

                if (result > 0)
                {
                    MessageBox.Show("Usuario eliminado con éxito");
                    ListUsers();
                }
                else MessageBox.Show("No se ha realizado operación, intente nuevamente");
            }
            else
                MessageBox.Show("Por favor seleccione una fila", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
