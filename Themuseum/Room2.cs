using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Themuseum
{
    class Room2 
    {
        Room1 room1;
        private Texture2D Map_Sprite;
        private LanternRefill lanternRefill;
        private Vector2 LanternRefillPos;
        //private float scale = 10f;
        //private float depth = 0f;
        //private float rotation = 0.0f;
        private Texture2D TileStatic;
        //private AnimatedTexture TileAnimated;
        private Texture2D Door;
        private Vector2 DoorPos;
        private Rectangle DoorCollision;
        private Rectangle EndofHallway;
        private Texture2D WallArea_Tex;
        private List<Rectangle> WallArea_Col = new List<Rectangle>();
        private Texture2D R2_T1_Trigger_Tex;
        private Vector2 R2_T1_Trigger_Pos;
        private Rectangle R2_T1_Trigger_Col;
        private Rectangle piece2Col;
        private KeyboardState KeyControls;
        private Texture2D piece2;
        private Vector2 piece2Pos;
        private bool player_pieceActive = false;
        private KeyboardState OldKey;
        Random random = new Random();
        private KeyManagement keyManagement;
        public Room2()
        {
            keyManagement = new KeyManagement();    
            room1 = new Room1();
            LanternRefillPos = new Vector2(550, 500);
            lanternRefill = new LanternRefill(LanternRefillPos);
            random = new Random();
            DoorPos = new Vector2(840, 252);
            DoorCollision = new Rectangle((int)DoorPos.X, (int)DoorPos.Y, 32, 64);
            //TileAnimated = new AnimatedTexture(Vector2.Zero,rotation,scale,depth);
            WallArea_Col.Add(new Rectangle(0, 0, 432, 400));
            WallArea_Col.Add(new Rectangle(0, 360, 550, 400));
            WallArea_Col.Add(new Rectangle(850, 0, 432, 400));
            WallArea_Col.Add(new Rectangle(740, 360, 432, 400));
            WallArea_Col.Add(new Rectangle(0, 0, 1280, 200));


            R2_T1_Trigger_Pos = new Vector2(640,256);
            R2_T1_Trigger_Col = new Rectangle((int)R2_T1_Trigger_Pos.X, (int)R2_T1_Trigger_Pos.X, 128, 128);

            piece2Pos = new Vector2(random.Next(520,600), random.Next(200,250));
        }

       
        public void LoadSprite(ContentManager content)
        {
            Map_Sprite = content.Load<Texture2D>("MapSheet");
            TileStatic = content.Load<Texture2D>("Hallway1");
            //TileAnimated.Load(content, "css_sprites (2)", 12, 1, 15);
            Door = content.Load<Texture2D>("placeholderdoor");
            WallArea_Tex = content.Load<Texture2D>("wallplaceholder");
            R2_T1_Trigger_Tex = content.Load<Texture2D>("199-Support07");
            piece2 = content.Load<Texture2D>("middlePiece");
            lanternRefill.LoadSprite(content);
        }

        public void Draw(SpriteBatch SB, LanternLight light, Color roomcolor)
        {
            SB.Draw(TileStatic, Vector2.Zero, roomcolor);
            //TileAnimated.DrawFrame(SB, Vector2.Zero, 1);
            //SB.Draw(Door, DoorPos, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
            //SB.Draw(R2_T1_Trigger_Tex, R2_T1_Trigger_Pos, new Rectangle(0, 0, 64, 64), Color.White);

            /*for(int i = 0; i < WallArea_Col.Count; i++)
            {
                SB.Draw(WallArea_Tex, WallArea_Col[i], Color.White);
            }*/

            if(player_pieceActive == true) 
            {
                if(light.Collision.Intersects(piece2Col)){
                    SB.Draw(piece2, piece2Pos, Color.White);
                }
                
            }

            lanternRefill.DrawSprite(SB);
        }

        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed, DialogueBox dialogue, LanternLight light, SoundSystem sound,Ghost ghost, Staminabar UI)
        {
           
            KeyControls = Keyboard.GetState();

            //Wall Collision
            

            for(int i = 0; i < WallArea_Col.Count; i++)
            {
                if (WallArea_Col[i].Intersects(player.collision))
                {
                    if (player.collision.Right >= WallArea_Col[i].Right)
                    {
                        if (player.speed == 2)
                        {
                            player.SelfPosition.X += player.speed * 2;
                        }
                        else
                            player.SelfPosition.X += player.speed;
                    }
                    if (player.collision.Left <= WallArea_Col[i].Left)
                    {
                        if (player.speed == 2)
                        {
                            player.SelfPosition.X -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.X -= player.speed;
                    }
                    if (player.collision.Top >= WallArea_Col[i].Top)
                    {
                        if (player.speed == 2)
                        {
                            player.SelfPosition.Y += player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y += player.speed;
                    }
                    if(player.collision.Bottom <= WallArea_Col[i].Bottom)
                    {
                        if (player.speed == 2 )
                        {
                            
                            player.SelfPosition.Y -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y -= player.speed;

                    }

                }

               if(Keymanager.MRB_PieceActive == true)
               {
                    player_pieceActive = true;
               }
               else
               {
                    player_pieceActive = false;
               }
               

                //TileAnimated.UpdateFrame(elapsed);

            }
            //Object Behavior
            DoorPos = new Vector2(0, 600);
            DoorCollision = new Rectangle((int)DoorPos.X, (int)DoorPos.Y, 1280, 20);
            EndofHallway = new Rectangle(589, 167, 111, 53);
            R2_T1_Trigger_Pos = new Vector2(540, 256);
            R2_T1_Trigger_Col = new Rectangle((int)R2_T1_Trigger_Pos.X, (int)R2_T1_Trigger_Pos.Y, 128, 128);

            piece2Col = new Rectangle((int)piece2Pos.X, (int)piece2Pos.Y, 24, 54);

            //Object Interaction

            if (player.collision.Intersects(DoorCollision) == true)
            {
                    ghost.Prechase(player);
                    sound.PlaySfx(1);
                    Console.WriteLine("Door Opened");
                    player.ChangeStartingPosition(new Vector2(player.SelfPosition.X, 72));
                    roomManager.Roomchange(1);
                
            }

            if(R2_T1_Trigger_Col.Intersects(player.collision) == true && Keymanager.R2_T1 == false)
            {
                dialogue.SettingParameter("Hint Block", 0, 0, "You feel evil presence pursuing you, find shire to repel it", Color.DarkRed);
                dialogue.Activation(true);
                UI.ChangeObjectiveText("Evade evil spirit", "Hint: Candlelight can slows down the spirit, Find shire to dispel it");
                Keymanager.KeyTrigger("R2_T1");
                Console.WriteLine("R2_T1 Triggered");
                player.IsHaunted = true;
                roomManager.mapcolor = Color.Red;
                sound.PlayBGM(1);
            }

            if(player.collision.Intersects(EndofHallway) == true)
            {
                player.StatusTextDisplay("Press K to Interact");
                if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K)){
                    ghost.Prechase(player);
                    Console.WriteLine("Changed to Room3");
                    sound.PlaySfx(1);
                    player.ChangeStartingPosition(new Vector2(player.SelfPosition.X, 500));
                    roomManager.Roomchange(3);
                }
                
            }

            //Piece Collect
            if (player.collision.Intersects(piece2Col) == true && Keymanager.MRB_PieceActive == true)
            {
                if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K))
                {
                    dialogue.SettingParameter("placeholderblock", 200, 200, "Statue piece collected", Color.Green);
                    dialogue.Activation(true);
                    sound.PlaySfx(0);
                    Keymanager.MRB_Pieces += 1;
                    Console.WriteLine(Keymanager.MRB_Pieces);
                    UI.ChangeObjectiveText($"Find pieces for statue {Keymanager.MRB_Pieces}/3", "Hint: Pieces are hidden items");
                    piece2Pos.X = 5000;
                }
            }
            


            lanternRefill.Behavior(player, sound);
            
            OldKey = KeyControls;
        }

        public void Reset()
        {
            lanternRefill.ResetState();
            piece2Pos = new Vector2(random.Next(520, 600), random.Next(190, 200));
        }

    }
}
