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
            WallArea_Col.Add(new Rectangle(0, 450, 445, 300));
            WallArea_Col.Add(new Rectangle(835, 450, 1000, 2000));
            WallArea_Col.Add(new Rectangle(0, 0, 68, 2000));
            WallArea_Col.Add(new Rectangle(70, 0, 2000, 192));
            DoorPos = new Vector2(610, 35);
        }

        public void Loadsprite(ContentManager content)
        {
            Map_Sprite = content.Load<Texture2D>("MapSheet");
            TileStatic = content.Load<Texture2D>("Room3 bg");
            Door = content.Load<Texture2D>("placeholderdoor");
            
        }

        public void Draw(SpriteBatch SB, Color roomcolor)
        {
            SB.Draw(TileStatic, Vector2.Zero, roomcolor);
        }
        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed, DialogueBox dialogue, LanternLight light,SoundSystem sound, Ghost ghost)
        {
            KeyControls = Keyboard.GetState();
            //Object Hitbox
            DoorCollision_MRB_MRC_C = new Rectangle(1200, 0, 64, 640);
            BacktoRoom2 = new Rectangle(400, 600, 400, 200);
            DoorCollision = new Rectangle(568, 57, 138, 194);
            
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
                        if (player.speed == 3)
                        {
                            player.SelfPosition.X += player.speed * 2;
                        }
                        else
                            player.SelfPosition.X += player.speed;
                    }
                    if (player.collision.Left <= WallArea_Col[i].Left)
                    {
                        if (player.speed == 3)
                        {
                            player.SelfPosition.X -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.X -= player.speed;
                    }
                    if (player.collision.Top >= WallArea_Col[i].Top)
                    {
                        if (player.speed == 3)
                        {
                            player.SelfPosition.Y += player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y += player.speed;
                    }
                    if (player.collision.Bottom <= WallArea_Col[i].Bottom)
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
                if (player.collision.Intersects(BacktoRoom2) == true)
                {
                    ghost.Prechase(player);
                    Console.WriteLine("Changed to Room2");
                    sound.PlaySfx(1);
                    player.ChangeStartingPosition(new Vector2(player.SelfPosition.X, 200));

                    roomManager.Roomchange(2);
                }
                if (player.collision.Intersects(DoorCollision) == true)
                {
                    player.StatusTextDisplay("Press E to Interact");
                    if (KeyControls.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E) && Keymanager.R1_S2 == true && Keymanager.R1_S3 == true)
                    {
                        ghost.Prechase(player);
                        sound.PlaySfx(1);
                        Keymanager.MRB_PieceActive = true;
                        player.ChangeStartingPosition(new Vector2(player.SelfPosition.X, 75*7));
                        
                        roomManager.Roomchange(4);
                    }
                    else if(KeyControls.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E) && Keymanager.R1_S2 == false || KeyControls.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E) && Keymanager.R1_S3 == false)
                    {
                        sound.PlaySfx(2);
                        dialogue.SettingParameter("Hint Block", 0, 0, "The door is locked, Find a way to open it", Color.Red);
                        dialogue.Activation(true);
                    }
                }
            if (player.collision.Intersects(DoorCollision_MRB_MRC_C) == true)
            {
                player.StatusTextDisplay("Press E to Interact");
                if (KeyControls.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E) && Keymanager.MRB_StatueActive == true)
                {
                    ghost.Prechase(player);
                    sound.PlaySfx(1);
                    player.ChangeStartingPosition(new Vector2(75, 8*32));
                    roomManager.Roomchange(5);
                }
                else if(KeyControls.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E) && Keymanager.MRB_StatueActive == false)
                {
                    sound.PlaySfx(2);
                    dialogue.SettingParameter("Hint Block", 0, 0, "There's something blocking the way. But I can't see it", Color.Red);
                    dialogue.Activation(true);
                }
            }
            Oldkey_ = KeyControls;
        }
        public void Reset()
        {

        }

        }
    }
