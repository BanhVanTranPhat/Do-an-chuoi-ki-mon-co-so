using System;
using System.Windows.Forms;

namespace Do_an_chuoi_ki_mon_co_so
{
    public partial class SelectLevelForm : Form
    {
        public SelectLevelForm()
        {
            InitializeComponent();
        }

        private void btnEasy_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form hiện tại (SelectLevelForm)
            Form2 easyForm = new Form2(); // Tạo instance của form EasyForm
            easyForm.ShowDialog(); // Hiển thị form EasyForm
        }

        private void btnMedium_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form hiện tại (SelectLevelForm)
            Form3 mediumForm = new Form3(); // Tạo instance của form MediumForm
            mediumForm.ShowDialog(); // Hiển thị form MediumForm
        }

        private void btnHard_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form hiện tại (SelectLevelForm)
            Hard hardForm = new Hard(); // Tạo instance của form Hard
            hardForm.ShowDialog(); // Hiển thị form Hard
        }

        private void SelectLevelForm_Load(object sender, EventArgs e)
        {

        }

        void button1_Click(object sender, EventArgs e)
        {

            this.Hide(); // Ẩn form hiện tại (SelectLevelForm)
            Form6 supportForm = new Form6(); // Tạo instance của form Hard
            supportForm.ShowDialog(); // Hiển thị form Hard
        }
    }
}



