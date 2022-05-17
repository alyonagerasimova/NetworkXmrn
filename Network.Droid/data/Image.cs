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

        private String imageName;

        private String flagName;

        public Image(String countryName, String flagName)
        {
            this.imageName = countryName;
            this.flagName = flagName;
        }

        public String getImageName()
        {
            return imageName;
        }

        public String getFlagName()
        {
            return flagName;
        }
    public String toString()
        {
            return this.imageName;
        }

    }
}