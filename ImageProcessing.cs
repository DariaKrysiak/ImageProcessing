using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageProcessing
{
    public class ImageProcessing
    {
        public Image OpenImage(string filePath)
        {
            return Image.FromFile(filePath);
        }

        public void SaveImage(Image img, string newFileName)
        {
            img.Save(newFileName);
        }
    }
}
