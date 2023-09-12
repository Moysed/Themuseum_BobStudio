﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Themuseum
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        private Player player;
        private LanternLight light;
        private Staminabar staminabar;
        private RoomManager roomManager;
        private KeyboardState Keystate;
        
        
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
            player = new Player(new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2,_graphics.GraphicsDevice.Viewport.Height / 2));
            light = new LanternLight();
            staminabar = new Staminabar(player.MaxStamina);
            roomManager = new RoomManager(1);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadSprite(Content);
            light.LoadSprite(Content);
            staminabar.LoadSprite(Content);
            roomManager.LoadAssets(Content);
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Keystate = Keyboard.GetState();

            player.Controls(Keystate,light);
            player.UpdateAnimation(elapsed);
            staminabar.UpdateBar(player.SelfPosition, player.UpdateStamina());

            roomManager.RoomFunction(_graphics, player);
           
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            roomManager.Draw(_spriteBatch);
            player.Draw(_spriteBatch,Keystate);
            light.Drawlight(_spriteBatch);
            staminabar.Drawbar(_spriteBatch);

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}