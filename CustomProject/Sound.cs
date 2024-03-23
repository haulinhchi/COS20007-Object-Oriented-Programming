using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class Sound
    {
        public void BackgroundMusic()
        {
            SplashKit.LoadSoundEffect("bgmusic", "background.mp3");
            SoundEffect sound = SplashKit.SoundEffectNamed("bgmusic");
            sound.Play();
        }

        public void ProjectileSound()
        {
            SplashKit.LoadSoundEffect("projectilesound", "projectile.mp3");
            SoundEffect sound = SplashKit.SoundEffectNamed("projectilesound");
            sound.Play();
        }

        public void EatFish()
        {
            SplashKit.LoadSoundEffect("eatfish", "eatfish.mp3");
            SoundEffect sound = SplashKit.SoundEffectNamed("eatfish");
            sound.Play();
        }

        public void EatGhost()
        {
            SplashKit.LoadSoundEffect("eatghost", "eatghost.mp3");
            SoundEffect sound = SplashKit.SoundEffectNamed("eatghost");
            sound.Play();
        }
    }
}