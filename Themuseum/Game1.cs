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
        private Mainmenu mMainmenu;
        public GameOver mGameoverScreen;
        public WinScreen  winScreen;
        public GamePlay mGameplay;

        MouseState m;
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //roomManager.RoomFunction(_graphics, player, KeyManagement, elapsed,dialogue,light);
            dialogue.behavior();
            mCurrentScreen.Update(gameTime);
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

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