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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //b1 kết nối database
            SqlConnection Conn = new SqlConnection("Data Source=DESKTOP-5EPQAIB\\SQLEXPRESS;Initial Catalog=QLDiemSV;Integrated Security=True");
            Conn.Open();

            //b2 Tạo đối tượng command lấy dl từ data
            SqlCommand cmd = new SqlCommand("select * from SinhVien", Conn);

            //b3 tạo đôi tượng data adapter đọc dữ liệu từ cmd
            SqlDataAdapter dap = new SqlDataAdapter();
            dap.SelectCommand = cmd;

            //b4 đổ dl từ Adapter vào bảng
            DataTable tb = new DataTable();
            dap.Fill(tb);

            //b5 dổ dl từ datagrikkview
            dgv.DataSource = tb;
            dgv.Refresh();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string p_hoten = textBox1.Text.Trim();
                 //b1 kết nối database
            SqlConnection Conn = new SqlConnection("Data Source=DESKTOP-5EPQAIB\\SQLEXPRESS;Initial Catalog=QLDiemSV;Integrated Security=True");
            Conn.Open();
            //b2 tạo đối tg cmd
            SqlCommand cmd = new SqlCommand("SinhVienn_Find", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Hoten", SqlDbType.NVarChar, 50).Value = p_hoten;
            //b3 tạo đôi tượng data adapter đọc dữ liệu từ cmd
            SqlDataAdapter dap = new SqlDataAdapter();
            dap.SelectCommand = cmd;

            //b4 đổ dl từ Adapter vào bảng
            DataTable tb = new DataTable();
            dap.Fill(tb);

            //b5 dổ dl từ datagrikkview
            dgv.DataSource = tb;
            dgv.Refresh();
            dgv.Show();
        }
    }
}
