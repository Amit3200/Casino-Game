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
    public partial class Form2 : Form
    {
       public static int xp = 0;
       public static int total = 1000;
        public static int count1 = 0;
        string player;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string user)
        {
            InitializeComponent();
            player = user;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label8.Text = player;
            label12.Text = "1000";
            try
            {
                string myConnection = "datasource=localhost;port=3306;username=root;password=amit;database=gamers;";
                MySqlConnection myConn = new MySqlConnection(myConnection);
                myConn.Open();
                MySqlCommand SelectCommand = new MySqlCommand(" select username from  gamers.players where username!='" + player + "';", myConn);
                MySqlDataReader myReader;
                myReader = SelectCommand.ExecuteReader();
                while (myReader.Read())
                {
                    label14.Text +="\n"+ myReader.GetString(0);
                    panel1.Visible = true;
                }
                myConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string bet = textBox1.Text;
            if (bet == "")
            {
                MessageBox.Show("Please Fill Amount");
            }
            if (bet != "")
            {
                int money = Convert.ToInt32(bet);
                int number = Convert.ToInt32(textBox2.Text);
                if (number >= 14 || number <= 0)
                {
                    MessageBox.Show("Choose any number between the range of 1 to 13");
                }
                if (total >= money)
                {
                    if (money >= 50)
                    {

                        label15.Text = "";
                        if (xp >= 25 && xp < 50)
                        {
                            total = total + 100;
                            label15.Text = "You earned $100 for your experience";
                            xp = 10;
                            label9.Text = xp.ToString();
                        }
                        if (xp >= 50 && xp < 100)
                        {
                            total = total + (money * 5) / 10 + 200;
                            label15.Text = "You earned $" + ((money * 5) / 10 + 200) + " for your experience";
                            xp = 5;
                            label12.Text = total.ToString();
                            label9.Text = xp.ToString();
                        }
                        if (xp >= 100)
                        {
                            total = total + (money * 5) / 10 + 500;
                            label15.Text = "You earned $" + ((money * 5) / 10 + 500) + " for your experience";
                            xp = 10;
                            label9.Text = xp.ToString();
                            label12.Text = total.ToString();
                        }
                        label12.Text = total.ToString();
                        if (money == 100 && money < 500)
                        {
                            xp += 5;
                            label9.Text = xp.ToString();

                        }
                        else if (money >= 500)
                        {
                            xp += 25;
                            label9.Text = xp.ToString();
                        }
                        else if (money <= 0)
                        {
                            xp = 0;
                            MessageBox.Show("Invalid Amount Entered");
                        }
                        Random rnd = new Random();
                        int winner = rnd.Next(1, 13);
                        label13.Text = (winner + 13).ToString();
                        if (number == winner)
                        {
                            xp += 100;
                            label9.Text = xp.ToString();
                            int garbage = (money * 50) / 10 - money;
                            total = total + (money * 50) / 10 - money;
                            label10.Text = "You Won $" + garbage + "\nYour Guess Was Correct";
                            label12.Text = total.ToString();
                        }
                        else
                        {
                            xp -= 2;
                            if (xp < 0)
                            {
                                xp = 0;
                                count1 = count1 + 1;
                                if (count1 == 12)
                                {
                                    MessageBox.Show("Your xp went low 12 times than required");
                                    MessageBox.Show("You have been kicked out of the game");
                                    this.Hide();
                                    Form1 r = new Form1();
                                    r.Show();
                                }
                            }
                            label9.Text = xp.ToString();
                            int loss = money + (money * 5) / 10;
                            label10.Text = "You Lost this turn and your $" + loss;
                            total -= (money + (money * 5) / 10);
                            label12.Text = total.ToString();

                            if (total < 0)
                            {
                                MessageBox.Show("You are under Debt now\n You have to pay $" + (-total));
                                MessageBox.Show("You have been locked under casino because you went bankrupt");
                                this.Hide();
                                Form1 r = new Form1();
                                r.Show();
                            }
                            else
                                label12.Text = total.ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Minimum Amount is $50 Try Again Please");
                    }
                }
                else if (total == 0)
                {
                    MessageBox.Show("You Have Been kicked out of Casino\nYour Balance is $0");
                    label10.Text = "Balance $0 You can't bet any further";
                    button1.Enabled = false;
                    button2.Enabled = false;
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("You can't bet more than your amount");
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (total <= 1000)
            {
                MessageBox.Show("You Have WithDrawn With $" + total);
                label10.Text = "You have quit the game with $" + total;
            }
            else if(total>1000)
            {
                MessageBox.Show("Congratulations!,You Have WithDrawn With $" + total);
                label10.Text = "Congratulations! You have quit the game with $" + total;
            }
            button2.Enabled = false;
            button1.Enabled = false;
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logged Out Successfully!");
            this.Hide();
            Form1 obj1 = new Form1();
            obj1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
