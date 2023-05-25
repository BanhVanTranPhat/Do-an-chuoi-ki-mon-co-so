using System;
using System.Windows.Forms;

namespace Do_an_chuoi_ki_mon_co_so
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form hiện tại 
            SelectLevelForm Back = new SelectLevelForm(); // Tạo instance của form SelectLevelForm
            Back.ShowDialog(); // Hiển thị form Level
        }
    }
}
