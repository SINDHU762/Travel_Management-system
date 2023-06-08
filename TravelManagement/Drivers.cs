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
    public partial class Drivers : Form
    {
        public Drivers()
        {
            InitializeComponent();
            //GetCars();
            Showdrivers();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Vehicles obj = new Vehicles();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Bookings obj = new Bookings();
            obj.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void Drivers_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Clear()
        {
            DrNameTb.Text = "";
            GenCb.SelectedIndex = -1;
            PhoneTb.Text = "";
            DrAddTb.Text = "";
        }
        private void Showdrivers()
        {
            Con.Open();
            string Query = "select * from DriverTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DriverDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (DrNameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "" || DrAddTb.Text == "" || RatingCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into DriverTbl (DrName,DrPhone,DrAdd,DrDOB,DrJoinDate,DrGen,DrRating) values(@DRN,@DrP,@DrA,@DrD,@DrJ,@DrG,@DrR) ", Con);
                    cmd.Parameters.AddWithValue("@DRN", DrNameTb.Text);
                    cmd.Parameters.AddWithValue("@DRP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@DRA", DrAddTb.Text);
                    cmd.Parameters.AddWithValue("@DRD", DOB.Value.ToString());
                    cmd.Parameters.AddWithValue("@DRJ", JoinDate.Value.ToString());
                    cmd.Parameters.AddWithValue("@DRG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DRR", RatingCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Driver Recorded");
                    Con.Close();
                    Showdrivers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sindh\OneDrive\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");
        /*private void GetCars()
        { 
        Con.Open();
            SqlCommand cmd = new SqlCommand("Select * from VehicleTbl",Con);
            SqlDataReader rdr;
            rdr =cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("VLp",typeof(string));
            dt.Load(rdr);
            VehicleCb.ValueMember = "VLp";
            VehicleCb.DataSource = dt;
        Con.Close();
        }*/
        private void button2_Click(object sender, EventArgs e)
        {
            if (DrNameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "" || DrAddTb.Text == "" || RatingCb.SelectedIndex == -1)
            {
                MessageBox.Show("Select Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update DriverTbl set DrName=@DRN,DrPhone=@DrP,DrAdd=@DrA,DrDOB=@DrD,DrJoinDate=@DrJ,DrGen=@DrG,DrRating=@DrR where DrId=@DrKey ", Con);
                    cmd.Parameters.AddWithValue("@DRN", DrNameTb.Text);
                    cmd.Parameters.AddWithValue("@DRP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@DRA", DrAddTb.Text);
                    cmd.Parameters.AddWithValue("@DRD", DOB.Value.ToString());
                    cmd.Parameters.AddWithValue("@DRJ", JoinDate.Value.ToString());
                    cmd.Parameters.AddWithValue("@DRG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DRR", RatingCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DrKey",Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Driver Updated");
                    Con.Close();
                    Showdrivers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
        string Driver;
        private void CountBookingByDriver()
        {
            Con.Open();
            string Query = "select count(*) from BookingTbl where Driver='" +Driver + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CountBookingsLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        /*private void SumAmt()
        {
            Con.Open();
            string Query = "select Sum(Amount) from BookingTbl where CustName='" + Customer + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            TotAmtLbl.Text = "Rs " + dt.Rows[0][0].ToString();
            Con.Close();
        }*/
        int Rate;
        private void GetStars()
        {
            Rate = Convert.ToInt32(DriverDGV.SelectedRows[0].Cells[7].Value.ToString());
            RateLbl.Text = "" + Rate;
            if (Rate == 1 || Rate == 2)
            {
                LevelLbl.Text = "OK";
                LevelLbl.ForeColor = Color.Red;
            }
            else if (Rate == 3 || Rate == 4)
            {
                LevelLbl.Text = "GOOD";
                LevelLbl.ForeColor = Color.DodgerBlue;
            }
            else {
                LevelLbl.Text = "Excellent";
                LevelLbl.ForeColor = Color.Green;
            }
        }
        int Key = 0;
        private void DriverDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DrNameTb.Text = DriverDGV.SelectedRows[0].Cells[1].Value.ToString();
            Driver = DrNameTb.Text;
            PhoneTb.Text = DriverDGV.SelectedRows[0].Cells[2].Value.ToString();
            DrAddTb.Text = DriverDGV.SelectedRows[0].Cells[3].Value.ToString();
            DOB.Text = DriverDGV.SelectedRows[0].Cells[4].Value.ToString();
            JoinDate.Text = DriverDGV.SelectedRows[0].Cells[5].Value.ToString();
            GenCb.Text = DriverDGV.SelectedRows[0].Cells[6].Value.ToString();
            RatingCb.Text = DriverDGV.SelectedRows[0].Cells[7].Value.ToString();
            if (DrNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(DriverDGV.SelectedRows[0].Cells[0].Value.ToString());
                CountBookingByDriver();
                GetStars();
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Driver");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from DriverTbl where DrId=@DrKey", Con);
                    cmd.Parameters.AddWithValue("@DrKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Driver Deleted");
                    Con.Close();
                    Showdrivers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            this.Hide();
            Obj.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            this.Hide();
            Obj.Show();
        }

        private void DrNameTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
