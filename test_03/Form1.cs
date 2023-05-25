using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace test_03
{

    public partial class Form1 : Form
    {
        Label firstClicked, secondClicked;
        public Form1()
        {
            InitializeComponent();
        }
        private List<string> icons = new List<string>()
        {
            "img1.png","img1.png","B","B","C","C","D","D",
            "E","E","F","F","G","G","H","H"
        };

        private Random random = new Random();

        private void AssignIconToSquare()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label label = control as Label;
                if (label != null)
                {
                    int index = random.Next(icons.Count);
                    label.Text = icons[index];
                    label.ForeColor = label.BackColor;
                    icons.RemoveAt(index);
                }
            }
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                {
                    return;
                }

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start();
            }
        }
        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label label = control as Label;
                if (label != null && label.ForeColor == label.BackColor)
                {
                    return;
                }
            }

            MessageBox.Show("Congratulations! You won the game!");
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

    }
}
