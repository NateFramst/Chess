using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class StartScreen : UserControl
    {
        public StartScreen()
        {
            InitializeComponent();

            startButton.Location = new Point((this.Width / 2) - (startButton.Width / 2), (this.Height / 2) - (startButton.Height / 2) - 50);
            exitButton.Location = new Point((this.Width / 2) - (exitButton.Width / 2), (this.Height / 2) - (exitButton.Height / 2) + 50);
            titleLabel.Location = new Point((this.Width / 2) - (titleLabel.Width / 2), (this.Height / 2) - (titleLabel.Height / 2) - 150);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Form1.changeScreens(this, new GameScreen());
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
