﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection.Metadata;

namespace Themuseum
{
    public class GamePlay : Screen
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private LanternLight light;
        private LanternRefill refill;
        private Staminabar staminabar;
        private KeyboardState Keystate;
        private KeyManagement KeyManagement;
        private Ghost ghost;
        private DialogueBox dialogue;
        private SoundSystem soundSystem;
        private Map map;
        RoomManager roomManager;
        Texture2D guide;
        Vector2 guidepos = Vector2.Zero;
        

        Game1 game; public GamePlay(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            refill = new LanternRefill(Vector2.Zero);
            player = new Player(new Vector2(game._graphics.GraphicsDevice.Viewport.Width / 2, game._graphics.GraphicsDevice.Viewport.Height / 2));
            light = new LanternLight();
            map = new Map();
            staminabar = new Staminabar(player.MaxStamina,player.MaxFuel);
            soundSystem = new SoundSystem();
            KeyManagement = new KeyManagement();
            ghost = new Ghost(new Vector2(10000, 10000));
            dialogue = new DialogueBox("placeholderblock", 200, 200);
            player.LoadSprite(game.Content);
            staminabar.LoadSprite(game.Content);
            ghost.LoadSprite(game.Content);
            dialogue.LoadResource(game.Content);
            roomManager = new RoomManager(1);
            roomManager.LoadAssets(game.Content);
            light.LoadSprite(game.Content);
            map.LoadSprite(game.Content);
            soundSystem.LoadContent(game.Content);
            guide = game.Content.Load<Texture2D>("GuidelineEIEi");

            soundSystem.PlayBGM(0);

            this._graphics = game._graphics;
            this.game = game;

        }
        public override void Update(GameTime theTime)
        {
            float elapsed = (float)theTime.ElapsedGameTime.TotalSeconds;
            Keystate = Keyboard.GetState();

            roomManager.RoomFunction(_graphics, player, KeyManagement, elapsed, dialogue, light,map,soundSystem);

            player.Controls(Keystate, light,KeyManagement);
            player.UpdateAnimation(elapsed);
            map.Behavior(roomManager);
            staminabar.UpdateBar(player.CurrentStamina, player.CurrentFuel, elapsed) ;
            ghost.Behavior(player, light);
            ghost.UpdateAnimation(elapsed);
            dialogue.behavior();
            


            
            if(KeyManagement.GameEnded == true)
            {
                Console.WriteLine("Game Won");
                ScreenEvent.Invoke(game.winScreen, new EventArgs());

                Reset();

                return;
            }

            if (ghost.gameOver == true)
            {
                Console.WriteLine("Game Over");
                ScreenEvent.Invoke(game.mGameoverScreen, new EventArgs());

                Reset();

                return;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.E)== true)
            {
                guidepos = new Vector2(1000, 2000000);
            }
            base.Update(theTime);
        }
        public override void Draw(SpriteBatch theBatch)
        {
            
            roomManager.Draw(theBatch, light);
            player.Draw(theBatch, Keystate);
            light.Drawlight(theBatch);
            ghost.Draw(theBatch);
            staminabar.Drawbar(theBatch);
            map.DrawMap(theBatch);
            dialogue.Draw(theBatch);
            theBatch.Draw(guide, guidepos, Color.White);
        }

        public void Reset()
        {
            soundSystem.PlayBGM(0);
            soundSystem.StopBGM();
            ghost.gameOver = false;
            ghost.Changestartingposition(new Vector2(10000, 10000));
            player.Reset();
            KeyManagement.Reset();
            light.IsActive = false;
            map.IsActive = false;
            roomManager.RoomReset();
            
        }
    }
}