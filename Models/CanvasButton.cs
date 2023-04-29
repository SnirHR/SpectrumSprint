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
    public class CanvasButton : Square
    {
        protected string Text { get; set; }
        protected Color TextColor { get; set; }
        protected Color FillColor { get; set; }
        public CanvasButton(float x, float y, float width, float height, Color fillColor, string text, Color textColor) : base(x, y, width, height, fillColor)
        {
            this.Text = text;
            this.TextColor = textColor;
            this.FillColor = fillColor;
        }
        public override void Draw(Canvas canvas)
        {
            this.paint.Color = this.FillColor;
            base.Draw(canvas);
            this.paint.TextAlign = Paint.Align.Center;
            this.paint.Color = this.TextColor;
            this.paint.TextSize = 60;
            canvas.DrawText(this.Text, canvas.Width / 2, (this.Top + this.Bottom) / 2, this.paint);
        }
        public Color GetFillColor()
        {
            return this.FillColor;
        }
    }
}