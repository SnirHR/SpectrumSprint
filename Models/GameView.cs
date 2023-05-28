using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Gms.Tasks;
using Android.Graphics;
using Android.Graphics.Drawables.Shapes;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Views;
using Android.Widget;
using Java.Util;
using Kotlin.Jvm.Internal;
using SpectrumSprint.Activities;
using SpectrumSprint.Constants;
using SpectrumSprint.Handlers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Locale = Java.Util.Locale;
using TextToSpeech = Android.Speech.Tts.TextToSpeech;

namespace SpectrumSprint.Models
{
    public class GameView : SurfaceView, TextToSpeech.IOnInitListener
    {
        protected int screenWidth, screenHeight; // משתנים של גודל המסך לחישוב גודל כפתורים במשחק
        private List<Color> colorArrangement;        // רשימה שמחזיקה את סדר הצבעים שבהם יופיעו במהלך המשחק
        private Color[] colors = new Color[] { Color.DarkGreen, Color.DarkBlue, Color.DeepPink, Color.DarkOrange, Color.Green, Color.Blue, Color.LightPink, Color.Orange }; //מערך של כל הצבעים במשחק
        private CanvasButton[] colorButtons; // מערך של כפתורי הצבעים
        private float buttonSize; // משתנה שיחזיק את גודל הכפתורים
        private Square indicator; // הריבוע בראש המסך שמציג את הצבע שצריך ללחוץ עליו כעת
        private float margin; // משתנה שיחזיק את כמות הרווח שיש בין כפתור לכפתור
        private Game game; // עצם שמחזיק את כל פרטי המשחק הנחוצים לפעילותו
        private long seed; // ה-SEED מוודא שהשחקנים שבאותו החדר ישחקו באותה המפה בדיוק
        private int counter;
        private Thread thread;
        private Context context;
        private Canvas canvas;
        public bool IsRunning { get; set; } = true; // בודק אם המשחק פעיל
        private TextToSpeech textToSpeech;

        public GameView(Context context, int screenWidth, int screenHeight,long seed) : base(context)
        {
           this.screenWidth = screenWidth;
           this.screenHeight = screenHeight;
           this.context = context;
           this.colorButtons = new CanvasButton[8];
           this.textToSpeech = new TextToSpeech(context, this);
           this.margin = 22;
           this.counter = 0;
           this.buttonSize = (screenWidth - 5 * margin) / 4;
           this.seed = seed;
           StartGame();
        }
        public void StartGame()
        {
            for (int i = 0; i < 8; i++)
            {
                float x = margin + (i % 4) * (buttonSize + margin);
                float y = this.screenHeight - buttonSize - margin - (i / 4) * (buttonSize + margin);
                colorButtons[i] = new CanvasButton(x, y, buttonSize, buttonSize, colors[i], "", Color.White);
            }
            this.thread = new Thread(Update);
            thread.Start();
            this.game = new Game(this.seed);
            this.colorArrangement = game.GetColorArrangement();
            this.indicator = new Square((screenWidth - 200) / 2, 20, 200, 200, Color.White);
        }

        public void Update()
        {
            while (this.IsRunning)
            {
                if (this.Holder.Surface.IsValid)
                {
                    canvas = Holder.LockCanvas();
                    if (canvas != null)
                    {
                        indicator.SetColor(colorArrangement[0]);
                        indicator.Draw(canvas);
                        foreach (CanvasButton button in colorButtons)
                        {
                            button.Draw(canvas);
                        }
                    }
                    Holder.UnlockCanvasAndPost(canvas);
                }
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (IsRunning)
            {
                if (e.Action == MotionEventActions.Down)
                {
                    foreach (CanvasButton button in colorButtons)
                    {
                        if (button.IsTouching(e.GetX(), e.GetY()))
                        {
                            if (button.GetFillColor() != colorArrangement[0])
                            {
                                textToSpeech.Speak("Wrong", QueueMode.Flush, null);
                                Finish();
                                return true;
                            
                            }
                            counter++;
                            game.Played();
                            return true;
                        }
                    }
                }
            }
            return true;
        }
        private void Finish()
        {
            var shared = Application.Context.GetSharedPreferences(PathConstants.CURRENT_USER_FILE, FileCreationMode.Private);
            this.IsRunning = false;
            if (shared.GetString("Name","Unnamed") != "")
            {
                Networker.CreateLeaderboardScore(counter);
            }
            Intent intent = new Intent(context, typeof(ActivityEnd));
            intent.PutExtra("Score", counter);
            Context.StartActivity(intent);
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