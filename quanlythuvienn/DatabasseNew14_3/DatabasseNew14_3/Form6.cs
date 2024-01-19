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
    public partial class Form6 : Form
    {
        SqlConnection Conn = new SqlConnection("Data Source=DESKTOP-5EPQAIB\\SQLEXPRESS;Initial Catalog=QLDiemSV;Integrated Security=True");
        public Form6()
        {
            InitializeComponent();
        }
        private void Load_cbKhoa()
        {
            //mo ket noi
            if(Conn.State != ConnectionState.Open)
            //b1 ket noi database
            Conn.Open();
            //b2 tao doi tuong cmd
            SqlCommand cmd = new SqlCommand("select * from khoa",Conn);
            //b3 tao doi tuong adapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            Conn.Close();
            //b4 do du lieu vao bang table
            DataTable tb = new DataTable();
            da.Fill(tb);

            //chen them 1 dong vao cb
            DataRow r = tb.NewRow();
            r["Tenkhoa"] = "-Chon khoa-";
            r["Makhoa"] = "";
            tb.Rows.InsertAt(r, 0);
            //b5 do tu tb vao cbkhoa
          cbkhoa.DataSource = tb;
            cbkhoa.DisplayMember = "Tenkhoa";
            cbkhoa.ValueMember = "Makhoa";
        }
        //Tim kiem sinh vien theo ma lop ,ten lop,...
        private void Load_grv_tk(string p_malop, string p_tenlop, string p_makhoa)
        {
            //mo ket noi
            if (Conn.State != ConnectionState.Open)
                //b1 ket noi database
                Conn.Open();
            //b2 tao doi tuong cmd
            SqlCommand cmd = new SqlCommand("Lop_f", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Malop",SqlDbType.NVarChar,50).Value = p_malop;
            cmd.Parameters.Add("@Tenlop", SqlDbType.NVarChar, 50).Value = p_tenlop;
            cmd.Parameters.Add("@Makhoa", SqlDbType.NVarChar, 50).Value = p_makhoa;
            cmd.ExecuteNonQuery();
            //b3 tao doi tuong adapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            Conn.Close();
            //b4 do du lieu vao bang table
            DataTable tb = new DataTable();
            da.Fill(tb);
            //b5 do dl vao datagrikvieww
            dataGridView1.DataSource = tb;
            dataGridView1.Refresh();

        }
        private void Form6_Load(object sender, EventArgs e)
        {
            Load_cbKhoa();
            Load_grv_tk("","","");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string p_malop = txtmalop.Text.Trim();
            string p_tenlop = txttenlop.Text.Trim();
            string p_makhoa = cbkhoa.SelectedValue.ToString();
            Load_grv_tk(p_malop, p_tenlop, p_makhoa);


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
