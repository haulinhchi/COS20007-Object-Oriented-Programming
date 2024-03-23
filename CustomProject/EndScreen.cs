using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class EndScreen
    {
        private Bitmap _endScreen;
        public string _endText;

        public EndScreen(Window window, string endText)
        {
            _endScreen = new Bitmap("End", "end.png");
            SplashKit.DrawBitmap(_endScreen, 0, 0);

            _endText = endText;
            DrawEndText();
        }

        private void DrawEndText()
        {
            SplashKit.DrawText(_endText, Color.Brown, 170, 430);
        }
    }

}
