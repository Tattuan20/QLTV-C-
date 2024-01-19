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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String p_user = "admin";
            string p_password = "123";
            p_user = txtus.Text.Trim();
            p_password = txtpw.Text.Trim();

            if (txtus.Text == "admin" && txtpw.Text == "123")
            {

                MessageBox.Show("Đăng nhập thành công", "Thông báo");
                Form f2 = new Form1();
               f2.Show();
                this.Hide();

            }
            else if (txtus.Text == "" || txtpw.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!!", "Thông báo");
                
            }
            else
            {

                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!!", "Thông báo");
                txtus.Focus();
                txtus.BackColor = Color.Black;
                txtus.ForeColor = Color.Red;
                txtpw.Focus();
                txtpw.BackColor = Color.Black;
                txtpw.ForeColor = Color.Red;
            }
        }
    }
}
