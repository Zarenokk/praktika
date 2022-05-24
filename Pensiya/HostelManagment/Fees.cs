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
    public partial class Fees : Form
    {
        public Fees()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\cardi\Documents\HostelMgmt.mdf;Integrated Security=True;Connect Timeout=30");
        void FillFeesDGV()
        {
            Con.Open();
            string myquery = "Select * from Fees_tbl";
            SqlDataAdapter da = new SqlDataAdapter(myquery, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            PaymentDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        public void FillUsnCb()
        {
            Con.Open();
            string query = "select StdUsn from Student_tbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StdUsn", typeof(string));
            dt.Load(rdr);
            UsnCb.ValueMember = "StdUsn";
            UsnCb.DataSource = dt;
            Con.Close();
        }
        string studname;
        public void fecthdata()
        {
            Con.Open();
            string query = "select * from Student_tbl where StdUsn = '"+UsnCb.SelectedValue.ToString()+"'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd); 
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                studname = dr ["StdName"].ToString();
                StudentNameTb.Text = studname;
                
            }
            Con.Close();

        }
        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 Home = new Form1();
            Home.Show();
            this.Hide();
        }

        private void UsnCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fecthdata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PaymentIdTb.Text == "" || StudentNameTb.Text == "" || UsnCb.Text == "" || AmountTb.Text == "")
            {
                MessageBox.Show("Пусте поле!");
            }
            else
            {
                string paymentperiode;
                paymentperiode = Periode.Value.Month.ToString() + Periode.Value.Year.ToString();
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Fees_tbl where StudentUSN = '"+ UsnCb.SelectedValue.ToString() +"' and PaymentMonth = '"+paymentperiode.ToString()+"' ", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString()=="1")
                {
                    MessageBox.Show("Немає виплати за цей місяць");
                }
                else 
                {
                    
                    String query = "insert into Fees_tbl values('" + PaymentIdTb.Text + "', '" + UsnCb.SelectedValue.ToString() + "', '" + StudentNameTb.Text + "', '" + paymentperiode.ToString() + "', '" + AmountTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Виплата успішно добавлена");
                }
                    Con.Close();
                    FillFeesDGV();
            }
        }

        private void PaymentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PaymentIdTb.Text = PaymentDGV.SelectedRows[0].Cells[0].Value.ToString();
            StudentNameTb.Text = PaymentDGV.SelectedRows[0].Cells[1].Value.ToString();
            AmountTb.Text = PaymentDGV.SelectedRows[0].Cells[3].Value.ToString();
            UsnCb.SelectedItem = PaymentDGV.SelectedRows[0].Cells[4].Value.ToString();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (PaymentIdTb.Text == "")
            {
                MessageBox.Show("Введіть ID виплати");
            }
            else
            {
                Con.Open();
                String query = "delete from Fees_tbl where PaymentId = '" + PaymentIdTb.Text + "' ";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Виплата успішно видалена");
                Con.Close();
                FillFeesDGV();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (PaymentIdTb.Text == "")
            {
                MessageBox.Show("Введіть ID виплати");
            }
            else
            {
                Con.Open();
                string paymentperiode;
                paymentperiode = Periode.Value.Month.ToString() + Periode.Value.Year.ToString();
                string query = "update Fees_tbl set StudentUSN= '" + UsnCb.SelectedValue.ToString() + "', StudentName = '" + StudentNameTb.Text + "', Amount = '" + AmountTb.Text + "', PaymentMonth = '" + paymentperiode + "' where PaymentId = '" + PaymentIdTb.Text + "'  ";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Інформація про виплату успішно оновлена");
                Con.Close();
                FillFeesDGV();
                
            }
        }

        private void RoomNumTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void Fees_Load(object sender, EventArgs e)
        {
            FillUsnCb();
            FillFeesDGV();
        }

        private void StudentNameTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
