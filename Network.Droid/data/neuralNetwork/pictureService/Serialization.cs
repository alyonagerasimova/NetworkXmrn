using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace NueralNetrwork.Network.pictureService
{
    class Serialization
    {

        private Context context;

        public Serialization(Context context)
        {
            this.context = context;
        }

        public double[] readPatterns(String str)
        {
            double[] patterns = new double[Const.NUMBER_INPUT_NEURONS];
            StreamReader sr = new StreamReader(str + ".txt");
            try
            {
                for (int i = 0; i < patterns.Length; i++)
                {
                    patterns[i] = Convert.ToDouble(sr.ReadLine());
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
            finally
            {
                if (sr != null)
                {
                    try
                    {
                        sr.Close();
                    }
                    catch (IOException ignored)
                    {
                    }
                }
            }
            return patterns;
        }

        public Bitmap readImage(String imageName)
        {
            try
            {
                Bitmap bmp = new Bitmap(imageName + ".png");
                return bmp;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public double[] ReadValuesForTraining(String letter)
        {
            return readPatterns(letter);
        }

    }
}

