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
    public partial class Bookings : Form
    {
        public Bookings()
        {
            InitializeComponent();
            GetCustomers();
            ShowBookings();
            GetCars();
            UnameLbl.Text = Login.User;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sindh\OneDrive\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void GetCustomers()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select * from CustomerTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustName", typeof(string));
            dt.Load(rdr);
            CustCb.ValueMember = "CustName";
            CustCb.DataSource = dt;
            Con.Close();
        }
        private void GetDriver()
        {
            Con.Open();
            string Query = "select * from VehicleTbl where VLP='"+VehicleCb.SelectedValue.ToString()+"'";
            SqlCommand cmd=new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda= new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                DriverTb.Text = dr["Driver"].ToString();
            }
            Con.Close() ;
        }
        private void GetCars()
        {
            string IsBooked = "No";
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select * from VehicleTbl where Booked='"+IsBooked+"'", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("VLP", typeof(string));
            dt.Load(rdr);
            VehicleCb.ValueMember = "VLP";
            VehicleCb.DataSource = dt;
            Con.Close();
        }
        private void Clear()
        {
            CustCb.SelectedIndex = -1;
            VehicleCb.SelectedIndex = -1;
            DriverTb.Text = "";
            AmountTb.Text = "";
        }
        private void ShowBookings()
        {
            Con.Open();
            string Query = "select * from BookingTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookingsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void UpdateVehicle()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update VehicleTbl set Booked=@VB where VLp=@Vp ", Con);
                cmd.Parameters.AddWithValue("@VP", VehicleCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@VB", "Yes");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Vehicle Updated");
                Con.Close();
                Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CustCb.SelectedIndex == -1 || VehicleCb.SelectedIndex == -1 || DriverTb.Text == "" || AmountTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BookingTbl(CustName,Vehicle,Driver,PickupDate,DropoffDate,Amount,BUser) values(@CN,@Veh,@Dri,@PDate,@DDate,@Am,@UN) ", Con);
                    cmd.Parameters.AddWithValue("@CN", CustCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Veh", VehicleCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Dri", DriverTb.Text);
                    cmd.Parameters.AddWithValue("@PDate", PickUpDate.Value.Date);
                    cmd.Parameters.AddWithValue("@DDate", RetDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Am", AmountTb.Text);
                    cmd.Parameters.AddWithValue("@UN", UnameLbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Booked");
                    Con.Close();
                    ShowBookings();
                    UpdateVehicle();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void VehicleCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDriver();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Vehicles obj = new Vehicles();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Drivers obj = new Drivers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BookingsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}
