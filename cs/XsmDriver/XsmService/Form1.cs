using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ViSysMon;
using XsmController;
using XsmService.Utils;


namespace XsmService
{
    public partial class Form1 : Form
    {
        private const string confFile = "XsmConfig.json";
        private XsmConfig config;
        PrivateFontCollection pfc = new PrivateFontCollection();
        List<Label> labels = new List<Label>();
        private int currenpPage = 0;
        private SysMonAnalyst sa = new SysMonAnalyst();
        ComController contr;

        public Form1()
        {
            InitializeComponent();


            pfc.AddFontFile(Path.Combine(Application.StartupPath, "SUBWT.ttf"));
            label1.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            label1.ForeColor = Color.Blue;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (false && File.Exists(confFile))
                config = JsonUtil<XsmConfig>.ObjFromStr(File.ReadAllText(confFile));
            else
            {
                config = new XsmConfig
                {
                    port = "COM8",
                    cols = 20,
                    rows = 4,
                    leds = 2,
                    Tick = 500,
                    diplays = new List<string[]>
                        {
                            new[] {"   *INFO-1*", "CPU:{0:D3}%{3:10}", "RAM USED {1:D4} MB", "{6}"},
                            new[] {"   *INFO-2*", "CPU:{0:D3}%{3:10}", "RAM:{5:D3}%{4:10}", "RAM USE {1:D4}/{2:D4}MB"},
                            new[] {"   *INFO-3*", "CPU:{0:D3}% RAM {1:D4}MB", "CPU{3:15}", "RAM{4:15}"}
                        }
                };

                File.WriteAllText(confFile, JsonUtil<XsmConfig>.ObjToStr(config));
            }

            for (int i = 0; i < config.rows; i++)
            {
                Label lb = new Label();
                lb.Left = label1.Left;
                lb.Top = label1.Top + (label1.Height + 20) * (i + 1);
                lb.Text = @"123"; lb.Width = this.Width;

                lb.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
                lb.ForeColor = Color.Blue;
                lb.Parent = this;

                labels.Add(lb);
            }

            contr = new ComController(config.port);
            contr.CommandReceived += Contr_CommandReceived;

            timer1.Interval = config.Tick;
            timer1.Enabled = true;

        }

        private void Contr_CommandReceived(object o, CommandInfo c)
        {
            if (c.command == Commands.click)
            {
                switch (c.p)
                {
                    case 4:
                        button2_Click(null, null); break;
                    case 5:
                        button3_Click(null, null); break;
                }
            }
        }

        void ShowRow(int n, string s)
        {
            // labels[n].Text = s.Normalize(config.cols);
            contr.SendCommand(Commands.Led_string, n, s);

        }

        ConsoleProgressBar prc1 = new ConsoleProgressBar(), prc_2 = new ConsoleProgressBar();

        private void timer1_Tick(object sender, EventArgs e)
        {
            var info = sa.GetSysStatus(true);
            int cpu_p = (int)info.UseCpu;
            int ram_a = (int)info.AvailableMemoryMB;
            int ram_t = (int)info.TotalMemoryMB;
            int ram_u = ram_t - ram_a;
            int ram_p = ram_u*100 / ram_t;

            for (int i = 0; i < config.diplays[currenpPage].Length; i++)
            {
                string s = string.Format(config.diplays[currenpPage][i], cpu_p, ram_u, ram_t,
                    prc1.SetPrcInt(cpu_p),
                    prc_2.SetPrcInt(ram_p), ram_p,DateTime.Now);

                ShowRow(i, s);
            }
        }

        static int Incr(int n, int i, int m)
        {
            return (n + i + m) % m;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currenpPage = Incr(currenpPage, 1, config.diplays.Count);
            timer1_Tick(sender, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currenpPage = Incr(currenpPage, -1, config.diplays.Count);
            timer1_Tick(sender, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1_Tick(null, null);
        }
    }
}
