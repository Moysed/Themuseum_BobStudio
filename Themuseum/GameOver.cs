using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        SpriteFont font;
        int counter = 120;
        
       
        Game1 game; public GameOver(Game1 game,
         EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Load
            font = game.Content.Load<SpriteFont>("Start");
            gameOver = game.Content.Load<Texture2D>("GameOver");
            player = new Player(Vector2.Zero);
            this.game = game;
            
        }
        public override void Update(GameTime theTime)
        {
            counter--;
            
            if (Keyboard.GetState().IsKeyDown(Keys.R) == true)
            {
                ScreenEvent.Invoke(game.mMainmenu, new EventArgs());
                player.IsHaunted = false;
           
                
                return;
            }
          
            base.Update(theTime);
        }
        public override void Draw(SpriteBatch theBatch)
        {
            string str = "Press \" R \" to return to Main menu";
           theBatch.Draw(gameOver , Vector2.Zero, Color.White);
            
            if (counter <= 0)
            {
                theBatch.DrawString(font, str, new Vector2(450, 550), Color.White);
                
            } 


        }
    }
}
