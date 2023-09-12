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
    class Room2 
    {
        private Texture2D TileStatic;
        private Texture2D Door;
        private Vector2 DoorPos;
        private Rectangle DoorCollision;
        private Texture2D WallArea_Tex;
        private List<Rectangle> WallArea_Col = new List<Rectangle>();
        private KeyboardState KeyControls;
        private KeyboardState OldKey;
        public Room2()
        {
            DoorPos = new Vector2(1280-64, 640 / 2);
            DoorCollision = new Rectangle((int)DoorPos.X, (int)DoorPos.Y, 32, 64);
            
            WallArea_Col.Add(new Rectangle(0, 0, 500, 640));
            WallArea_Col.Add(new Rectangle(700, 0, 1000, 300));
        }

       
        public void LoadSprite(ContentManager content)
        {
            TileStatic = content.Load<Texture2D>("room2_placeholder");
            Door = content.Load<Texture2D>("placeholderdoor");
            WallArea_Tex = content.Load<Texture2D>("wallplaceholder");
        }

        public void Draw(SpriteBatch SB)
        {
            SB.Draw(TileStatic, Vector2.Zero, Color.White);
            SB.Draw(Door, DoorPos, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);

            for(int i = 0; i < WallArea_Col.Count; i++)
            {
                SB.Draw(WallArea_Tex, WallArea_Col[i], Color.White);
            }

        }

        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager)
        {
            DoorPos = new Vector2(1280-64, 640 / 2);
            DoorCollision = new Rectangle((int)DoorPos.X, (int)DoorPos.Y, 32, 64);
            KeyControls = Keyboard.GetState();
            //Wall Collision
            if (player.SelfPosition.X >= _graphics.GraphicsDevice.Viewport.Bounds.Right - 64)
            {
                player.SelfPosition.X -= player.speed;
            }

            else if (player.SelfPosition.X <= _graphics.GraphicsDevice.Viewport.Bounds.Left + 64)
            {
                player.SelfPosition.X += player.speed;
            }

            else if (player.SelfPosition.Y <= _graphics.GraphicsDevice.Viewport.Bounds.Top + 64)
            {
                player.SelfPosition.Y += player.speed;
            }
            else if (player.SelfPosition.Y >= _graphics.GraphicsDevice.Viewport.Bounds.Bottom - 64)
            {
                player.SelfPosition.Y -= player.speed;
            }

            for(int i = 0; i < WallArea_Col.Count; i++)
            {
                if (WallArea_Col[i].Intersects(player.collision))
                {
                    if (player.collision.Right > WallArea_Col[i].Right)
                    {
                        player.SelfPosition.X += player.speed;
                    }
                    if(player.collision.Left < WallArea_Col[i].Left)
                    {
                        player.SelfPosition.X -= player.speed;
                    }
                    if (player.collision.Top > WallArea_Col[i].Top)
                    {
                        player.SelfPosition.Y += player.speed;
                    }
                    if(player.collision.Bottom < WallArea_Col[i].Bottom)
                    {
                        player.SelfPosition.Y -= player.speed;

                    }

                }
               
               
                

            }

            //Object Interaction
            if (player.collision.Intersects(DoorCollision) == true)
            {
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    player.ChangeStartingPosition(new Vector2(64, 640 / 2));
                    roomManager.Roomchange(1);
                }


            }
            OldKey = KeyControls;
        }
    }
}
