using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Do_an_chuoi_ki_mon_co_so
{
    public partial class Hard : Form
    {


        int wrongAttempts = 0;




        public string playerName;


        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "i","i","N","N",",",",","k","k",
            "b","b","w","w","z","z","u","u",
            "f", "f","m","m","s","s","q","q",
            "j","j","l","l","h","h"
        };

        Label firstClicked, secondClicked;
        private SoundPlayer backgroundMusicPlayer;
        private SoundPlayer winSoundPlayer;
        private SoundPlayer loseSoundPlayer;
        private SoundPlayer flipCardSoundPlayer;

        public Hard()
        {
            InitializeComponent();
            InitializeSoundPlayers();
            AssignIconToSquares();
            PlayBackgroundMusic();


            // Initialize the flip card sound player object
            flipCardSoundPlayer = new SoundPlayer(@"C:\Users\USER\Music\flipcard-91468.wav");

            // Initialize the win sound player object
            winSoundPlayer = new SoundPlayer(@"C:\Users\USER\Music\wingame.wav");

            // Initialize the background music sound player object
            backgroundMusicPlayer = new SoundPlayer(@"C:\Users\USER\Music\Lobby-Time.wav");

            loseSoundPlayer = new SoundPlayer(@"C:\Users\USER\Music\wrongwav.wav");

            // Tạo thanh menu "File"
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");

            // Thêm tùy chọn "New game" vào menu
            ToolStripMenuItem newGameItem = new ToolStripMenuItem("New game");
            newGameItem.Click += new EventHandler(NewGame_Click);
            fileMenu.DropDownItems.Add(newGameItem);

            // Thêm tùy chọn "Exit" vào menu
            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += new EventHandler(Exit_Click);
            fileMenu.DropDownItems.Add(exitItem);

            // Thêm thanh menu vào thanh menu của biểu mẫu
            menuStrip1.Items.Add(fileMenu);

            // Set the text of the time remaining label to 60 seconds
            labelTimeRemaining.Text = "   Time: 60 seconds";

            // Start the countdown timer
            timer2.Start();
        }
        private void InitializeSoundPlayers()
        {
            backgroundMusicPlayer = new SoundPlayer(@"C:\Users\USER\Music\Lobby-Time.wav");
            winSoundPlayer = new SoundPlayer(@"C:\Users\USER\Music\wingame.wav");
            loseSoundPlayer = new SoundPlayer(@"C:\Users\USER\Music\wrongwav.wav");
            flipCardSoundPlayer = new SoundPlayer(@"C:\Users\USER\Music\flipcard-91468.wav");

        }
        private void PlayBackgroundMusic()
        {
            // Check if the background music player is not null
            if (backgroundMusicPlayer != null)
            {
                backgroundMusicPlayer.PlayLooping();
            }
        }

        private void StopBackgroundMusic()
        {
            // Check if the background music player is not null
            if (backgroundMusicPlayer != null)
            {
                backgroundMusicPlayer.Stop();
            }
        }

        private void PlayFlipCardSound()
        {
            flipCardSoundPlayer.Play();
        }

        private void PlayWinSound()
        {
            winSoundPlayer.Play();
        }

        private void PlayLoseSound()
        {
            loseSoundPlayer.Play();
        }


        private void NewGame_Click(object sender, EventArgs e)
        {
            Label label;
            int randomNumber;


            wrongAttempts = 0;


            // Set the text of the time remaining label to 60 seconds
            labelTimeRemaining.Text = "   Time: 60 seconds";

            for (int i = 0; i < tableLayoutPanel1.Controls.Count && i < icons.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                {
                    label = (Label)tableLayoutPanel1.Controls[i];
                    randomNumber = random.Next(0, icons.Count);
                    label.Text = icons[randomNumber];
                    icons.RemoveAt(randomNumber);
                }
            }
            PlayFlipCardSound();
            ResetGame();
        }


        private void Exit_Click(object sender, EventArgs e)
        {
            StopBackgroundMusic();
            Close();
        }
        private void AssignIconToSquares()
        {
            Label label;
            int randomNumber;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                    label = (Label)tableLayoutPanel1.Controls[i];
                else
                    continue;

                if (icons.Count == 0)
                {
                    MessageBox.Show("No more icons available.");
                    return;
                }

                randomNumber = random.Next(0, icons.Count);
                label.Text = icons[randomNumber];

                icons.RemoveAt(randomNumber);
            }
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelClick(object sender, EventArgs e)
        {
            if (firstClicked != null && secondClicked != null)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel == null)
                return;
            if (clickedLabel.ForeColor == Color.Black)
                return;
            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                PlayFlipCardSound();
                return;
            }

            secondClicked = clickedLabel;

            secondClicked.ForeColor = Color.Black;

            if (firstClicked.Text == secondClicked.Text)
            {


                firstClicked = null;
                secondClicked = null;
                dieu_kien_thang();
            }
            else
            {
                // Nếu chọn sai
                wrongAttempts++;


                timer1.Start(); // delay 0.5s trước khi đóng 2 ô

                if (wrongAttempts == 30) // Nếu người chơi đã chọn sai quá 30 lần
                {
                    loseSoundPlayer.Play();
                    MessageBox.Show("You've been wrong too many times!");
                    Close();
                }
                else
                {
                    dieu_kien_thang();
                }
            }
        }


        private void dieu_kien_thang()
        {
            Label hinh;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                hinh = tableLayoutPanel1.Controls[i] as Label;

                if (hinh != null && hinh.ForeColor == hinh.BackColor)
                    return;
            }


            PlayWinSound();
            Close();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // Decrease the time remaining by 1 second
            int timeRemaining = int.Parse(labelTimeRemaining.Text.Replace("   Time: ", "").Replace(" seconds", ""));
            timeRemaining--;
            labelTimeRemaining.Text = "   Time: " + timeRemaining.ToString() + " seconds";

            // If the time remaining is 0, stop the timer and show a message box
            if (timeRemaining == 0)
            {
                timer2.Stop();
                StopBackgroundMusic();
                PlayLoseSound();
                MessageBox.Show("Time up!");
                Close();
            }
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            PlayBackgroundMusic();
            this.Hide(); // Ẩn form hiện tại 
            SelectLevelForm Back = new SelectLevelForm(); // Tạo instance của form SelectLevelForm
            Back.ShowDialog(); // Hiển thị form Level
        }

        private void labelTimeRemaining_Click(object sender, EventArgs e)
        {

        }
        private void ResetGame()
        {

            wrongAttempts = 0;


            labelScore.Text = "0"; // Đặt điểm số về 0

            // Đặt lại màu chữ của tất cả các ô
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Label label)
                {
                    label.ForeColor = label.BackColor;
                }
            }

            AssignIconToSquares(); // Gán lại biểu tượng cho các ô

            // Đặt lại thời gian còn lại về 60 giây
            labelTimeRemaining.Text = "   Time: 60 seconds";
            timer2.Start(); // Khởi động lại đồng hồ đếm thời gian
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PlayBackgroundMusic();

        }
    }
}
