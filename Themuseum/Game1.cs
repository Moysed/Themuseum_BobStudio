using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        //private RoomManager roomManager;
        private KeyboardState Keystate;
        private KeyManagement KeyManagement;
        private Ghost ghost;
        private DialogueBox dialogue;
        private Mainmenu mainmenu;
        private Screen mCurrentScreen;
        public GameOver mGameoverScreen;
        public WinScreen  winScreen;
        public GamePlay mGameplay;


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
            KeyManagement = new KeyManagement();
           
            dialogue = new DialogueBox("placeholderblock", 200, 200);
            mainmenu = new Mainmenu(this, new EventHandler(GameplayScreenEvent));
            mGameoverScreen = new GameOver(this , new EventHandler(GameplayScreenEvent));
            winScreen = new WinScreen(this, new EventHandler(GameplayScreenEvent));
            mGameplay = new GamePlay(this, new EventHandler(GameplayScreenEvent));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            dialogue.LoadResource(Content);
            mCurrentScreen = mainmenu;
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Keystate = Keyboard.GetState();


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