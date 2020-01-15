using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms.Forms
{
    public partial class LoadingForm : Form
    {
        private const int max = 255, min = 0, step = 30;
        private int r = max, g = min, b = min, index = 0;
        private TextBox[] boxes;
        public Color Color;

        //public bool Hidden { get; set; }

        public bool Closing
        {
            set
            {
                if (value)
                {
                    Close();
                    Dispose();
                }
            }
        }
        public int Index
        {
            get => index;
        }

        public LoadingForm(Color c)
        {
            Form1.loadingForm = this;
            r = c.R;
            g = c.G;
            b = c.B;
            //index = i;
            Color = c;

            InitializeComponent();
            //CenterToScreen();
            CenterToParent();
            boxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
            foreach (var box in boxes)
            {
                box.BackColor = Color;
            }
            //boxes.ToList().ForEach(x => x.BackColor = Color);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //if (this.Visible) return;

            if (r >= max && b <= min)
                g += step;
            if (g >= max && b <= min)
                r -= step;
            if (r <= min && g >= max)
                b += step;
            if (b >= max && r <= min)
                g -= step;
            if (g <= min && b >= max)
                r += step;
            if (r >= max && g <= min)
                b -= step;

            r = r > max ? max : r < min ? min : r;
            g = g > max ? max : g < min ? min : g;
            b = b > max ? max : b < min ? min : b;

            index = index < boxes.Count() - 1 ? index + 1 : 0;
            boxes[index].BackColor = Color.FromArgb(r, g, b);
            Color = Color.FromArgb(index, r, g, b);
        }
    }
}
