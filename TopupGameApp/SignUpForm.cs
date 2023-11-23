using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopupGameApp
{
    public partial class SignupForm : Form
    {
        public bool isSignUp = false;
        public bool State = false;
        public SignupForm(bool isSignUp)
        {
            this.isSignUp = isSignUp;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn đóng form không?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            if (isSignUp == true)
            {
                SignUp();
            }
            else
            {
                LogIn();
            }
        }

        private void LogIn()
        {
            string strConnectString = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"]
                                           .ToString();

            string strCommand = "dbLogin";

            SqlConnection myConnection = new SqlConnection(strConnectString);
            myConnection.Open();

            SqlCommand myCommand = new SqlCommand(strCommand, myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("@Username", txtName.Text);
            myCommand.Parameters.AddWithValue("@Password", txtPassword.Text);
            myCommand.Parameters.Add(new SqlParameter("@Check", SqlDbType.VarChar, 10));
            myCommand.Parameters["@Check"].Direction = ParameterDirection.Output;
            myCommand.ExecuteNonQuery();
            if (Convert.ToBoolean(myCommand.Parameters["@Check"].Value.ToString()))
            {
                MessageBox.Show("Dang nhap thanh cong");
                UserAccount.setCurrentUsername(txtName.Text);
                UserAccount.setCurrentPassword(txtPassword.Text);
                this.Hide();
                MainWeb mainWeb = new MainWeb(true);
                mainWeb.ShowDialog();
            }
            else
            {
                MessageBox.Show("Dang nhap that bai, vui long kiem tra lai username va password");

            };
            myConnection.Close();
            this.Close();
        }

        private void SignUp()
        {
            string strConnectString = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"]
                               .ToString();
            string strCommand = "Insert into ACCOUNT (usn, pwd) values (" +
                " '" + txtName.Text + "'," +
                " '" + txtPassword.Text + "')";

            SqlConnection myConnection = new SqlConnection(strConnectString);
            myConnection.Open();
            SqlCommand myCommand = new SqlCommand(strCommand, myConnection);
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            MessageBox.Show("Dang ki tai khoan thanh cong");

            this.Close();
        }

        private void SignupForm_Load(object sender, EventArgs e)
        {
            if (isSignUp == false)
            {
                SignUplb.Text = "LOG IN";
                btnSignup.Text = "Log In";
                Text = "Log In";
            }

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void Emaillb_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
