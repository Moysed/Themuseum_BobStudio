using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Themuseum
{
    public class WinScreen : Screen
    {
        Texture2D winScreen;
        MouseState mouseState;
        RoomManager roomManager;
        KeyManagement KeyManagement;
        GraphicsDeviceManager _graphics;
        Player player;
        LanternLight light;
        DialogueBox dialogue;

        Game1 game; public WinScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Load

            winScreen = game.Content.Load<Texture2D>("win_placeholder");    
            KeyManagement = new KeyManagement();
            player = new Player(new Vector2(0, 0));
            light = new LanternLight();
            KeyManagement = new KeyManagement();
            dialogue = new DialogueBox("placeholderblock", 200, 200);
            this.game = game;

        }
        public override void Update(GameTime theTime)
        {
            mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            Rectangle StartHitbox = new Rectangle(600, 320, 150, 80);
            Rectangle Mousehitbox = new Rectangle(mousePosition.X, mousePosition.Y, 10, 10);



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

            theBatch.Draw(winScreen, Vector2.Zero, Color.White); base.Draw(theBatch);
         

        }
    }
}
