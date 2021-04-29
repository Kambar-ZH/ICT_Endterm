using System;
using System.Drawing;
using System.Collections.Generic;

namespace Paint
{
    class Utils
    {
        public static Rectangle GetMyRectangle(Point a, Point b)
        {
            int leftUpperCornerX = Math.Min(a.X, b.X);
            int leftUpperCornerY = Math.Min(a.Y, b.Y);
            int width = Math.Abs(a.X - b.X);
            int height = Math.Abs(a.Y - b.Y);
            return new Rectangle(leftUpperCornerX, leftUpperCornerY, width, height);
        }
        public static Point[] GetMyTriangle(Rectangle rect)
        {
            Point pt1 = new Point(rect.X + rect.Width / 2, rect.Y);
            Point pt2 = new Point(rect.X, rect.Y + rect.Height);
            Point pt3 = new Point(rect.X + rect.Width, rect.Y + rect.Height);
            Point[] points = { pt1, pt2, pt3 };
            return points;
        }
        public static Bitmap Fill(Bitmap bmp,
            Point originPoint,
            Color originColor,
            Color fillColor)
        {
            Queue<Point> q = new Queue<Point>();
            q.Enqueue(originPoint);
            bmp.SetPixel(originPoint.X, originPoint.Y, fillColor);
            while (q.Count != 0)
            {
                Point cur = q.Dequeue();
                if (cur.X + 1 >= 0 && cur.Y >= 0 && cur.X + 1 < bmp.Width && cur.Y < bmp.Height)
                {
                    if (bmp.GetPixel(cur.X + 1, cur.Y) == originColor)
                    {
                        bmp.SetPixel(cur.X + 1, cur.Y, fillColor);
                        q.Enqueue(new Point(cur.X + 1, cur.Y));
                    }
                }
                if (cur.X >= 0 && cur.Y + 1 >= 0 && cur.X < bmp.Width && cur.Y + 1 < bmp.Height)
                {
                    if (bmp.GetPixel(cur.X, cur.Y + 1) == originColor)
                    {
                        bmp.SetPixel(cur.X, cur.Y + 1, fillColor);
                        q.Enqueue(new Point(cur.X, cur.Y + 1));
                    }
                }
                if (cur.X - 1 >= 0 && cur.Y >= 0 && cur.X - 1 < bmp.Width && cur.Y < bmp.Height)
                {
                    if (bmp.GetPixel(cur.X - 1, cur.Y) == originColor)
                    {
                        bmp.SetPixel(cur.X - 1, cur.Y, fillColor);
                        q.Enqueue(new Point(cur.X - 1, cur.Y));
                    }
                }
                if (cur.X >= 0 && cur.Y - 1 >= 0 && cur.X < bmp.Width && cur.Y - 1 < bmp.Height)
                {
                    if (bmp.GetPixel(cur.X, cur.Y - 1) == originColor)
                    {
                        bmp.SetPixel(cur.X, cur.Y - 1, fillColor);
                        q.Enqueue(new Point(cur.X, cur.Y - 1));
                    }
                }
            }
            return bmp;
        }
    }
}