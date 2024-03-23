using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class Instruction
    {
        private Bitmap _instruction;

        public Instruction(Window window)
        {
            _instruction = new Bitmap("Instruction", "instruction.png");
            SplashKit.DrawBitmap(_instruction, 0, 0);
        }
    }
}