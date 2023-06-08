using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace TravelManagement
{
    public partial class Vehicles : Form
    {
        public Vehicles()
        {
            InitializeComponent();
            ShowVehicles();
            GetDrivers();  
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sindh\OneDrive\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Clear() {
            LPlateTb.Text = "";
            MarkCb.SelectedIndex = -1;
            ModelTb.Text = "";
            VYearCb.SelectedIndex = -1;
            EngTypeCb.SelectedIndex = -1;
            ColorTb.Text = "";
            MilleageTb.Text = "";
            TypeCb.SelectedIndex = -1; 
            BookedCb.SelectedIndex = -1;
        }
        private void ShowVehicles()
        {
            Con.Open();
            string Query = "select * from VehicleTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder= new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            VehicleDGV.DataSource= ds.Tables[0];
            Con.Close();   
        }
        private void GetDrivers()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select * from DriverTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DrName", typeof(string));
            dt.Load(rdr);
            DriverCb.ValueMember = "DrName";
            DriverCb.DataSource = dt;
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "" || MarkCb.SelectedIndex == -1 || ModelTb.Text == "" || VYearCb.SelectedIndex == -1 || EngTypeCb.SelectedIndex == -1 || ColorTb.Text == "" || MilleageTb.Text == "" || TypeCb.SelectedIndex == -1 || BookedCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else 
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into VehicleTbl (VLp,Vmark,Vmodel,VYear,VEngType,VColor,VMilleage,VType,Booked,Driver) values(@VP,@Vma,@Vmo,@VY,@VEng,@VCo,@VMi,@VTy,@VB,@DR) ", Con);
                    cmd.Parameters.AddWithValue("@VP",LPlateTb.Text);
                    cmd.Parameters.AddWithValue("@Vma",MarkCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Vmo",ModelTb.Text);
                    cmd.Parameters.AddWithValue("@VY", VYearCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VEng", EngTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VCo", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@VMi", MilleageTb.Text);
                    cmd.Parameters.AddWithValue("@VTY", TypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VB", BookedCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Dr", DriverCb.SelectedValue.ToString());
                    cmd.ExecuteNonQuery();  
                    MessageBox.Show("Vehicle Recorded");
                    Con.Close();
                    ShowVehicles();
                    Clear();
                }
                catch (Exception Ex) {
                    MessageBox.Show(Ex.Message);
                }
                
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "")
            {
                MessageBox.Show("Select a Vehicle");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from VehicleTbl where VLP=@VPlate", Con);
                    cmd.Parameters.AddWithValue("@VPlate", LPlateTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Deleted");
                    Con.Close();
                    ShowVehicles();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
        string V;
        private void CountBookingByVehicle()
        {
            Con.Open();
            string Query = "select count(*) from BookingTbl where Vehicle='" + V + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            VNumLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void VehicleDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LPlateTb.Text = VehicleDGV.SelectedRows[0].Cells[0].Value.ToString();
            V=LPlateTb.Text;
            CountBookingByVehicle();
            MarkCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[1].Value.ToString(); 
            ModelTb.Text = VehicleDGV.SelectedRows[0].Cells[2].Value.ToString();
            VYearCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[3].Value.ToString();
            EngTypeCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[4].Value.ToString();
            ColorTb.Text = VehicleDGV.SelectedRows[0].Cells[5].Value.ToString();
            MilleageTb.Text= VehicleDGV.SelectedRows[0].Cells[6].Value.ToString();
            TypeCb.SelectedItem= VehicleDGV.SelectedRows[0].Cells[7].Value.ToString();
            BookedCb.SelectedItem= VehicleDGV.SelectedRows[0].Cells[8].Value.ToString();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "" || MarkCb.SelectedIndex == -1 || ModelTb.Text == "" || VYearCb.SelectedIndex == -1 || EngTypeCb.SelectedIndex == -1 || ColorTb.Text == "" || MilleageTb.Text == "" || TypeCb.SelectedIndex == -1 || BookedCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update VehicleTbl set Vmark=@Vma,Vmodel=@Vmo,VYear=@VY,VEngType=@VEng,VColor=@VCo,VMilleage=@VMi,VType=@VTy,Booked=@VB,Driver=@Dr where VLp=@Vp ", Con);
                    cmd.Parameters.AddWithValue("@VP", LPlateTb.Text);
                    cmd.Parameters.AddWithValue("@Vma", MarkCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Vmo", ModelTb.Text);
                    cmd.Parameters.AddWithValue("@VY", VYearCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VEng", EngTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VCo", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@VMi", MilleageTb.Text);
                    cmd.Parameters.AddWithValue("@VTY", TypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VB", BookedCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Dr", DriverCb.SelectedValue.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Updated");
                    Con.Close();
                    ShowVehicles();
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
            Customers Obj = new Customers();
            this.Hide();
            Obj.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Bookings Obj = new Bookings();
            this.Hide();
            Obj.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            this.Hide();
            Obj.Show();
        }
    }
}
