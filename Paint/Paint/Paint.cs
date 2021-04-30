using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    enum Tool
    {
        Pen,
        Line,
        Rectangle,
        Triangle,
        BFS_Fill,
        GDI_Fill,
        Select,
        Delete,
        Circle
    }
    public partial class Paint : Form
    {
        Bitmap bitmap = default(Bitmap);
        Graphics graphics = default(Graphics);
        Pen pen = new Pen(Color.Black);
        Point prevPoint = default(Point);
        Point curPoint = default(Point);
        Tool curTool = Tool.Pen;
        bool isMousePressed = false;
        List<IShape> shapes = new List<IShape>();
        Button lastClickedButton = new Button();

        public Paint()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            pictureBox1.Image = bitmap;
            graphics.Clear(Color.White);
            button1.Select();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStrip1.Text = e.Location.ToString();
            if (isMousePressed)
            {
                switch (curTool)
                {
                    case Tool.Pen:
                        prevPoint = curPoint;
                        curPoint = e.Location;
                        graphics.DrawLine(pen, prevPoint, curPoint);
                        break;
                    case Tool.Circle:
                    case Tool.Line:
                    case Tool.Triangle:
                    case Tool.Rectangle:
                        curPoint = e.Location;
                        break;
                    case Tool.Select:
                        MoveShapes(e.Location);
                        break;
                    default:
                        break;
                }
                pictureBox1.Refresh();
            }
        }

        public void MoveShapes(Point point)
        {
            foreach (IShape shape in shapes)
            {
                if (shape.selected)
                {
                    shape.Move(point);
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMousePressed = true;
            switch (curTool)
            {
                case Tool.Pen:
                case Tool.Line:
                case Tool.Triangle:
                case Tool.Circle:
                case Tool.Rectangle:
                    prevPoint = e.Location;
                    curPoint = e.Location;
                    break;
                case Tool.BFS_Fill:
                    BFS_Fill(e.Location);
                    break;
                case Tool.GDI_Fill:
                    GDI_Fill(e.Location);
                    break;
                case Tool.Select:
                    SelectShapes(e.Location);
                    break;
                case Tool.Delete:
                    DeleteShapes(e.Location);
                    break;
                default:
                    break;
            }

        }
        public void BFS_Fill(Point point)
        {
            if (bitmap.GetPixel(point.X, point.Y).ToArgb() != pen.Color.ToArgb())
            {
                DrawShapes();
                bitmap = Utils.Fill(bitmap, point, bitmap.GetPixel(point.X, point.Y), pen.Color);
                graphics = Graphics.FromImage(bitmap);
                pictureBox1.Image = bitmap;
                ClearShapes();
                pictureBox1.Refresh();
            }
        }
        public void GDI_Fill(Point point)
        {
            DrawShapes();
            MapFill mf = new MapFill();
            mf.Fill(graphics, point, pen.Color, ref bitmap);
            graphics = Graphics.FromImage(bitmap);
            pictureBox1.Image = bitmap;
            ClearShapes();
            pictureBox1.Refresh();
        }
        public void DeleteShapes(Point point)
        {
            List<IShape> remainingShapes = new List<IShape>();
            foreach (IShape shape in shapes)
            {
                if (!shape.boundingRect.Contains(point))
                {
                    remainingShapes.Add(shape);
                }
            }
            shapes = remainingShapes;
        }
        public void SelectShapes(Point point)
        {
            foreach (IShape shape in shapes)
            {
                if (shape.boundingRect.Contains(point))
                {
                    shape.pressedAt = point;
                    shape.selected = true;
                    shape.selectedForChange = true;
                }
            }
        }
        // Working with shapes
        public void DrawShapes()
        {
            foreach (IShape shape in shapes)
            {
                shape.Draw(graphics);
            }
        }
        public void ClearShapes()
        {
            foreach (IShape shape in shapes)
            {
                shape.Clear(graphics);
            }
        }
        public void ShapesApplyNewPosition()
        {
            foreach (IShape shape in shapes)
            {
                shape.selected = false;
                shape.ApplyNewPosition();
            }
        }
        public void UnselectShapes()
        {
            foreach (IShape shape in shapes)
            {
                shape.selectedForChange = false;
            }
        }


        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMousePressed = false;
            IShape shape;
            switch (curTool)
            {
                case Tool.Pen:
                    break;
                case Tool.Line:
                    shape = new MyLine(prevPoint, curPoint, pen);
                    shapes.Add(shape);
                    break;
                case Tool.Circle:
                    shape = new MyCircle(prevPoint, curPoint, pen);
                    shapes.Add(shape);
                    break;
                case Tool.Rectangle:
                    shape = new MyRectangle(prevPoint, curPoint, pen);
                    shapes.Add(shape);
                    break;
                case Tool.Triangle:
                    shape = new MyTriangle(prevPoint, curPoint, pen);
                    shapes.Add(shape);
                    break;
                case Tool.Select:
                    ShapesApplyNewPosition();
                    break;
                default:
                    break;
            }
            prevPoint = e.Location;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            foreach (IShape shape in shapes)
            {
                shape.Draw(sender, e);
            }
            switch (curTool)
            {
                case Tool.Pen:
                    break;
                case Tool.Line:
                    e.Graphics.DrawLine(pen, prevPoint, curPoint);
                    break;
                case Tool.Triangle:
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    e.Graphics.DrawRectangle(pen, Utils.GetMyRectangle(prevPoint, curPoint));
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    e.Graphics.DrawPolygon(pen, Utils.GetMyTriangle(Utils.GetMyRectangle(prevPoint, curPoint)));
                    break;
                case Tool.Circle:
                    e.Graphics.DrawEllipse(pen, Utils.GetMyRectangle(prevPoint, curPoint));
                    break;
                case Tool.Rectangle:
                    e.Graphics.DrawRectangle(pen, Utils.GetMyRectangle(prevPoint, curPoint));
                    break;
                default:
                    break;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bitmap = Bitmap.FromFile(openFileDialog1.FileName) as Bitmap;
                pictureBox1.Image = bitmap;
                graphics = Graphics.FromImage(bitmap);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawShapes();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }
                fs.Close();
            }
            ClearShapes();
        }

        private void SetColor(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            pen.Color = colorDialog1.Color;
            button11.BackColor = button10.BackColor;
            button10.BackColor = colorDialog1.Color;
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pen.Width = Convert.ToInt32(trackBar1.Value);
            foreach (IShape shape in shapes)
            {
                if (shape.selectedForChange)
                {
                    shape.pen.Width = shape.whitePen.Width = pen.Width;
                }
            }
            pictureBox1.Refresh();
        }
        private void Button_Click(object sender)
        {
            lastClickedButton.BackColor = Color.LightGray;
            lastClickedButton = (Button)sender;
            lastClickedButton.BackColor = Color.SpringGreen;
            pen.Color = button10.BackColor;
            UnselectShapes();
        }
        private void penButton_Click(object sender, EventArgs e)
        {
            curTool = Tool.Pen;
            Button_Click(sender);
        }

        private void lineButton_Click(object sender, EventArgs e)
        {
            curTool = Tool.Line;
            Button_Click(sender);
        }

        private void rectangleButton_Click(object sender, EventArgs e)
        {
            curTool = Tool.Rectangle;
            Button_Click(sender);
        }

        private void fillBFS_Click(object sender, EventArgs e)
        {
            curTool = Tool.BFS_Fill;
            Button_Click(sender);
        }

        private void fillGDI_Click(object sender, EventArgs e)
        {
            curTool = Tool.GDI_Fill;
            Button_Click(sender);
        }

        private void select_Click(object sender, EventArgs e)
        {
            curTool = Tool.Select;
            Button_Click(sender);
        }

        private void triangle_Click(object sender, EventArgs e)
        {
            curTool = Tool.Triangle;
            Button_Click(sender);
        }

        private void delete_Click(object sender, EventArgs e)
        {
            curTool = Tool.Delete;
            Button_Click(sender);
        }

        private void prevColor_Click(object sender, EventArgs e)
        {
            Color color = button10.BackColor;
            button10.BackColor = button11.BackColor;
            button11.BackColor = color;
            pen.Color = button10.BackColor;
        }

        private void circleButton_Click(object sender, EventArgs e)
        {
            curTool = Tool.Circle;
            Button_Click(sender);
        }

        private void eraserButton_Click(object sender, EventArgs e)
        {
            lastClickedButton.BackColor = Color.LightGray;
            lastClickedButton = (Button)sender;
            lastClickedButton.BackColor = Color.SpringGreen;
            curTool = Tool.Pen;
            pen.Color = Color.White;
        }
    }
}