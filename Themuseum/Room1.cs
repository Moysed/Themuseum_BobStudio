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
    class Room1
    {
        public Map map;
        bool switch1_opened = false;
        bool switch2_opened = false;
        bool switch3_opened = false;
        private Texture2D Lantern;
        private Vector2 lanternPos;
        private Texture2D Map;
        private Texture2D Map_Sprite;
        private Vector2 mapPos;
        private Texture2D hint;
        private Vector2 hintPos;
        private Texture2D TileStatic;
        private Texture2D Door;
        private Texture2D HiddenSwitch_01_Tex;
        private Texture2D openedSwitch;
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
        private Rectangle piece1Col;
        private bool player_pieceActive = false;
        private KeyboardState KeyControls;
        private KeyboardState OldKey;
        private KeyManagement keyManagement;

        List<Rectangle> Counter_Col = new List<Rectangle>();
        

        Random r = new Random();


        public Room1()
        {
            keyManagement = new KeyManagement();
            mapPos = new Vector2(500, 290);
            hintPos = new Vector2(200, 400);
            lanternPos = new Vector2(740, 250);
            DoorPos = new Vector2(200, 0);
            DoorCollision = new Rectangle(520, 200, 200, 300);
            HiddenSwitch_01_Pos = new Vector2(1000, 320);
            HiddenSwitch_01_Col = new Rectangle((int)HiddenSwitch_01_Pos.X, (int)HiddenSwitch_01_Pos.Y, 64, 64);
            HiddenSwitch_02_Pos = new Vector2(10000, 10000);
            HiddenSwitch_02_Col = new Rectangle((int)HiddenSwitch_02_Pos.X, (int)HiddenSwitch_02_Pos.Y, 64, 64);
            HiddenSwitch_03_Pos = new Vector2(10000, 10000);
            HiddenSwitch_03_Col = new Rectangle((int)HiddenSwitch_03_Pos.X, (int)HiddenSwitch_03_Pos.Y, 64, 64);
            Counter_Col.Add(new Rectangle(465, 254, 345, 68));
            Counter_Col.Add(new Rectangle(383, 164, 88, 157));
            Counter_Col.Add(new Rectangle(806, 164, 88, 157));
            R1_Shire = new Shire(new Vector2(400, 340));
            

            piece1Pos = new Vector2(r.Next(150, 200), r.Next(150, 400));
        }

        public void LoadSprite(ContentManager content)
        {
            Map_Sprite = content.Load<Texture2D>("MapSheet");
            hint = content.Load<Texture2D>("Note");
            Map = content.Load<Texture2D>("Map");
            Lantern = content.Load<Texture2D>("Lantern");
            TileStatic = content.Load<Texture2D>("Bg");
            Door = content.Load<Texture2D>("placeholderdoor");
            openedSwitch = content.Load<Texture2D>("switch_Opened");
            HiddenSwitch_01_Tex = content.Load<Texture2D>("switch_Closed");
            HiddenSwitch_02_Tex = content.Load<Texture2D>("switch_Closed");
            HiddenSwitch_03_Tex = content.Load<Texture2D>("switch_Closed");
            R1_Shire.LoadSprite(content);
            
            piece1 = content.Load<Texture2D>("rightPiece");

            
        }

        public void Draw(SpriteBatch SB, LanternLight light, Color roomcolor)
        {

            SB.Draw(TileStatic, Vector2.Zero, roomcolor);
            //SB.Draw(Door, DoorPos, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
            if (light.Collision.Intersects(HiddenSwitch_01_Col))
            {
                if(switch1_opened == true)
                {
                    SB.Draw(openedSwitch, HiddenSwitch_01_Pos, Color.White);
                }
                else
                {
                    SB.Draw(HiddenSwitch_01_Tex, HiddenSwitch_01_Pos, Color.White);
                }
            }
            else if (light.Collision.Intersects(HiddenSwitch_02_Col))
            {
                if (switch2_opened == true)
                {
                    SB.Draw(openedSwitch, HiddenSwitch_02_Pos, Color.White);
                }
                else
                {
                    SB.Draw(HiddenSwitch_02_Tex, HiddenSwitch_02_Pos, Color.White);
                }
            }
            else if (light.Collision.Intersects(HiddenSwitch_03_Col))
            {
                if (switch3_opened == true)
                {
                    SB.Draw(openedSwitch, HiddenSwitch_03_Pos, Color.White);
                }
                else
                {
                    SB.Draw(HiddenSwitch_03_Tex, HiddenSwitch_03_Pos, Color.White);
                }
            }

            if (player_pieceActive == true)
            {
                if (light.Collision.Intersects(piece1Col))
                {
                    SB.Draw(piece1, piece1Pos, Color.White);
                }
            }
            SB.Draw(hint, hintPos, Color.White);
            
            if(light.IsActive == false)
            {
                SB.Draw(Lantern, lanternPos, Color.White);
            }

            
            
            R1_Shire.Draw(SB);
            SB.Draw(Map, mapPos, Color.White);


        }

        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed, DialogueBox dialogue, LanternLight light, Map map,SoundSystem sound,Ghost ghost,Staminabar UI)
        {

            KeyControls = Keyboard.GetState();

            //Wall Collision
            if (player.SelfPosition.X >= _graphics.GraphicsDevice.Viewport.Bounds.Right - 150)
            {
                if (player.speed >= 2)
                {
                    player.SelfPosition.X -= player.speed * 2;
                }
                else
                    player.SelfPosition.X -= player.speed;

            }

            else if (player.SelfPosition.X <= _graphics.GraphicsDevice.Viewport.Bounds.Left + 85)
            {
                if (player.speed >= 2)
                {
                    player.SelfPosition.X += player.speed * 2;
                }
                else
                    player.SelfPosition.X += player.speed;
            }

            else if (player.SelfPosition.Y <= _graphics.GraphicsDevice.Viewport.Bounds.Top + 64)
            {
                if (player.speed >= 2)
                {
                    player.SelfPosition.Y += player.speed * 2;
                }
                else
                    player.SelfPosition.Y += player.speed;
            }
            else if (player.SelfPosition.Y >= _graphics.GraphicsDevice.Viewport.Bounds.Bottom - 142)
            {
                if (player.speed >= 2)
                {
                    player.SelfPosition.Y -= player.speed * 2;
                }
                else
                    player.SelfPosition.Y -= player.speed;
            }

            for (int i = 0; i < Counter_Col.Count; i++)
            {
                if (Counter_Col[i].Intersects(player.collision))
                {
                    if (player.collision.Right >= Counter_Col[i].Right)
                    {
                        if (player.speed == 3)
                        {
                            player.SelfPosition.X += player.speed * 2;
                        }
                        else
                            player.SelfPosition.X += player.speed;
                    }
                    if (player.collision.Left <= Counter_Col[i].Left)
                    {
                        if (player.speed == 3)
                        {
                            player.SelfPosition.X -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.X -= player.speed;
                    }
                    if (player.collision.Top >= Counter_Col[i].Top)
                    {
                        if (player.speed == 3)
                        {
                            player.SelfPosition.Y += player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y += player.speed;
                    }
                    if (player.collision.Bottom <= Counter_Col[i].Bottom)
                    {
                        if (player.speed == 3)
                        {

                            player.SelfPosition.Y -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y -= player.speed;

                    }
                }
            }

           
            

                    //COunter col

                    //Objects Behavior
                    Rectangle hint = new Rectangle((int)hintPos.X, (int)hintPos.Y, 64, 64);
                    Rectangle Lantern = new Rectangle((int)lanternPos.X, (int)lanternPos.Y , 40, 95);
                    Rectangle mapCol = new Rectangle((int)mapPos.X, (int)mapPos.Y, 74, 60);
                    DoorCollision = new Rectangle(540, 0, 200, 140);
                    R1_Shire.Behavior(player, elapsed,sound, roomManager);
                    HiddenSwitch_01_Col = new Rectangle((int)HiddenSwitch_01_Pos.X, (int)HiddenSwitch_01_Pos.Y, 64, 64);
                    HiddenSwitch_02_Col = new Rectangle((int)HiddenSwitch_02_Pos.X, (int)HiddenSwitch_02_Pos.Y, 64, 64);
                    HiddenSwitch_03_Col = new Rectangle((int)HiddenSwitch_03_Pos.X, (int)HiddenSwitch_03_Pos.Y, 64, 64);

                    piece1Col = new Rectangle((int)piece1Pos.X, (int)piece1Pos.Y, 24, 54);
                    if(Keymanager.R1_T0 == false)
                    {
                        HiddenSwitch_01_Pos = new Vector2(10000, 10000);
            }
                    else if(Keymanager.R1_T0 == true)
                    {
                        HiddenSwitch_01_Pos = new Vector2(950, 28);
                    }

                    if (Keymanager.R2_T1 == false)
                    {
                        HiddenSwitch_02_Pos = new Vector2(10000, 10000);
                        HiddenSwitch_03_Pos = new Vector2(10000, 10000);
            }
                    else if (Keymanager.R2_T1 == true)
                    {
                        HiddenSwitch_02_Pos = new Vector2(320, 28);
                        HiddenSwitch_03_Pos = new Vector2(540, 572);
                    }

                    //Object Interaction
                    if (player.collision.Intersects(DoorCollision) == true)
                    {
                            player.StatusTextDisplay("Press K to Interact");
                        if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K) && Keymanager.R1_S1 == true)
                        {
                            sound.PlaySfx(1);
                            ghost.Prechase(player);
                            player.ChangeStartingPosition(new Vector2(player.SelfPosition.X, _graphics.GraphicsDevice.Viewport.Bounds.Bottom - 128));
                            roomManager.Roomchange(2);
                            Console.WriteLine("Door Opened");
                        }
                        else if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K) && Keymanager.R1_S1 == false)
                        {
                            sound.PlaySfx(2);
                    
                            Keymanager.R1_T0 = true;
                            UI.ChangeObjectiveText("Find Hidden Switch", "Hint: Use lantern to find hidden object");
                            dialogue.SettingParameter("Hint Block", 0, 0, "There's something blocking the way. But I can't see it. Find the switch to break it", Color.Red);
                            dialogue.Activation(true);
                        }

                    }
                   

                    if (player.collision.Intersects(HiddenSwitch_02_Col) == true && Keymanager.R1_S2 == false)
                    {
                        if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K))
                        {
                            switch2_opened = true;
                            sound.PlaySfx(1);
                            dialogue.SettingParameter("placeholderblock", 200, 200, "2nd Hidden Switch Activated", Color.Green);
                            dialogue.Activation(true);
                            Keymanager.KeyTrigger("R1_S2");
                            Keymanager.MRBSwitchcount++;
                            UI.ChangeObjectiveText($"Find Hidden Switches {Keymanager.MRBSwitchcount}/2", "Hint: Try exploring other rooms");
                            Console.WriteLine("R1_S2");
                        }
                    }
                    if (player.collision.Intersects(HiddenSwitch_03_Col) == true && Keymanager.R1_S3 == false)
                    {
                        if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K))
                        {
                            switch3_opened = true;
                            sound.PlaySfx(1);
                            dialogue.SettingParameter(
                            "placeholderblock", 200, 200, "3rd Hidden Switch Activated", Color.Green);
                            dialogue.Activation(true);
                            Keymanager.KeyTrigger("R1_S3");
                            Keymanager.MRBSwitchcount++;
                            UI.ChangeObjectiveText($"Find Hidden Switches {Keymanager.MRBSwitchcount}/2", "Hint: Try exploring other rooms");
                            Console.WriteLine("R1_S3");
                        }
                    }
                    if (Keymanager.MRB_PieceActive == true)
                    {
                        player_pieceActive = true;
                    }
                    else
                    {
                        player_pieceActive = false;
                    }

                    //Piece Collect
                    if (player.collision.Intersects(piece1Col) == true && player_pieceActive == true)
                    {
                        if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K))
                        {
                            sound.PlaySfx(0);
                            Keymanager.MRB_Pieces += 1;
                            dialogue.SettingParameter("placeholderblock", 200, 200, "Statue piece collected", Color.Green);
                            dialogue.Activation(true);
                            Console.WriteLine(Keymanager.MRB_Pieces);
                            UI.ChangeObjectiveText($"Find pieces for statue {Keymanager.MRB_Pieces}/3", "Hint: Pieces are hidden items");
                            piece1Pos.X = 5000;
                        }
                    }
                    if (player.collision.Intersects(HiddenSwitch_01_Col) == true)
                    {
                        if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K))
                        {
                            switch1_opened = true;
                            sound.PlaySfx(1);
                            Keymanager.KeyTrigger("R1_S1");
                            UI.ChangeObjectiveText("Continue to the next room", "Hint: Candle is limited. Find some candles to replace old one");
                            dialogue.SettingParameter("placeholderblock", 200, 200, "1st Hidden Switch Activated", Color.Green);
                            dialogue.Activation(true);
                            Console.WriteLine("R1_S1");
                        }
                    }
            

            //hint Collision
            if ( player.collision.Intersects(hint) == true)
                    {
                            player.StatusTextDisplay("Press K to Interact");
                        if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K))
                        {
                            sound.PlaySfx(3);
                            dialogue.SettingParameter("Hint Block", 200, 200, "Survive, be careful not to get caught by that creature. \r\nIf you are followed, the shire may be able to help you.", Color.Green);
                            dialogue.Activation(true);
                        }
                        
                
        
                    }

            
           



            /*else
            {
                dialogue.Activation(false);
            }*/

            if (player.collision.Intersects(Lantern) == true)
                    {
                        player.StatusTextDisplay("Press K to Interact");
                        if(KeyControls.IsKeyDown(Keys.K) == true && OldKey.IsKeyUp(Keys.K))
                        {
                            sound.PlaySfx(0);
                            light.IsActive = true;
                            light.lightStart = true;
                            lanternPos.X = 20000;
                            dialogue.SettingParameter("Hint Block", 0, 0, "There's Lantern. It could be useful.  Hold L to use Lantern\r\n Light may show you \"H I D D E N \" objects", Color.Red);
                            dialogue.Activation(true);
                            Console.WriteLine(player.CurrentFuel);
                        }
                       
                    }

                    if (player.collision.Intersects(mapCol) == true)
                    {
                        player.StatusTextDisplay("Press K to Interact");
                        if (KeyControls.IsKeyDown(Keys.K) == true && OldKey.IsKeyUp(Keys.K))
                        {

                            sound.PlaySfx(3);
                            map.IsActive = true;
                            mapPos.X = 20000;
                            dialogue.SettingParameter("Hint Block", 0, 0, "There's map. It's contained the layout of this building.  Press M to Open Map", Color.Red);
                            dialogue.Activation(true);
                            Console.WriteLine("Map Collected");
                        }
                   
                    }
  

                    OldKey = KeyControls;
                }
        public void Reset()
        {
            switch1_opened = false;
            switch2_opened = false;
            switch3_opened = false;
            lanternPos = new Vector2(740, 250);
            mapPos = new Vector2(430, 290);
            piece1Pos = new Vector2(r.Next(110, 200), r.Next(80, 250));
        }
    }
            
        }
 
    
           







