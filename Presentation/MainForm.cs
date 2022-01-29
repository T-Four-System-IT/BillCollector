using Common;
using Domain;
using Presentation.ChildForms;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class MainForm : BaseForms.BaseMainForm
    {
        #region -> Definición de Campos
        private DragControl dragControl;
        private List<Form> listChildForms; 
        private Form activeChildForm;
        #endregion

        #region -> Constructores
        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            dragControl = new DragControl(panelTitleBar, this);
            listChildForms = new List<Form>();
            linkProfile.Visible = false;
        }

        public MainForm(UserModel userModel)
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            dragControl = new DragControl(panelTitleBar, this);
            listChildForms = new List<Form>();
            LoadUserData(userModel);
        }
        #endregion

        #region -> Definición de Métodos
        public void LoadUserData(UserModel userModel)
        {
            //Cargar los datos del usuario conectado en el menú lateral.
            lblName.Text = userModel.FirstName;
            lblLastName.Text = userModel.LastName;
            lblPosition.Text = userModel.Position;
            if (userModel.Photo != null)
                pictureBoxPhoto.Image = Utils.ItemConverter.BinaryToImage(userModel.Photo);
            else pictureBoxPhoto.Image = Properties.Resources.DefaultUserProfile;
        }

        private void ManagePermissions()
        {//Administrar los permisos de usuario, esto es simplemente una demostración,
            //Puedes eliminarlo si no lo necesitas.
            switch (ActiveUser.Position)
            {
                case Positions.GeneralManager:

                    break;
                case Positions.Accountant:
                    btnUsers.Enabled = false;
                    BtbProducts.Enabled = false;
                    BtnClients.Enabled = false;
                    BtnEmailTemplate.Enabled = false;
                    break;
                case Positions.AdministrativeAssistant:
                    BtnReport.Enabled = false;
                    break;
                case Positions.Receptionist:
                    BtnReport.Enabled = false;
                    btnUsers.Enabled = false;
                    break;
                case Positions.HMR:

                    break;
                case Positions.MarketingGuru:

                    break;
                case Positions.SystemAdministrator:

                    break;
            }
        }

        private void Security()
        {//Puede hacer lo mismo en cualquier formulario que te parezca necesario.
            var userModel = new UserModel();
            if (userModel.ValidateActiveUser() == false)//Si el usuario no se ha autenticado, Cerrar la aplicacion.
            {
                MessageBox.Show("Error Fatal, favor fazer o login.");
                Application.Exit();
            }
            //Opcional, muchas veces en las aplicaciones de escritorio no es necesario.
        }

        private void OpenChildForm<ChildForm>(object senderMenuButton) where ChildForm : Form, new()
        {
            Button menuButton = (Button)senderMenuButton;
            Form form = listChildForms.OfType<ChildForm>().FirstOrDefault();

            if (activeChildForm != null && form == activeChildForm)
                return;

            if (form == null)
            {

                form = new ChildForm();
                form.FormBorderStyle = FormBorderStyle.None;
                form.TopLevel = false;
                form.Dock = DockStyle.Fill; 
                listChildForms.Add(form);

                if (menuButton != null)
                {
                    ActivateButton(menuButton);
                    form.FormClosed += (s, e) =>
                    {
                        DeactivateButton(menuButton);
                    };
                }
                btnChildFormClose.Visible = true;
            }
            CleanDesktop();
            panelDesktop.Controls.Add(form);
            panelDesktop.Tag = form;
            form.Show();
            form.BringToFront();
            form.Focus();
            lblCaption.Text = form.Text;
            activeChildForm = form;
        }

        private void CloseChildForm()
        {

            if (activeChildForm != null)
            {
                listChildForms.Remove(activeChildForm);
                panelDesktop.Controls.Remove(activeChildForm);
                activeChildForm.Close();
                RefreshDesktop();
            }
        }
        private void CleanDesktop()
        {

            if (activeChildForm != null)
            {
                activeChildForm.Hide();
                panelDesktop.Controls.Remove(activeChildForm);
            }
        }

        private void RefreshDesktop()
        {

            var childForm = listChildForms.LastOrDefault();
            if (childForm != null)
            {
                activeChildForm = childForm;
                panelDesktop.Controls.Add(childForm);
                panelDesktop.Tag = childForm;
                childForm.Show();
                childForm.BringToFront();
                lblCaption.Text = childForm.Text;
            }
            else 
            {
                ResetDefaults();
            }
        }
        private void ResetDefaults()
        {
            activeChildForm = null;
            lblCaption.Text = "   Home";
            btnChildFormClose.Visible = false;
        }

        private void ActivateButton(Button menuButton)
        {
            menuButton.ForeColor = Color.FromArgb(0, 100, 182);
            //menuButton.BackColor = panelMenuHeader.BackColor;
        }
        private void DeactivateButton(Button menuButton)
        {
            menuButton.ForeColor = Color.DarkGray;
            //menuButton.BackColor = panelSideMenu.BackColor;
        }
        #endregion

        #region -> Metodos de evento
        private void Form1_Load(object sender, EventArgs e)
        {
            ResetDefaults();
            Security();
            ManagePermissions();
        }
        #region - Cerrar sesión, Cerrar aplicación, minimizar y maximizar.

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma logout?", "Mensagem",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.CloseApp();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            this.MaximizeRestore();
            if (this.WindowState == FormWindowState.Maximized)
                btnMaximize.Image = Properties.Resources.btnRestore;
            else btnMaximize.Image = Properties.Resources.btnMaximize;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.Minimize();
        }
        private void btnChildFormClose_Click(object sender, EventArgs e)
        {
            CloseChildForm();
        }
        #endregion

        #region - Convertir foto de perfil a circular

        private void pictureBoxPhoto_Paint(object sender, PaintEventArgs e)
        {
            using (GraphicsPath graphicsPath = new GraphicsPath())
            {
                var rectangle = new Rectangle(0, 0, pictureBoxPhoto.Width - 1, pictureBoxPhoto.Height - 1);
                graphicsPath.AddEllipse(rectangle);
                pictureBoxPhoto.Region = new Region(graphicsPath);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var pen = new Pen(new SolidBrush(pictureBoxPhoto.BackColor), 1);
                e.Graphics.DrawEllipse(pen, rectangle);
            }
        }
        #endregion

        #region - Contraer o Expandir menú lateral

        private void btnSideMenu_Click(object sender, EventArgs e)
        {
            if (panelSideMenu.Width > 100)
            {
                panelSideMenu.Width = 52;
                foreach (Control control in panelMenuHeader.Controls)
                {
                    if (control != btnSideMenu)
                        control.Visible = false;
                }
            }
            else
            {
                panelSideMenu.Width = 230;
                foreach (Control control in panelMenuHeader.Controls)
                {
                    control.Visible = true;
                }
            }
        }
        #endregion

        #region - Abrir formularios secundarios
        private void linkProfile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenChildForm<FormUserProfile>(null);
        }
        private void BtnDashboard_Click(object sender, EventArgs e)
        {

        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            // OK
            OpenChildForm<FormUsers>(sender);
        }

        private void BtnProducts_Click(object sender, EventArgs e)
        {
            // Ok
            OpenChildForm<FormProducts>(sender);
        }

        private void BtnClients_Click(object sender, EventArgs e)
        {
            // Ok
            OpenChildForm<FormClients>(sender);
        }

        private void BtnClientProdut_Click(object sender, EventArgs e)
        {
            // Ok
            OpenChildForm<FormClientProduct>(sender);
        }

        private void BtnEmailTemplate_Click(object sender, EventArgs e)
        {
            OpenChildForm<FormEmailTemplate>(sender);
        }

        private void BtnBillingRule_Click(object sender, EventArgs e)
        {
            OpenChildForm<FormBillingRule>(sender);
        }


        private void BtnScaleRule_Click(object sender, EventArgs e)
        {
            OpenChildForm<FormScaleRule>(sender);
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            OpenChildForm<FormImport>(sender);
        }

        private void BtnSendEmail_Click(object sender, EventArgs e)
        {
            OpenChildForm<FormSendEmail>(sender);
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            OpenChildForm<FormReport>(sender);
        }
        #endregion

        #endregion
    }
}