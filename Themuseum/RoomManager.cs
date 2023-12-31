﻿using System;
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
    class RoomManager 
    {
        private int roomnum = 0;
        private Room1 room1;
        private Room2 room2;
        private Room3 room3;
        private MainRoomB MRB;
        private MRB_To_MRC_Corridor MRB_MRC_Cor;
        private MRC mrc;
        private ChasingScene chasingScene;
        public Color mapcolor;
       
         public RoomManager(int startingroom)
        {
           
            roomnum = startingroom;
            room1 = new Room1();
            room2 = new Room2();
            room3 = new Room3();
            MRB = new MainRoomB();
            MRB_MRC_Cor = new MRB_To_MRC_Corridor();
            mrc = new MRC();
            chasingScene = new ChasingScene();
            mapcolor = Color.White;
            
        }

        public void LoadAssets(ContentManager content)
        {
            room1.LoadSprite(content);
            room2.LoadSprite(content);
            room3.Loadsprite(content);
            MRB.LoadContent(content);
            MRB_MRC_Cor.LoadSprite(content);
            mrc.LoadSprite(content);
            chasingScene.LoadSprite(content);
        }

        public void Draw(SpriteBatch SB, LanternLight light,KeyManagement key)
        {
            switch (roomnum)
            {
                case 1: room1.Draw(SB,light,mapcolor); break;
                case 2: room2.Draw(SB,light, mapcolor); break;
                case 3: room3.Draw(SB,light, mapcolor , key ); break;
                case 4: MRB.Draw(SB, mapcolor); break;
                case 5: MRB_MRC_Cor.Draw(SB, mapcolor); break;
                case 6: mrc.Draw(SB, mapcolor , key); break;
                case 7: chasingScene.Draw(SB, mapcolor); break;
            }
        }
        public void RoomFunction(GraphicsDeviceManager _graphics, Player player , KeyManagement keymanager, float elapsed, DialogueBox dialogue, LanternLight light,Map map,SoundSystem sound, Ghost ghost,Staminabar UI)
        {
            switch (roomnum)
            {
                case 1: room1.Function(_graphics,player,this, keymanager, elapsed,dialogue,light,map,sound,ghost,UI); break;
                case 2: room2.Function(_graphics, player,this, keymanager, elapsed,dialogue, light,sound, ghost,UI); break;
                case 3: room3.Function(_graphics, player, this, keymanager, elapsed, dialogue, light,sound, ghost,UI); break;       
                case 4: MRB.Function(_graphics, player, this, keymanager, elapsed, dialogue, light,sound, ghost,UI); break;
                case 5: MRB_MRC_Cor.Function(_graphics, player, this, keymanager, elapsed, dialogue, light,sound, ghost,UI); break;
                case 6: mrc.Function(_graphics, player, this, keymanager, elapsed, dialogue, light,sound, ghost,UI); break;
                case 7: chasingScene.Function(_graphics, player, this, keymanager, elapsed, dialogue, light, sound, ghost, UI); break;

            }
        }

        public void Roomchange(int roomnumber)
        {
            roomnum = roomnumber;
        }

        public void RoomReset()
        {
            roomnum = 1;
            room1.Reset();
            room2.Reset();
            room3.Reset();
            MRB.Reset();
            mrc.Reset();
            mapcolor = Color.White;

        }

    }
}
