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
    public partial class Clients : Form
    {
        public Clients()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\cardi\Documents\HostelMgmt.mdf;Integrated Security=True;Connect Timeout=30");
        void FillStudentDGV()
        {
            Con.Open();
            string myquery = "Select * from Student_tbl";
            SqlDataAdapter da = new SqlDataAdapter(myquery, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            StudentDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
       
        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 Home = new Form1();
            Home.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (StudUsn.Text == "" || StudName.Text == "" || FatherName.Text == "" || MotherName.Text == "" || AddressTb.Text == "" || CollegeTb.Text == "")
            {
                MessageBox.Show("Пусте поле!");
            }
            else
            {

                Con.Open();
                String query = "insert into Student_tbl values('"+StudUsn.Text +"', '" + StudName.Text + "', '" + FatherName.Text + "', '"+MotherName.Text+"', '"+AddressTb.Text+"', '"+CollegeTb.Text+"')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Запис успішно добавлений");
                Con.Close();
                FillStudentDGV();
               
                
            }
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            FillStudentDGV();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (StudUsn.Text == "")
            {
                MessageBox.Show("Введіть ID запису");
            }
            else
            {
                Con.Open();
                String query = "delete from Student_tbl where StdUsn = '"+StudUsn.Text+"' ";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Запис успішно видалений");
                Con.Close();
                
                FillStudentDGV();
                
            }
        }

        private void StudentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            StudUsn.Text = StudentDGV.SelectedRows[0].Cells[0].Value.ToString();
            StudName.Text = StudentDGV.SelectedRows[0].Cells[1].Value.ToString();
            FatherName.Text = StudentDGV.SelectedRows[0].Cells[2].Value.ToString();
            MotherName.Text = StudentDGV.SelectedRows[0].Cells[3].Value.ToString();
            AddressTb.Text = StudentDGV.SelectedRows[0].Cells[4].Value.ToString();
            CollegeTb.Text = StudentDGV.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (StudUsn.Text == "")
            {
                MessageBox.Show("Введіть іноформацію про запис");
            }
            else
            {
                Con.Open();
                string query = "update Student_tbl set StdName = '"+StudName.Text+"', FatherName = '"+FatherName.Text+"', MotherName = '"+MotherName.Text+"', StdAddress = '"+AddressTb.Text+"', College = '"+CollegeTb.Text+"' where StdUsn = '"+StudUsn.Text+"' ";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Інформація успішно оновлена");
                Con.Close();
                FillStudentDGV();
                
            }
        }

        private void CollegeTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void StudStatusCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

