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
     public class KeyManagement
    {

        //Trigger naming format {Room number}_{Type of triggers and its number}
        public bool R1_T0 = false;
        public bool R1_S1 = false;
        public bool R1_S2 = false;
        public bool R1_S3 = false;
        public bool R2_T1 = false;
        public bool MRB_StatueActive = false;
        public bool MRB_PieceActive = false;
        public int MRB_Pieces = 0;
        public int MRBSwitchcount = 0;
        public bool MRC_R_B = false;
        public bool MRC_B_B = false;
        public bool MRC_Y_B = false;
        public bool MRC_G_B = false;
        public bool MRC_Unlock = false;
        public bool GameEnded = false;
        public bool KeyCollectB = false;
        public bool KeyCollectC = false;
        public bool chasescenetrigger = false;
       

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
                case "MRB_Statue": MRB_StatueActive = true; break;
                case "MRB_Pieces": MRB_PieceActive = true; break;
                case "MRC_Unlock": MRC_Unlock=true; break;
                case "KeyCollectB": KeyCollectB = true; break;
                case "KeyCollectC": KeyCollectC = true; break;
            }
            
            
        }
        public void Reset()
        {
         R1_T0 = false;
         R1_S1 = false;
         R1_S2 = false;
         R1_S3 = false;
         R2_T1 = false;
         MRB_StatueActive = false;
         MRB_PieceActive = false;
         MRB_Pieces = 0;
         MRBSwitchcount = 0;
         MRC_R_B = false;
         MRC_B_B = false;
         MRC_Y_B = false;
         MRC_G_B = false;
         MRC_Unlock = false;
         GameEnded = false;
         KeyCollectC = false;
         KeyCollectB = false;
         chasescenetrigger = false;
    }
        
    }
}
