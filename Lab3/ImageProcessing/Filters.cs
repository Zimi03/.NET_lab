using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public static class Filters
    {
        public static Bitmap ToGrayScale(Bitmap source)
        {
            Bitmap result = new Bitmap(source);
            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    Color pixel = result.GetPixel(x, y);
                    int gray = (int)(0.3 * pixel.R + 0.59 * pixel.G + 0.11 * pixel.B);
                    Color grayColor = Color.FromArgb(gray, gray, gray);
                    result.SetPixel(x, y, grayColor);
                }
            }
            return result;
        }

        public static Bitmap ToThreshold(Bitmap source, int threshold = 128)
        {
            Bitmap result = new Bitmap(source);
            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    Color pixel = result.GetPixel(x, y);
                    int gray = (int)(0.3 * pixel.R + 0.59 * pixel.G + 0.11 * pixel.B);
                    Color color = gray < threshold ? Color.Black : Color.White;
                    result.SetPixel(x, y, color);
                }
            }
            return result;
        }

        public static Bitmap ToNegative(Bitmap source)
        {
            Bitmap result = new Bitmap(source);
            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    Color pixel = result.GetPixel(x, y);
                    Color negColor = Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                    result.SetPixel(x, y, negColor);
                }
            }
            return result;
        }

        public static Bitmap ToMirror(Bitmap source)
        {
            Bitmap result = new Bitmap(source);
            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width / 2; x++)
                {
                    Color left = result.GetPixel(x, y);
                    Color right = result.GetPixel(result.Width - x - 1, y);
                    result.SetPixel(x, y, right);
                    result.SetPixel(result.Width - x - 1, y, left);
                }
            }
            return result;
        }
    }
}
