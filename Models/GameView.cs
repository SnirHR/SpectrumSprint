using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Views;
using Android.Widget;
using Java.Util;
using SpectrumSprint.Activities;
using System.Threading;


namespace SpectrumSprint.Models
{
    public class GameView : SurfaceView, TextToSpeech.IOnInitListener
    {
        Color[] colors = new Color[] { Color.DarkGreen, Color.DarkBlue, Color.DeepPink, Color.DarkOrange, Color.Green, Color.Blue, Color.LightPink, Color.Orange };
        private CanvasButton[] colorButtons;
        private Square indicator;
        private TextToSpeech textToSpeech;
        private Context context;
        private float buttonSize; 
        private float margin;
        protected int screenWidth, screenHeight;
        public bool IsRunning { get; set; } = true;

        public GameView(Context context, int screenWidth, int screenHeight) : base(context)
        {
           this.colorButtons = new CanvasButton[8];
           this.textToSpeech = new TextToSpeech(context, this);
           this.indicator = new Square((screenWidth / 2) ,20, 200,200,Color.White);
           this.context = context;
           this.screenWidth = screenWidth;
           this.screenHeight = screenHeight;
           this.margin = 22;
           this.buttonSize = (screenWidth - 5 * margin) / 4;
           Init();
        }
        public void Init()
        {
            for (int i = 0; i < 8; i++)
            {
                float x = margin + (i % 4) * (buttonSize + margin);
                float y = this.screenHeight - buttonSize - margin - (i / 4) * (buttonSize + margin);
                colorButtons[i] = new CanvasButton(x, y, buttonSize, buttonSize, colors[i], "", Color.White);
            }

            Thread thread = new Thread(Run);
            thread.Start();
        }

        public void Run()
        {
            while (this.IsRunning)
            {
                if (this.Holder.Surface.IsValid)
                {
                    Canvas canvas = Holder.LockCanvas();
                    if (canvas != null)
                    {
                        indicator.Draw(canvas);
                        foreach (CanvasButton button in colorButtons)
                        {
                            button.Draw(canvas);
                        }
                        Holder.UnlockCanvasAndPost(canvas);
                    }
                }
            }
        }
        public override bool OnTouchEvent(MotionEvent e)
        {
            if (e.Action == MotionEventActions.Down)
            {
                foreach (CanvasButton button in colorButtons)
                {
                    if (button.IsTouching(e.GetX(), e.GetY()))
                    {
                        Toast.MakeText(this.context, button.GetFillColor().ToString(), ToastLength.Short).Show();
                        return true;
                    }
                }
            }
            return false;
        }

        public void OnInit([GeneratedEnum] OperationResult status)
        {
            if (status == OperationResult.Success)
            {
                textToSpeech.SetLanguage(Locale.Us);
            }
        }
    }
}