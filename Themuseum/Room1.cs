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
        private Texture2D HiddenSwitch_02_Tex;
        private Rectangle HiddenSwitch_02_Col;
        private Vector2 HiddenSwitch_02_Pos;
        private Texture2D HiddenSwitch_03_Tex;
        private Rectangle HiddenSwitch_03_Col;
        private Vector2 HiddenSwitch_03_Pos;
        private Vector2 DoorPos;
        private Rectangle DoorCollision;
        private Shire R1_Shire;
        private Texture2D piece1;
        private Vector2 piece1Pos;
        private Texture2D piece2;
        private Vector2 piece2Pos;
        private int player_pieceActive = 0;
        private KeyboardState KeyControls;
        private KeyboardState OldKey;
       Random r = new Random();

        

        public Room1()
        {
            DoorPos = new Vector2(64, 640 / 2);
            DoorCollision = new Rectangle((int)DoorPos.X, (int)DoorPos.Y, 32, 64);
            HiddenSwitch_01_Pos = new Vector2(1000,320);
            HiddenSwitch_01_Col = new Rectangle((int)HiddenSwitch_01_Pos.X, (int)HiddenSwitch_01_Pos.Y,64,64);
            HiddenSwitch_02_Pos = new Vector2(10000, 10000);
            HiddenSwitch_02_Col = new Rectangle((int)HiddenSwitch_02_Pos.X, (int)HiddenSwitch_02_Pos.Y, 64, 64);
            HiddenSwitch_03_Pos = new Vector2(10000, 10000);
            HiddenSwitch_03_Col = new Rectangle((int)HiddenSwitch_03_Pos.X, (int)HiddenSwitch_03_Pos.Y, 64, 64);
            R1_Shire = new Shire(new Vector2(1100, 70));
            
            piece1Pos = new Vector2(r.Next(110,200),r.Next(80, 250));
            piece2Pos = new Vector2(r.Next(250,420), r.Next(275,480));
        }

        public void LoadSprite(ContentManager content)
        {
            TileStatic = content.Load<Texture2D>("Room1(new)");
            Door = content.Load<Texture2D>("placeholderdoor");
            HiddenSwitch_01_Tex = content.Load<Texture2D>("hiddenswitch01_placeholder");
            HiddenSwitch_02_Tex = content.Load<Texture2D>("hiddenswitch02_placeholder");
            HiddenSwitch_03_Tex = content.Load<Texture2D>("hiddenswtich03_placeholder");
            R1_Shire.LoadSprite(content);
            piece1 = content.Load<Texture2D>("Piece1");
            piece2 = content.Load<Texture2D>("Piece2");
        }
        
        public void Draw(SpriteBatch SB)
        {
            SB.Draw(TileStatic, Vector2.Zero, Color.White);
            SB.Draw(Door, DoorPos, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
            SB.Draw(HiddenSwitch_01_Tex, HiddenSwitch_01_Pos, Color.White);
            SB.Draw(HiddenSwitch_02_Tex, HiddenSwitch_02_Pos, Color.White);
            SB.Draw(HiddenSwitch_03_Tex, HiddenSwitch_03_Pos, Color.White);
            R1_Shire.Draw(SB);
            if (player_pieceActive == 1)
            {
                SB.Draw(piece1, piece1Pos, Color.White);
                SB.Draw(piece2, piece2Pos, Color.White);
            }
        }

        public void Function(GraphicsDeviceManager _graphics, Player player,RoomManager roomManager, KeyManagement Keymanager, float elapsed)
        {
            
            KeyControls = Keyboard.GetState();
            
            //Wall Collision
            if (player.SelfPosition.X >= _graphics.GraphicsDevice.Viewport.Bounds.Right - 150)
            {
                if(player.speed >= 4)
                {
                    player.SelfPosition.X -= player.speed * 2;
                }
                else
                player.SelfPosition.X -= player.speed;
                
            } 

            else if (player.SelfPosition.X <= _graphics.GraphicsDevice.Viewport.Bounds.Left + 85)
            {
                if (player.speed >= 4)
                {
                    player.SelfPosition.X += player.speed * 2;
                }
                else
                    player.SelfPosition.X += player.speed;
            }

             else if (player.SelfPosition.Y <= _graphics.GraphicsDevice.Viewport.Bounds.Top + 64)
            {
                if (player.speed >= 4)
                {
                    player.SelfPosition.Y += player.speed * 2;
                }
                else
                    player.SelfPosition.Y += player.speed;
            }
            else if (player.SelfPosition.Y >= _graphics.GraphicsDevice.Viewport.Bounds.Bottom - 128 - 64)
            {
                if (player.speed >= 4)
                {
                    player.SelfPosition.Y -= player.speed * 2;
                }
                else
                    player.SelfPosition.Y -= player.speed;
            }
            //Objects Behavior
            DoorPos = new Vector2(64, 640 / 2);
            DoorCollision = new Rectangle((int)DoorPos.X, (int)DoorPos.Y, 32, 64);
            R1_Shire.Behavior(player, elapsed);
            HiddenSwitch_01_Pos = new Vector2(1000, 320);
            HiddenSwitch_01_Col = new Rectangle((int)HiddenSwitch_01_Pos.X, (int)HiddenSwitch_01_Pos.Y, 64, 64);
            HiddenSwitch_02_Col = new Rectangle((int)HiddenSwitch_02_Pos.X, (int)HiddenSwitch_02_Pos.Y, 64, 64);
            HiddenSwitch_03_Col = new Rectangle((int)HiddenSwitch_03_Pos.X, (int)HiddenSwitch_03_Pos.Y, 64, 64);

            Rectangle piece1Col = new Rectangle((int)piece1Pos.X, (int)piece1Pos.Y, 64, 64);
            Rectangle piece2Col = new Rectangle((int)piece2Pos.X, (int)piece2Pos.Y, 64, 64);

            if (Keymanager.R2_T1 == false)
            {
                HiddenSwitch_02_Pos = new Vector2(10000, 10000);
                HiddenSwitch_03_Pos = new Vector2(10000, 10000);
            }
            else if(Keymanager.R2_T1 == true)
            {
                HiddenSwitch_02_Pos = new Vector2(320, 128);
                HiddenSwitch_03_Pos = new Vector2(540, 512);
            }

            //Object Interaction
            if (player.collision.Intersects(DoorCollision) == true )
            {
                if(KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E) && Keymanager.R1_S1 == true)
                {
                    player.ChangeStartingPosition(new Vector2(1280 - 64, player.SelfPosition.Y));
                    roomManager.Roomchange(2);
                    Console.WriteLine("Door Opened");
                }
                
                
            }
            if(player.collision.Intersects(HiddenSwitch_01_Col) == true)
            {
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    Keymanager.KeyTrigger("R1_S1");
                    Console.WriteLine("R1_S1");
                }
            }
            if (player.collision.Intersects(HiddenSwitch_02_Col) == true)
            {
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    Keymanager.KeyTrigger("R1_S2");
                    Console.WriteLine("R1_S2");
                }
            }
            if (player.collision.Intersects(HiddenSwitch_03_Col) == true)
            {
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    Keymanager.KeyTrigger("R1_S3");
                    Console.WriteLine("R1_S3");
                }
            }
            if (Keymanager.MRB_PieceActive == true)
            {
                player_pieceActive = 1;
            }

            //Piece Collect
            if (player.collision.Intersects(piece1Col) == true)
            {
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    Keymanager.MRB_Pieces += 1;
                    Console.WriteLine(Keymanager.MRB_Pieces);
                    piece1Pos.X = 5000;
                }
            }
            if (player.collision.Intersects(piece2Col) == true)
            {
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    Keymanager.MRB_Pieces += 1;
                    Console.WriteLine(Keymanager.MRB_Pieces);
                    piece2Pos.X = 5000;
                }
            }


            OldKey = KeyControls;
        }

        
    }
}
