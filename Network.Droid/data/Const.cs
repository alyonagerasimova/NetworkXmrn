using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NueralNetrwork.Network
{
    class Const
    {
        public static int NUMBER_INPUT_NEURONS = 400;
        public static int NUMBER_OUTPUT_NEURONS = 26;
        public static String[] ARRAY_LETTERS = new String[]{"A", "B", "C", "D", "E", "F",
            "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
        public static double A = 1.2;
        public static double B = 0.6;
    }
}