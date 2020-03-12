using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

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
                    Color newPixelColor = ToMainPixelColor(pixelColor);
                    bitmap.SetPixel(x, y, newPixelColor);
                }
            }
            return bitmap;
        }

        private Color ToMainPixelColor(Color pixelColor)
        {
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
            return newPixelColor;
        }

        public async Task<Image> ToMainColorsAsync(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            Task<Color[]>[] tasks = new Task<Color[]>[bitmap.Width];

            for (int x = 0; x < bitmap.Width; x++)
            {
                tasks[x] = Task.Run(() =>
                {
                    Color[] row = new Color[bitmap.Height];
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

                        row[y] = newPixelColor;
                    }

                    return row;
                });
            }
            Task.WaitAll(tasks);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    bitmap.SetPixel(x, y, tasks[x].Result[y]);
                }
            }
            return bitmap;
        }

        public void SaveImage(Image image, string newFilePath)
        {
            image.Save(newFilePath);
        }
    }
}
