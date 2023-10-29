using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Themuseum
{
    public class WinScreen : Screen
    {
        Texture2D[] winScreen;
        MouseState mouseState;
        RoomManager roomManager;
        KeyManagement KeyManagement;
        GraphicsDeviceManager _graphics;
        Player player;
        LanternLight light;
        DialogueBox dialogue;
        SpriteFont font;
        int counter = 120;
        

        Game1 game; public WinScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Load
            winScreen = new Texture2D[2];
            winScreen[0] = game.Content.Load<Texture2D>("Escape1");
            winScreen[1] = game.Content.Load<Texture2D>("Escape2");
            KeyManagement = new KeyManagement();
            player = new Player(new Vector2(0, 0));
            light = new LanternLight();
            KeyManagement = new KeyManagement();
            dialogue = new DialogueBox("placeholderblock", 200, 200);
            font = game.Content.Load<SpriteFont>("Start");
            this.game = game;

        }
        public override void Update(GameTime theTime)
        {
            counter--;
            mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            Rectangle StartHitbox = new Rectangle(600, 320, 150, 80);
            Rectangle Mousehitbox = new Rectangle(mousePosition.X, mousePosition.Y, 10, 10);



            if (Keyboard.GetState().IsKeyDown(Keys.R) == true)
            {
                ScreenEvent.Invoke(game.mMainmenu, new EventArgs());
                game.mGameplay.Reset();
                return;
            }

            base.Update(theTime);
        }
        public override void Draw(SpriteBatch theBatch)
        {
            string str = "Press \" R \" to return to Main menu";
            game.GraphicsDevice.Clear(Color.Black);
            theBatch.Draw(winScreen[0], Vector2.Zero, Color.White); base.Draw(theBatch);
            if (counter <= 0)
            {
                theBatch.Draw(winScreen[1], Vector2.Zero, Color.White); base.Draw(theBatch);
            }
            if( counter <= -120  )
            {
                theBatch.DrawString(font, str, new Vector2(450, 550), Color.White);
            }


        }
    }
}