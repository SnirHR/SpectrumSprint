﻿using Android.App;
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
        protected float x, y;
        protected float width;
        protected float height;
        protected Paint paint;
        public float Right { get { return x + width; } }
        public float Left { get { return x; } }
        public float Top { get { return y; } }
        protected float Bottom { get { return y + height; } }

        public Square(float x, float y, float width, float height, Color color)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.paint = new Paint();
            this.paint.Color = color;
        }
        public virtual void Draw(Canvas canvas)
        {
            canvas.DrawRect(this.Left, this.Top, this.Right, this.Bottom, paint);
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