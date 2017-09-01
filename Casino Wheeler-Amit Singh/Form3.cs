using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string pass = textBox2.Text;
            string pass1 = textBox3.Text;
            if(pass==pass1)
            {
                try
                {
                    string myConnection = "datasource=localhost;port=3306;username=root;password=amit;database=gamers;";
                    MySqlConnection myConn = new MySqlConnection(myConnection);
                    myConn.Open();
                    MySqlCommand SelectCommand = new MySqlCommand("insert into gamers.players(username,pass,score) values('"+user+"','"+pass1+"',0);", myConn);
                    if (SelectCommand.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Account Created\nProceed with the Login");
                        this.Hide();
                        Form1 o = new Form1();
                        o.Show();
                    }
                    else
                    {
                        MessageBox.Show("Account Not Created\nTry again Later");
                    }
                    myConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }   
            }
            else
            {
                MessageBox.Show("Password not Matched");
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
