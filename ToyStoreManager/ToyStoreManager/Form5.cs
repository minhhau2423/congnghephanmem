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

namespace ToyStoreManager
{
    public partial class FormNV : Form
    {
        SqlConnection conn;
        public FormNV()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            MaximumSize = MinimumSize = new System.Drawing.Size(710, 480);
            Screen scr = Screen.PrimaryScreen; //đi lấy màn hình chính
            this.Left = (scr.WorkingArea.Width - this.Width) / 2;
            this.Top = (scr.WorkingArea.Height - this.Height) / 2;

            conn = new SqlConnection(@"Data Source=LAPTOP-7P911SC5;Initial Catalog=ToyStoreManager;Integrated Security=True");
            conn.Open();
            HienThiNV();
        }
        public void HienThiNV()
        {
            string sqlSelect = "SELECT MaNV as 'Mã nhân viên', TenNV as 'Tên', GioiTinh as 'Giới tính',NgaySinh as 'Ngày sinh' , Email as 'Mail' , SDT as 'Số điện thoại', DiaChi as 'Địa chỉ', ViTri as 'Vị trí' FROM NhanVien";
            SqlCommand cmd = new SqlCommand(sqlSelect, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataXemNV.DataSource = dt;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
    }
}
