using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlythuvienn
{
    public partial class Form3 : Form

    {
        private String maphieumuon, madocgia,hoten, diachi,soluong, tensach,ngaymuon,ngaytra;

        public Form3()
        {
            InitializeComponent();
        }
        public Form3(string maphieumuon, string madocgia, string hoten, string diachi, string tensach, string soluong, string ngaymuon, string ngaytra) : this()
        {
            this.P_maphieumuon = maphieumuon;
            this.P_madocgia = madocgia;
            this.P_hoten = hoten;
            this.P_diachi = diachi;
            this.P_tensach = tensach;
            this.P_soluong = soluong;
            this.P_ngaymuon = ngaymuon;
            this.P_ngaytra = ngaytra;


        }
        public string P_maphieumuon { get; }
        public string P_madocgia { get; }
        public string P_hoten { get; }
        public string P_diachi { get; }
        public string P_tensach { get; }
        public string P_soluong{ get; }
        public string P_ngaymuon { get; }
        public string P_ngaytra{ get; }
        private void Form3_Load(object sender, EventArgs e)
        {
            lbmaphieu.Text = P_maphieumuon;
            lbmadocgia.Text = P_madocgia;
            lbhoten.Text = P_hoten;
            lbdiachi.Text = P_diachi;
            lbtensach.Text = P_tensach;
            lbsoluong.Text = P_soluong;
            lbngaymuon.Text = P_ngaymuon;
            lbngaytra.Text = P_ngaytra;

        }
    }
}
