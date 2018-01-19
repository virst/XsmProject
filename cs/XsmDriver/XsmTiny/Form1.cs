using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XsmTiny
{
    public partial class Form1 : Form
    {
        private SerialPort sp;
        delegate void LizyCallback();

        public Form1()
        {
            InitializeComponent();

            sp = new SerialPort(Program.AppParams["port"]);
            sp.RtsEnable = true;
            sp.DataReceived += Sp_DataReceived;
            timer1.Interval = Convert.ToInt32(Program.AppParams["interval"]);
        }

        private string tmp_data = "";
        private int tmp_v = 0;

        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var s = sp.ReadLine().Split(';');
            if (s.Length < 2) return;
            tmp_data = s[0];
            int.TryParse(s[1], out tmp_v);

            void Sd()
            {
                textBox1.Text = tmp_data;
                numericUpDown1.Value = tmp_v;
            }

            this.Invoke((LizyCallback)Sd, new object[] { });
        }

        private readonly byte[] _bb = { 0, 0, 0 };

        void SendVal(byte n, int val)
        {
            _bb[0] = n;
            _bb[1] = (byte)((val / 100) % 100);
            _bb[2] = (byte)(val % 100);
            sp.Write(_bb, 0, 3);
        }

        void SendVal(byte n, string val)
        {
            _bb[0] = n;
            sp.Write(_bb, 0, 3);
            sp.WriteLine(val);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendVal(100, (int)numericUpDown1.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendVal(101, textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SendVal(200,0);
        }
    }
}
