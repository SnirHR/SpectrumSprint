using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumSprint.Activities
{
    [Activity(Label = "ActivityEnd")]
    public class ActivityEnd : Activity
    {
        private TextView scoreText;
        private Button BackButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_over);
            this.scoreText = FindViewById<TextView>(Resource.Id.textOverScore);
            this.BackButton = FindViewById<Button>(Resource.Id.buttonGoHome);
            scoreText.Text = Intent.GetIntExtra("Score", -1).ToString();
            BackButton.Click += BackButton_Click;

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}