using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    public class MyRectangle : IShape
    {
        public int newX;
        public int newY;
        public MyRectangle(Point pt1, Point pt2, Pen pen) : base(pt1, pt2, pen)
        {
            this.newX = boundingRect.X;
            this.newY = boundingRect.Y;
        }
        public override void Move(Point p)
        {
            newX = boundingRect.X - (pressedAt.X - p.X);
            newY = boundingRect.Y - (pressedAt.Y - p.Y);
        }
        public override void Draw(object sender, PaintEventArgs e)
        {
            Rectangle temp = new Rectangle(newX, newY, boundingRect.Width, boundingRect.Height);
            e.Graphics.DrawRectangle(pen, temp);
        }
        public override void Draw(Graphics g)
        {
            g.DrawRectangle(pen, boundingRect);
        }
        public override void Clear(Graphics g)
        {
            g.DrawRectangle(whitePen, boundingRect);
        }
        public override void ApplyNewPosition()
        {
            boundingRect = new Rectangle(newX, newY, boundingRect.Width, boundingRect.Height);
        }
    }
}