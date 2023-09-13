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
        //Trigger naming format {Room number}_{Type of triggers and its number}
        public bool R1_S1 = false;
        public bool R1_S2 = false;
        public bool R1_S3 = false;
        public bool R2_T1 = false;
        public KeyManagement()
        {

        }
        public void KeyTrigger(string KeyID)
        {
            switch (KeyID)
            {
                case "R1_S1": R1_S1 = true; break;
                case "R1_S2": R1_S2 = true; break;
                case "R1_S3": R1_S3 = true; break;
                case "R2_T1": R2_T1 = true; break;
            }
        }

        
    }
}
