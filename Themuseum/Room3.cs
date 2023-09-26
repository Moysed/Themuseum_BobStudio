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
    class Room3
    {
        Room1 room1;
        private Texture2D Map_Sprite;
        Rectangle BacktoRoom2;
        private Texture2D TileStatic;
        KeyboardState KeyControls;
        private Texture2D Door;
        private Vector2 DoorPos;
        private Rectangle DoorCollision;
        private Vector2 DoorPos_MRB_MRC_C;
        private Rectangle DoorCollision_MRB_MRC_C;
        private Texture2D WallArea_Tex;
        KeyboardState Oldkey_;
        private List<Rectangle> WallArea_Col = new List<Rectangle>();


        public Room3()
        {
            room1 = new Room1();
            WallArea_Col.Add(new Rectangle(0, 300, 500, 2000));
            WallArea_Col.Add(new Rectangle(700, 300, 1000, 2000));
            WallArea_Col.Add(new Rectangle(0, 0, 2000, 100));
            WallArea_Col.Add(new Rectangle(0, 0, 100, 500));
            WallArea_Col.Add(new Rectangle(1180, 0, 100, 500));
            DoorPos = new Vector2(610, 35);
        }

        public void Loadsprite(ContentManager content)
        {
            Map_Sprite = content.Load<Texture2D>("MapSheet");
            TileStatic = content.Load<Texture2D>("room3_placeholder");
            WallArea_Tex = content.Load<Texture2D>("wallplaceholder");
            Door = content.Load<Texture2D>("placeholderdoor");
            
        }

        public void Draw(SpriteBatch SB)
        {
            SB.Draw(TileStatic, Vector2.Zero, Color.White);


            for (int i = 0; i < WallArea_Col.Count; i++)
            {
                SB.Draw(WallArea_Tex, WallArea_Col[i], Color.White);
            }
            SB.Draw(Door, DoorPos, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
            SB.Draw(Door, DoorPos_MRB_MRC_C, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
            if (room1.showMap == true)
            {
                SB.Draw(Map_Sprite, new Vector2(320, 0), Color.White);
            }
        }
        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed, DialogueBox dialogue, LanternLight light)
        {
            KeyControls = Keyboard.GetState();
            //Object Hitbox
            DoorPos = new Vector2(610, 35);
            DoorPos_MRB_MRC_C = new Vector2(1180, 200);
            DoorCollision_MRB_MRC_C = new Rectangle((int)DoorPos_MRB_MRC_C.X - 10, (int)DoorPos_MRB_MRC_C.Y, 32, 70);
            BacktoRoom2 = new Rectangle(501, 600, 190, 200);
            DoorCollision = new Rectangle((int)DoorPos.X +5, (int)DoorPos.Y, 40, 72);
            
            //Wall Collision
            if (player.SelfPosition.X >= _graphics.GraphicsDevice.Viewport.Bounds.Right - 64)
            {
                if (player.speed == 4)
                {
                    player.SelfPosition.X -= player.speed * 2;
                }
                else
                    player.SelfPosition.X -= player.speed;
            }

            else if (player.SelfPosition.X <= _graphics.GraphicsDevice.Viewport.Bounds.Left + 64)
            {
                if (player.speed == 4)
                {
                    player.SelfPosition.X += player.speed * 2;
                }
                else
                    player.SelfPosition.X += player.speed;
            }

            else if (player.SelfPosition.Y <= _graphics.GraphicsDevice.Viewport.Bounds.Top + 64)
            {
                if (player.speed == 4)
                {
                    player.SelfPosition.Y += player.speed * 2;
                }
                else
                    player.SelfPosition.Y += player.speed;
            }

            for (int i = 0; i < WallArea_Col.Count; i++)
            {
                if (WallArea_Col[i].Intersects(player.collision))
                {
                    if (player.collision.Right >= WallArea_Col[i].Right)
                    {
                        if (player.speed == 4)
                        {
                            player.SelfPosition.X += player.speed * 2;
                        }
                        else
                            player.SelfPosition.X += player.speed;
                    }
                    if (player.collision.Left <= WallArea_Col[i].Left)
                    {
                        if (player.speed == 4)
                        {
                            player.SelfPosition.X -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.X -= player.speed;
                    }
                    if (player.collision.Top >= WallArea_Col[i].Top)
                    {
                        if (player.speed == 4)
                        {
                            player.SelfPosition.Y += player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y += player.speed;
                    }
                    if (player.collision.Bottom <= WallArea_Col[i].Bottom)
                    {
                        if (player.speed == 4)
                        {

                            player.SelfPosition.Y -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y -= player.speed;

                    }
                }
            }
                if (player.collision.Intersects(BacktoRoom2) == true)
                {
                    Console.WriteLine("Changed to Room2");
                    player.ChangeStartingPosition(new Vector2(player.SelfPosition.X, 200));
                    roomManager.Roomchange(2);
                }
                if (player.collision.Intersects(DoorCollision) == true)
                {
                    
                    if (KeyControls.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E) && Keymanager.R1_S2 == true && Keymanager.R1_S3 == true)
                    {
                        Keymanager.MRB_PieceActive = true;
                        player.ChangeStartingPosition(new Vector2(player.SelfPosition.X, 75*7));
                        roomManager.Roomchange(4);
                    }
                }
                if(player.collision.Intersects(DoorCollision_MRB_MRC_C) == true)
                {
                    if (KeyControls.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E) && Keymanager.MRB_StatueActive == true)
                    {
                        
                        player.ChangeStartingPosition(new Vector2(75,player.SelfPosition.Y));
                        roomManager.Roomchange(5);
                    }
                }
            if (room1.mapActive = true && Keyboard.GetState().IsKeyDown(Keys.G))
            {
                room1.showMap = true;
            }
            else
            {
                room1.showMap = false;
            }
        }

        }
    }
