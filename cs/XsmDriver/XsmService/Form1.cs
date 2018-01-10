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

        public Form1()
        {
            InitializeComponent();

            
            pfc.AddFontFile(Path.Combine(Application.StartupPath, "SUBWT.ttf"));
            label1.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            label1.ForeColor = Color.Blue;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
                if (File.Exists(confFile))
                    config = JsonUtil<XsmConfig>.ObjFromStr(File.ReadAllText(confFile));
                else
                {
                    config = new XsmConfig
                    {
                        cols = 20,
                        rows = 4,
                        leds = 2,
                        diplays = new List<string[]>
                        {
                            new[] {"   *INFO-1*", "CPU:{0:3}%{3:10}", "RAM USED {1:4} MB", ""},
                            new[] {"   *INFO-2*", "CPU:{0:3}%{3:10}", "RAN:{5:3}%{4:10}", "RAM USE {1:4}/{2:4}MB"},
                            new[] {"   *INFO-3*", "CPU:{0:3}% RAM {1:4}MB", "CPU {3:15}", "RAM {4:15}"}
                        }
                    };

                    File.WriteAllText(confFile, JsonUtil<XsmConfig>.ObjToStr(config));
                }

                for (int i = 0; i < config.rows; i++)
                {
                    Label lb = new Label();
                    lb.Left = label1.Left;
                    lb.Top = label1.Top + (label1.Height + 20) * (i + 1);
                    lb.Text = @"123";
                   // lb.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
                   // lb.ForeColor = Color.Blue;
                    lb.Parent = this;

                    labels.Add(lb);
                }

            timer1.Interval = config.Tick;
            timer1.Enabled = true;

        }

        void ShowRow(int n, string s)
        {
            labels[n].Text = s.Normalize(config.cols);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var info = sa.GetSysStatus(true);
            int cpu_p = (int)info.UseCpu;
            int ram_a = (int) info.AvailableMemoryMB;
            int ram_t = (int)info.TotalMemoryMB;
            int ram_u = ram_t - ram_a;
            int ram_p = ram_u / ram_t;

            for (int i = 0; i < config.diplays[currenpPage].Length; i++)
            {
                string s = string.Format(config.diplays[currenpPage][i], cpu_p, ram_u, ram_t,
                    ConsoleProgressBar.DefaultBar.SetPrc(cpu_p), 
                    ConsoleProgressBar.DefaultBar.SetPrc(ram_p), ram_p);

                ShowRow(i, s);
            }
        }

        static int Incr(int n, int i, int m)
        {
            return (n + i + m) % m;
        }
    }
}
