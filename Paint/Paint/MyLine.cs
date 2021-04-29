using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    public class MyLine : IShape
    {
        public Point pt1, pt2;
        private Point new_pt1, new_pt2;
        public MyLine(Point pt1, Point pt2, Pen pen) : base(pt1, pt2, pen)
        {
            this.new_pt1 = this.pt1 = pt1;
            this.new_pt2 = this.pt2 = pt2;
        }
        public override void Move(Point p)
        {
            new_pt1 = new Point(pt1.X - (pressedAt.X - p.X), pt1.Y - (pressedAt.Y - p.Y));
            new_pt2 = new Point(pt2.X - (pressedAt.X - p.X), pt2.Y - (pressedAt.Y - p.Y));
        }
        public override void Draw(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(pen, new_pt1, new_pt2);
        }
        public override void Draw(Graphics g)
        {
            g.DrawLine(pen, new_pt1, new_pt2);
        }
        public override void Clear(Graphics g)
        {
            g.DrawLine(whitePen, new_pt1, new_pt2);
        }
        public override void ApplyNewPosition()
        {
            pt1 = new_pt1;
            pt2 = new_pt2;
        }
    }
}