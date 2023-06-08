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

namespace TravelManagement
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            ShowCustomers();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sindh\OneDrive\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Clear()
        {
            CustNameTb.Text = "";
            CustGenCb.SelectedIndex = -1;
            CustPhoneTb.Text = "";
            CustAddTb.Text = "";
        }
        private void ShowCustomers()
        {
            Con.Open();
            string Query = "select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustGenCb.SelectedIndex == -1 || CustPhoneTb.Text == "" ||   CustAddTb.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl (CustName,CustAdd,CustPhone,CustGen) values(@CN,@CA,@CP,@CG) ", Con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CG", CustGenCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Recorded");
                    Con.Close();
                    ShowCustomers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int Key = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Customer");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from CustomerTbl where CustId=@CustKey", Con);
                    cmd.Parameters.AddWithValue("@CustKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted");
                    Con.Close();
                    ShowCustomers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
        string Customer;
        private void CountBookingByCust()
        {
            Con.Open();
            string Query = "select count(*) from BookingTbl where CustName='" + Customer + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BNumLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void SumAmt()
        {
            Con.Open();
            string Query = "select Sum(Amount) from BookingTbl where CustName='" + Customer + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            TotAmtLbl.Text = "Rs "+dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustNameTb.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            Customer = CustNameTb.Text;
            CustAddTb.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            CustPhoneTb.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
            CustGenCb.SelectedItem = CustomerDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (CustNameTb.Text == "")
            {
                Key = 0;
            }
            else {
                Key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
                CountBookingByCust();
                SumAmt();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustGenCb.SelectedIndex == -1 || CustPhoneTb.Text == "" || CustAddTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update CustomerTbl set CustName=@CN,CustAdd=@CA,CustPhone=@CP,CustGen=@CG where CustId=@Ckey ", Con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CG", CustGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated");
                    Con.Close();
                    ShowCustomers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Drivers obj = new Drivers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Vehicles obj = new Vehicles();
            obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Bookings obj = new Bookings();
            obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            this.Hide();
            Obj.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            this.Hide();
            Obj.Show();
        }
    }
}
