using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageProcessingLibrary
{
    public class ImageProcessing
    {
        public Image OpenImage(string filePath)
        {
            return Image.FromFile(filePath);
        }

        public Image ToMainColors(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);

                    int alpha = pixelColor.A;
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;

                    Color newPixelColor = Color.FromArgb(alpha, 0, 0, 0);
                    if (red >= green && red >= blue)
                    {
                        newPixelColor = Color.FromArgb(alpha, 255, 0, 0);
                    }
                    else if (green > red && green > blue)
                    {
                        newPixelColor = Color.FromArgb(alpha, 0, 255, 0);
                    }
                    else if (blue > red && blue >= green)
                    {
                        newPixelColor = Color.FromArgb(alpha, 0, 0, 255);
                    }

                    bitmap.SetPixel(x, y, newPixelColor);
                }
            }

            return (Image)bitmap;
        }

        public void SaveImage(Image image, string newFilePath)
        {
            image.Save(newFilePath);
        }
    }
}
