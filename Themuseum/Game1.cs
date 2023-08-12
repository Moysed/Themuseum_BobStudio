﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Themuseum
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private AnimatedTexture Character;
        private Texture2D Key;
        private const float Rotation = 0;
        private const float Scale = 1.0f;
        private const float Depth = 0.5f;
        private int speed = 3;

        private int Frames = 4;
        private const int FramesPerSec = 15;
        private int FramesRow = 4;

        private int score = 0;
        private bool personHit;

        private int currentrow = 1;

        private Vector2 CharPos = new Vector2(0, 0);
        private Vector2 keyPos = new Vector2(200,200);

        private KeyboardState _keyboardState;

        private Texture2D Tileset;

        List<int> Tile_X = new List<int>();
        List<int> Tile_Y = new List<int>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Character = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Tileset = Content.Load<Texture2D>("placeholder_tileset");
            Key = Content.Load<Texture2D>("key-white");
            
            for(int i = 0; i < (int)GraphicsDevice.Viewport.Width / 32; i++)
            {
                Tile_X.Add(i);
            }
            for (int i = 0; i < (int)GraphicsDevice.Viewport.Height / 32; i++)
            {
                Tile_Y.Add(i);
            }
            //Character Position Initial
            CharPos = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Character.Load(Content, "placeholdersprite", Frames, FramesRow, FramesPerSec);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            //Collision check
            Rectangle charRectangle = new Rectangle((int)CharPos.X, (int)CharPos.Y, 32, 32);
            {
                Rectangle blockRectangle = new Rectangle((int)keyPos.X, (int)keyPos.Y, 24, 24);

                if (charRectangle.Intersects(blockRectangle) == true)
                {
                    personHit = true;
                    keyPos = new Vector2(20000, 10000);
                    score += 1;
                }
                else if (charRectangle.Intersects(blockRectangle) == false)
                {
                    personHit = false;
                }

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            _keyboardState = Keyboard.GetState();
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Player Movement
            if (_keyboardState.IsKeyDown(Keys.A))
            {
                CharPos.X -= speed;
            }
            else if (_keyboardState.IsKeyDown(Keys.D))
            {
                CharPos.X += speed;
            }
            else if (_keyboardState.IsKeyDown(Keys.W))
            {
                CharPos.Y -= speed;
            }
            else if (_keyboardState.IsKeyDown(Keys.S))
            {
                CharPos.Y += speed;
            }

            //Wall Collision Check
            if (CharPos.X >= _graphics.GraphicsDevice.Viewport.Bounds.Right - 55)
            {
                CharPos.X -= 3;
            }
            else if (CharPos.X <= _graphics.GraphicsDevice.Viewport.Bounds.Left + 22)
            {
                CharPos.X += 3;
            }
            else if (CharPos.Y <= _graphics.GraphicsDevice.Viewport.Bounds.Top + 20)
            {
                CharPos.Y += 3;
            }
            else if (CharPos.Y >= _graphics.GraphicsDevice.Viewport.Bounds.Bottom - 72)
            {
                CharPos.Y -= 3;
            }
            Character.UpdateFrame(elapsed);
            base.Update(gameTime);
        }
    }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            //Key
            _spriteBatch.Draw(Key, keyPos, new Rectangle(0,0,32,32),Color.White); 

            //Wall Tile Drawing (Upper Wall)
            for(int i = 1; i < ((int)GraphicsDevice.Viewport.Width / 32) - 1; i++)
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
                Character.DrawFrame(_spriteBatch, 2 , CharPos, currentrow);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}