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
        Texture2D[] endcredit;
        SpriteFont font;

        Vector2 victory;

        Vector2 end1;
        Vector2 end2;
        Vector2 end3;
        Vector2 end4;
        Vector2 end5;
        Vector2 end6;
        Vector2 end7;
        Vector2 end8;
        Vector2 end9;

        public int counter = 120;
        

        Game1 game; public WinScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Load
            victory = Vector2.Zero;
            end1 = new Vector2 (250,0);
            end2 = new Vector2(1000000000, 0);
            end3 = new Vector2(1000000000, 0);
            end4 = new Vector2(1000000000, 0);
            end5 = new Vector2(1000000000, 0);
            end6 = new Vector2(1000000000, 0);
            end7 = new Vector2(1000000000, 0);
            end8 = new Vector2(1000000000, 0);
            end9 = new Vector2(1000000000, 0);

            endcredit = new Texture2D[9];
            winScreen = new Texture2D[2];
            winScreen[0] = game.Content.Load<Texture2D>("Escape1");
            winScreen[1] = game.Content.Load<Texture2D>("Escape2");
            for (int i = 1; i <= 8; i++)
            {
                endcredit[i] = game.Content.Load<Texture2D>("Untitled_Artwork-" + i);
            }
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
            theBatch.Draw(winScreen[0], victory, Color.White); base.Draw(theBatch);
            if (counter <= 0)
            {
                theBatch.Draw(winScreen[1], victory, Color.White); base.Draw(theBatch);
            }
              if(counter <= -240 && counter >= -360)
            {
                victory = new Vector2(1000000000000, 0);
                game.GraphicsDevice.Clear(Color.Black);
                theBatch.Draw(endcredit[1], end1, Color.White); base.Draw(theBatch);
                
            }
             if (counter <= -360 &&  counter >= -480)
            {
                end1 = new Vector2(10000000000000, 0);
                end2 = new Vector2(300,0);
                victory = new Vector2(1000000000000, 0);
                game.GraphicsDevice.Clear(Color.Black);
                theBatch.Draw(endcredit[2], end2, Color.White); base.Draw(theBatch);
            }
             if(counter <= -480 && counter >= -600)
            {
                end1 = new Vector2(1000000000, 0);
                end2 = new Vector2(1000000000, 0);
                end3 = new Vector2(300, 0);
                victory = new Vector2(1000000000000, 0);
                game.GraphicsDevice.Clear(Color.Black);
                theBatch.Draw(endcredit[3], end3, Color.White); base.Draw(theBatch);
            }
             if(counter <= -600 && counter >= -720)
            {
                end1 = new Vector2(1000000000000, 0);
                end2 = new Vector2(1000000000000, 0);
                end3 = new Vector2(100000000000,0);
                end4 = new Vector2(300, 0);
                victory = new Vector2(1000000000000, 0);
                game.GraphicsDevice.Clear(Color.Black);
                theBatch.Draw(endcredit[4], end4, Color.White); base.Draw(theBatch);
            }
             if(counter <= -720 && counter >= -840)
            {
                end1 = new Vector2(1000000000000, 0);
                end2 = new Vector2(1000000000000, 0);;
                end3 = new Vector2(1000000000000,0);
                end4 = new Vector2(100000000000, 0);
                end5 = new Vector2(300, 0);
                victory = new Vector2(1000000000000, 0);
                game.GraphicsDevice.Clear(Color.Black);
                theBatch.Draw(endcredit[5], end5, Color.White); base.Draw(theBatch);
            }
             if(counter <= -840 && counter >= -960) 
            {
                end1 = new Vector2(1000000000000, 0);
                end2 = new Vector2(1000000000000, 0); ;
                end3 = new Vector2(100000000000, 0);
                end4 = new Vector2(10000000000, 0);
                end5 = new Vector2(10000000000, 0);
                end6 = new Vector2(240, 0);
                victory = new Vector2(1000000000000, 0);
                game.GraphicsDevice.Clear(Color.Black);
                theBatch.Draw(endcredit[6], end6, Color.White); base.Draw(theBatch);
            }
            if(counter <= -960 && counter >= -1080)
            {
                end1 = new Vector2(1000000000, 0);
                end2 = new Vector2(1000000000, 0); ;
                end3 = new Vector2(10000000000, 0);
                end4 = new Vector2(1000000000, 0);
                end5 = new Vector2(1000000000, 0);
                end6 = new Vector2(10000000000, 0);
                end7 = new Vector2(270, 0);
                victory = new Vector2(1000000000000, 0);
                game.GraphicsDevice.Clear(Color.Black);
                theBatch.Draw(endcredit[7], end7, Color.White); base.Draw(theBatch);
            }
             if(counter <= -1080 )
            {
                end1 = new Vector2(1000000000, 0);
                end2 = new Vector2(1000000000, 0); ;
                end3 = new Vector2(10000000000, 0);
                end4 = new Vector2(1000000000, 0);
                end5 = new Vector2(1000000000, 0);
                end6 = new Vector2(1000000000, 0);
                end7 = new Vector2(1000000000, 0);
                end8 = new Vector2(170, 0);
                victory = new Vector2(1000000000000, 0);
                game.GraphicsDevice.Clear(Color.Black);
                theBatch.Draw(endcredit[8], end8, Color.White); base.Draw(theBatch);
            }
            
            if (counter <= -120)
            {
                theBatch.DrawString(font, str, new Vector2(450, 550), Color.White);
            }


        }
    }
}