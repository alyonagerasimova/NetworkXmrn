using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NueralNetrwork.Network.pictureService
{
    class PictureService
    {
        private static double blackPixel = 1.0;
        private static double whitePixel = 0.0;
      
        public static double[] getPixelColor(Bitmap image)
        {
            Color clr;
            int k = -1;
            Bitmap b = new Bitmap(image);
            double[] pixelsValues = new double[400];
            double[,] pixelsValuesMatrix = new double[20,20];
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    k++;
                    clr = b.GetPixel(j, i);        
                    if (clr.R == 176 && clr.G == 244 && clr.B == 254)
                    {
                        pixelsValuesMatrix[i,j] = blackPixel;
                    }
                    else
                    {
                        pixelsValuesMatrix[i,j] = whitePixel;
                    }
                    pixelsValues[k] = pixelsValuesMatrix[i,j];
                }
            }
            return pixelsValues;
        }
    }
}