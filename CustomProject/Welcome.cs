using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class Welcome
    {
        private Bitmap _welcomePage;

        public Welcome(Window window)
        {
            _welcomePage = new Bitmap("Welcome", "welcome.png");
            SplashKit.DrawBitmap(_welcomePage, 0, 0);
        }
    }
}