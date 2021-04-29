using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    class MyTriangle : IShape
    {
        public int newX;
        public int newY;
        public MyTriangle(Point pt1, Point pt2, Pen pen) : base(pt1, pt2, pen)
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
            e.Graphics.DrawPolygon(pen, Utils.GetMyTriangle(new Rectangle(newX, newY, boundingRect.Width, boundingRect.Height)));
        }
        public override void Draw(Graphics g)
        {
            g.DrawPolygon(pen, Utils.GetMyTriangle(new Rectangle(newX, newY, boundingRect.Width, boundingRect.Height)));
        }
        public override void Clear(Graphics g)
        {
            g.DrawPolygon(whitePen, Utils.GetMyTriangle(new Rectangle(newX, newY, boundingRect.Width, boundingRect.Height)));
        }
        public override void ApplyNewPosition()
        {
            boundingRect = new Rectangle(newX, newY, boundingRect.Width, boundingRect.Height);
        }
    }
}