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
        private Texture2D KeyB;
        Rectangle keyBcol;
        Rectangle BacktoRoom2;
        private Texture2D piece3;
        private Vector2 piece3Pos;
        private Rectangle piece3col;
        private Texture2D TileStatic;
        KeyboardState KeyControls;
        private Texture2D Door;
        private Vector2 DoorPos;
        private Rectangle DoorCollision;
        private Vector2 DoorPos_MRB_MRC_C;
        private Rectangle DoorCollision_MRB_MRC_C;
        private Texture2D WallArea_Tex;
        private Vector2 keyBPos;
        KeyboardState Oldkey_;
        private List<Rectangle> WallArea_Col = new List<Rectangle>();
        Random random = new Random();
        private Texture2D wood;

        private Texture2D hint;
        private Vector2 hintPos;


        public Room3()
        {

            piece3Pos = new Vector2(random.Next(100, 700), random.Next(250, 400));
            room1 = new Room1();
            hintPos = new Vector2(400, 270);
            WallArea_Col.Add(new Rectangle(0, 450, 445, 300));
            WallArea_Col.Add(new Rectangle(835, 450, 1000, 2000));
            WallArea_Col.Add(new Rectangle(0, 0, 68, 2000));
            WallArea_Col.Add(new Rectangle(70, 0, 2000, 192));
            WallArea_Col.Add(new Rectangle(100, 250, 100, 200));
            DoorPos = new Vector2(610, 35);
            keyBPos = Vector2.Zero;
        }

        public void Loadsprite(ContentManager content)
        {
            hint = content.Load<Texture2D>("Note");
            Map_Sprite = content.Load<Texture2D>("MapSheet");
            TileStatic = content.Load<Texture2D>("Room3 bg");
            Door = content.Load<Texture2D>("placeholderdoor");
            KeyB = content.Load<Texture2D>("smallKey");
            piece3 = content.Load<Texture2D>("leftPiece");
            wood = content.Load<Texture2D>("Wood");
        }

        public void Draw(SpriteBatch SB,LanternLight light, Color roomcolor , KeyManagement key)
        {
           
            SB.Draw(TileStatic, Vector2.Zero, roomcolor);
            SB.Draw(wood, new Vector2(100,300), roomcolor);
            SB.Draw(wood, new Vector2(150, 350), roomcolor);
            SB.Draw(wood, new Vector2(150, 300), roomcolor);
            SB.Draw(wood, new Vector2(100, 350), roomcolor);
            SB.Draw(wood, new Vector2(150, 250), roomcolor);
            SB.Draw(wood, new Vector2(100, 250), roomcolor);
            if (key.MRB_StatueActive == true)
            {
                SB.Draw(KeyB, keyBPos ,Color.White);
            }
            if(key.MRB_PieceActive == true) {

                if (light.Collision.Intersects(piece3col))
                {
                    SB.Draw(piece3, piece3Pos, Color.White);
                }
                
            }
            
                SB.Draw(hint, hintPos, Color.White);
            
        }
        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed, DialogueBox dialogue, LanternLight light,SoundSystem sound, Ghost ghost, Staminabar UI)
        {
            KeyControls = Keyboard.GetState();
            //Object Hitbox
            Rectangle hint = new Rectangle((int)hintPos.X, (int)hintPos.Y, 64, 64);
            piece3col = new Rectangle((int)piece3Pos.X, (int)piece3Pos.Y, 24, 54);
            DoorCollision_MRB_MRC_C = new Rectangle(1200, 0, 64, 640);
            BacktoRoom2 = new Rectangle(400, 600, 400, 200);
            DoorCollision = new Rectangle(568, 57, 138, 194);
            keyBcol = new Rectangle((int)keyBPos.X, (int)keyBPos.Y, 26, 10);
            
            
            //Wall Collision
            if (player.SelfPosition.X >= _graphics.GraphicsDevice.Viewport.Bounds.Right - 64)
            {
                if (player.speed == 2)
                {
                    player.SelfPosition.X -= player.speed * 2;
                }
                else
                    player.SelfPosition.X -= player.speed;
            }

            else if (player.SelfPosition.X <= _graphics.GraphicsDevice.Viewport.Bounds.Left + 64)
            {
                if (player.speed == 2)
                {
                    player.SelfPosition.X += player.speed * 2;
                }
                else
                    player.SelfPosition.X += player.speed;
            }

            else if (player.SelfPosition.Y <= _graphics.GraphicsDevice.Viewport.Bounds.Top + 64)
            {
                if (player.speed == 2)
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
                    if (player.collision.Bottom <= WallArea_Col[i].Bottom)
                    {
                        if (player.speed == 2)
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
                    ghost.Prechase(player, Keymanager);
                    Console.WriteLine("Changed to Room2");
                    sound.PlaySfx(1);
                    player.ChangeStartingPosition(new Vector2(player.SelfPosition.X, 200));

                    roomManager.Roomchange(2);
                }
                if (player.collision.Intersects(DoorCollision) == true)
                {
                    player.StatusTextDisplay("Press K to Interact");
                    if (KeyControls.IsKeyDown(Keys.K) && Oldkey_.IsKeyUp(Keys.K) && Keymanager.R1_S2 == true && Keymanager.R1_S3 == true)
                    {
                        ghost.Prechase(player, Keymanager);
                        sound.PlaySfx(1);
                        Keymanager.MRB_PieceActive = true;
                        player.ChangeStartingPosition(new Vector2(player.SelfPosition.X, 75*6));
                        UI.ChangeObjectiveText("Find Clues and Useful items", "");
                        roomManager.Roomchange(4);
                    }
                    else if(KeyControls.IsKeyDown(Keys.K) && Oldkey_.IsKeyUp(Keys.K) && Keymanager.R1_S2 == false || KeyControls.IsKeyDown(Keys.K) && Oldkey_.IsKeyUp(Keys.K) && Keymanager.R1_S3 == false)
                    {
                        sound.PlaySfx(2);
                        dialogue.SettingParameter("Hint Block", 0, 0, "The door is locked, Find a way to open it", Color.Purple);
                        UI.ChangeObjectiveText($"Find Hidden Switches {Keymanager.MRBSwitchcount}/2", "Hint: Try exploring other rooms");
                        dialogue.Activation(true);
                    }
                }
            
                if (player.collision.Intersects(piece3col) == true && Keymanager.MRB_PieceActive == true)
                {
                if (light.Collision.Intersects(piece3col) == true)
                {
                    player.StatusTextDisplay("Press K to Interact");
                }
                if (KeyControls.IsKeyDown(Keys.K) && Oldkey_.IsKeyUp(Keys.K))
                {
                    dialogue.SettingParameter("placeholderblock", 200, 200, "Statue piece collected", Color.Green);
                    dialogue.Activation(true);
                    sound.PlaySfx(0);
                    Keymanager.MRB_Pieces += 1;
                    UI.ChangeObjectiveText($"Find pieces for statue {Keymanager.MRB_Pieces}/3", "Hint: Pieces are hidden items");
                    Console.WriteLine(Keymanager.MRB_Pieces);
                    piece3Pos.X = 5000;
                }
            }
            if (player.collision.Intersects(DoorCollision_MRB_MRC_C) == true)
            {
                player.StatusTextDisplay("Press K to Interact");
                if (KeyControls.IsKeyDown(Keys.K) && Oldkey_.IsKeyUp(Keys.K) && Keymanager.KeyCollectB == true)
                {
                    ghost.Prechase(player, Keymanager);
                    sound.PlaySfx(1);
                    player.ChangeStartingPosition(new Vector2(75, 8*32));
                    UI.ChangeObjectiveText("Find a way through the courtyard", "");
                    roomManager.Roomchange(7);
                }
                else if(KeyControls.IsKeyDown(Keys.K) && Oldkey_.IsKeyUp(Keys.K) && Keymanager.KeyCollectB == false)
                {
                    sound.PlaySfx(2);
                    dialogue.SettingParameter("Hint Block", 0, 0, "The door is locked, You need to find a key", Color.Purple);
                    dialogue.Activation(true);
                }
            }
            if (player.collision.Intersects(keyBcol) == true)
            {
                player.StatusTextDisplay("Press K to Interact");
                if (KeyControls.IsKeyDown(Keys.K) && Oldkey_.IsKeyUp(Keys.K))
                {
                    dialogue.SettingParameter("placeholderblock", 200, 200, "Key collected", Color.Green);
                    dialogue.Activation(true);
                    sound.PlaySfx(0);
                    keyBPos = new Vector2(5000,0);
                    Keymanager.KeyCollectB = true;
                }
            }
            if (Keymanager.MRB_StatueActive == true)
            {
                
                keyBPos = new Vector2(200, 300);
                if(Keymanager.KeyCollectB == true)
                {
                    keyBPos = new Vector2(500000, 300);
                }
            }

            if (player.collision.Intersects(hint) == true)
            {
                player.StatusTextDisplay("Press K to Interact");
                if (KeyControls.IsKeyDown(Keys.K) && Oldkey_.IsKeyUp(Keys.K))
                {
                    sound.PlaySfx(3);
                    dialogue.SettingParameter("Hint Block", 200, 200, "Turn back to where you first arrived. Two keys awaits.\r\nOne in the northwest, another in the south", Color.Brown);
                    dialogue.Activation(true);
                }



            }

            Oldkey_ = KeyControls;
        }
        public void Reset()
        {
            keyBPos = new Vector2(50000, 300);
            piece3Pos = new Vector2(random.Next(100, 700), random.Next(250, 400));
        }

        }
    }
