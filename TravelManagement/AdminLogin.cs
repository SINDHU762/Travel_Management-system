using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelManagement
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            this.Hide();
            Obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (PasswordTb.Text == "Password")
            {
                Users Obj = new Users();
                Obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Admin Password");
            }
        }
    }
}
