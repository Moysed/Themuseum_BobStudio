using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Timers;

namespace Themuseum
{
    public class Game1 : Game
    {
        int countdown = 60*4;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private AnimatedTexture Character;
        private AnimatedTexture Ghost;
        private AnimatedTexture MagicCircle;
        private AnimatedTexture MagicCrystal;
        private Texture2D Key;
        private Texture2D ExitDoor;
        private const float Rotation = 0;
        private const float Scale = 1.0f;
        private const float Depth = 0.5f;
        private int speed = 3;
        private SpriteFont _font;

        private int Frames = 4;
        private const int FramesPerSec = 15;
        private int FramesRow = 4;

        private int timer;

        Random r = new Random();

        private int currentrow = 1;
        private int currentspeed;

        private Vector2 CharPos = new Vector2(0, 0);
        private Vector2 keyPos = new Vector2(200, 200);

        private KeyboardState _keyboardState;
        private KeyboardState OldKey;

        private Texture2D Tileset;

        List<int> Tile_X = new List<int>();
        List<int> Tile_Y = new List<int>();

        List<Rectangle> CollisionBox = new List<Rectangle>();

        private Color Textcolor;
        private string displaytext = "";

        private bool HasKey = false;
        private bool IsUnlocked = false;

        private int Doorspriteframe = 8;
        private Vector2 Doorpos;
        private Vector2 Circlepos;
        private int CircleType = 1;
        private bool CircleActive = true;

        private Vector2 Notepos;

        List<SoundEffect> soundEffects = new List<SoundEffect>();

        //bool isGameplay;
        //public bool isRoom2;

        private int LevelIndicator = 1;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Character = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);
            Ghost = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);
            MagicCircle = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);
            MagicCrystal = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Tileset = Content.Load<Texture2D>("040-Tower02");
            Key = Content.Load<Texture2D>("key-white");
            _font = Content.Load<SpriteFont>("Keycollect");
            ExitDoor = Content.Load<Texture2D>("placeholderdoor");
            timer = 0;

            for (int i = 0; i < (int)GraphicsDevice.Viewport.Width / 32; i++)
            {
                Tile_X.Add(i);
            }
            for (int i = 0; i < (int)GraphicsDevice.Viewport.Height / 32; i++)
            {
                Tile_Y.Add(i);
            }

            /*
            for(int i = 0; i < (int)GraphicsDevice.Viewport.Width / 32 + (int)GraphicsDevice.Viewport.Width / 32; i++)
            {
                
               CollisionBox.Add(new Rectangle(0, 0, 32, 32));
                
            }*/

            /*
            for (int i = 0; i < 4; i++)
            {

                CollisionBox.Add(new Rectangle(0, 0, 32, 32));

            }
            */

            
            keyPos = new Vector2(32 * 5, 32 * 12);
            Circlepos = new Vector2(keyPos.X - 16, keyPos.Y - 20);
            
            CircleType = r.Next(1, 4);
            //Character Position Initial
            CharPos = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Character.Load(Content, "placeholdersprite", Frames, FramesRow, FramesPerSec);
            Ghost.Load(Content, "054-Undead04", Frames, FramesRow, FramesPerSec);
            MagicCircle.Load(Content, "199-Support07", Frames, FramesRow, FramesPerSec);
            MagicCrystal.Load(Content, "198-Support06", Frames, FramesRow, FramesPerSec);

            soundEffects.Add(Content.Load<SoundEffect>("005-System05")); //Pick-up sfx
            soundEffects.Add(Content.Load<SoundEffect>("024-Door01")); //Door Open Sfx
            soundEffects.Add(Content.Load<SoundEffect>("028-Door05")); //Door Locked Sfx
            soundEffects.Add(Content.Load<SoundEffect>("046-Book01")); // Note read sfx
            soundEffects.Add(Content.Load<SoundEffect>("147-Support05")); // Crystal Approve Sfx
            soundEffects.Add(Content.Load<SoundEffect>("140-Darkness03")); // Crystal Denied sfx

        }

        
       
        private void Room1_Draw()
        {
            //Drawing
            
            //Wall Tile Drawing (Upper Wall)
            for (int i = 1; i < ((int)GraphicsDevice.Viewport.Width / 32) - 1; i++)
            {
                _spriteBatch.Draw(Tileset, new Vector2(Tile_X[i] * 32, 0), new Rectangle(32, 32 * 5, 32, 64), Color.White);
            }

            //Wall Roof Drawing (Left)
            for (int i = 0; i < ((int)GraphicsDevice.Viewport.Height / 32) - 1; i++)
            {
                _spriteBatch.Draw(Tileset, new Vector2(0, Tile_Y[i] * 32), new Rectangle(32 * 2, 32 * 3, 32, 32), Color.White);
            }

            //Wall Roof Drawing (Right)
            for (int i = 0; i < ((int)GraphicsDevice.Viewport.Height / 32) - 1; i++)
            {
                _spriteBatch.Draw(Tileset, new Vector2((Tile_X.Count * 32) - 32, Tile_Y[i] * 32), new Rectangle(0, 32 * 3, 32, 32), Color.White);
            }

            //Wall Roof Drawing (Down)
            for (int i = 1; i < ((int)GraphicsDevice.Viewport.Width / 32) - 1; i++)
            {
                _spriteBatch.Draw(Tileset, new Vector2(Tile_X[i] * 32, (Tile_Y.Count * 32) - 32), new Rectangle(32, 32 * 2, 32, 32), Color.White);
            }

            _spriteBatch.Draw(Tileset, new Vector2(0, (Tile_Y.Count * 32) - 32), new Rectangle(32, 32 * 3, 32, 32), Color.White);
            _spriteBatch.Draw(Tileset, new Vector2((Tile_X.Count * 32) - 32, (Tile_Y.Count * 32) - 32), new Rectangle(32, 32 * 3, 32, 32), Color.White);

            //Floor Drawing
            for (int i = 1; i < ((int)GraphicsDevice.Viewport.Width / 32) - 1; i++)
            {
                for (int j = 2; j < ((int)GraphicsDevice.Viewport.Height / 32) - 1; j++)
                {
                    _spriteBatch.Draw(Tileset, new Vector2(Tile_X[i] * 32, Tile_X[j] * 32), new Rectangle(32 * 3, 0, 32, 32), Color.White);
                }
            }

            //MagicCircle
            MagicCircle.DrawFrame(_spriteBatch, Circlepos, CircleType);
            //Key
            _spriteBatch.Draw(Key, keyPos, new Rectangle(0, 0, 32, 32), Color.White);
            //Door
            _spriteBatch.Draw(ExitDoor, new Vector2(Doorpos.X, Doorpos.Y), new Rectangle(6 * 32, Doorspriteframe * 32, 32, 64), Color.White);
            

            //MagicCrystal
            MagicCrystal.DrawFrame(_spriteBatch, new Vector2(64, 32), 1);
            MagicCrystal.DrawFrame(_spriteBatch, new Vector2(64 + 32 + 10, 32), 2);
            MagicCrystal.DrawFrame(_spriteBatch, new Vector2(64 + 64 + 20, 32), 3);
            MagicCrystal.DrawFrame(_spriteBatch, new Vector2(64 + 96 + 30, 32), 4);

            //Note
            _spriteBatch.Draw(Tileset, Notepos, new Rectangle(5 * 32, 27 * 32, 32, 32), Color.White);
        }

        

        private void Room2_Draw()
        {
            //Wall Tile Drawing (Upper Wall)
            for (int i = 1; i < ((int)GraphicsDevice.Viewport.Width / 32) - 1; i++)
            {
                _spriteBatch.Draw(Tileset, new Vector2(Tile_X[i] * 32, 0), new Rectangle(32, 32 * 5, 32, 64), Color.White);
            }

            //Wall Roof Drawing (Left)
            for (int i = 0; i < ((int)GraphicsDevice.Viewport.Height / 32) - 1; i++)
            {
                _spriteBatch.Draw(Tileset, new Vector2(0, Tile_Y[i] * 32), new Rectangle(32 * 2, 32 * 3, 32, 32), Color.White);
            }

            //Wall Roof Drawing (Right)
            for (int i = 0; i < ((int)GraphicsDevice.Viewport.Height / 32) - 1; i++)
            {
                _spriteBatch.Draw(Tileset, new Vector2((Tile_X.Count * 32) - 32, Tile_Y[i] * 32), new Rectangle(0, 32 * 3, 32, 32), Color.White);
            }

            //Wall Roof Drawing (Down)
            for (int i = 1; i < ((int)GraphicsDevice.Viewport.Width / 32) - 1; i++)
            {
                _spriteBatch.Draw(Tileset, new Vector2(Tile_X[i] * 32, (Tile_Y.Count * 32) - 32), new Rectangle(32, 32 * 2, 32, 32), Color.White);
            }

            _spriteBatch.Draw(Tileset, new Vector2(0, (Tile_Y.Count * 32) - 32), new Rectangle(32, 32 * 3, 32, 32), Color.White);
            _spriteBatch.Draw(Tileset, new Vector2((Tile_X.Count * 32) - 32, (Tile_Y.Count * 32) - 32), new Rectangle(32, 32 * 3, 32, 32), Color.White);

            //Floor Drawing
            for (int i = 1; i < ((int)GraphicsDevice.Viewport.Width / 32) - 1; i++)
            {
                for (int j = 2; j < ((int)GraphicsDevice.Viewport.Height / 32) - 1; j++)
                {
                    _spriteBatch.Draw(Tileset, new Vector2(Tile_X[i] * 32, Tile_X[j] * 32), new Rectangle(32 * 3, 0, 32, 32), Color.White);
                }
            }
            //Door
            _spriteBatch.Draw(Tileset, Doorpos, new Rectangle(32 * 3, 0, 32, 32), Color.White);
        }

        protected override void Update(GameTime gameTime)
        {
            timer--;
            Rectangle charRectangle = new Rectangle((int)CharPos.X, (int)CharPos.Y, 32, 48);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _keyboardState = Keyboard.GetState();
            

            //Player Movement 

            if (_keyboardState.IsKeyDown(Keys.A))
            {
                CharPos.X -= speed;
                currentspeed = speed;
                if (_keyboardState.IsKeyDown(Keys.A) && _keyboardState.IsKeyDown(Keys.LeftShift))
                {
                    speed = 5;
                    CharPos.X -= speed;
                    currentspeed = speed;
                }
            }
            else if (_keyboardState.IsKeyDown(Keys.D))
            {
                CharPos.X += speed;
                currentspeed = speed;
                if (_keyboardState.IsKeyDown(Keys.D) && _keyboardState.IsKeyDown(Keys.LeftShift))
                {
                    speed = 5;
                    CharPos.X += speed;
                    currentspeed = speed;
                }
            }
            else if (_keyboardState.IsKeyDown(Keys.W))
            {
                CharPos.Y -= speed;
                currentspeed = speed;
                if (_keyboardState.IsKeyDown(Keys.W) && _keyboardState.IsKeyDown(Keys.LeftShift))
                {
                    speed = 5;
                    CharPos.Y -= speed;
                    currentspeed = speed;
                }
            }
            else if (_keyboardState.IsKeyDown(Keys.S))
            {
                CharPos.Y += speed;
                currentspeed = speed;
                if (_keyboardState.IsKeyDown(Keys.S) && _keyboardState.IsKeyDown(Keys.LeftShift))
                {
                    speed = 5;
                    CharPos.Y += speed;
                    currentspeed = speed;
                }
            }

            //Wal collision check
            if (CharPos.X >= _graphics.GraphicsDevice.Viewport.Bounds.Right - 54)
            {
                CharPos.X -= currentspeed;
            }
            else if (CharPos.X <= _graphics.GraphicsDevice.Viewport.Bounds.Left + 20)
            {
                CharPos.X += currentspeed;
            }
            else if (CharPos.Y <= _graphics.GraphicsDevice.Viewport.Bounds.Top + 34)
            {
                CharPos.Y += currentspeed;
            }
            else if (CharPos.Y >= _graphics.GraphicsDevice.Viewport.Bounds.Bottom - 72)
            {
                CharPos.Y -= currentspeed;
            }


            if (timer == 0)
            {
                displaytext = string.Empty;

            }

            switch (LevelIndicator)
            {
                case 1: Room1(gameTime, charRectangle); break;
                case 2: Room2(gameTime, charRectangle); break;
            }


            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            OldKey = _keyboardState;
            Character.UpdateFrame(elapsed);
            base.Update(gameTime);
        }

        private void Room1(GameTime gameTime, Rectangle PlayerCol)
        {
            //System
            Notepos = new Vector2(32 * 20, 256);
            Doorpos = new Vector2(32 * 20, 0);
            Rectangle KeyRectangle = new Rectangle((int)keyPos.X, (int)keyPos.Y, 24, 24);
            Rectangle DoorRectangle = new Rectangle((int)Doorpos.X, (int)Doorpos.Y, 32, 64);
            Rectangle CircleBox = new Rectangle((int)Circlepos.X, (int)Circlepos.Y, 64, 64);
            Rectangle Crystal_1 = new Rectangle(64, 32, 32, 64);
            Rectangle Crystal_2 = new Rectangle(64 + 32 + 10, 32, 32, 64);
            Rectangle Crystal_3 = new Rectangle(64 + 64 + 20, 32, 32, 64);
            Rectangle Crystal_4 = new Rectangle(64 + 96 + 30, 32, 32, 64);
            Rectangle NoteBox = new Rectangle((int)Notepos.X, (int)Notepos.Y, 32, 32);




            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;



            //Object Interactions(Room1)
            if (PlayerCol.Intersects(KeyRectangle) == true)
            {

                if (_keyboardState.IsKeyUp(Keys.E) && OldKey.IsKeyDown(Keys.E) && CircleActive == false)
                {
                    soundEffects[0].Play();
                    HasKey = true;
                    keyPos = new Vector2(10000, 10000);
                    Textcolor = Color.Gold;
                    displaytext = "Key Collected";
                    timer = countdown;
                }
                else if (_keyboardState.IsKeyUp(Keys.E) && OldKey.IsKeyDown(Keys.E) && CircleActive == true)
                {
                    soundEffects[5].Play();
                    Textcolor = Color.LightPink;
                    displaytext = "The magic circle is preventing the key from moving";
                    timer = countdown;
                }

            }

            if (PlayerCol.Intersects(DoorRectangle) == true && IsUnlocked == false)
            {

                if (_keyboardState.IsKeyUp(Keys.E) && OldKey.IsKeyDown(Keys.E) && HasKey == false)
                {
                    soundEffects[2].Play();
                    Textcolor = Color.Red;
                    displaytext = "The door is locked";
                    timer = countdown;
                }
                else if (_keyboardState.IsKeyUp(Keys.E) && OldKey.IsKeyDown(Keys.E) && HasKey == true)
                {
                    soundEffects[1].Play();
                    Textcolor = Color.LightGreen;
                    displaytext = "You unlocked the door";
                    Doorspriteframe = 14;
                    IsUnlocked = true;

                }
            }
            else if (PlayerCol.Intersects(DoorRectangle) == true && IsUnlocked == true)
            {
                if (_keyboardState.IsKeyUp(Keys.E) && OldKey.IsKeyDown(Keys.E))
                {
                    soundEffects[1].Play();
                    Textcolor = Color.LightGreen;
                    displaytext = "The door is opened";
                    timer = countdown;


                    LevelIndicator = 2;

                    CharPos = new Vector2(CharPos.X, GraphicsDevice.Viewport.Height - 64);
                }


            }

            if (PlayerCol.Intersects(Crystal_1) && CircleActive == true)
            {
                if (_keyboardState.IsKeyUp(Keys.E) && OldKey.IsKeyDown(Keys.E) && CircleType == 1)
                {
                    soundEffects[4].Play();
                    Textcolor = Color.Aqua;
                    displaytext = "Barrier circle deactivated";
                    CircleActive = false;
                    timer = countdown;
                }
                else if (_keyboardState.IsKeyUp(Keys.E) && OldKey.IsKeyDown(Keys.E))
                {
                    soundEffects[5].Play();
                    Textcolor = Color.Red;
                    displaytext = "You choose wrong, prepare for consequences!";
                    timer = countdown;
                }
            }
            if (PlayerCol.Intersects(Crystal_2) && CircleActive == true)
            {
                if (_keyboardState.IsKeyUp(Keys.E) && OldKey.IsKeyDown(Keys.E) && CircleType == 2)
                {
                    soundEffects[4].Play();
                    Textcolor = Color.Aqua;
                    displaytext = "Barrier circle deactivated";
                    CircleActive = false;
                    timer = countdown;
                }
                else if (_keyboardState.IsKeyUp(Keys.E) && OldKey.IsKeyDown(Keys.E))
                {
                    soundEffects[5].Play();
                    Textcolor = Color.Red;
                    displaytext = "You choose wrong, prepare for consequences!";
                    timer = countdown;
                }
            }
            if (PlayerCol.Intersects(Crystal_3) && CircleActive == true)
            {
                if (_keyboardState.IsKeyUp(Keys.E) && OldKey.IsKeyDown(Keys.E) && CircleType == 3)
                {
                    soundEffects[4].Play();
                    Textcolor = Color.Aqua;
                    displaytext = "Barrier circle deactivated";
                    CircleActive = false;
                    timer = countdown;
                }
                else if (_keyboardState.IsKeyUp(Keys.E) && OldKey.IsKeyDown(Keys.E))
                {
                    soundEffects[5].Play();
                    Textcolor = Color.Red;
                    displaytext = "You choose wrong, prepare for consequences!";
                    timer = countdown;
                }
            }
            if (PlayerCol.Intersects(Crystal_4) && CircleActive == true)
            {
                if (_keyboardState.IsKeyUp(Keys.E) && OldKey.IsKeyDown(Keys.E) && CircleType == 4)
                {
                    soundEffects[4].Play();
                    Textcolor = Color.Aqua;
                    displaytext = "Barrier circle deactivated";
                    CircleActive = false;
                    timer = countdown;
                }
                else if (_keyboardState.IsKeyUp(Keys.E) && OldKey.IsKeyDown(Keys.E))
                {
                    soundEffects[5].Play();
                    Textcolor = Color.Red;
                    displaytext = "You choose wrong, prepare for consequences!";
                    timer = countdown;
                }
            }

            if (CircleActive == false)
            {
                Circlepos = new Vector2(10000, 10000);
            }

            if (PlayerCol.Intersects(NoteBox) && _keyboardState.IsKeyUp(Keys.E) && OldKey.IsKeyDown(Keys.E))
            {
                soundEffects[3].Play();
                Textcolor = Color.Orange;
                displaytext = "Hint:\n\n G = Y \n\n R = B \n\n B = R \n\n Y = G";
                timer = countdown;
            }



            //Wall Collision Check
            if (CharPos.X >= _graphics.GraphicsDevice.Viewport.Bounds.Right - 54)
            {
                CharPos.X -= currentspeed;
            }
            else if (CharPos.X <= _graphics.GraphicsDevice.Viewport.Bounds.Left + 20)
            {
                CharPos.X += currentspeed;
            }
            else if (CharPos.Y <= _graphics.GraphicsDevice.Viewport.Bounds.Top + 34)
            {
                CharPos.Y += currentspeed;
            }
            else if (CharPos.Y >= _graphics.GraphicsDevice.Viewport.Bounds.Bottom - 72)
            {
                CharPos.Y -= currentspeed;
            }




            MagicCircle.UpdateFrame(elapsed);

            MagicCrystal.UpdateFrame(elapsed);


        }

        private void Room2(GameTime gameTime, Rectangle PlayerCol)
        {
            Rectangle DoorRectangle = new Rectangle((int)Doorpos.X, (int)Doorpos.Y, 32, 32);
            Doorpos = new Vector2(32 * 20, GraphicsDevice.Viewport.Height - 32);
            //Wall Collision Check
            if (CharPos.X >= _graphics.GraphicsDevice.Viewport.Bounds.Right - 54)
            {
                CharPos.X -= currentspeed;
            }
            else if (CharPos.X <= _graphics.GraphicsDevice.Viewport.Bounds.Left + 20)
            {
                CharPos.X += currentspeed;
            }
            else if (CharPos.Y <= _graphics.GraphicsDevice.Viewport.Bounds.Top + 34)
            {
                CharPos.Y += currentspeed;
            }
            else if (CharPos.Y >= _graphics.GraphicsDevice.Viewport.Bounds.Bottom - 72)
            {
                CharPos.Y -= currentspeed;
            }

            //Object Interactions (Room2)
            if (PlayerCol.Intersects(DoorRectangle) && _keyboardState.IsKeyUp(Keys.E) && OldKey.IsKeyDown(Keys.E))
            {
                soundEffects[1].Play();

                LevelIndicator = 1;

                CharPos = new Vector2(CharPos.X, 72);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            switch (LevelIndicator)
            {
                case 1: Room1_Draw(); break;
                case 2: Room2_Draw(); break;
            }

            //Player Animation
            if (_keyboardState.IsKeyDown(Keys.A))
            {
                currentrow = 2;
                Character.DrawFrame(_spriteBatch, CharPos, currentrow);
            }
            else if (_keyboardState.IsKeyDown(Keys.D))
            {
                currentrow = 3;
                Character.DrawFrame(_spriteBatch, CharPos, currentrow);
            }
            else if (_keyboardState.IsKeyDown(Keys.W))
            {
                currentrow = 4;
                Character.DrawFrame(_spriteBatch, CharPos, currentrow);
            }
            else if (_keyboardState.IsKeyDown(Keys.S))
            {
                currentrow = 1;
                Character.DrawFrame(_spriteBatch, CharPos, currentrow);
            }
            else
            {
                Character.DrawFrame(_spriteBatch, 2, CharPos, currentrow);
            }

            //Text
            _spriteBatch.DrawString(_font, displaytext, new Vector2(CharPos.X - 64, CharPos.Y - 48), Textcolor, 0, Vector2.Zero, 0.75f, SpriteEffects.None, 1);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}


