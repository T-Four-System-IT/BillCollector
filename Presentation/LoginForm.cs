using Common;
using Domain;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class LoginForm : Form
    {
        #region -> Fields

        private DragControl dragControl;//Permite arrastrar el formulario
        private string usernamePlaceholder;//Marca de agua(Placeholder) para el cuadro de texto usuario.
        private string passwordPlaceholder;//Marca de agua(Placeholder) para el cuadro de texto contraseña.
        private Color placeholderColor;//Color del marca de agua(Placeholder).
        private Color textColor;//Color para el texto del cuadro texto.
        #endregion

        #region -> Constructor

        public LoginForm()
        {
            InitializeComponent();
            dragControl = new DragControl(this, this);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            usernamePlaceholder = txtUsername.Text;
            passwordPlaceholder = txtPassword.Text;
            placeholderColor = txtUsername.ForeColor;
            textColor = Color.Gainsboro;

            label1.Select();
        }
        #endregion

        #region -> Métodos

        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsername.Text = usernamePlaceholder;
                txtUsername.ForeColor = placeholderColor;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = passwordPlaceholder;
                txtPassword.ForeColor = placeholderColor;
                txtPassword.UseSystemPasswordChar = false;
            }
        }
        private void RemovePlaceHolder(TextBox textBox, string placeholderText)
        {
            if (textBox.Text == placeholderText)
            {
                textBox.Text = "";
                textBox.ForeColor = textColor;
                if (textBox == txtPassword)
                    textBox.UseSystemPasswordChar = true;
            }
        }
        private void Login()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || txtUsername.Text == usernamePlaceholder)
            {
                ShowMessage("Informe o login do usuário");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text == passwordPlaceholder)
            {
                ShowMessage("Informe a senha");
                return;
            }

            var userModel = new UserModel().Login(txtUsername.Text, txtPassword.Text);
            if (userModel != null)
            {
                Form mainForm;

                if (ActiveUser.Position == Positions.GeneralManager || ActiveUser.Position == Positions.Accountant
                    || ActiveUser.Position == Positions.AdministrativeAssistant || ActiveUser.Position == Positions.SystemAdministrator)
                {
                    mainForm = new MainForm(userModel);
                }
                else if (ActiveUser.Position == Positions.HMR)
                {
                    mainForm = new ChildForms.FormUsers();
                }
                else if (ActiveUser.Position == Positions.Receptionist)
                {
                    mainForm = new ChildForms.FormProducts();
                }
                else if (ActiveUser.Position == Positions.MarketingGuru)
                {
                    mainForm = new ChildForms.FormValidation();
                }
                else
                {
                    mainForm = null;
                    MessageBox.Show("Perfil não encontrado.", "Validação Perfil",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Hide();
                
                mainForm.FormClosed += new FormClosedEventHandler(MainForm_SessionClosed);
                mainForm.Show();
            }
            else 
                ShowMessage("Usuário ou senha incorretos");

        }

        private void Logout()
        {
            this.Show();
            txtUsername.Clear();
            txtPassword.Clear();
            SetPlaceholder();         
            label1.Select();
            lblErrorMessage.Visible = false;
        }
        private void ShowMessage(string message)
        {
            lblErrorMessage.Text = "    " + message;
            lblErrorMessage.Visible = true;
        }
        #endregion

        #region -> Métodos dos Evento

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Login();
        }
        private void MainForm_SessionClosed(object sender, FormClosedEventArgs e)
        {
            Logout();
        }

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Gray, 1), txtPassword.Location.X, txtPassword.Bottom + 5, txtPassword.Right, txtPassword.Bottom + 5);
            e.Graphics.DrawLine(new Pen(Color.Gray, 1), txtUsername.Location.X, txtUsername.Bottom + 5, txtUsername.Right, txtUsername.Bottom + 5);
            e.Graphics.DrawRectangle(new Pen(Color.Gray), 0, 0, this.Width - 1, this.Height - 1);
        }     

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            RemovePlaceHolder(txtUsername, usernamePlaceholder);
        }
        private void txtUsername_Leave(object sender, EventArgs e)
        {
            SetPlaceholder();
        }
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            RemovePlaceHolder(txtPassword, passwordPlaceholder);
        }
        private void txtPassword_Leave(object sender, EventArgs e)
        {
            SetPlaceholder();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lblForgotPass_Click(object sender, EventArgs e)
        {
            var frm = new ChildForms.FormRecoverPassword();
            frm.ShowDialog();
        }
        #endregion

        #region -> Overrides

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams param = base.CreateParams;
                param.Style |= 0x20000; 
                return param;
            }
        }
        #endregion
    }
}
