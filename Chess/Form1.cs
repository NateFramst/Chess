using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Chess
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

            changeScreens(this, new StartScreen());
        }

        public static void changeScreens(object sender, UserControl next)
        {
            Form f;
            if (sender is Form)
            {
                f = (Form)sender;
            }
            else
            {
                UserControl current = (UserControl)sender;
                f = current.FindForm();
                f.Controls.Remove(current);
            }
            next.Location = new Point((f.Width - next.Width) / 2, (f.Height - next.Height) / 2);
            f.Controls.Add((next));
        }
    }
}