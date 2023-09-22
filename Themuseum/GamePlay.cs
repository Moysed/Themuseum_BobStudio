﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace Themuseum
{
    public class GamePlay : Screen
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private LanternLight light;
        private Staminabar staminabar;
        private KeyboardState Keystate;
        private KeyManagement KeyManagement;
        private Ghost ghost;
        private DialogueBox dialogue;
        RoomManager roomManager;
        private MRC mrc;

        Game1 game; public GamePlay(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            mrc = new MRC();
            player = new Player(new Vector2(game._graphics.GraphicsDevice.Viewport.Width / 2, game._graphics.GraphicsDevice.Viewport.Height / 2));
            light = new LanternLight();
            staminabar = new Staminabar(player.MaxStamina);

            //roomManager = new RoomManager(6);
            KeyManagement = new KeyManagement();
            ghost = new Ghost(new Vector2(10000, 10000));
            dialogue = new DialogueBox("placeholderblock", 200, 200);
            player.LoadSprite(game.Content);
            staminabar.LoadSprite(game.Content);
            ghost.LoadSprite(game.Content);
            dialogue.LoadResource(game.Content);
            roomManager = new RoomManager(6);
            roomManager.LoadAssets(game.Content);
            light.LoadSprite(game.Content);
            this._graphics = game._graphics;
            this.game = game;

        }
        public override void Update(GameTime theTime)
        {
            float elapsed = (float)theTime.ElapsedGameTime.TotalSeconds;
            Keystate = Keyboard.GetState();

            roomManager.RoomFunction(_graphics, player, KeyManagement, elapsed, dialogue, light);

            player.Controls(Keystate, light);
            player.UpdateAnimation(elapsed);

            staminabar.UpdateBar(player.SelfPosition, player.UpdateStamina());
            ghost.Behavior(player, light);
            ghost.UpdateAnimation(elapsed);
            dialogue.behavior();
            mrc.EndCheck();



            if(mrc.IsEnded == true)
            {
                ScreenEvent.Invoke(game.winScreen, new EventArgs());
                return;
            }

           
           

            base.Update(theTime);
        }
        public override void Draw(SpriteBatch theBatch)
        {
            roomManager.Draw(theBatch, light);
            player.Draw(theBatch, Keystate);
            light.Drawlight(theBatch);
            staminabar.Drawbar(theBatch);
            ghost.Draw(theBatch);
            dialogue.Draw(theBatch);
        }

      
    }
}