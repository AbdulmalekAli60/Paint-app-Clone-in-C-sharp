using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_app
{
    public partial class Form1 : Form
    {
        Bitmap bm;
        Graphics g;
        bool paint = false;

        Point px, py;

        Pen p = new Pen(Color.Black,1);
        Pen eraser = new Pen(Color.White,10); // for eraser

        int x, y, sx ,sy, cx , cy; // for ellipse start point, end point , the difference between them 

        int index;

        ColorDialog cd = new ColorDialog();
        Color new_Color; // variable of type Color 

        bool fillShape = false;

        private void btn_llipse_Click(object sender, EventArgs e) // Ellipse 
        {
            index = 3;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pic.Image = bm;
            index = 0;
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_Color = cd.Color;
            btn_color.BackColor = cd.Color;
            pic.BackColor = cd.Color;
            
            p.Color = cd.Color;
        }

        private void btn_rectangle_Click(object sender, EventArgs e)
        {
            index = 4;
        }

        private void btn_line_Click(object sender, EventArgs e)
        {
            index = 5;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|BMP Image|*.bmp";
            saveDialog.Title = "Save Image";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveDialog.FileName;
                bm.Save(fileName);
                MessageBox.Show("Image saved successfully");
            }
        }

        private void btn_fill_Click(object sender, EventArgs e)
        {
            fillShape = !fillShape;
        }

        private void pic_Paint(object sender, PaintEventArgs e)
        {
               Graphics g = e.Graphics;
            if (paint)
            {
                if(index == 3)
                {
                    g.DrawEllipse(p, cx, cy, sx, sy);
                }
            }  
        }

        public Form1()
        {
            InitializeComponent();
            this.Width = 800;
            this.Height = 600;
            bm = new Bitmap(pic.Width, pic.Height); // graphics size to draw 
            g = Graphics.FromImage(bm);
            g.Clear(Color.White); // clear the form when start 
            pic.Image = bm;
        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            cx = e.X; // starting value assigning variable e is the event , take current x value
            cx = e.X; // starting value assigning variable e is the event , take current y value 

            paint = true;
            py = e.Location; // current location of the mouse 
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            x = e.X; // new values of x and y
            y = e.Y; // new values of x and y

            sx = e.X - cx; //tacking the difference between them
            sy = e.Y - cy; //tacking the difference between them

            if (paint)
            {
                if(index == 1)
                {
                    px = e.Location;
                    g.DrawLine(p, px, py);
                    py = px;
                }
            }
            pic.Refresh();

            if(index == 2)
            {
                px = e.Location;
                g.DrawLine(eraser, px, py);
                py = px;

            }
        }

        private void btn_pencil_Click(object sender, EventArgs e)
        {
            index = 1;
        }

        private void btn_eraser_Click(object sender, EventArgs e)
        {
            index = 2;
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;

            sx = x - cx; //tacking the difference between them again , final calculation 
            sy = y - cy; //tacking the difference between them again , final calculation 

            if (index == 3)
            {
                if (fillShape)
                {

                    g.FillEllipse(p.Brush, cx, cy, sx, sy); // Fill ellipse
                }
                else
                {
                    g.DrawEllipse(p, cx, cy, sx, sy); // Draw ellipse (pen, start value, start value, size x , size y)
                }
            }

            if (index == 4)
            {
                if (fillShape)
                {
                    g.FillRectangle(p.Brush, cx, cy, sx, sy); // Fill rectangle
                }
                else
                {
                    g.DrawRectangle(p, cx, cy, sx, sy); // Draw rectangle
                }
            }

            if (index == 5)
            {
                g.DrawLine(p, cx, cy, x, y); // Draw line
            }
        }




    }
    }

