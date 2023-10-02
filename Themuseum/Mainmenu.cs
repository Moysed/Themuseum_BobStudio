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
        Player player;
        LanternLight light;
        DialogueBox dialogue;
         
        Game1 game; public Mainmenu(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Load
            
            mainMenuTexture = game.Content.Load<Texture2D>("Main menu");
            Button = game.Content.Load<Texture2D>("startbutton_placeholder");
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
            Rectangle StartHitbox = new Rectangle((int)buttonPos.X, (int)buttonPos.Y, 150, 80);

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
               
                return;
            }

            Old_mouseState = mouseState;
            base.Update(theTime);
        }
        public override void Draw(SpriteBatch theBatch)
        {
            
            theBatch.Draw(mainMenuTexture, Vector2.Zero, Color.White); base.Draw(theBatch);
            theBatch.Draw(Button, buttonPos, Color.White);
            
        }
    }
}
