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
        public bool showMap = false;
        public bool mapActive = false;
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
        private int player_pieceActive = 0;
        private KeyboardState KeyControls;
        private KeyboardState OldKey;
        KeyboardMethod method;
        private SoundSystem sound;

        Random r = new Random();


        public Room1()
        {
            mapPos = new Vector2(430, 290);
            hintPos = new Vector2(300, 400);
            lanternPos = new Vector2(740, 250);
            DoorPos = new Vector2(0, 64);
            DoorCollision = new Rectangle((int)DoorPos.X, (int)DoorPos.Y, 32, 64);
            HiddenSwitch_01_Pos = new Vector2(1000, 320);
            HiddenSwitch_01_Col = new Rectangle((int)HiddenSwitch_01_Pos.X, (int)HiddenSwitch_01_Pos.Y, 64, 64);
            HiddenSwitch_02_Pos = new Vector2(10000, 10000);
            HiddenSwitch_02_Col = new Rectangle((int)HiddenSwitch_02_Pos.X, (int)HiddenSwitch_02_Pos.Y, 64, 64);
            HiddenSwitch_03_Pos = new Vector2(10000, 10000);
            HiddenSwitch_03_Col = new Rectangle((int)HiddenSwitch_03_Pos.X, (int)HiddenSwitch_03_Pos.Y, 64, 64);
            R1_Shire = new Shire(new Vector2(1100, 70));
            sound = new SoundSystem();
            method = new KeyboardMethod();
            piece1Pos = new Vector2(r.Next(110, 200), r.Next(80, 250));
        }

        public void LoadSprite(ContentManager content)
        {
            Map_Sprite = content.Load<Texture2D>("MapSheet");
            hint = content.Load<Texture2D>("Hint");
            Map = content.Load<Texture2D>("Map");
            Lantern = content.Load<Texture2D>("Lantern");
            TileStatic = content.Load<Texture2D>("Bg");
            Door = content.Load<Texture2D>("placeholderdoor");
            HiddenSwitch_01_Tex = content.Load<Texture2D>("hiddenswitch01_placeholder");
            HiddenSwitch_02_Tex = content.Load<Texture2D>("hiddenswitch02_placeholder");
            HiddenSwitch_03_Tex = content.Load<Texture2D>("hiddenswtich03_placeholder");
            R1_Shire.LoadSprite(content);
            piece1 = content.Load<Texture2D>("Piece1");
            sound.LoadContent(content);
        }

        public void Draw(SpriteBatch SB, LanternLight light)
        {
            SB.Draw(TileStatic, Vector2.Zero, Color.White);
            SB.Draw(Door, DoorPos, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
            if (light.Collision.Intersects(HiddenSwitch_01_Col))
            {
                SB.Draw(HiddenSwitch_01_Tex, HiddenSwitch_01_Pos, Color.White);
            }
            else if (light.Collision.Intersects(HiddenSwitch_02_Col))
            {
                SB.Draw(HiddenSwitch_02_Tex, HiddenSwitch_02_Pos, Color.White);
            }
            else if (light.Collision.Intersects(HiddenSwitch_03_Col))
            {
                SB.Draw(HiddenSwitch_03_Tex, HiddenSwitch_03_Pos, Color.White);
            }

            if (player_pieceActive == 1)
            {
                if (light.Collision.Intersects(piece1Col))
                {
                    SB.Draw(piece1, piece1Pos, Color.White);
                }
            }
            SB.Draw(hint, hintPos, Color.White);
            SB.Draw(Lantern, lanternPos, Color.White);
            R1_Shire.Draw(SB);
            SB.Draw(Map, mapPos, Color.White);

            if (showMap == true)
            {
                SB.Draw(Map_Sprite, new Vector2(320, 0), Color.White);
                Console.WriteLine(showMap);
            }
        }

        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed, DialogueBox dialogue, LanternLight light )
        {

            KeyControls = Keyboard.GetState();

            //Wall Collision
            if (player.SelfPosition.X >= _graphics.GraphicsDevice.Viewport.Bounds.Right - 150)
            {
                if (player.speed >= 4)
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
            else if (player.SelfPosition.Y >= _graphics.GraphicsDevice.Viewport.Bounds.Bottom - 142)
            {
                if (player.speed >= 4)
                {
                    player.SelfPosition.Y -= player.speed * 2;
                }
                else
                    player.SelfPosition.Y -= player.speed;
            }
            //Objects Behavior
            Rectangle hint = new Rectangle((int)hintPos.X, (int)hintPos.Y, 64, 64);
            Rectangle Lantern = new Rectangle((int)lanternPos.X, (int)lanternPos.Y, 40, 70);
            Rectangle map = new Rectangle((int)mapPos.X, (int)mapPos.Y, 74, 34);
            DoorPos = new Vector2(300, 50);
            DoorCollision = new Rectangle((int)DoorPos.X, (int)DoorPos.Y, 32, 64);
            R1_Shire.Behavior(player, elapsed);
            HiddenSwitch_01_Pos = new Vector2(1000, 320);
            HiddenSwitch_01_Col = new Rectangle((int)HiddenSwitch_01_Pos.X, (int)HiddenSwitch_01_Pos.Y, 64, 64);
            HiddenSwitch_02_Col = new Rectangle((int)HiddenSwitch_02_Pos.X, (int)HiddenSwitch_02_Pos.Y, 64, 64);
            HiddenSwitch_03_Col = new Rectangle((int)HiddenSwitch_03_Pos.X, (int)HiddenSwitch_03_Pos.Y, 64, 64);

            piece1Col = new Rectangle((int)piece1Pos.X, (int)piece1Pos.Y, 64, 64);

            if (Keymanager.R2_T1 == false)
            {
                HiddenSwitch_02_Pos = new Vector2(10000, 10000);
                HiddenSwitch_03_Pos = new Vector2(10000, 10000);
            }
            else if (Keymanager.R2_T1 == true)
            {
                HiddenSwitch_02_Pos = new Vector2(320, 128);
                HiddenSwitch_03_Pos = new Vector2(540, 512);
            }

            //Object Interaction
            if (player.collision.Intersects(DoorCollision) == true)
            {
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E) && Keymanager.R1_S1 == true)
                {
                    sound.PlaySfx(1);
                    player.ChangeStartingPosition(new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2, _graphics.GraphicsDevice.Viewport.Bounds.Bottom - 128));
                    roomManager.Roomchange(2);
                    Console.WriteLine("Door Opened");
                }

            }
            else if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
            {
                sound.PlaySfx(2);
            }

            if (player.collision.Intersects(HiddenSwitch_02_Col) == true)
            {
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    sound.PlaySfx(1);
                    Keymanager.KeyTrigger("R1_S2");
                    Console.WriteLine("R1_S2");
                }
            }
            if (player.collision.Intersects(HiddenSwitch_03_Col) == true)
            {
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    sound.PlaySfx(1);
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
                    sound.PlaySfx(0);
                    Keymanager.MRB_Pieces += 1;
                    Console.WriteLine(Keymanager.MRB_Pieces);
                    piece1Pos.X = 5000;
                }
            }
            if (player.collision.Intersects(HiddenSwitch_01_Col) == true)
            {
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    sound.PlaySfx(1);
                    Keymanager.KeyTrigger("R1_S1");
                    dialogue.SettingParameter("placeholderblock", 200, 200, "1st Hidden Switch Activated", Color.Green);
                    //dialogue.Activation(true);
                    Console.WriteLine("R1_S1");
                }
            }

            //hint Collision
            if (player.collision.Intersects(hint) == true && KeyControls.IsKeyDown(Keys.E) && dialogue.IsActive == false && !OldKey.IsKeyDown(Keys.E))
            {
                
                dialogue.SettingParameter("Hint Block", 200, 200, "Light will guide you home", Color.Green);
                dialogue.Activation(true);
                OldKey = KeyControls;
            }

            else if(player.collision.Intersects(hint) == true && dialogue.IsActive == true && KeyControls.IsKeyDown(Keys.E) && !OldKey.IsKeyDown(Keys.E))
            {
                dialogue.Activation(false);
            }

           
              
            /*else
            {
                dialogue.Activation(false);
            }*/

            if (player.collision.Intersects(Lantern) == true && KeyControls.IsKeyDown(Keys.F))
            {
                sound.PlaySfx(0);
                light.IsActive = true;
                lanternPos.X = 20000;
                Console.WriteLine(player.CurrentFuel);
            }

            if (player.collision.Intersects(map) == true && KeyControls.IsKeyDown(Keys.E))
            {
                mapActive = true;
                mapPos.X = 20000;
                Console.WriteLine(mapActive);
            }

            else
            {
                mapActive = false;
            }

            if (mapActive = true && Keyboard.GetState().IsKeyDown(Keys.M))
            {
                showMap = true;
            }
            else
            {
                showMap = false;
            }

            OldKey = KeyControls;
        }
    }
}
    
           







