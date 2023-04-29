using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SpectrumSprint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumSprint.Activities
{
    [Activity(Label = "GameActivity")]
    public class GameActivity : Activity
    {
        private GameView gameView;
        private Point WindowSize;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            WindowSize = new Point();
            WindowManager.DefaultDisplay.GetSize(this.WindowSize);
            this.gameView = new GameView(this, WindowSize.X, WindowSize.Y);
            SetContentView(this.gameView);

        }
    }
}