using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Media;
using XsmController;

namespace MainApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            panel1.Enabled = false;
        }

        ComController cont;
        List<TextBox> tbl = new List<TextBox>();

        private void btPortUpd_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(SerialPort.GetPortNames());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btPortUpd_Click(sender, e);
            numericUpDownC.Value = 20;
            numericUpDownR.Value = 4;
            numericUpDownL.Value = 2;
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            if(listBox1.SelectedIndex >=0)
            cont = new ComController(listBox1.SelectedItem.ToString());
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDownC_ValueChanged(object sender, EventArgs e)
        {
            foreach (var tb in tbl)
                Form1_TextChanged(tb, null);
        }

        private void numericUpDownR_ValueChanged(object sender, EventArgs e)
        {
            while (tbl.Count < numericUpDownR.Value)
            {
                tbl.Add(new TextBox());
                tbl[tbl.Count - 1].TextChanged += Form1_TextChanged;
               
            }

            while (tbl.Count > numericUpDownR.Value)
                tbl.RemoveAt(tbl.Count - 1);

            for (int i = 0; i < tbl.Count; i++)
            {
                var tb = tbl[i];
                tb.Parent = groupBox2;
                
                tb.Top = 20 + (tb.Height + 10) * i;
                tb.Left = 10;
                tb.Width = groupBox2.Width - 20;
                tb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Top;
            }
        }

        private void Form1_TextChanged(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb == null)
                return;
            int p = tb.SelectionStart;
            if (tb.Text.Length > (int)numericUpDownC.Value)
            {
                tb.Text = tb.Text.Substring(0, (int)numericUpDownC.Value);
                SystemSounds.Beep.Play();
            }
            if (p > tb.Text.Length)
                tb.SelectionStart = tb.Text.Length;
            else
                tb.SelectionStart = p;

        }

        private void numericUpDownL_ValueChanged(object sender, EventArgs e)
        {
            while(flowLayoutPanel1.Controls.Count < numericUpDownL.Value)
            {
                Button bt = new Button();
                bt.BackColor = Color.White;
                bt.Height = bt.Width = 35;
                bt.Click += Bt_Click;                
                bt.Text = flowLayoutPanel1.Controls.Count.ToString("D2");
                bt.Parent = flowLayoutPanel1;
            }

            while (flowLayoutPanel1.Controls.Count > numericUpDownL.Value)
                flowLayoutPanel1.Controls.RemoveAt(flowLayoutPanel1.Controls.Count - 1);            
        }

        private void Bt_Click(object sender, EventArgs e)
        {
            var bt = sender as Button;
            if (bt == null) return;
            colorDialog1.Color = bt.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                bt.BackColor = colorDialog1.Color;
            cont.SendCommand(Commands.led_color, Convert.ToInt32(bt.Text), bt.BackColor);
        }

        private void btStringSend_Click(object sender, EventArgs e)
        {
            for(int i=0;i<tbl.Count;i++)
            {
                cont.SendCommand(Commands.Led_string, i, tbl[i].Text);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Color c = Color.FromArgb(trackBar1.Value, 100 - trackBar1.Value, 0);
            Color c2 = Color.FromArgb(c.R * 2, c.G * 2, 0);
            panel2.BackColor = c2;
        }
    }
}
