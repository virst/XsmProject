using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NativeMon
{
    class FormDragger : Component, ISupportInitialize
    {
        Point oldPos;
        bool isDragging = false;
        Point oldMouse;
        private Form MyForm;

       

        public FormDragger(Form f)
        {
            MyForm = f;
        }

        private void MyForm_MouseDown(object sender, MouseEventArgs e)
        {
            this.isDragging = true;
            this.oldPos = MyForm.Location;
            this.oldMouse = e.Location;
            
        }

        private void MyForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isDragging)
            {
                MyForm.Location = new Point(oldPos.X + (e.X - oldMouse.X), oldPos.Y + (e.Y - oldMouse.Y));
            }
        }

        private void MyForm_MouseUp(object sender, MouseEventArgs e)
        {
            this.isDragging = false;
        }

        public void BeginInit()
        {
            //throw new NotImplementedException();
        }

        public void EndInit()
        {
            MyForm.MouseDown += MyForm_MouseDown;
            MyForm.MouseMove += MyForm_MouseMove;
            MyForm.MouseUp += MyForm_MouseUp;
        }
    }
}
