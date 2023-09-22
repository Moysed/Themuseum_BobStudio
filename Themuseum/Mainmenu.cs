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
        MouseState mouseState;
        RoomManager roomManager;
        KeyManagement KeyManagement;
        GraphicsDeviceManager _graphics;
        Player player;
        LanternLight light;
        DialogueBox dialogue;
         
        Game1 game; public Mainmenu(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Load
            
            mainMenuTexture = game.Content.Load<Texture2D>("mainmenu_placeholder");
            Button = game.Content.Load<Texture2D>("startbutton_placeholder");
            KeyManagement = new KeyManagement();
            player = new Player(new Vector2(0,0));
            light = new LanternLight();
            //roomManager = new RoomManager(6);
            KeyManagement = new KeyManagement();
            dialogue = new DialogueBox("placeholderblock", 200, 200);
            this.game = game;

        }
        public override void Update(GameTime theTime)
        {
            mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            Rectangle StartHitbox = new Rectangle(600, 320, 150, 80);
            Rectangle Mousehitbox = new Rectangle(mousePosition.X, mousePosition.Y , 10 , 10);
            
          

            if (Keyboard.GetState().IsKeyDown(Keys.A) == true)
            {
                ScreenEvent.Invoke(game.mGameplay, new EventArgs());
                return;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.C) == true)
            {
                ScreenEvent.Invoke(game.winScreen, new EventArgs());
                return;
            }

            base.Update(theTime);
        }
        public override void Draw(SpriteBatch theBatch)
        {
            
            theBatch.Draw(mainMenuTexture, Vector2.Zero, Color.White); base.Draw(theBatch);
            theBatch.Draw(Button, new Vector2(600 ,320), Color.White);
            
        }
    }
}
