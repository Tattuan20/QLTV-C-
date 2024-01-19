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
using xls = Microsoft.Office.Interop.Excel;

namespace DatabasseNew14_3
{
    public partial class Form5 : Form
    {
        SqlConnection Conn = new SqlConnection("Data Source=DESKTOP-5EPQAIB\\SQLEXPRESS;Initial Catalog=QLDiemSV;Integrated Security=True");
        private string p_file;
        public Form5()
        {
            InitializeComponent();
        }
        private void Load_Grlophoc()
        {
            //b1 mở connect
            Conn.Open();
            
            
            //b2 tạo cmd 
            SqlCommand cmd = new SqlCommand("select * from Lop",Conn);

            //b3 do data từ cmd  vào dataAdapter

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            //b4 do data từ da vào datatble 
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            Conn.Close();

            //b5 đổ data từ tb  vào 
            datalophoc.DataSource = tb;
            datalophoc.Refresh();


        }
        //xaay dung phuong thuc them moi lop hoc
        private void Themmoilop(String p_malop, String p_tenlop, string p_makhoa, int p_so)
        {
            //B1 mở connec 
            Conn.Open();
            //B2 tao doi tg cmd
            SqlCommand cmd = new SqlCommand("Lop_ins", Conn);
          cmd.CommandType  = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Malop", SqlDbType.NVarChar, 50).Value = p_malop;
            cmd.Parameters.Add("@Tenlop", SqlDbType.NVarChar, 50).Value = p_tenlop;
            cmd.Parameters.Add("@Makhoa", SqlDbType.NVarChar, 50).Value = p_makhoa;
            cmd.Parameters.Add("@Siso", SqlDbType.Int).Value = p_so;
            cmd.ExecuteNonQuery();
            cmd.Dispose(); //thu hồi
            Conn.Close(); //ngắt ket noi db
        }
        //xay dung phương thức doc du lieu từ file excel
        private void Read_Excel()
        {
            //Mở app excel
            xls.Application ex = new xls.Application();
            //mo file excel dc chon
            ex.Workbooks.Open(p_file);
            //doc du lieu tren tung sheet
            foreach(xls.Worksheet ws in ex.Worksheets)
            {
                int i = 2;
                do
                {
                    if (ws.Cells[i, 1].Value == null) break;
                    else
                    {
                        string p_malop = ws.Cells[i, 1].Value;
                        string p_tenlop = ws.Cells[i, 2].Value;
                        string p_makhoa = ws.Cells[i, 3].Value;
                        int p_so = Convert.ToInt16(ws.Cells[i, 4].Value);
                        Themmoilop(p_malop, p_tenlop, p_makhoa, p_so); i++;
                    }
                } while (true);
            }
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            Load_Grlophoc();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Excel file | *.xls; *.xlsx";
            f.FilterIndex = 1;
            f.RestoreDirectory = true;
            f.Multiselect = false;
            if(f.ShowDialog() == DialogResult.OK)
            {
                p_file = f.FileName;
                txtlink.Text = p_file;
                Read_Excel();
            }
            MessageBox.Show("Thành công", "Thông báo");
            Load_Grlophoc();
    }
}
}
