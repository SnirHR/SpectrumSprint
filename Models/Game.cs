using Android.Content;
using Android.Graphics;
using SpectrumSprint.Activities;
using System;
using System.Collections.Generic;

namespace SpectrumSprint.Models
{
    public class Game
    {
        List<Color> ColorArrangement;
        private long seed;
        private long cycles;

        public Game(long seed)
        { 
            this.ColorArrangement = new List<Color>();
            this.seed = seed;
            this.cycles = 0;
            CreateColorArrangement(CreateArrangement());
        }

        public List<Color> GetColorArrangement()
        {
            return ColorArrangement;
        }

        public long CreateArrangement()
        {
            long tempseed = (seed + cycles);
            Random rnd = new Random((int)tempseed);
            return rnd.Next(0, 2147483647);
        }
        public void NextSection()
        {
            cycles++;
            CreateColorArrangement(CreateArrangement());

        }
        public void Played()
        {
            if (ColorArrangement.Count < 2)
            {
                NextSection();
            }
            ColorArrangement.Remove(ColorArrangement[0]);
        }
        private void CreateColorArrangement(long arrangement)
        {
            string temp = arrangement.ToString();
            for (int i = 0; i < temp.Length; i++)
            {
                switch (temp[i])
                {
                    case '0':
                        this.ColorArrangement.Add(Color.DeepPink);
                        break;
                    case '1':
                        this.ColorArrangement.Add(Color.DarkGreen);
                        break;
                    case '2':
                        this.ColorArrangement.Add(Color.DarkBlue);
                        break;
                    case '3':
                        this.ColorArrangement.Add(Color.DeepPink);
                        break;
                    case '4':
                        this.ColorArrangement.Add(Color.DarkOrange);
                        break;
                    case '5':
                        this.ColorArrangement.Add(Color.Green);
                        break;
                    case '6':
                        this.ColorArrangement.Add(Color.Blue);
                        break;
                    case '7':
                        this.ColorArrangement.Add(Color.LightPink);
                        break;
                    case '8':
                        this.ColorArrangement.Add(Color.Orange);
                        break;
                    case '9':
                        this.ColorArrangement.Add(Color.LightPink);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}