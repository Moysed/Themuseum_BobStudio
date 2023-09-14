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
    class RoomManager
    {
        private int roomnum = 0;
        private Room1 room1;
        private Room2 room2;
        private Room3 room3;
        private MainRoomB room4;
        public RoomManager(int startingroom)
        {
            roomnum = startingroom;
            room1 = new Room1();
            room2 = new Room2();
            room3 = new Room3();
            room4 = new MainRoomB();
            
        }

        public void LoadAssets(ContentManager content)
        {
            room1.LoadSprite(content);
            room2.LoadSprite(content);
            room3.Loadsprite(content);
            room4.LoadContent(content);
        }

        public void Draw(SpriteBatch SB)
        {
            switch (roomnum)
            {
                case 1: room1.Draw(SB); break;
                case 2: room2.Draw(SB); break;
                case 3: room3.Draw(SB); break;
                case 4: room4.Draw(SB); break;
            }
        }
        public void RoomFunction(GraphicsDeviceManager _graphics, Player player , KeyManagement keymanager, float elapsed)
        {
            switch (roomnum)
            {
                case 1: room1.Function(_graphics,player,this, keymanager, elapsed); break;
                case 2: room2.Function(_graphics, player,this, keymanager, elapsed); break;
                case 3: room3.Function(_graphics, player, this, keymanager, elapsed); break;       
                case 4: room4.Function(_graphics, player, this, keymanager, elapsed); break;
                         
            }
        }

        public void Roomchange(int roomnumber)
        {
            roomnum = roomnumber;
        }

    }
}
