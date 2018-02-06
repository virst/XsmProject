using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViSysMon;

namespace NativeMon
{
    public partial class Form1 : Form
    {
        static System.Drawing.Text.PrivateFontCollection _pfc;
        public static Font DsDigital , Subwt; //SUBWT.ttf
        private readonly SysMonAnalyst _sa = new SysMonAnalyst();

        static Form1()
        {
            _pfc = new System.Drawing.Text.PrivateFontCollection();
            _pfc.AddFontFile("DS-Digital.ttf");
            _pfc.AddFontFile("SUBWT.ttf");
            DsDigital = new Font(_pfc.Families[0], 18f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Subwt = new Font(_pfc.Families[1], 16f, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        public Form1()
        {
            InitializeComponent();

            label1.Font = Subwt;
           /* this.AllowTransparency = true;
            this.BackColor = Color.AliceBlue;//цвет фона  
            this.TransparencyKey = this.BackColor;//он же будет заменен на прозрачный цвет
            */
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var info = _sa.GetSysStatus(false);
            int cpu_p = (int)info.UseCpu;
            int ram_a = (int)info.AvailableMemoryMB;
            int ram_t = (int)info.TotalMemoryMB;
            int ram_u = ram_t - ram_a;
            int mlc = info.Messages?.Length ?? 0;

            label1.Text = string.Format("P:{0:D2} C:{1:D3} R:{2:D5}", mlc, cpu_p, ram_u);
        }
    }
}
