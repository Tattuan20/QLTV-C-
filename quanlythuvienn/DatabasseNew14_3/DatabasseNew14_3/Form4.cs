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
    public partial class Form4 : Form
    {
        SqlConnection Conn = new SqlConnection("Data Source=DESKTOP-5EPQAIB\\SQLEXPRESS;Initial Catalog=QLDiemSV;Integrated Security=True");
       

        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Load_cbkhoa()
        {
            //B1 ket noi database
            Conn.Open();
            //B2 Tao dt cmd 
            SqlCommand cmd = new SqlCommand("select * from Khoa ", Conn);
            //B3 đổ dl adapter từ cmd

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            Conn.Close();
            //B4 đổ dl vào bảng
            DataTable tb = new DataTable();
            da.Fill(tb);
            ///
            DataRow r = tb.NewRow();
            r["Makhoa"] = "";
            r["Tenkhoa"] = "-Chọn dữ liệu";
            tb.Rows.InsertAt(r,0);
            //B5 từ bảng đổ vào combobox
            cbmakhoa.DataSource = tb;
            cbmakhoa.DisplayMember = "Tenlop";
            cbmakhoa.ValueMember = "Makhoa";


        }
        private void Load_Lop()
        {
            //B1 ket noi database
            Conn.Open();
            //B2 Tao dt cmd 
            SqlCommand cmd = new SqlCommand("select * from Lop ", Conn);
            //B3 đổ dl adapter từ cmd

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Dispose();
            Conn.Close();
            //B4 đổ dl vào bảng
            DataTable tb = new DataTable();
            da.Fill(tb);
            //B5 từ bảng đổ vào combobox
            dataGridView1.DataSource = tb;
            dataGridView1.Refresh();



        }
        private void Form4_Load(object sender, EventArgs e)
        {
            Load_cbkhoa();
            Load_Lop();
           
        }


        //kiem tra du lieu nhap vao la kieu int hoac text
        private bool Isnumberic(String text)
        {
            int p_siso = int.Parse(txtsiso.Text.Trim());
            return int.TryParse(text, out p_siso);
        }



        //kiem tra tranh them trung du lieu ma lop
        private void Kiemtra_trungmalop(string p_malop, ref int p_ketqua)
        {
           
            //Tạo kết nối dtb
            if (Conn.State != ConnectionState.Open)
                Conn.Open();
            //Tạo đt cmd để đẩy dl vào đb
            SqlCommand cmd = new SqlCommand("Kiemtra_trungmalop", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Malop", SqlDbType.NVarChar, 50).Value = p_malop;
            SqlParameter kq = new SqlParameter("@kq", SqlDbType.Int);
            cmd.Parameters.Add(kq).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            p_ketqua = (int)kq.Value;
          
        }



        private void button1_Click(object sender, EventArgs e)
        {
            String p_malop = txtmalop.Text.Trim();
            String p_tenlop = txttenlop.Text.Trim();
            String p_makhoa = cbmakhoa.SelectedValue.ToString();
            int p_siso = int.Parse(txtsiso.Text.Trim());

            int p_kq = 0;
            //kiem tra malop khong dc rong
            if (p_malop == "")
            
                {
                txtmalop.Focus();
                txtmalop.BackColor = Color.Red;
                MessageBox.Show("Ma lop khong de trong");
            }
               
            //kiem tra du lieu la sô
           else if (Isnumberic(txtsiso.Text) == false)
            {
                txtsiso.Focus();
                txtsiso.BackColor = Color.Red;
                MessageBox.Show("Nhap so");
            }
            
            else
            {
                //Tạo kết nối dtb
                if (Conn.State != ConnectionState.Open)
                    Conn.Open();
                Conn.Close();                
                //Tạo đt cmd để đẩy dl vào đb
                SqlCommand cmd = new SqlCommand("Lop_ins", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Malop", SqlDbType.NVarChar, 50).Value = p_malop;
                cmd.Parameters.Add("@Tenlop", SqlDbType.NVarChar, 50).Value = p_tenlop;
                cmd.Parameters.Add("@Makhoa", SqlDbType.NVarChar, 50).Value = p_makhoa;
                cmd.Parameters.Add("@Sosv", SqlDbType.Int).Value = p_siso;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Conn.Close();
                Load_Lop();
                MessageBox.Show("Thanh cong", "Thong bao");
            }

            // kiem tra trung malop khong

            Kiemtra_trungmalop(p_malop, ref p_kq);
            if (p_kq == 1) MessageBox.Show("Ma lop da ton tai", "Thong bao");
            else
            {
                //Tạo kết nối dtb
                if (Conn.State != ConnectionState.Open)
                    Conn.Open();

                //Tạo đt cmd để đẩy dl vào đb
                SqlCommand cmd = new SqlCommand("Lop_ins", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Malop", SqlDbType.NVarChar, 50).Value = p_malop;
                cmd.Parameters.Add("@Tenlop", SqlDbType.NVarChar, 50).Value = p_tenlop;
                cmd.Parameters.Add("@Makhoa", SqlDbType.NVarChar, 50).Value = p_makhoa;
                cmd.Parameters.Add("@Sosv", SqlDbType.Int).Value = p_siso;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Conn.Close();
                Load_Lop();
                MessageBox.Show("Thanh cong", "Thong bao");
            }






        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtmalop.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txttenlop.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            cbmakhoa.DisplayMember = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtsiso.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtmalop.Enabled = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //b1 lay data tu cac control

            String p_malop = txtmalop.Text.Trim();
            String p_tenlop = txttenlop.Text.Trim();
            String p_makhoa = cbmakhoa.SelectedValue.ToString();
            int p_siso = int.Parse(txtsiso.Text.Trim());

            //b2 mo ket noi
            Conn.Open();

            //b3 tao cmd  = tao store
            SqlCommand cmd = new SqlCommand("Lop_Updatee", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Malop", SqlDbType.NVarChar, 50).Value = p_malop;
            cmd.Parameters.Add("@Tenlop", SqlDbType.NVarChar, 50).Value = p_tenlop;
            cmd.Parameters.Add("@Makhoa", SqlDbType.NVarChar, 50).Value = p_makhoa;
            cmd.Parameters.Add("@Sosv", SqlDbType.Int).Value = p_siso;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            Conn.Close();
            Load_Lop();
            MessageBox.Show("Sua thanh cong", "Thong bao");
            txtmalop.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String p_malop = txtmalop.Text.Trim();
            //b2 ket noi
            Conn.Open();
           
            //b3 tao doi tuong cmd
            SqlCommand cmd = new SqlCommand("Lop_Del", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Malop", SqlDbType.NVarChar, 50).Value = p_malop;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            Conn.Close();
            Load_Lop();
            MessageBox.Show("Xoa thanh cong", "Thong bao");
            txtmalop.Enabled = true;
        }
        //tim kiem
        private void button5_Click(object sender, EventArgs e)
        {
            String p_malop = txtmalop.Text.Trim();
            String p_tenlop = txttenlop.Text.Trim();
            String p_makhoa = cbmakhoa.SelectedValue.ToString();
            Conn.Open();

            //b3 tao cmd  = tao store
            SqlCommand cmd = new SqlCommand("Lop_f", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Malop", SqlDbType.NVarChar, 50).Value = p_malop;
            cmd.Parameters.Add("@Tenlop", SqlDbType.NVarChar, 50).Value = p_tenlop;
            cmd.Parameters.Add("@Makhoa", SqlDbType.NVarChar, 50).Value = p_makhoa;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            Conn.Close();
            //b4 tao dataAdapter do data tu cmd
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //b5 do data tu da vao datatble
            DataTable tb = new DataTable();
            da.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Refresh();








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
                head.Value2 = "DANH SÁCH LỚP HỌC";
                head.Font.Bold = true;
                head.Font.Name = "Tahoma";
                head.Font.Size = "18";
                head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                // Tạo tiêu đề cột 
                Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
                cl1.Value2 = "MÃ LỚP";
                cl1.ColumnWidth = 7.5;
                Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
                cl2.Value2 = "TÊN LỚP";

                cl2.ColumnWidth = 25.0;
                Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
                cl3.Value2 = "MÃ KHOA";
                cl3.ColumnWidth = 25.0;
                Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
                cl4.Value2 = "SĨ SỐ";
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


        private void button1_Click_1(object sender, EventArgs e)
        {
            String p_malop = txtmalop.Text.Trim();
            String p_tenlop = txttenlop.Text.Trim();
            String p_makhoa = cbmakhoa.SelectedValue.ToString();
            Conn.Open();

            //b3 tao cmd  = tao store
            SqlCommand cmd = new SqlCommand("Lop_f", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Malop", SqlDbType.NVarChar, 50).Value = p_malop;
            cmd.Parameters.Add("@Tenlop", SqlDbType.NVarChar, 50).Value = p_tenlop;
            cmd.Parameters.Add("@Makhoa", SqlDbType.NVarChar, 50).Value = p_makhoa;
            cmd.ExecuteNonQuery();
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
