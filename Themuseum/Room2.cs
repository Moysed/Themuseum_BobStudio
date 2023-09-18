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
        private Rectangle EndofHallway;
        private Texture2D WallArea_Tex;
        private List<Rectangle> WallArea_Col = new List<Rectangle>();
        private Texture2D R2_T1_Trigger_Tex;
        private Vector2 R2_T1_Trigger_Pos;
        private Rectangle R2_T1_Trigger_Col;
        private KeyboardState KeyControls;
        private Texture2D piece3;
        private Vector2 piece3Pos;
        private Texture2D piece4;
        private Vector2 piece4Pos;
        private int player_pieceActive = 0;
        private KeyboardState OldKey;
        Random random = new Random();
        public Room2()
        {
            random = new Random();
            DoorPos = new Vector2(1280-64, 640 / 2);
            DoorCollision = new Rectangle((int)DoorPos.X, (int)DoorPos.Y, 32, 64);
            
            /*WallArea_Col.Add(new Rectangle(0, 0, 500, 640));
            WallArea_Col.Add(new Rectangle(700, 0, 1000, 300));*/

            R2_T1_Trigger_Pos = new Vector2(640,256);
            R2_T1_Trigger_Col = new Rectangle((int)R2_T1_Trigger_Pos.X, (int)R2_T1_Trigger_Pos.X, 128, 128);

            piece3Pos = new Vector2(random.Next(520,600), random.Next(190,200));
            piece4Pos = new Vector2(random.Next(600,650), random.Next(360,400));
        }

       
        public void LoadSprite(ContentManager content)
        {
            TileStatic = content.Load<Texture2D>("Hallway1");
            Door = content.Load<Texture2D>("placeholderdoor");
            WallArea_Tex = content.Load<Texture2D>("wallplaceholder");
            R2_T1_Trigger_Tex = content.Load<Texture2D>("199-Support07");
            piece3 = content.Load<Texture2D>("Piece3");
            piece4 = content.Load<Texture2D>("Piece4");
        }

        public void Draw(SpriteBatch SB)
        {
            SB.Draw(TileStatic, Vector2.Zero, Color.White);
            SB.Draw(Door, DoorPos, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
            SB.Draw(R2_T1_Trigger_Tex, R2_T1_Trigger_Pos, new Rectangle(0, 0, 64, 64), Color.White);

            /*for(int i = 0; i < WallArea_Col.Count; i++)
            {
                SB.Draw(WallArea_Tex, WallArea_Col[i], Color.White);
            }*/

            if(player_pieceActive == 1) 
            {
                SB.Draw(piece3, piece3Pos, Color.White);
                SB.Draw(piece4, piece4Pos, Color.White);
            }
        }

        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed)
        {
           
            KeyControls = Keyboard.GetState();

            //Wall Collision
            if (player.SelfPosition.X >= _graphics.GraphicsDevice.Viewport.Bounds.Right - 475)
            {
                if (player.SelfPosition.Y <= _graphics.GraphicsDevice.Viewport.Bounds.Top + 365)
                {
                    if (player.speed == 4)
                    {
                        player.SelfPosition.X -= player.speed * 2;
                    }
                    else
                        player.SelfPosition.X -= player.speed;
                }
            }
            if (player.SelfPosition.X >= _graphics.GraphicsDevice.Viewport.Bounds.Right - 590)
            {
                if (player.SelfPosition.Y > _graphics.GraphicsDevice.Viewport.Bounds.Top + 300)
                {
                    if (player.speed == 4)
                    {
                        player.SelfPosition.X -= player.speed * 2;
                    }
                    else
                        player.SelfPosition.X -= player.speed;
                }
            }
            else if (player.SelfPosition.X <= _graphics.GraphicsDevice.Viewport.Bounds.Left + 435)
            {
                if (player.SelfPosition.Y <= _graphics.GraphicsDevice.Viewport.Bounds.Top + 365)
                {
                    if (player.speed == 4)
                    {
                        player.SelfPosition.X += player.speed * 2;
                    }
                    else
                        player.SelfPosition.X += player.speed;
                }
            }
            else if (player.SelfPosition.X <= _graphics.GraphicsDevice.Viewport.Bounds.Left + 544)            
            {
                if (player.SelfPosition.Y > _graphics.GraphicsDevice.Viewport.Bounds.Top + 300)
                {
                    if (player.speed == 4)
                    {
                        player.SelfPosition.X += player.speed * 2;
                    }
                    else
                        player.SelfPosition.X += player.speed;
                }
            }

            else if (player.SelfPosition.Y <= _graphics.GraphicsDevice.Viewport.Bounds.Top + 180)
            {
                if (player.speed == 4)
                {
                    player.SelfPosition.Y += player.speed * 2;
                }
                else
                    player.SelfPosition.Y += player.speed;
            }
            else if (player.SelfPosition.Y >= _graphics.GraphicsDevice.Viewport.Bounds.Bottom - 185)
            {
                if (player.speed == 4)
                {
                    player.SelfPosition.Y -= player.speed * 2;
                }
                else
                    player.SelfPosition.Y -= player.speed;
            }

            for(int i = 0; i < WallArea_Col.Count; i++)
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
                    else if (player.SelfPosition.X <= _graphics.GraphicsDevice.Viewport.Bounds.Left + 435)
                    {
                        if (player.SelfPosition.Y <= _graphics.GraphicsDevice.Viewport.Bounds.Top + 365)
                        {
                            if (player.speed == 4)
                            {
                                player.SelfPosition.X += player.speed * 2;
                            }
                            else
                                player.SelfPosition.X += player.speed;
                        }
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
                    if(player.collision.Bottom <= WallArea_Col[i].Bottom)
                    {
                        if (player.speed == 4 )
                        {
                            
                            player.SelfPosition.Y -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y -= player.speed;

                    }

                }

               if(Keymanager.MRB_PieceActive == true)
                {
                    player_pieceActive = 1;
                }
               
                

            }
            //Object Behavior
            DoorPos = new Vector2(1280 - 64, 640 / 2);
            DoorCollision = new Rectangle((int)DoorPos.X, (int)DoorPos.Y, 32, 64);
            EndofHallway = new Rectangle(500, 0, 200, 70);
            R2_T1_Trigger_Pos = new Vector2(540, 256);
            R2_T1_Trigger_Col = new Rectangle((int)R2_T1_Trigger_Pos.X, (int)R2_T1_Trigger_Pos.Y, 128, 128);

            Rectangle piece3Col = new Rectangle((int)piece3Pos.X, (int)piece3Pos.Y, 64, 64);
            Rectangle piece4Col = new Rectangle((int)piece4Pos.X, (int)piece4Pos.Y, 64, 64);

            //Object Interaction

            if (player.collision.Intersects(DoorCollision) == true)
            {
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    Console.WriteLine("Door Opened");
                    player.ChangeStartingPosition(new Vector2(64, player.SelfPosition.Y));
                    roomManager.Roomchange(1);
                }
            }

            if(R2_T1_Trigger_Col.Intersects(player.collision) == true && Keymanager.R2_T1 == false)
            {
                Keymanager.KeyTrigger("R2_T1");
                Console.WriteLine("R2_T1 Triggered");
                player.IsHaunted = true;
            }

            if(player.collision.Intersects(EndofHallway) == true)
            {
                Console.WriteLine("Changed to Room3");
                player.ChangeStartingPosition(new Vector2(player.SelfPosition.X, 450));
                roomManager.Roomchange(3);
            }

            //Piece Collect
            if (player.collision.Intersects(piece3Col) == true)
            {
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    Keymanager.MRB_Pieces += 1;
                    Console.WriteLine(Keymanager.MRB_Pieces);
                    piece3Pos.X = 5000;
                }
            }
            if (player.collision.Intersects(piece4Col) == true)
            {
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    Keymanager.MRB_Pieces += 1;
                    Console.WriteLine(Keymanager.MRB_Pieces);
                    piece4Pos.X = 5000;
                }
            }

            OldKey = KeyControls;
        }
    }
}
