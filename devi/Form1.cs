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

namespace devi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        SqlConnection connection = new SqlConnection(Connection.connection_string);

        private void connection_init()
        {
            // SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();

            if (connection.State.Equals(ConnectionState.Open))
            {
                MessageBox.Show("Connection is valid and open!");
            }
            else
            {
                MessageBox.Show("Connection error");
            }
            connection.Close();
        }

        private void reader_init()
        {
            string query = "SELECT * FROM Consultanti";

            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("You have to complete user and password fields");
            }
            else if (!textBox1.Text.Contains("@"))
            {
                MessageBox.Show("Insert a valid email format");
            }
            else
            {

                string username = null;
                string pass = null;

                while (reader.Read())
                {
                    username = reader.GetString(3);
                    pass = reader.GetString(4);
                }

                if(username.Equals(textBox1.Text) && pass.Equals(textBox2.Text))
                {
                    Meniu f = new Meniu();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Try again");
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form Register = new Register();
            Register.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reader_init();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //connection_init();
        }
    }
}
