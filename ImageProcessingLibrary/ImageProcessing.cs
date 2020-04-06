using System.Drawing;
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

        public Image ToMainColorsAsync(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            int width = bitmap.Width;
            int height = bitmap.Height;

            RectangleF cloneRect = new RectangleF(0, 0, width, height);
            System.Drawing.Imaging.PixelFormat format = bitmap.PixelFormat;
            Bitmap cloneBitmap = bitmap.Clone(cloneRect, format);

            for (int x = 0; x < width; x++)
            {
                Parallel.For(0, height, y =>
                {
                    Color pixelColor = cloneBitmap.GetPixel(x, y);
                    Color newPixelColor = ToMainPixelColor(pixelColor);
                    bitmap.SetPixel(x, y, newPixelColor);
                });
            }
            return bitmap;
        }

        public void SaveImage(Image image, string newFilePath)
        {
            image.Save(newFilePath);
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
    }
}
