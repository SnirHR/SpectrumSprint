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

namespace SpectrumSprint.Resources.Models
{
    public class Game
    {
        private long arrangement;
        private int seed;
        private int cycles;

        public Game(int seed)
        { 
            this.seed = seed;
            this.cycles = 0;
        }


        public long CreateLevel(int seed)
        {
            Random rnd = new Random(seed + cycles);
            int arrangement = rnd.Next(0, 2147483647);
            return arrangement;
        }

        public List<Color> CreateColorArrangement(int arrangement)
        {
            List<Color> colorArrangement = new List<Color>();
            string temp = arrangement.ToString();
            for (int i = 0; i < temp.Length; i++)
            {
                switch (temp[i])
                {
                    case '1':
                        colorArrangement.Add(Color.DarkGreen);
                        break;
                    case '2':
                        colorArrangement.Add(Color.DarkBlue);
                        break;
                    case '3':
                        colorArrangement.Add(Color.DeepPink);
                        break;
                    case '4':
                        colorArrangement.Add(Color.DarkOrange);
                        break;
                    case '5':
                        colorArrangement.Add(Color.Green);
                        break;
                    case '6':
                        colorArrangement.Add(Color.Blue);
                        break;
                    case '7':
                        colorArrangement.Add(Color.LightPink);
                        break;
                    case '8':
                        colorArrangement.Add(Color.Orange);
                        break;
                    default:
                        break;
                }
            }
            return colorArrangement;
        }
    }
}