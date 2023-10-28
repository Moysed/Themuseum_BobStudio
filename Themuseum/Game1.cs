using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace Themuseum
{
    public class Game1 : Game
    {
        //Game Object
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private LanternLight light;
        private Staminabar staminabar;
        private KeyManagement KeyManagement;
        private Ghost ghost;
        private DialogueBox dialogue;
        private Screen mCurrentScreen;
        public Mainmenu mMainmenu;
        public GameOver mGameoverScreen;
        public WinScreen  winScreen;
        public GamePlay mGameplay;
        public bool enablefullscreen = false;
        private KeyboardState keyboardState;
        private KeyboardState oldkey;
        MouseState m;
        private GameWindow _window;
        bool _isFullscreen = false;
        bool _isBorderless = false;
        int _width = 1280;
        int _height = 640;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 640;
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            
            // TODO: Add your initialization logic here
            
            //roomManager = new RoomManager(6);
            m = Mouse.GetState();

            KeyManagement = new KeyManagement();

            dialogue = new DialogueBox("placeholderblock", 200, 200);
            mMainmenu = new Mainmenu(this, new EventHandler(GameplayScreenEvent));
            mGameoverScreen = new GameOver(this , new EventHandler(GameplayScreenEvent));
            winScreen = new WinScreen(this, new EventHandler(GameplayScreenEvent));
            mGameplay = new GamePlay(this, new EventHandler(GameplayScreenEvent));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            dialogue.LoadResource(Content);
            mCurrentScreen = mMainmenu;
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Fullscreen system WIP (Do not use)
            
            /*
            if(keyboardState.IsKeyDown(Keys.D1) && oldkey.IsKeyUp(Keys.D1) && enablefullscreen == false)
            {
                enablefullscreen = true;
                SetFullscreen();
                
            }
            else if(keyboardState.IsKeyDown(Keys.D1) && oldkey.IsKeyUp(Keys.D1) && enablefullscreen == true)
            {
                enablefullscreen = false;
                UnsetFullscreen();
            }
            */

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //roomManager.RoomFunction(_graphics, player, KeyManagement, elapsed,dialogue,light);
            dialogue.behavior();
            mCurrentScreen.Update(gameTime);

            oldkey = keyboardState;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void ToggleFullscreen()
        {
            bool oldIsFullscreen = _isFullscreen;

            if (_isBorderless)
            {
                _isBorderless = false;
            }
            else
            {
                _isFullscreen = !_isFullscreen;
            }

            ApplyFullscreenChange(oldIsFullscreen);
        }
        public void ToggleBorderless()
        {
            bool oldIsFullscreen = _isFullscreen;

            _isBorderless = !_isBorderless;
            _isFullscreen = _isBorderless;

            ApplyFullscreenChange(oldIsFullscreen);
        }

        private void ApplyFullscreenChange(bool oldIsFullscreen)
        {
            if (_isFullscreen)
            {
                if (oldIsFullscreen)
                {
                    ApplyHardwareMode();
                }
                else
                {
                    SetFullscreen();
                }
            }
            else
            {
                UnsetFullscreen();
            }
        }
        private void ApplyHardwareMode()
        {
            _graphics.HardwareModeSwitch = !_isBorderless;
            _graphics.ApplyChanges();
        }
        private void SetFullscreen()
        {
            _width = Window.ClientBounds.Width;
            _height = Window.ClientBounds.Height;

            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.HardwareModeSwitch = !_isBorderless;

            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
        }
        private void UnsetFullscreen()
        {
            _graphics.PreferredBackBufferWidth = _width;
            _graphics.PreferredBackBufferHeight = _height;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
        }
        protected override void Draw(GameTime gameTime)
        {
            var ScaleX = (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / _width;
            var ScaleY = (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / _height;
            

            if (enablefullscreen == true)
            {
                ScaleX = (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / _width;
                ScaleY = (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / _height;
            }
            else if(enablefullscreen == false)
            {
                ScaleX = 1f;
                ScaleY = 1f;
            }

            var matrix = Matrix.CreateScale(ScaleX, ScaleY, 1f);
            _spriteBatch.Begin(transformMatrix: matrix);



            GraphicsDevice.Clear(Color.CornflowerBlue);
            //roomManager.Draw(_spriteBatch,light);

            dialogue.Draw(_spriteBatch);
            mCurrentScreen.Draw(_spriteBatch);

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        public void GameplayScreenEvent(object obj, EventArgs e)
        {
            mCurrentScreen = (Screen)obj;
        }
    }
}