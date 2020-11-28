using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToyStoreManager
{
    public partial class FormChangePass : Form
    {
        public FormChangePass()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormChangePass_Load(object sender, EventArgs e)
        {
            MaximumSize= MinimumSize = new System.Drawing.Size(410,400);
            Screen scr = Screen.PrimaryScreen; //đi lấy màn hình chính
            this.Left = (scr.WorkingArea.Width  - this.Width)/ 2;
            this.Top = (scr.WorkingArea.Height - this.Height)/ 2;
        }
    }
}
