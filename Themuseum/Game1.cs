using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System.Collections.Generic;

namespace Themuseum
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private AnimatedTexture Character;
        private const float Rotation = 0;
        private const float Scale = 1.0f;
        private const float Depth = 0.5f;
        private int speed = 3;

        private int Frames = 4;
        private const int FramesPerSec = 15;
        private int FramesRow = 4;

        private int currentrow = 1;

        private Vector2 CharPos = new Vector2(0, 0);

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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

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