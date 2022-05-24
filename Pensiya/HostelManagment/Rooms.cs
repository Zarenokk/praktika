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

namespace HostelManagment
{
    public partial class Rooms : Form
    {
        public Rooms()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\cardi\Documents\HostelMgmt.mdf;Integrated Security=True;Connect Timeout=30");
        void FillRoomDGV()
        {
            Con.Open();
            string myquery = "Select * from Application_tbl";
            SqlDataAdapter da = new SqlDataAdapter(myquery, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RoomDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            
            if (RoomNumtb.Text == "")
            {
                MessageBox.Show("Введіть номер кімнати");
            }
            else
            {
                Con.Open();
                String query = "delete from Application_tbl where NameA='"+RoomNumtb.Text+"' ";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Кімната успішно видалена");
                Con.Close();
                FillRoomDGV();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (RoomNumtb.Text == "")
            {
                MessageBox.Show("Введіть номер кімнати");
            }
            else
            {
                

                Con.Open();
                String query = "update Application_tbl set StatusA = '" + RoomStatusCb.SelectedItem.ToString()+"' where NameA = '"+RoomNumtb.Text+"' ";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Кімната успішно оновлена");
                Con.Close();
                FillRoomDGV();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 Home = new Form1();
            Home.Show();
            this.Hide();

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(RoomNumtb.Text == "")
            {
                MessageBox.Show("Введіть номер кімнати");
            }
            else
            {

            Con.Open();
                String query = "insert into Application_tbl values('" +RoomNumtb.Text+ "', '" + RoomStatusCb.SelectedItem.ToString() + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Кімната успішно добавлена");
                Con.Close();
                FillRoomDGV();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RoomNumtb.Text = RoomDGV.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void Rooms_Load(object sender, EventArgs e)
        {
            FillRoomDGV();
        }
    }
}
