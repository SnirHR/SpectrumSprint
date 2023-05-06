using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumSprint.Models
{
    public class Square
    {
        protected Paint paint;
        protected float height;
        protected float width;
        protected float x, y;
        public float Right { get { return x + width; } }
        public float Left { get { return x; } }
        public float Top { get { return y; } }
        protected float Bottom { get { return y + height; } }

        public Square(float x, float y, float width, float height, Color color)
        {
            this.paint = new Paint();
            this.paint.Color = color;
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;
        }
        public void SetColor(Color newColor)
        {
            this.paint.Color = newColor;
        }
        public virtual void Draw(Canvas canvas)
        {
            RectF rect = new RectF(this.Left, this.Top, this.Right, this.Bottom);
            canvas.DrawRoundRect(rect,40,40,this.paint);
        }
        public virtual bool IsTouching(float x, float y)
        {
            if (x >= this.Left && x <= this.Right && y >= this.Top && y <= this.Bottom)
            {
                return true;
            }
            return false;
        }
    }
}