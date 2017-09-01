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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pass;
            string user;
            user =textBox1.Text;
            pass = textBox2.Text;
            try
            {
                string myConnection = "datasource=localhost;port=3306;username=root;password=amit;database=gamers;";
                MySqlConnection myConn = new MySqlConnection(myConnection);
                MySqlCommand SelectCommand = new MySqlCommand(" select * from  gamers.players where username='" + user + "' and pass='"+ pass +"';", myConn);
                MySqlDataReader myReader;
                myConn.Open();
                myReader = SelectCommand.ExecuteReader();
                int count = 0;
                while(myReader.Read())
                {
                    count = count + 1;
                }
                myConn.Close();
                myReader.Close();
                if (count == 1)
                {
                    MessageBox.Show("Welcome : "+user+"\nWelcome to Wheeler game!");
                    this.Hide();
                    Form2 obj2 = new Form2(user);
                    obj2.Show();
                }
                else if(count>1)
                {
                    MessageBox.Show("Login Denied - Contact Administrator!");
                }
                else
                {
                    MessageBox.Show("Login Failed - Incorrect Password or Username");
                    myConn.Close();
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 sign = new Form3();
            sign.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
