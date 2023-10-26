using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Themuseum
{
    public class GameOver : Screen
    {
        Texture2D gameOver;
        private Player player;
        Ghost ghost;
        Game1 game; public GameOver(Game1 game,
         EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Load
            gameOver = game.Content.Load<Texture2D>("gameover_placeholder");
            player = new Player(Vector2.Zero);
            this.game = game;
        }
        public override void Update(GameTime theTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true)
            {
                ScreenEvent.Invoke(game.mMainmenu, new EventArgs());
                player.IsHaunted = false;
           
                
                return;
            }
          
            base.Update(theTime);
        }
        public override void Draw(SpriteBatch theBatch)
        {

           theBatch.Draw(gameOver , Vector2.Zero, Color.White);

        }
    }
}
