using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Themuseum
{
    public class Mainmenu : Screen
    {
        Texture2D mainMenuTexture;
        Texture2D Button;
        Vector2 buttonPos;
        MouseState mouseState;
        MouseState Old_mouseState;
        RoomManager roomManager;
        KeyManagement KeyManagement;
        GraphicsDeviceManager _graphics;
        SpriteFont spriteFont;
        Player player;
        LanternLight light;
        DialogueBox dialogue;
        int countdown = 20;
         
        Game1 game; public Mainmenu(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Load
            
            mainMenuTexture = game.Content.Load<Texture2D>("Mainmenu_BG");
            spriteFont = game.Content.Load<SpriteFont>("Start");
            //Button = game.Content.Load<Texture2D>("startbutton_placeholder");
            KeyManagement = new KeyManagement();
            player = new Player(new Vector2(0,0));
            buttonPos = new Vector2(558, 528);
            light = new LanternLight();
            KeyManagement = new KeyManagement();
            dialogue = new DialogueBox("placeholderblock", 300, 200);
         
            this.game = game;
            IsMouseVisible = true;
        }
        public override void Update(GameTime theTime)
        {
            mouseState = Mouse.GetState();
            countdown--;

            /*if (StartHitbox.Contains(mouseState.X, mouseState.Y) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Console.WriteLine("Clicked");
                ScreenEvent.Invoke(game.mGameplay, new EventArgs());
                return;
            }*/
            
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true)
            {
                Console.WriteLine("Game Enter");
                ScreenEvent.Invoke(game.mGameplay, new EventArgs());
                game.mGameplay.ResetElapsedTime();
                //game.mGameplay.Reset();
               
                return;
            }

            Old_mouseState = mouseState;
            base.Update(theTime);
        }
        public override void Draw(SpriteBatch theBatch)
        {
            string srt;
            srt = "Press \"Enter\" to Start";
            theBatch.Draw(mainMenuTexture, Vector2.Zero, Color.White); base.Draw(theBatch);
            if( countdown<= 20 )
            {
                theBatch.DrawString(spriteFont, srt, new Vector2(515, 550), Color.Red);
            }
            if(countdown <= -10 )
            {
                countdown = 80;
            }
            
            
        }
    }
}
