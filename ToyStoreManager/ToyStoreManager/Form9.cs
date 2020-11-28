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
    public partial class Form9 : Form
    {
        SqlConnection conn;
        int sl;
        public Form9()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void Form9_Load(object sender, EventArgs e)
        {
            MaximumSize = MinimumSize = new System.Drawing.Size(690, 627);
            Screen scr = Screen.PrimaryScreen; //đi lấy màn hình chính
            this.Left = (scr.WorkingArea.Width - this.Width) / 2;
            this.Top = (scr.WorkingArea.Height - this.Height) / 2;

            conn = new SqlConnection(@"Data Source=LAPTOP-7P911SC5;Initial Catalog=ToyStoreManager;Integrated Security=True");
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT MaNV FROM NhanVien WHERE ViTri like 'Bán hàng'  ", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbbMaNV.DisplayMember = "MaNV";
            cbbMaNV.ValueMember = "MaNV";
            cbbMaNV.DataSource = dt;

            SqlDataAdapter da1 = new SqlDataAdapter("SELECT MaSP FROM SanPham ", conn);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            cbbMaSP.DisplayMember = "MaSP";
            cbbMaSP.ValueMember = "MaSP";
            cbbMaSP.DataSource = dt1;
            MaHD();
            MaKH();


           


        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView.AllowUserToAddRows = false;
            ADD_VALUE();
        }
        private void ADD_VALUE()
        {
            dataGridView.Rows.Add(1);
            int indexRow = dataGridView.Rows.Count - 1;
            dataGridView[0, indexRow].Value = cbbMaSP.Text;
            dataGridView[1, indexRow].Value = txtTenSP.Text;
            dataGridView[2, indexRow].Value = txtDonGia.Text;
            dataGridView[3, indexRow].Value = txtSoLuong.Text;
            dataGridView[4, indexRow].Value = txtGiamGia.Text;
            dataGridView[5, indexRow].Value = txtThanhTien.Text;
            int tmp = int.Parse(txtThanhTien.Text);
            if (txtTongTien.Text == "")
            {
                txtTongTien.Text= txtThanhTien.Text;
            }
            else
            {
                txtTongTien.Text = (int.Parse(txtTongTien.Text) + tmp).ToString();
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int RowIndex = dataGridView.CurrentRow.Index;
            txtTongTien.Text = (int.Parse(txtTongTien.Text) - int.Parse(dataGridView[5, RowIndex].Value.ToString())).ToString();
            dataGridView.Rows.RemoveAt(RowIndex);
        }

        private void cbbMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT TenNV FROM NhanVien WHERE MaNV=@MaNV", conn);
            cmd.Parameters.AddWithValue("MaNV", cbbMaNV.Text.ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable d = new DataTable();
            d.Load(dr);

            txtTenNV.Text = d.Rows[0][0].ToString();
        }

        private void cbbMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT TenSP, DonGia, Soluong FROM SanPham WHERE MaSP=@MaSP", conn);
            cmd.Parameters.AddWithValue("MaSP", cbbMaSP.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable d = new DataTable();
            d.Load(dr);
            txtTenSP.Text = d.Rows[0][0].ToString();
            txtDonGia.Text = d.Rows[0][1].ToString();
            sl = int.Parse(d.Rows[0][2].ToString());
            thanhtien();

        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void txtSoLuong_Leave(object sender, EventArgs e)
        {
            if (int.Parse(txtSoLuong.Text) > sl)
            {
                MessageBox.Show("Số lượng sản phẩm còn lại không đủ.");
                txtSoLuong.Clear();
                txtSoLuong.Focus();
            }else thanhtien();
        }
        public void thanhtien()
        {
            if (txtGiamGia.Text != "")
            {
                int x = (int.Parse(txtSoLuong.Text) * int.Parse(txtDonGia.Text));
                txtThanhTien.Text = (x - x/100 * int.Parse(txtGiamGia.Text.ToString())).ToString();
            }
        }

        private void txtGiamGia_Leave(object sender, EventArgs e)
        {
            thanhtien();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtTenKH.Text == "")
            {
                MessageBox.Show("Hãy điền thông tin khách hàng.");
            }
            else { 
                Add_Khach_hang();
                Add_HoaDon();
                Add_ChiTietHoaDon();
                MessageBox.Show("Đã lưu Hóa đơn.");
            }

            
        }

        public void Add_Khach_hang()
        {
            String query = "INSERT INTO KhachHang VALUES(@MaKH, @TenKH, @SdtKH, @DiaChiKH)";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("MaKH", txtMaKH.Text);
            command.Parameters.AddWithValue("TenKH", txtTenKH.Text);
            command.Parameters.AddWithValue("SdtKH", txtSDT.Text);
            command.Parameters.AddWithValue("DiaChiKH", txtDiaChi.Text);
            command.ExecuteNonQuery();
        }
        public void Add_HoaDon()
        {
            String query = "INSERT INTO HoaDon VALUES(@MaHD, @MaNV, @MaKH, @NgayLap, @TongTien)";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("MaHD", txtMaHD.Text);
            command.Parameters.AddWithValue("MaNV", cbbMaNV.Text);
            command.Parameters.AddWithValue("MaKH", txtMaKH.Text);
            command.Parameters.AddWithValue("NgayLap", ngaylap.Value);
            command.Parameters.AddWithValue("TongTien", txtTongTien.Text);
            command.ExecuteNonQuery();
        }
        public void Add_ChiTietHoaDon()
        {
            
            int count = dataGridView.Rows.Count ;
            for (int i = 0; i < count; i++) {
                String query = "INSERT INTO ChiTietHoaDon VALUES(@MaHD, @SanPham, @SoLuong, @ThanhTien)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("MaHD", txtMaHD.Text);
                command.Parameters.AddWithValue("SanPham", dataGridView[0, i].Value);
                command.Parameters.AddWithValue("SoLuong", dataGridView[3, i].Value);
                command.Parameters.AddWithValue("ThanhTien", dataGridView[5, i].Value);
                command.ExecuteNonQuery();


                //

                String query1 = "UPDATE SanPham SET SoLuong = @SoLuong WHERE MaSP=@MaSP ";
                SqlCommand command1 = new SqlCommand(query1, conn);
                sl = (sl - int.Parse(dataGridView[3, i].Value.ToString()));
                command1.Parameters.AddWithValue("SoLuong", sl);
                command1.Parameters.AddWithValue("MaSP", dataGridView[0, i].Value);
                command1.ExecuteNonQuery();


                String query2 = "UPDATE Kho SET NgayXuatGanNhat = @Ngay, SoLuongXuatGanNhat=@sl WHERE MaSP=@MaSP ";
                SqlCommand command2 = new SqlCommand(query2, conn);
                command2.Parameters.AddWithValue("sl", dataGridView[3, i].Value);
                command2.Parameters.AddWithValue("ngay", ngaylap.Value);
                command2.Parameters.AddWithValue("MaSP", dataGridView[0, i].Value);
                command2.ExecuteNonQuery();



            }
        }
        public string RandomChar(int numberRD)
        {
            string randomStr = "";
            try
            {
                string[] myIntArray = new string[numberRD];
                int x;
                Random autoRand = new Random();
                for (x = 0; x < numberRD; x++)
                {
                    myIntArray[x] = Convert.ToChar(Convert.ToInt32(autoRand.Next(65, 87))).ToString();
                    randomStr += (myIntArray[x].ToString());
                }
            }
            catch (Exception ex)
            {
                randomStr = "error";
            }
            return randomStr;
        }
        public void MaHD()
        {
            txtMaHD.Text = "HD_"+RandomChar(5);
            string sqlSelect = "SELECT count(*) FROM HoaDon WHERE MaHD = @MaHD";
            SqlCommand cmd = new SqlCommand(sqlSelect, conn);
            cmd.Parameters.AddWithValue("MaHD", txtMaHD.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            if (dt.Rows[0][0].ToString() != "0")
            {
                txtMaHD.Text = "HD_"+RandomChar(5);
            }
        }
        public void MaKH()
        {
            txtMaKH.Text = "KH_" + RandomChar(5);
            string sqlSelect = "SELECT count(*) FROM KhachHang WHERE MaKH = @MaKH";
            SqlCommand cmd = new SqlCommand(sqlSelect, conn);
            cmd.Parameters.AddWithValue("MaKH", txtMaKH.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            if (dt.Rows[0][0].ToString() != "0")
            {
                txtMaKH.Text = "KH_" + RandomChar(5);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }
    }
}
