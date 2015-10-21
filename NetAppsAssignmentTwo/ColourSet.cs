﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NetAppsAssignmentTwo
{
    public enum PaletteName
    {
        HighTech, Banana, BlueCandy, Rainbow, Contrast, Random
    }

    public abstract class Palette
    {
        protected Color[] clut;
        protected Brush[] brushes;

        protected Palette () { }

        protected Palette(uint colorCount)
        {
            clut = new Color[colorCount];
            brushes = new Brush[colorCount];
        }

        public static Palette MakePalette(PaletteName name)
        {
            uint colorCount = CellState.NUM_STATES;

            switch(name)
            {
                default:
                case PaletteName.HighTech: return new HighTechPalette(colorCount);
                case PaletteName.BlueCandy: return new BlueCandyPalette(colorCount);
                case PaletteName.Banana: return new BananaPalette(colorCount);
                case PaletteName.Rainbow: return new RainbowPalette();
                case PaletteName.Contrast: return new ContrastPalette(colorCount);
                case PaletteName.Random: return new RandomPalette(colorCount);
            }
        }

        protected void ClutToBrushes()
        {
            for(int i = 0; i < clut.Length; i++)
                brushes[i] = new SolidBrush(clut[i]);
        }

        public virtual Color StateToColor(CellState state)
        {
            return clut[state];
        }

        public virtual Brush StateToBrush(CellState state)
        {
            return brushes[state];
        }
    }

    public class HighTechPalette : Palette
    {
        public HighTechPalette(uint colorCount)
            : base(colorCount)
        {
            // Compute the color lookup table
            for(int i = 0; i < clut.Length; i++)
            {
                float luminance = (float)i / (float)clut.Length * 200.0f;

                clut[i] = Color.FromArgb(
                    0,
                    (int)(luminance),
                    55);
            }

            ClutToBrushes();
        }
    }

    public class BlueCandyPalette : Palette
    {
        public BlueCandyPalette(uint colorCount)
            : base(colorCount)
        {
            // Compute the color lookup table
            for (int i = 0; i < clut.Length; i++)
            {
                float luminance = (float)i / (float)clut.Length;

                clut[i] = Color.FromArgb(
                    200 - (int)(luminance * 200f),
                    225 - (int)(luminance * 100f),
                    255 - (int)(luminance * 25f));
            }

            ClutToBrushes();
        }
    }

    public class BananaPalette : Palette
    {
        public BananaPalette(uint colorCount)
            : base(colorCount)
        {
            // Compute the color lookup table
            for (int i = 0; i < clut.Length; i++)
            {
                float luminance = (float)i / (float)clut.Length;

                clut[i] = Color.FromArgb(
                    225 - (int)(luminance * 50f),
                    240 - (int)(luminance * 200f),
                    180 - (int)(luminance * 180f));
            }

            ClutToBrushes();
        }
    }

    public class RainbowPalette : Palette
    {
        private Brush[] rainbowPal;

        public RainbowPalette ()
        {
            rainbowPal = new Brush[]
            {
                Brushes.Red, Brushes.DarkViolet, Brushes.Blue,
                Brushes.LightBlue, Brushes.DarkGreen, Brushes.Green,
                Brushes.Yellow, Brushes.Orange
            };
        }

        public override Brush StateToBrush(CellState state)
        {
            return rainbowPal[state % rainbowPal.Length];
        }
    }

    public class ContrastPalette : Palette
    {
        private const int MAX_LUM = 255;

        public ContrastPalette(uint colorCount)
            : base(colorCount)
        {
            int i;
            for (i = 0; i < clut.Length / 2; i++)
                clut[i] = Color.LightGray;
            for (; i < clut.Length; i++)
                clut[i] = Color.Black;

            ClutToBrushes();
        }
    }

    public class RandomPalette : Palette
    {
        private const int MAX_LUM = 255;
        private Random random = new Random();

        public RandomPalette(uint colorCount) : base(colorCount)
        {
            for (int i = 0; i < clut.Length; i++)
                clut[i] = Color.FromArgb(
                    random.Next() % MAX_LUM,
                    random.Next() % MAX_LUM,
                    random.Next() % MAX_LUM);

            ClutToBrushes();
        }
    }
}
