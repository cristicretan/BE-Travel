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

namespace devi
{
    public partial class Register : Form
    {
        SqlConnection connection = new SqlConnection(Connection.connection_string);
        public Register()
        {
            InitializeComponent();
        }

        string _Nume;
        string _Prenume;
        string _Email;
        string _Parola;
        string _City;
        string _State;

        public void data_init()
        {
            _Nume = textBox1.Text;
            _Prenume = textBox2.Text;
            _Email = textBox3.Text;
            _Parola = textBox4.Text;
            _City = textBox6.Text;
            _State = textBox7.Text;

            SqlCommand cmd;
            string query = "INSERT INTO Consultanti (Nume,Prenume,Email,Parola,City,State) VALUES(@Nume,@Prenume,@Email,@Parola,@City, @State)";
            cmd = new SqlCommand(query, connection);

            if (string.IsNullOrEmpty(_Nume) || string.IsNullOrEmpty(_Prenume) || string.IsNullOrEmpty(_Email) || string.IsNullOrEmpty(_Parola) || string.IsNullOrEmpty(_City) || string.IsNullOrEmpty(_State))
            {
                MessageBox.Show("You have to complete all fields ");
            }
            else if (_Parola != textBox5.Text)
            {
                MessageBox.Show("Passwords doesnt match! ");
            }
            else if (!_Email.Contains("@"))
            {
                MessageBox.Show("You have to insert a valid email format! ");
            }

            else
            {
                cmd.Parameters.AddWithValue("@Nume", _Nume);
                cmd.Parameters.AddWithValue("@Prenume", _Prenume);
                cmd.Parameters.AddWithValue("@Email", _Email);
                cmd.Parameters.AddWithValue("@Parola", _Parola);
                cmd.Parameters.AddWithValue("@City", _City);
                cmd.Parameters.AddWithValue("@State", _State);

                try
                {
                    connection.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Account succesfully created!");
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data_init();
            Form1 f = new devi.Form1();
            f.Show();
            this.Hide();
        }
    }
 }

