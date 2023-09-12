using System;
using System.Collections.Generic;
using System.Linq;
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
    class KeyManagement
    {
        public bool R1_S1 = false;
        public KeyManagement()
        {

        }
        public void KeyTrigger(string KeyID)
        {
            switch (KeyID)
            {
                case "R1_S1": R1_S1 = true; break; 
            }
        }

        
    }
}
