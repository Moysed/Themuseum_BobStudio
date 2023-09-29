using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Themuseum
{
    public class SoundSystem
    {
        List<SoundEffect> soundEffects = new List<SoundEffect>();
        List<Song> BGM = new List<Song>();

        public SoundSystem(){
            
        
        }

        public void LoadContent(ContentManager content)
        {
            soundEffects.Add(content.Load<SoundEffect>("005-System05")); //Pick-up sfx 0
            soundEffects.Add(content.Load<SoundEffect>("024-Door01")); //Door Open Sfx 1
            soundEffects.Add(content.Load<SoundEffect>("028-Door05")); //Door Locked Sfx 2
            soundEffects.Add(content.Load<SoundEffect>("046-Book01")); // Note read sfx 3
            soundEffects.Add(content.Load<SoundEffect>("147-Support05")); // Crystal Approve Sfx 4
            soundEffects.Add(content.Load<SoundEffect>("140-Darkness03")); // Crystal Denied sfx 5
            soundEffects.Add(content.Load<SoundEffect>("081-Monster03")); // Monster Sound 6

            BGM.Add(content.Load<Song>("BGM(Concept)")); //0
            BGM.Add(content.Load<Song>("Marked")); //1
        }

        public void PlaySfx(int i)
        {
            soundEffects[i].Play();
        }

        public void PlayBGM(int i)
        {
            MediaPlayer.Play(BGM[i]);
            MediaPlayer.IsRepeating = true;
        }

        public void StopBGM()
        {
            MediaPlayer.Stop();
        }


    }
}
