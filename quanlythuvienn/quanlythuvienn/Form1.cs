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
using System.Data.OleDb;
using System.Data.Odbc;
namespace quanlythuvienn
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-S4ESN68K\\MAYAO2;Initial Catalog=QLThuVien;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

       private void tabPage2_Click(object sender, EventArgs e)
      {
           Load_grvthuvien();
        }
        public void Load_grvthuvien()
        {
            //B1: Kết nối đến DB
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-S4ESN68K\\MAYAO2;Initial Catalog=QLThuVien;Integrated Security=True");
            con.Open();
            //B2: tạo đối tượng command
            SqlCommand cmd = new SqlCommand("select*from Phieumuon", con);
            cmd.ExecuteNonQuery();
            //B3: Đổ dữ liệu từ cmd vào DataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //b4: Đổ dữ liệu từ da vào bảng
            DataTable tb = new DataTable();
            da.Fill(tb);
            //thêm 1 dòng vào vị trí đầu tiên của bảng tb
            // DataRow r = tb.NewRow();
            //r["malop"] = "";
            //r["tenlop"] = "--Chọn lớp học--";
            // tb.Rows.InsertAt(r, 0);
            //b5: Đổ dữ liệu từ tb vào combobox

            // cbloaisach.ValueMember = "matheloai";
            //cbloaisach.DisplayMember = "tentheloai";
            //Đổ dữ liệu vào datagridview
            DataTable tb1 = new DataTable();
            da.Fill(tb1);
            grvthuvien.DataSource = tb1;
            grvthuvien.Refresh();
        }



        private void btthem_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu từ các control
            string p_maphieum = txtmpm.Text.Trim();
            string p_madocgia = txtmadocgia.Text.Trim();
            string p_hoten = ht.Text.Trim();
            string p_diachi = dc.Text.Trim();
            string p_tensach = txttensach.Text.Trim();
            int p_soluong = int.Parse(txtsoluong.Text.Trim());
            DateTime p_ngaymuonsach = dtngaymuon.Value;
            DateTime p_ngaytrasach = dtngaytra.Value;
          
                con.Open();

                //Tạo đối tượng command để đẩy dl vào DB
                SqlCommand cmd = new SqlCommand("Phieumuon_Ins", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Maphieumuon", SqlDbType.NVarChar, 50).Value = p_maphieum;
                cmd.Parameters.Add("@Madocgia", SqlDbType.NVarChar, 50).Value = p_madocgia;
                cmd.Parameters.Add("@Hovaten", SqlDbType.NVarChar, 50).Value = p_hoten;
                cmd.Parameters.Add("@Diachi", SqlDbType.NVarChar, 50).Value = p_diachi;
                cmd.Parameters.Add("@tensach", SqlDbType.NVarChar, 50).Value = p_tensach;
                cmd.Parameters.Add("@soluong", SqlDbType.Int).Value = p_soluong;
                cmd.Parameters.Add("@ngaymuonsach", SqlDbType.Date).Value = p_ngaymuonsach;
                cmd.Parameters.Add("@ngaytrasach", SqlDbType.Date).Value = p_ngaytrasach;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                Load_grvthuvien();
                MessageBox.Show("Thêm mới thành công!");

                txtmpm.Clear();
                txtmadocgia.Clear();
                dc.Clear();
                txttensach.Clear();
                ht.Clear();
                txtsoluong.Clear();
            
        }

      

       

        private void btsua_Click_1(object sender, EventArgs e)
        {
            string p_maphieum = txtmpm.Text.Trim();
            string p_madocgia = txtmadocgia.Text.Trim();
            string p_hoten = ht.Text.Trim();
            string p_diachi = dc.Text.Trim();
            string p_tensach = txttensach.Text.Trim();
            int p_soluong = int.Parse(txtsoluong.Text.Trim());
            DateTime p_ngaymuonsach = dtngaymuon.Value;
            DateTime p_ngaytrasach = dtngaytra.Value;

            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Phieumuon_Upd", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Maphieumuon", SqlDbType.NVarChar, 50).Value = p_maphieum;
            cmd.Parameters.Add("@Madocgia", SqlDbType.NVarChar, 50).Value = p_madocgia;
            cmd.Parameters.Add("@Hovaten", SqlDbType.NVarChar, 50).Value = p_hoten;
            cmd.Parameters.Add("@Diachi", SqlDbType.NVarChar, 50).Value = p_diachi;
            cmd.Parameters.Add("@tensach", SqlDbType.NVarChar, 50).Value = p_tensach;
            cmd.Parameters.Add("@soluong", SqlDbType.Int).Value = p_soluong;
            cmd.Parameters.Add("@ngaymuonsach", SqlDbType.Date).Value = p_ngaymuonsach;
            cmd.Parameters.Add("@ngaytrasach", SqlDbType.Date).Value = p_ngaytrasach;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            Load_grvthuvien();
            MessageBox.Show("Sua thành công!");
            txtmpm.Clear();
            txtmadocgia.Clear();
            dc.Clear();
            txttensach.Clear();
            ht.Clear();
            txtsoluong.Clear();
        }

        private void btxoa_Click_1(object sender, EventArgs e)
        {
            string p_maphieu = txtmpm.Text.Trim();
            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Phieumuon_Del", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Maphieumuon", SqlDbType.NVarChar, 50).Value = p_maphieu;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            Load_grvthuvien();
            MessageBox.Show("Xoa  thành công!");
            txtmpm.Clear();
            txtmadocgia.Clear();
            dc.Clear();
            txttensach.Clear();
            ht.Clear();
            txtsoluong.Clear();
        }

        private void grvthuvien_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtmpm.Text = grvthuvien.Rows[i].Cells[0].Value.ToString();
            txtmadocgia.Text = grvthuvien.Rows[i].Cells[1].Value.ToString();
            ht.Text = grvthuvien.Rows[i].Cells[2].Value.ToString();
            dc.Text = grvthuvien.Rows[i].Cells[3].Value.ToString();
            txttensach.Text = grvthuvien.Rows[i].Cells[4].Value.ToString();
            txtsoluong.Text = grvthuvien.Rows[i].Cells[5].Value.ToString();
            dtngaymuon.Text = grvthuvien.Rows[i].Cells[6].Value.ToString();
            dtngaytra.Text = grvthuvien.Rows[i].Cells[7].Value.ToString();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            Load_grvtacgia();
        }
        public void Load_grvtacgia()
        {
            //B1: Kết nối đến DB
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-S4ESN68K\\MAYAO2;Initial Catalog=QLThuVien;Integrated Security=True");
            con.Open();
            //B2: tạo đối tượng command
            SqlCommand cmd = new SqlCommand("Select*from tacgia", con);
            cmd.ExecuteNonQuery();
            //B3: Đổ dữ liệu từ cmd vào DataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //b4: Đổ dữ liệu từ da vào bảng
            DataTable tb = new DataTable();
            da.Fill(tb);
            //thêm 1 dòng vào vị trí đầu tiên của bảng tb
            // DataRow r = tb.NewRow();
            //r["malop"] = "";
            //r["tenlop"] = "--Chọn lớp học--";
            // tb.Rows.InsertAt(r, 0);
            //b5: Đổ dữ liệu từ tb vào combobox

            // cbloaisach.ValueMember = "matheloai";
            //cbloaisach.DisplayMember = "tentheloai";
            //Đổ dữ liệu vào datagridview
            DataTable tb1 = new DataTable();
            da.Fill(tb1);
            grvtacgia.DataSource = tb1;
            grvtacgia.Refresh();
        }

        private void grvtacgia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtmtg.Text = grvtacgia.Rows[i].Cells[0].Value.ToString();
            txthoten.Text = grvtacgia.Rows[i].Cells[1].Value.ToString();
            txtgioitinh.Text = grvtacgia.Rows[i].Cells[2].Value.ToString();
            txtquequan1.Text = grvtacgia.Rows[i].Cells[3].Value.ToString();
          
        }
        private void Kiemtra_trungmatacgia(string p_matacgia, ref int p_ketqua)
        {

            //Tạo kết nối dtb
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            //Tạo đt cmd để đẩy dl vào đb
            SqlCommand cmd = new SqlCommand("Kiemtra_trungmatacgia", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Matacgia", SqlDbType.NChar, 10).Value = p_matacgia;
            SqlParameter kq = new SqlParameter("@kq", SqlDbType.Int);
            cmd.Parameters.Add(kq).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            p_ketqua = (int)kq.Value;

        }
        private void bthem_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu từ các control
            string p_Matacgia = txtmtg.Text.Trim();
            string p_Hovaten = txthoten.Text.Trim();
            string p_Gioitinh = txtgioitinh.Text.Trim();
            string p_Quequan = txtquequan1.Text.Trim();

            //Tạo kết nối DB

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            int p_kq = 0;
            // kiem tra trung masach khong
            Kiemtra_trungmatacgia(p_Matacgia, ref p_kq);
            //kiem tra masach khong dc rong
            if (p_Matacgia == "")

            {
                txtmtg.Focus();

                MessageBox.Show("Ma tac gia khong de trong");
            }

            else if (p_kq == 1)
            {

                MessageBox.Show("Ma tac gia da ton tai", "Thong bao");
            }
            else if (p_Hovaten == "")
            {
                txthoten.Focus();
                txthoten.BackColor = Color.Green;
                MessageBox.Show("Bạn chưa nhập ho ten", "Thong bao");
            }
            else
            {
                //Tạo đối tượng command để đẩy dl vào DB
                SqlCommand cmd = new SqlCommand("Tacgia_Ins", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Matacgia", SqlDbType.NChar, 10).Value = p_Matacgia;
                cmd.Parameters.Add("@Hovaten", SqlDbType.NChar, 50).Value = p_Hovaten;
                cmd.Parameters.Add("@Gioitinh", SqlDbType.NVarChar, 10).Value = p_Gioitinh;
                cmd.Parameters.Add("@Quequan", SqlDbType.NVarChar, 10).Value = p_Quequan;

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                Load_grvtacgia();
                MessageBox.Show("Thêm mới thành công!");
                txtmtg.Enabled = true;
                txtmtg.Clear();
                txthoten.Clear();
                txtgioitinh.Clear();
                txtquequan1.Clear();
            }
        }
        private void bsua_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu từ các control
            string p_Matacgia = txtmtg.Text.Trim();
            string p_Hovaten = txthoten.Text.Trim();
            string p_Gioitinh = txtgioitinh.Text.Trim();
            string p_Quequan = label30.Text.Trim();

            //Tạo kết nối DB

            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Tacgia_Upd", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Matacgia", SqlDbType.NChar, 10).Value = p_Matacgia;
            cmd.Parameters.Add("@Hovaten", SqlDbType.NChar, 50).Value = p_Hovaten;
            cmd.Parameters.Add("@Gioitinh", SqlDbType.NVarChar, 10).Value = p_Gioitinh;
            cmd.Parameters.Add("@Quequan", SqlDbType.NVarChar, 10).Value = p_Quequan;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            Load_grvtacgia();
            MessageBox.Show("Sửa thành công!");
            txtmtg.Enabled = true;
            txtmtg.Clear();
            txthoten.Clear();
            txtgioitinh.Clear();
            txtquequan1.Clear();
          
        }

        private void bxoa_Click(object sender, EventArgs e)
        {
            string p_Matacgia = txtmtg.Text.Trim();
            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Tacgia_Del", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Matacgia", SqlDbType.NChar, 10).Value = p_Matacgia;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            Load_grvtacgia();
            MessageBox.Show("Xoa  thành công!");
            txtmtg.Enabled = true;
            txtmtg.Clear();
            txthoten.Clear();
            txtgioitinh.Clear();
            txtquequan1.Clear();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
            Load_grvsach();
        }
        public void Load_grvsach()
        {
            //B1: Kết nối đến DB
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-S4ESN68K\\MAYAO2;Initial Catalog=QLThuVien;Integrated Security=True");
            con.Open();
            //B2: tạo đối tượng command
            SqlCommand cmd = new SqlCommand("Select*from Sach", con);
            cmd.ExecuteNonQuery();
            //B3: Đổ dữ liệu từ cmd vào DataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //b4: Đổ dữ liệu từ da vào bảng
            DataTable tb = new DataTable();
            da.Fill(tb);
            //thêm 1 dòng vào vị trí đầu tiên của bảng tb
            // DataRow r = tb.NewRow();
            //r["malop"] = "";
            //r["tenlop"] = "--Chọn lớp học--";
            // tb.Rows.InsertAt(r, 0);
            //b5: Đổ dữ liệu từ tb vào combobox

            // cbloaisach.ValueMember = "matheloai";
            //cbloaisach.DisplayMember = "tentheloai";
            //Đổ dữ liệu vào datagridview
            DataTable tb1 = new DataTable();
            da.Fill(tb1);
            grvsach.DataSource = tb1;
            grvsach.Refresh();
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {
            Load_grvdocgia();
        }
        public void Load_grvdocgia()
        {
            //B1: Kết nối đến DB
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-S4ESN68K\\MAYAO2;Initial Catalog=QLThuVien;Integrated Security=True");
            con.Open();
            //B2: tạo đối tượng command
            SqlCommand cmd = new SqlCommand("Select*from Docgia", con);
            cmd.ExecuteNonQuery();
            //B3: Đổ dữ liệu từ cmd vào DataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //b4: Đổ dữ liệu từ da vào bảng
            DataTable tb = new DataTable();
            da.Fill(tb);
            //thêm 1 dòng vào vị trí đầu tiên của bảng tb
            // DataRow r = tb.NewRow();
            //r["malop"] = "";
            //r["tenlop"] = "--Chọn lớp học--";
            // tb.Rows.InsertAt(r, 0);
            //b5: Đổ dữ liệu từ tb vào combobox

            // cbloaisach.ValueMember = "matheloai";
            //cbloaisach.DisplayMember = "tentheloai";
            //Đổ dữ liệu vào datagridview
            DataTable tb1 = new DataTable();
            da.Fill(tb1);
            grvdocgia.DataSource = tb1;
            grvdocgia.Refresh();
        }

        private void buttonthem_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu từ các control
            string p_mdg = txtmdg.Text.Trim();
            string p_hoten = txthovaten.Text.Trim();
            DateTime p_ngaysinh = dtngaysinh.Value;
            string p_Gioitinh = txtgioitinh1.Text.Trim();
            string p_diachi = txtdiachi.Text.Trim();
            DateTime p_ngaylamthe = dtngaylamthe.Value;
            //Tạo kết nối DB

            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Docgia_Ins", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Madocgia", SqlDbType.NChar, 10).Value = p_mdg;
            cmd.Parameters.Add("@Hovaten", SqlDbType.NChar, 50).Value = p_hoten;
            cmd.Parameters.Add("@Ngaysinh", SqlDbType.Date).Value = p_ngaysinh;
            cmd.Parameters.Add("@Gioitinh", SqlDbType.NVarChar, 10).Value = p_Gioitinh;
            cmd.Parameters.Add("@Diachi", SqlDbType.NVarChar, 50).Value = p_diachi;
            cmd.Parameters.Add("@Ngaylamthe", SqlDbType.Date).Value = p_ngaylamthe;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            Load_grvdocgia();
            MessageBox.Show("Thêm mới thành công!");
            txtmdg.Enabled = true;
            txtmdg.Clear();
            txthovaten.Clear();
         
            txtgioitinh1.Clear();
            txtdiachi.Clear();
        }

        private void buttonsua_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu từ các control
            string p_mdg = txtmdg.Text.Trim();
            string p_hoten = txthovaten.Text.Trim();
            DateTime p_ngaysinh = dtngaysinh.Value;
            string p_Gioitinh = txtgioitinh1.Text.Trim();
            string p_diachi = txtdiachi.Text.Trim();
            DateTime p_ngaylamthe = dtngaylamthe.Value;
            //Tạo kết nối DB

            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Docgia_Upd", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Madocgia", SqlDbType.NChar, 10).Value = p_mdg;
            cmd.Parameters.Add("@Hovaten", SqlDbType.NChar, 50).Value = p_hoten;
            cmd.Parameters.Add("@Ngaysinh", SqlDbType.DateTime).Value = p_ngaysinh;
            cmd.Parameters.Add("@Gioitinh", SqlDbType.NVarChar, 10).Value = p_Gioitinh;
            cmd.Parameters.Add("@Diachi", SqlDbType.NVarChar, 50).Value = p_diachi;
            cmd.Parameters.Add("@Ngaylamthe", SqlDbType.DateTime).Value = p_ngaylamthe;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            Load_grvdocgia();
            MessageBox.Show("Sua thành công!");
            txtmdg.Enabled = true;
            txtmdg.Clear();
            txthovaten.Clear();

            txtgioitinh1.Clear();
            txtdiachi.Clear();
        }

        private void buttonxoa_Click(object sender, EventArgs e)
        {
            string p_mdg = txtmdg.Text.Trim();
            con.Open();
            SqlCommand cmd = new SqlCommand("Docgia_Del", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Madocgia", SqlDbType.NChar, 10).Value = p_mdg;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            Load_grvdocgia();
            MessageBox.Show("Xoa thành công!");
            txtmdg.Enabled = true;
            txtmdg.Clear();
            txthovaten.Clear();

            txtgioitinh1.Clear();
            txtdiachi.Clear();
        }

        private void grvdocgia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtmdg.Text = grvdocgia.Rows[i].Cells[0].Value.ToString();
            txthovaten.Text = grvdocgia.Rows[i].Cells[1].Value.ToString();
            dtngaysinh.Text = grvdocgia.Rows[i].Cells[2].Value.ToString();
            txtgioitinh.Text = grvdocgia.Rows[i].Cells[3].Value.ToString();
            txtdiachi.Text = grvdocgia.Rows[i].Cells[4].Value.ToString();
            dtngaylamthe.Text = grvdocgia.Rows[i].Cells[5].Value.ToString();
        }
        private void Kiemtra_trungmasach(string p_masach, ref int p_ketqua)
        {

            //Tạo kết nối dtb
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            //Tạo đt cmd để đẩy dl vào đb
            SqlCommand cmd = new SqlCommand("Kiemtra_trungmasachh", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Masach", SqlDbType.NChar, 10).Value = p_masach;
            SqlParameter kq = new SqlParameter("@kq", SqlDbType.Int);
            cmd.Parameters.Add(kq).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            p_ketqua = (int)kq.Value;

        }
        private void butthem_Click(object sender, EventArgs e)
        {
            string p_masach = txtmasach1.Text.Trim();
            string p_tensach = txttensach1.Text.Trim();
            String p_theloai = txttheloai.Text.Trim();
            string p_nxb = txtnhaxuatban.Text.Trim();
            int p_namxb = int.Parse(txtnamxuatban.Text.Trim());
            string p_mtg = txtmtg1.Text.Trim();
            int p_soluong = int.Parse(txtsoluong1.Text.Trim());

            int p_gia = int.Parse(txtgiatien.Text.Trim());
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            int p_kq = 0;
            // kiem tra trung masach khong
            Kiemtra_trungmasach(p_masach, ref p_kq);
            //kiem tra masach khong dc rong
            if (p_masach == "")

            {
                txtmasach1.Focus();

                MessageBox.Show("Ma sach khong de trong");
            }

            else if (p_kq == 1)
            {

                MessageBox.Show("Ma sach da ton tai", "Thong bao");
            }
            else if (p_tensach == "")
            {
                txttensach1.Focus();
                txttensach1.BackColor = Color.Green;
                MessageBox.Show("Bạn chưa nhập tên sách", "Thong bao");
            }
            //Tạo kết nối DB

            else
            {

                //Tạo đối tượng command để đẩy dl vào DB
                SqlCommand cmd = new SqlCommand("Sach_Ins", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Masach", SqlDbType.NChar, 10).Value = p_masach;
                cmd.Parameters.Add("@Tensach", SqlDbType.NChar, 50).Value = p_tensach;
                cmd.Parameters.Add("@Theloai", SqlDbType.NVarChar, 50).Value = p_theloai;
                cmd.Parameters.Add("@Nhaxuatban", SqlDbType.NVarChar, 10).Value = p_nxb;
                cmd.Parameters.Add("@Namxuatban", SqlDbType.Int).Value = p_namxb;
                cmd.Parameters.Add("@Matacgia", SqlDbType.NChar, 10).Value = p_mtg;
                cmd.Parameters.Add("@Soluong", SqlDbType.Int).Value = p_soluong;
                cmd.Parameters.Add("@Giatien", SqlDbType.Int).Value = p_gia;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                Load_grvsach();
                MessageBox.Show("Thêm mới thành công!");
                txtmasach1.Clear();
                txttensach1.Clear();
                txtnhaxuatban.Clear();
                txttheloai.Clear();
                txtnamxuatban.Clear();
                txtmtg1.Clear();
                txtsoluong1.Clear();
                txtgiatien.Clear();
            }
        }

        private void butsua_Click(object sender, EventArgs e)
        {
            string p_masach = txtmasach1.Text.Trim();
            string p_tensach = txttensach1.Text.Trim();
            String p_theloai = txttheloai.Text.Trim();
            string p_nxb = txtnhaxuatban.Text.Trim();
            int p_namxb = int.Parse(txtnamxuatban.Text.Trim());
            string p_mtg = txtmtg1.Text.Trim();
            int p_soluong = int.Parse(txtsoluong1.Text.Trim());

            int p_gia = int.Parse(txtgiatien.Text.Trim());
            DateTime p_ngaylamthe = dtngaylamthe.Value;
            //Tạo kết nối DB

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Sach_Upd", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Masach", SqlDbType.NChar, 10).Value = p_masach;
            cmd.Parameters.Add("@Tensach", SqlDbType.NChar, 50).Value = p_tensach;
            cmd.Parameters.Add("@Theloai", SqlDbType.NVarChar, 50).Value = p_theloai;
            cmd.Parameters.Add("@Nhaxuatban", SqlDbType.NVarChar, 10).Value = p_nxb;
            cmd.Parameters.Add("@Namxuatban", SqlDbType.Int).Value = p_namxb;
            cmd.Parameters.Add("@Matacgia", SqlDbType.NChar, 10).Value = p_mtg;
            cmd.Parameters.Add("@Soluong", SqlDbType.Int).Value = p_soluong;
            cmd.Parameters.Add("@Giatien", SqlDbType.Int).Value = p_gia;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            Load_grvsach();
            MessageBox.Show("Sua mới thành công!");
            txtmasach1.Clear();
            txttensach1.Clear();
            txtnhaxuatban.Clear();
            txttheloai.Clear();
            txtnamxuatban.Clear();
            txtmtg1.Clear();
            txtsoluong1.Clear();
            txtgiatien.Clear();
        }

        private void butxoa_Click(object sender, EventArgs e)
        {
            string p_masach = txtmasach1.Text.Trim();
            con.Open();
            SqlCommand cmd = new SqlCommand("Sach_Del", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Masach", SqlDbType.NChar, 10).Value = p_masach;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            Load_grvsach();
            MessageBox.Show("Xoa thành công!");
            txtmasach1.Clear();
            txttensach1.Clear();
            txtnhaxuatban.Clear();
            txttheloai.Clear();
            txtnamxuatban.Clear();
            txtmtg1.Clear();
            txtsoluong1.Clear();
            txtgiatien.Clear();
        }

        private void grvsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtmasach1.Text = grvsach.Rows[i].Cells[0].Value.ToString();
            txttensach1.Text = grvsach.Rows[i].Cells[1].Value.ToString();
            txttheloai.Text = grvsach.Rows[i].Cells[2].Value.ToString();
            txtnhaxuatban.Text = grvsach.Rows[i].Cells[3].Value.ToString();
            txtnamxuatban.Text = grvsach.Rows[i].Cells[4].Value.ToString();
            txtmtg1.Text = grvsach.Rows[i].Cells[5].Value.ToString();
            txtsoluong1.Text = grvsach.Rows[i].Cells[6].Value.ToString();
            txtgiatien.Text = grvsach.Rows[i].Cells[7].Value.ToString();
        }

        private void btim_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu từ các control
            string p_Matacgia = txtmtg.Text.Trim();
          
            //Tạo kết nối DB

            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Tacgia_f", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Matacgia", SqlDbType.NChar, 10).Value = p_Matacgia;
         
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
             SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //do dl vao dataTable
            DataTable tb = new DataTable();
            da.Fill(tb);
            //do dl tu tb vao grv
            grvtacgia.DataSource = tb;
            grvtacgia.Refresh();
                
        }

        private void buttontim_Click(object sender, EventArgs e)
        {
            string p_Madocgia = txtmdg.Text.Trim();

            //Tạo kết nối DB

            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Docgia_f", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Madocgia", SqlDbType.NChar, 10).Value = p_Madocgia;

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //do dl vao dataTable
            DataTable tb = new DataTable();
            da.Fill(tb);
            //do dl tu tb vao grv
            grvdocgia.DataSource = tb;
            grvdocgia.Refresh();
        }

        private void buttim_Click(object sender, EventArgs e)
        {
            string p_Masach = txtmasach1.Text.Trim();

            //Tạo kết nối DB

            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Sach_f", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Masach", SqlDbType.NChar, 10).Value = p_Masach;

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //do dl vao dataTable
            DataTable tb = new DataTable();
            da.Fill(tb);
            //do dl tu tb vao grv
            grvsach.DataSource = tb;
            grvsach.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f1 = new Form2();
            f1.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            string p_Masach = textBox1.Text.Trim();

            //Tạo kết nối DB

            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Sach_f", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Masach", SqlDbType.NChar, 10).Value = p_Masach;

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //do dl vao dataTable
            DataTable tb = new DataTable();
            da.Fill(tb);
            //do dl tu tb vao grv
            dataGridView1.DataSource = tb;
            dataGridView1.Refresh();
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu từ các control
            string p_Matacgia = textBox1.Text.Trim();

            //Tạo kết nối DB

            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Tacgia_f", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Matacgia", SqlDbType.NChar, 10).Value = p_Matacgia;

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //do dl vao dataTable
            DataTable tb = new DataTable();
            da.Fill(tb);
            //do dl tu tb vao grv
            dataGridView1.DataSource = tb;
          dataGridView1.Refresh();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            string p_tendocgia = textBox1.Text.Trim();

            //Tạo kết nối DB

            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Docgia_f", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Madocgia", SqlDbType.NVarChar, 50).Value = p_tendocgia;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //do dl vao dataTable
            DataTable tb = new DataTable();
            da.Fill(tb);
            //do dl tu tb vao grv
           dataGridView1.DataSource = tb;
           dataGridView1.Refresh();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void grvthuvien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string p_tendocgia = txtmuon.Text.Trim();

            //Tạo kết nối DB

            //B1: Kết nối đến DB
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-S4ESN68K\\MAYAO2;Initial Catalog=QLThuVien;Integrated Security=True");
            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Phieumuon_f", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Hovaten", SqlDbType.NVarChar, 50).Value = p_tendocgia;

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //do dl vao dataTable
            DataTable tb = new DataTable();
            da.Fill(tb);
            //do dl tu tb vao grv
            grvthuvien.DataSource = tb;
            grvthuvien.Refresh();
            txtmuon.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {
            dataGridView2_Load();
        }

        private void dataGridView2_Load()
        {
            //B1: Kết nối đến DB
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-S4ESN68K\\MAYAO2;Initial Catalog=QLThuVien;Integrated Security=True");
            con.Open();
            //B2: tạo đối tượng command
            SqlCommand cmd = new SqlCommand("Select*from Phieutra", con);
        cmd.ExecuteNonQuery();
            //B3: Đổ dữ liệu từ cmd vào DataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
            //b4: Đổ dữ liệu từ da vào bảng
            DataTable tb = new DataTable();
        da.Fill(tb);
            DataTable tb1 = new DataTable();
        da.Fill(tb1);
            dataGridView2.DataSource = tb1;
            dataGridView2.Refresh();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu từ các control
            string p_maphieut = txtmpt.Text.Trim();
            string p_madocgia = madocgia.Text.Trim();
            string p_hoten = hoten.Text.Trim();
            string p_masach = masach.Text.Trim();
            string p_tensach = tensach.Text.Trim();
            int p_soluong = int.Parse(soluong.Text.Trim());
            string p_tinhtrang = txttinhtrang.Text.Trim();

            DateTime p_ngaymuonsach = dateTimePicker2.Value;
            DateTime p_ngaytrasach = dateTimePicker1.Value;
            //Tạo kết nối DB

            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Phieutra_Ins", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Maphieutra", SqlDbType.NVarChar, 50).Value = p_maphieut;
            cmd.Parameters.Add("@Madocgia", SqlDbType.NVarChar, 50).Value = p_madocgia;
            cmd.Parameters.Add("@Hovaten", SqlDbType.NVarChar, 50).Value = p_hoten;
            cmd.Parameters.Add("@masach", SqlDbType.NVarChar, 50).Value = p_masach;
            cmd.Parameters.Add("@tensach", SqlDbType.NVarChar, 50).Value = p_tensach;
            cmd.Parameters.Add("@soluong", SqlDbType.Int).Value = p_soluong;
            cmd.Parameters.Add("@ngaymuonsach", SqlDbType.Date).Value = p_ngaymuonsach;
            cmd.Parameters.Add("@ngaytrasach", SqlDbType.Date).Value = p_ngaytrasach;
            cmd.Parameters.Add("@tinhtrang", SqlDbType.NVarChar, 50).Value = p_tinhtrang;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            dataGridView2_Load();
            MessageBox.Show("Thêm thành công!");
            txtmpt.Clear();
            madocgia.Clear();
            hoten.Clear();
            masach.Clear();
            tensach.Clear();
            soluong.Clear();
            txttinhtrang.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu từ các control
            string p_maphieut = txtmpt.Text.Trim();
            string p_madocgia = madocgia.Text.Trim();
            string p_hoten = hoten.Text.Trim();
            string p_masach = masach.Text.Trim();
            string p_tensach = tensach.Text.Trim();
            int p_soluong = int.Parse(soluong.Text.Trim());
            string p_tinhtrang = txttinhtrang.Text.Trim();

            DateTime p_ngaymuonsach = dateTimePicker2.Value;
            DateTime p_ngaytrasach = dateTimePicker1.Value;
            //Tạo kết nối DB

            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Phieutra_Upd", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Maphieumuon", SqlDbType.NVarChar, 50).Value = p_maphieut;
            cmd.Parameters.Add("@Madocgia", SqlDbType.NVarChar, 50).Value = p_madocgia;
            cmd.Parameters.Add("@Hovaten", SqlDbType.NVarChar, 50).Value = p_hoten;
            cmd.Parameters.Add("@masach", SqlDbType.NVarChar, 50).Value = p_masach;
            cmd.Parameters.Add("@tensach", SqlDbType.NVarChar, 50).Value = p_tensach;
            cmd.Parameters.Add("@soluong", SqlDbType.Int).Value = p_soluong;
            cmd.Parameters.Add("@ngaymuonsach", SqlDbType.Date).Value = p_ngaymuonsach;
            cmd.Parameters.Add("@ngaytrasach", SqlDbType.Date).Value = p_ngaytrasach;
            cmd.Parameters.Add("@tinhtrang", SqlDbType.NVarChar, 50).Value = p_tinhtrang;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            dataGridView2_Load();
            MessageBox.Show("Sửa thành công!");
            txtmpt.Clear();
            madocgia.Clear();
            hoten.Clear();
            masach.Clear();
            tensach.Clear();
            soluong.Clear();
            txttinhtrang.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string p_maphieut = txtmpt.Text.Trim();
            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Phieutra_Del", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Maphieutra", SqlDbType.NVarChar, 50).Value = p_maphieut;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            dataGridView2_Load();
            MessageBox.Show("Xoa thành công!");
            txtmpt.Clear();
            madocgia.Clear();
            hoten.Clear();
            masach.Clear();
            tensach.Clear();
            soluong.Clear();
            txttinhtrang.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string p_tendocgia = txtnguoitra.Text.Trim();

            //Tạo kết nối DB

            //B1: Kết nối đến DB
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-S4ESN68K\\MAYAO2;Initial Catalog=QLThuVien;Integrated Security=True");
            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Phieutra_f", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Hovaten", SqlDbType.NVarChar, 50).Value = p_tendocgia;

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //do dl vao dataTable
            DataTable tb = new DataTable();
            da.Fill(tb);
            //do dl tu tb vao grv
            dataGridView2.DataSource = tb;
           dataGridView2.Refresh();
            txtnguoitra.Clear();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtmpt.Text = dataGridView2.Rows[i].Cells[0].Value.ToString();
            madocgia.Text = dataGridView2.Rows[i].Cells[1].Value.ToString();
            hoten.Text = dataGridView2.Rows[i].Cells[2].Value.ToString();
            masach.Text = dataGridView2.Rows[i].Cells[3].Value.ToString();
           tensach.Text = dataGridView2.Rows[i].Cells[4].Value.ToString();
            soluong.Text = dataGridView2.Rows[i].Cells[5].Value.ToString();
            dateTimePicker2.Text = dataGridView2.Rows[i].Cells[6].Value.ToString();
            dateTimePicker1.Text = dataGridView2.Rows[i].Cells[7].Value.ToString();
            txttinhtrang.Text = dataGridView2.Rows[i].Cells[8].Value.ToString();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            string maphieumuon = txtmpm.Text;
            string madocgia = txtmadocgia.Text;
            string hoten = ht.Text;
            string diachi = dc.Text;
            string tensach = txttensach.Text;
            string soluong = txtsoluong.Text;
            string ngaymuon = dtngaymuon.Text;
            string ngaytra = dtngaytra.Text;
            Form f = new Form3(maphieumuon, madocgia,hoten, diachi, tensach, soluong, ngaymuon,ngaytra);
            f.Show();
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
            head.Value2 = "THE DOC GIA";
            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = "18";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            // Tạo tiêu đề cột 
            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "MA PHIEU MUON ";
            cl1.ColumnWidth = 7.5;
            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "MA DOC GIA";

            cl2.ColumnWidth = 25.0;
            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
            cl2.Value2 = "HO VA TEN";

            cl2.ColumnWidth = 25.0;
            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
            cl3.Value2 = "DIA CHI";
            cl3.ColumnWidth = 25.0;
            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
            cl4.Value2 = "TEN SACH";
            cl4.ColumnWidth = 15.0;
            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
            cl4.Value2 = "SO LUONG";
            cl4.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("G3", "G3");
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

        private void button4_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu từ các control
            string p_maphieum = txtmpm.Text.Trim();
            string p_madocgia = txtmadocgia.Text.Trim();
            string p_hoten = ht.Text.Trim();
            string p_diachi = dc.Text.Trim();
            string p_tensach = txttensach.Text.Trim();
            int p_soluong = int.Parse(txtsoluong.Text.Trim());
            DateTime p_ngaymuonsach = dtngaymuon.Value;
            DateTime p_ngaytrasach = dtngaytra.Value;

            //Tạo kết nối DB

            con.Open();

            //Tạo đối tượng command để đẩy dl vào DB
            SqlCommand cmd = new SqlCommand("Phieumuon_Excel", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Maphieumuon", SqlDbType.NVarChar, 50).Value = p_maphieum;
            cmd.Parameters.Add("@Madocgia", SqlDbType.NVarChar, 50).Value = p_madocgia;
            cmd.Parameters.Add("@Hovaten", SqlDbType.NVarChar, 50).Value = p_hoten;
            cmd.Parameters.Add("@Diachi", SqlDbType.NVarChar, 50).Value = p_diachi;
            cmd.Parameters.Add("@tensach", SqlDbType.NVarChar, 50).Value = p_tensach;
            cmd.Parameters.Add("@soluong", SqlDbType.Int).Value = p_soluong;
            cmd.Parameters.Add("@ngaymuonsach", SqlDbType.Date).Value = p_ngaymuonsach;
            cmd.Parameters.Add("@ngaytrasach", SqlDbType.Date).Value = p_ngaytrasach;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            //b4 tao dataAdapter do data tu cmd
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //b5 do data tu da vao datatble
            DataTable tb = new DataTable();
            da.Fill(tb);
            XuatEcel(tb, "HoaDon");
        }

        private void grvsach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grvdocgia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}