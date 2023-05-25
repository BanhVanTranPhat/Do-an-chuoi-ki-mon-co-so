using System;
using System.Windows.Forms;

namespace Do_an_chuoi_ki_mon_co_so
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Tạo một instance của form đăng nhập và hiển thị nó
            Application.Run(new Form4());
        }
    }
}
