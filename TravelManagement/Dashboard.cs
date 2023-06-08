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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountVehicles();
            CountUsers();
            CountDrivers();
            CountBookings();
            CountCust();
            SumAmt();
            BestCust();
            BestDriver();
       }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sindh\OneDrive\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void CountVehicles()
        {
            Con.Open();
            string Query = "select count(*) from VehicleTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query,Con); 
            DataTable dt = new DataTable();
            sda.Fill(dt);
            VNumLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountUsers()
        {
            Con.Open();
            string Query = "select count(*) from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            UNumLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountDrivers()
        {
            Con.Open();
            string Query = "select count(*) from DriverTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DNumLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountBookings()
        {
            Con.Open();
            string Query = "select count(*) from BookingTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BookNumLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountCust()
        {
            Con.Open();
            string Query = "select count(*) from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CNumLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void SumAmt()
        {
            Con.Open();
            string Query = "select Sum(Amount) from BookingTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            IncNumLbl.Text = "Rs "+dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void BestCust()
        {
            Con.Open();
            string InnerQuery = "select Max(Amount) from BookingTbl";
            DataTable dt1=new DataTable();
            SqlDataAdapter sda1=new SqlDataAdapter(InnerQuery, Con);
            sda1.Fill(dt1);
            string Query = "select CustName from BookingTbl where Amount='"+ dt1.Rows[0][0].ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BestCustTb.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void BestDriver()
        {
            Con.Open();
            string Query = "select Driver,Count(*) from BookingTbl Group By Driver Order By Count(Driver) Desc";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BestDriverLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Bookings Obj = new Bookings();
            this.Hide();
            Obj.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Vehicles Obj = new Vehicles();
            this.Hide();
            Obj.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            this.Hide();
            Obj.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Drivers Obj = new Drivers();
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
