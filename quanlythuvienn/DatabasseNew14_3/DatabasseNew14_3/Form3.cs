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

namespace DatabasseNew14_3
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //B1 connect Database
            SqlConnection Conn = new SqlConnection("Data Source=DESKTOP-5EPQAIB\\SQLEXPRESS;Initial Catalog=QLDiemSV;Integrated Security=True");
            Conn.Open();

            //B2 Tao dt Command
            SqlCommand cmd = new SqlCommand("select * from Lop",Conn);
            cmd.ExecuteNonQuery();

            //B3 Dổ dl cmd vào DataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            //B4 đổ dl  từ da vào bảng

            DataTable tb = new DataTable();
            da.Fill(tb);


            // them 1 dòng vào vị trí đầu tiên trong bảng
            DataRow r = tb.NewRow();
            r["Malop"] = "";
            r["Tenlop"] = "-Chọn-";
            tb.Rows.InsertAt(r,0);


            //B5 đổ dl từ tb vào combobox
            comboBox1.DataSource = tb;
            comboBox1.ValueMember = "Malop";
            comboBox1.DisplayMember = "Tenlop";

            //B5 đổ dl vào datagrikkview 
            DataTable tb1 = new  DataTable();
            da.Fill(tb1);
            dataGridView1.DataSource = tb1;
            dataGridView1.Refresh();









        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtml.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txttl.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtmk.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtss.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string p_malop = txtml.Text.Trim();
            string p_tenlop = txttl.Text.Trim();
            string p_makhoa = txtmk.Text.Trim();
            string p_siso = txtss.Text.Trim();
            Form f = new truyengiatriform3denformnay(p_malop, p_tenlop, p_makhoa, p_siso);
            f.Show();
           
        }
    }
}
