using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Themuseum
{
    class Room1 : Game1
    {
        private Texture2D TileStatic;
        private Texture2D Door;
        private Texture2D HiddenSwitch_01_Tex;
        private Rectangle HiddenSwitch_01_Col;
        private Vector2 HiddenSwitch_01_Pos;
        private Vector2 DoorPos;
        private Rectangle DoorCollision;
        private KeyboardState KeyControls;
        private KeyboardState OldKey;
       

        

        public Room1()
        {
            DoorPos = new Vector2(64, 640 / 2);
            DoorCollision = new Rectangle((int)DoorPos.X, (int)DoorPos.Y, 32, 64);
            HiddenSwitch_01_Pos = new Vector2(1000,320);
            HiddenSwitch_01_Col = new Rectangle((int)HiddenSwitch_01_Pos.X, (int)HiddenSwitch_01_Pos.Y,64,64);
        }

        public void LoadSprite(ContentManager content)
        {
            TileStatic = content.Load<Texture2D>("room1_placeholder");
            Door = content.Load<Texture2D>("placeholderdoor");
            HiddenSwitch_01_Tex = content.Load<Texture2D>("hiddenswitch01_placeholder");
        }
        
        public void Draw(SpriteBatch SB)
        {
            SB.Draw(TileStatic, Vector2.Zero, Color.White);
            SB.Draw(Door, DoorPos, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
            SB.Draw(HiddenSwitch_01_Tex, HiddenSwitch_01_Pos, Color.White);
        }

        public void Function(GraphicsDeviceManager _graphics, Player player,RoomManager roomManager, KeyManagement Keymanager)
        {
            DoorPos = new Vector2(64, 640 / 2);
            DoorCollision = new Rectangle((int)DoorPos.X, (int)DoorPos.Y, 32, 64);
            HiddenSwitch_01_Pos = new Vector2(1000, 320);
            HiddenSwitch_01_Col = new Rectangle((int)HiddenSwitch_01_Pos.X, (int)HiddenSwitch_01_Pos.Y, 64, 64);
            KeyControls = Keyboard.GetState();
            
            //Wall Collision
            if (player.SelfPosition.X >= _graphics.GraphicsDevice.Viewport.Bounds.Right - 64)
            {
                if(player.speed >= 4)
                {
                    player.SelfPosition.X -= 8f;
                }
                else
                player.SelfPosition.X -= player.speed;
                
            } 

            else if (player.SelfPosition.X <= _graphics.GraphicsDevice.Viewport.Bounds.Left + 64)
            {
                if (player.speed >= 4)
                {
                    player.SelfPosition.X += 8f;
                }
                else
                    player.SelfPosition.X += player.speed;
            }

             else if (player.SelfPosition.Y <= _graphics.GraphicsDevice.Viewport.Bounds.Top + 64)
            {
                if (player.speed >= 4)
                {
                    player.SelfPosition.Y += 8f;
                }
                else
                    player.SelfPosition.Y += player.speed;
            }
            else if (player.SelfPosition.Y >= _graphics.GraphicsDevice.Viewport.Bounds.Bottom - 64)
            {
                if (player.speed >= 4)
                {
                    player.SelfPosition.Y -= 8f;
                }
                else
                    player.SelfPosition.Y -= player.speed;
            }

            //Object Interaction
            if (player.collision.Intersects(DoorCollision) == true )
            {
                if(KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E) && Keymanager.R1_S1 == true)
                {
                    player.ChangeStartingPosition(new Vector2(1280 - 64, 640 / 2));
                    roomManager.Roomchange(2);
                }
                
                
            }
            if(player.collision.Intersects(HiddenSwitch_01_Col) == true)
            {
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    Keymanager.KeyTrigger("R1_S1");
                }
            }

           
            OldKey = KeyControls;
        }

        
    }
}
