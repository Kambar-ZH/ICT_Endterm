using System;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    public class MyCircle : MyRectangle
    {
        public MyCircle(Point pt1, Point pt2, Pen pen) : base(pt1, pt2, pen)
        {
        }
        public override void Draw(object sender, PaintEventArgs e)
        {
            Rectangle temp = new Rectangle(newX, newY, boundingRect.Width, boundingRect.Height);
            e.Graphics.DrawEllipse(pen, temp);
        }
        public override void Draw(Graphics g)
        {
            g.DrawEllipse(pen, boundingRect);
        }
        public override void Clear(Graphics g)
        {
            g.DrawEllipse(whitePen, boundingRect);
        }
    }
}
