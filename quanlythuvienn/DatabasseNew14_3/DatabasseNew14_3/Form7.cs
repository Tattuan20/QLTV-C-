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
    public partial class Form7 : Form
    {
        SqlConnection Conn = new SqlConnection("Data Source=DESKTOP-5EPQAIB\\SQLEXPRESS;Initial Catalog=QLDiemSV;Integrated Security=True");
        public Form7()
        {
            InitializeComponent();
        }
        private void Load_cbLop()
        {
            //mo ket noi
            if (Conn.State != ConnectionState.Open)
                //b1 ket noi database
                Conn.Open();
            //b2 tao doi tuong cmd
            SqlCommand cmd = new SqlCommand("select * from Lop", Conn);
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
            r["Tenlop"] = "--Chon lop--";
            r["Malop"] = "";
            tb.Rows.InsertAt(r, 0);
            //b5 do tu tb vao cbkhoa
            cblop.DataSource = tb;
            cblop.DisplayMember = "Tenlop";
            cblop.ValueMember = "Malop";
        }
        private void Load_grv_tk(string p_masv, string p_hoten,string p_malop, string p_gt, string p_quequan, DateTime p_tungay , DateTime p_denngay )
        {
            if (Conn.State != ConnectionState.Open)
                //b1 ket noi database
                Conn.Open();
            //b2 tao doi tuong cmd
            SqlCommand cmd = new SqlCommand("SinhVien_ff1", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Masv", SqlDbType.NVarChar, 50).Value = p_masv;
            cmd.Parameters.Add("@Hoten", SqlDbType.NVarChar, 50).Value = p_hoten;
            cmd.Parameters.Add("@Gioitinh", SqlDbType.NVarChar, 50).Value = p_gt;
            cmd.Parameters.Add("@Malop", SqlDbType.NVarChar, 50).Value = p_malop;
            cmd.Parameters.Add("@Tinh", SqlDbType.NVarChar, 50).Value = p_quequan;
            cmd.Parameters.Add("@tungay", SqlDbType.DateTime).Value = p_tungay;
            cmd.Parameters.Add("@denngay", SqlDbType.DateTime).Value = p_denngay;
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
        private void Form7_Load(object sender, EventArgs e)
        {
            Load_cbLop();
            Load_grv_tk("", "", "", "","", Convert.ToDateTime("1/1/1900"), Convert.ToDateTime("29/12/2022"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string p_masv = txtmsv.Text.Trim();
            string p_hoten = txtht.Text;
            string p_malop = cblop.SelectedValue.ToString();
            string p_gt = txtgt.Text;
            string p_quequan = txtquequan.Text;
         
            DateTime p_tungay = dateTimePicker1.Value;
            DateTime p_denngay = dateTimePicker2.Value;
            Load_grv_tk(p_masv, p_hoten,p_malop, p_gt, p_quequan, p_tungay, p_denngay);
        }
        private void XuatEcel(DataTable tb, string sheetname)
        {
            //Tạo các đối tượng Excel

            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks oBooks;
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;
            //Tạo mới một Excel WorkBook 
            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;
            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oSheet.Name = sheetname;
            // Tạo phần đầu nếu muốn
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "D1");
            head.MergeCells = true;
            head.Value2 = "DANH SÁCH SINH VIÊN";
            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = "18";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            // Tạo tiêu đề cột 
            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "MÃ SV";
            cl1.ColumnWidth = 7.5;
            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "HỌ VÀ TÊN";

            cl2.ColumnWidth = 25.0;
            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("B4", "B4");
            cl2.Value2 = "TÊN LỚP";

            cl2.ColumnWidth = 25.0;
            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "GIỚI TÍNH";
            cl3.ColumnWidth = 25.0;
            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("D3", "D3");
            cl4.Value2 = "NGÀY SINH";
            cl4.ColumnWidth = 15.0;
            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("E3", "E3");
            cl4.Value2 = "QUÊ QUÁN";
            cl4.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "D3");
            rowHead.Font.Bold = true;
            // Kẻ viền
            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            // Thiết lập màu nền
            rowHead.Interior.ColorIndex = 15;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            // Tạo mảng đối tượng để lưu dữ toàn bồ dữ liệu trong DataTable,
            // vì dữ liệu được được gán vào các Cell trong Excel phải thông qua object thuần.
            object[,] arr = new object[tb.Rows.Count, tb.Columns.Count];
            //Chuyển dữ liệu từ DataTable vào mảng đối tượng
            for (int r = 0; r < tb.Rows.Count; r++)
            {
                DataRow dr = tb.Rows[r];
                for (int c = 0; c < tb.Columns.Count; c++)

                {
                    arr[r, c] = dr[c];
                }
            }
            //Thiết lập vùng điền dữ liệu
            int rowStart = 4;
            int columnStart = 1;
            int rowEnd = rowStart + tb.Rows.Count - 1;
            int columnEnd = tb.Columns.Count;
            // Ô bắt đầu điền dữ liệu
            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];
            // Ô kết thúc điền dữ liệu
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];
            // Lấy về vùng điền dữ liệu
            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);
            //Điền dữ liệu vào vùng đã thiết lập
            range.Value2 = arr;
            // Kẻ viền
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            // Căn giữa cột STT
            Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnStart];
            Microsoft.Office.Interop.Excel.Range c4 = oSheet.get_Range(c1, c3);
            oSheet.get_Range(c3, c4).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string p_masv = txtmsv.Text.Trim();
            string p_hoten = txtht.Text;
            string p_malop = cblop.SelectedValue.ToString();
            string p_gt = txtgt.Text;
            string p_quequan = txtquequan.Text;
            DateTime p_tungay = dateTimePicker1.Value;
            DateTime p_denngay = dateTimePicker2.Value;

            Conn.Open();

            //b3 tao cmd  = tao store
            SqlCommand cmd = new SqlCommand("SinhVien_ff1", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Masv", SqlDbType.NVarChar, 50).Value = p_masv;
            cmd.Parameters.Add("@Hoten", SqlDbType.NVarChar, 50).Value = p_hoten;
            cmd.Parameters.Add("@Gioitinh", SqlDbType.NVarChar, 50).Value = p_gt;
            cmd.Parameters.Add("@Malop", SqlDbType.NVarChar, 50).Value = p_malop;
            cmd.Parameters.Add("@Tinh", SqlDbType.NVarChar, 50).Value = p_quequan;
            cmd.Parameters.Add("@tungay", SqlDbType.DateTime).Value = p_tungay;
            cmd.Parameters.Add("@denngay", SqlDbType.DateTime).Value = p_denngay;
            cmd.Dispose();
            Conn.Close();
            //b4 tao dataAdapter do data tu cmd
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //b5 do data tu da vao datatble
            DataTable tb = new DataTable();
            da.Fill(tb);
            XuatEcel(tb, "Dslophoc");
        }
    }
}
