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
    public partial class AccountInfo : Form
    {
        public AccountInfo()
        {
            InitializeComponent();
        }

        private void ClearInfo()
        {
            txtNewPW.Text = "";
            txtPassword.Text = "";
        }

        private void SaveButton_Click(object sender, EventArgs e)
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
                string strDeleteAccount = "UPDATE ACCOUNT SET pwd = " + txtNewPW.Text + " WHERE usn='@Username'";
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            ClearInfo();
        }

        private void DELETETHIS_Click(object sender, EventArgs e)
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
    }
}
