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
using System.Xml.Linq;

namespace TopupGameApp
{
    public partial class AccInfo : Form
    {
        public AccInfo()
        {
            InitializeComponent();
        }

        private void ClearInfo()
        {
            txtNewPassword.Text = "";
            txtPassword.Text = "";
        }

        private void btnDelAcc_Click(object sender, EventArgs e)
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
                //code to delete account
                string strDeleteAccount = "DELETE FROM ACCOUNT WHERE usn='@Username'";
                myCommand.Parameters.AddWithValue("@Username", txtName.Text);
                myCommand.ExecuteNonQuery();
                ClearInfo();
                //logout? fuck sql aaaaaaaa
                MessageBox.Show("Xoa tai khoan thanh cong. Dang xuat khoi trang chu.");

            }
            else
            {
                MessageBox.Show("Vui long kiem tra lai mat khau.");

            };
            myConnection.Close();

            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
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
                //code to change password, pls check whether this blows up your kitchen or not
                string strDeleteAccount = "UPDATE ACCOUNT SET pwd = " + txtNewPassword.Text + " WHERE usn='@Username'";
                myCommand.Parameters.AddWithValue("@Username", txtName.Text);
                myCommand.ExecuteNonQuery();
                //txtNewPW.Text
                ClearInfo();
                MessageBox.Show("Thay doi mat khau thanh cong");
            }
            else
            {
                MessageBox.Show("Thay doi mat khau that bai. Vui long kiem tra lai mat khau cu");

            };
            myConnection.Close();

            this.Close();
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

        private void AccInfo_Load(object sender, EventArgs e)
        {
            txtName.Text = UserAccount.CurrentUsername;
        }
    }
}
