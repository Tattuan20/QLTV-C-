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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string p_tenlop = txtlop.Text.Trim();
            int tusiso = Convert.ToInt32(txttu.Text.Trim());
            int densiso = Convert.ToInt32(txtden.Text.Trim());

            //b1 kết nối database
            SqlConnection Conn = new SqlConnection("Data Source=DESKTOP-5EPQAIB\\SQLEXPRESS;Initial Catalog=QLDiemSV;Integrated Security=True");
            Conn.Open();

            //b2 tạo đối tg cmd
            SqlCommand cmd = new SqlCommand("Lop_Find", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Tenlop", SqlDbType.NVarChar, 50).Value = p_tenlop;
            cmd.Parameters.Add("@Tusiso", SqlDbType.Int).Value = tusiso;
            cmd.Parameters.Add("@densiso", SqlDbType.Int).Value = densiso;

            //b3 tạo đôi tượng data adapter đọc dữ liệu từ cmd
            SqlDataAdapter dap = new SqlDataAdapter();
            dap.SelectCommand = cmd;

            //b4 đổ dl từ Adapter vào bảng
            DataTable tb = new DataTable();
            dap.Fill(tb);

            //b5 dổ dl từ datagrikkview
            dataGridView1.DataSource = tb;
            dataGridView1.Refresh();
            dataGridView1.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.Hide();
        }
    }
    
}
