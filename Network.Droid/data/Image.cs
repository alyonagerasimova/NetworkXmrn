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
    class Image
    {

        private string imageName;

        private string flagName;

        public Image(string countryName, string flagName)
        {
            this.imageName = countryName;
            this.flagName = flagName;
        }

        public string getImageName()
        {
            return imageName;
        }

        public string getFlagName()
        {
            return flagName;
        }
    public string toString()
        {
            return this.imageName;
        }

    }
}