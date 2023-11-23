using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace TopupGameApp
{
    public partial class MainWeb : Form
    {
        public bool State = false; 
        public MainWeb(bool state)
        {
            this.State = state;
            InitializeComponent();
        }



        private void btnLogin_Click(object sender, EventArgs e)
        {
            SignupForm signupForm = new SignupForm(false);
            signupForm.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strConnectString = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"]
                    .ToString();

                string strSearch = "'%" + this.SearchBox.Text + "%'";
                string strCommand = "Select * from PRODUCT where ProdName like " + strSearch;
                SqlConnection myConnection = new SqlConnection(strConnectString);
                myConnection.Open();


                SqlCommand myCommand = new SqlCommand(strCommand, myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);

                DataSet ds = new DataSet();
                da.Fill(ds, "Product");

                this.dgvProduct.DataSource = ds;
                this.dgvProduct.DataMember = "Product";

                myConnection.Close();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void WebName_Click(object sender, EventArgs e)
        {

        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            SignupForm signupForm = new SignupForm(true);
            signupForm.ShowDialog();
        }

        private void btnMainPage_Click(object sender, EventArgs e)
        {

        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void MainWeb_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'storeDBDataSet1.PRODUCT' table. You can move, or remove it, as needed.
            this.pRODUCTTableAdapter1.Fill(this.storeDBDataSet1.PRODUCT);
            if (State)
            {
                btnSignOut.Visible = true;
                btnCart.Visible = true;
                btnLogin.Visible = false;
                btnSignup.Visible = false;
                lbUserInfo.Visible = true;
                lbUserInfo.Text = "Welcome, " + UserAccount.CurrentUsername;
            }

        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn đăng xuất không?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void lbUserInfo_Click(object sender, EventArgs e)
        {
            AccInfo accInfo = new AccInfo();
            accInfo.ShowDialog();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string strConnectString = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"]
                    .ToString();
            string SQLSTRING2 = "Select PRODUCT.ProdName, PRODUCT.gems, PRODUCT.credits, PRODUCT.ProdPrice  From PRODUCT inner join CART_ITEMS on CART_ITEMS.ProdID = PRODUCT.ProdID inner join CART on CART.CartID = CART_ITEMS.CartID inner join ACCOUNT on ACCOUNT.usn = " + "'" + UserAccount.CurrentUsername + "'";
            SqlConnection myConnection = new SqlConnection(strConnectString);
            myConnection.Open();

            SqlCommand myCommand = new SqlCommand(SQLSTRING2, myConnection);
            SqlDataAdapter da = new SqlDataAdapter(myCommand);

            DataSet ds = new DataSet();
            da.Fill(ds, "Product");

            this.dgvProduct.DataSource = ds;
            this.dgvProduct.DataMember = "Product"; 
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            string strConnectString = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"]
                    .ToString();
            string SQLSTRING2 = "Select PRODUCT.ProdName, PRODUCT.credits, PRODUCT.ProdPrice  From PRODUCT inner join CART_ITEMS on CART_ITEMS.ProdID = PRODUCT.ProdID inner join CART on CART.CartID = CART_ITEMS.CartID inner join ACCOUNT on ACCOUNT.usn = " +  "'" + UserAccount.CurrentUsername + "'";
            SqlConnection myConnection = new SqlConnection(strConnectString);
            myConnection.Open();

            SqlCommand myCommand = new SqlCommand(SQLSTRING2, myConnection);
            SqlDataAdapter da = new SqlDataAdapter(myCommand);

            DataSet ds = new DataSet();
            da.Fill(ds, "Product");

            this.dgvProduct.DataSource = ds;
            this.dgvProduct.DataMember = "Product";

        }
    }
}
