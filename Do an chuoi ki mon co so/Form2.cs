using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Do_an_chuoi_ki_mon_co_so
{
    public partial class Form2 : Form
    {
        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "i", "i", "N", "N", ",", ",", "k", "k",
            "b", "b", "w", "w"
        };
        Label firstClicked, secondClicked;

        private SoundPlayer flipCardSoundPlayer;
        private SoundPlayer winSoundPlayer;
        private SoundPlayer backgroundMusicPlayer;

        public Form2()
        {
            InitializeComponent();
            AssignIconToSquares();

            // Initialize the flip card sound player object
            flipCardSoundPlayer = new SoundPlayer(@"C:\Users\USER\Music\flipcard-91468.wav");

            // Initialize the win sound player object
            winSoundPlayer = new SoundPlayer(@"C:\Users\USER\Music\wingame.wav");

            // Initialize the background music sound player object
            backgroundMusicPlayer = new SoundPlayer(@"C:\Users\USER\Music\Lobby-Time.wav");

            // Set the text of the time remaining label to 60 seconds
            labelTimeRemaining.Text = "   Time: 60 seconds";

            // Start the countdown timer
            timer2.Start();
        }

        private void PlayWinSound()
        {
            try
            {
                winSoundPlayer.Play();
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Failed to play win sound:");
            }
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

        private void label1_Click(object sender, EventArgs e)
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

            dieu_kien_thang(); //ham kiem tra thang cuoc

            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
            }
            else
                timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            if (firstClicked != null && secondClicked != null)
            {
                if (firstClicked.ForeColor != null && firstClicked.BackColor != null)
                    firstClicked.ForeColor = firstClicked.BackColor;

                if (secondClicked.ForeColor != null && secondClicked.BackColor != null)
                    secondClicked.ForeColor = secondClicked.BackColor;
            }

            firstClicked = null;
            secondClicked = null;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            PlayBackgroundMusic();
        }

        private void CheckForWin()
        {
            Label hinh;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                hinh = tableLayoutPanel1.Controls[i] as Label;

                if (hinh != null && hinh.ForeColor == hinh.BackColor)
                    return;
            }

            PlayWinSound();
            MessageBox.Show(this, "You Win!!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void PlayFlipCardSound()
        {
            flipCardSoundPlayer.Play();
        }

        private void PlayBackgroundMusic()
        {
            backgroundMusicPlayer.PlayLooping();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            backgroundMusicPlayer.Stop();
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
                MessageBox.Show("Time up!");
                Close();
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

        private void back_btn_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form hiện tại 
            SelectLevelForm Back = new SelectLevelForm(); // Tạo instance của form SelectLevelForm
            Back.ShowDialog(); // Hiển thị form Level
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void ResetGame()
        {
            // Clear the clicked labels and reset their appearance
            if (firstClicked != null)
            {
                firstClicked.ForeColor = firstClicked.BackColor;
                firstClicked = null;
            }
            if (secondClicked != null)
            {
                secondClicked.ForeColor = secondClicked.BackColor;
                secondClicked = null;
            }

            // Store the first icon selected
            string firstIcon = "";

            // Check if firstClicked is not null and assign the first icon to firstIcon
            if (firstClicked != null)
            {
                firstIcon = firstClicked.Text;
            }

            // Reset the icon assignment
            icons = new List<string>()
            {
                firstIcon, "i", "i", "N", "N", ",", ",", "k", "k",
                "b", "b", "w", "w"
            };

            // Shuffle the icons
            AssignIconToSquares();

            // Reset the time remaining label
            labelTimeRemaining.Text = "   Time: 60 seconds";

            // Restart the countdown timer
            timer2.Stop();
            timer2.Start();
        }
        private void menuStrip1_ItemClicked(object sender, EventArgs e)
        {

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
            MessageBox.Show(this, "You Win!!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            Close();
        }
    }
}
