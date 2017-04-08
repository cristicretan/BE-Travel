using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GoogleMaps.LocationServices;
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
    public partial class Meniu : Form
    {
        public Meniu()
        {
            InitializeComponent();
        }

        List<double> lats = new List<double>();
        List<double> longs = new List<double>();

        Consultanti con = new Consultanti();

        SqlConnection connection = new SqlConnection(Connection.connection_string);

        private void button1_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            string position = textBox1.Text + ", " + textBox3.Text;
            gMapControl1.SetPositionByKeywords(position);

            var address = position;

            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);

            var latitude = point.Latitude;
            var longitude = point.Longitude;

            gMapControl1.ShowCenter = false;

            string query = "SELECT * FROM Consultanti";

            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            string nume = null;
            string city = null;
            string prenume = null;
            string state = null;
            string email = null;
            string parola = null;
            int id = 2;

            while (reader.Read())
            {
                id = reader.GetInt32(0);
                city = reader.GetString(5);
                nume = reader.GetString(1);
                prenume = reader.GetString(2);
                email = reader.GetString(3);
                parola = reader.GetString(4);
                state = reader.GetString(6);
            }

            connection.Close();

            /// <summary>
            /// I tried.
            /// </summary>
            /*
            connection.Open();

            string add = "INSERT INTO Consultanti (Id_consultanti, Nume,Prenume,Email,Parola,City,State) VALUES(@Id_consultanti, @Nume,@Prenume,@Email,@Parola,@City, @State)";
            cmd = new SqlCommand(add, connection);

            cmd.Parameters.AddWithValue("@Id_consultanti", id);
            cmd.Parameters.AddWithValue("@Nume", nume);
            cmd.Parameters.AddWithValue("@Prenume", prenume);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Parola", parola);
            cmd.Parameters.AddWithValue("@City", city);
            cmd.Parameters.AddWithValue("@State", state);

            connection.Close();
            */

            gMapControl1.ShowCenter = false;


            lats.Add(latitude);
            longs.Add(longitude);

            if (city.Equals("Timisoara"))
            {
                GMapOverlay markers = new GMapOverlay("markers");
                GMapMarker marker = new GMarkerGoogle(
                    new PointLatLng(latitude, longitude),
                    GMarkerGoogleType.blue_pushpin);
                markers.Markers.Add(marker);
                gMapControl1.Overlays.Add(markers);
            }
            else if(city.Equals("Bucuresti"))
            {
                GMapOverlay markers = new GMapOverlay("markers");
                GMapMarker marker = new GMarkerGoogle(
                    new PointLatLng(latitude, longitude),
                    GMarkerGoogleType.red_pushpin);
                markers.Markers.Add(marker);
                gMapControl1.Overlays.Add(markers);
            }


               
        }

        int id;

        private void button2_Click(object sender, EventArgs e)
        {
            

            string _Nume = textBox2.Text;
            string _Prenume = textBox4.Text;
            string _Email = textBox5.Text;
            string _Parola = textBox6.Text;
            string _City = textBox7.Text;
            string _State = textBox8.Text;

           

            string add = "UPDATE DATABASE.EDATA SET Consultanti (Nume,Prenume,Email,Parola,City,State) VALUES(@Nume,@Prenume,@Email,@Parola,@City, @State)";
            SqlCommand cmd = new SqlCommand(add, connection);

            cmd.Parameters.AddWithValue("@Nume", _Nume);
            cmd.Parameters.AddWithValue("@Prenume", _Prenume);
            cmd.Parameters.AddWithValue("@Email", _Email);
            cmd.Parameters.AddWithValue("@Parola", _Parola);
            cmd.Parameters.AddWithValue("@City", _City);
            cmd.Parameters.AddWithValue("@State", _State);
            connection.Close();

            MessageBox.Show("Data has been successfully saved!");
        }

        private void Meniu_Load(object sender, EventArgs e)
        {
            connection.Open();
            string query = "SELECT * FROM Consultanti";

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();

            string nume = null;
            string city = null;
            string prenume = null;
            string state = null;
            string email = null;
            string parola = null;
            id = 2;

            while (reader.Read())
            {
                id = reader.GetInt32(0);
                city = reader.GetString(5);
                nume = reader.GetString(1);
                prenume = reader.GetString(2);
                email = reader.GetString(3);
                parola = reader.GetString(4);
                state = reader.GetString(6);
            }

            connection.Close();

            textBox2.Text = nume;
            textBox4.Text = prenume;
            textBox6.Text = parola;
            textBox5.Text = email;
            textBox7.Text = city;
            textBox8.Text = state;
        }
    }
}
