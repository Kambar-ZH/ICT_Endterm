using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    public abstract class IShape
    {
        public Rectangle boundingRect;
        public Point pressedAt;
        public bool selected = false;
        public bool selectedForChange = false;
        public Pen pen = default(Pen);
        public Pen whitePen = default(Pen);
        public IShape(Point pt1, Point pt2, Pen pen)
        {
            this.boundingRect = Utils.GetMyRectangle(pt1, pt2);
            this.pen = (Pen)pen.Clone();
            this.whitePen = (Pen)pen.Clone();
            whitePen.Width = pen.Width + 1;
            whitePen.Color = Color.White;
        }
        public abstract void Move(Point p);
        public abstract void Draw(object sender, PaintEventArgs e);
        public abstract void Draw(Graphics g);
        public abstract void Clear(Graphics g);
        public abstract void ApplyNewPosition();
    }
}