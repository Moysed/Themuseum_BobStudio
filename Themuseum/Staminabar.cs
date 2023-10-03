using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct3D9;


namespace Themuseum
{
    class Staminabar
    {

        private Texture2D KeynCandlebg;
        private Texture2D Lantern;
        private Texture2D lanternButton;
        private Texture2D Map;
        private Texture2D mapButton;
        private Texture2D bg;
        private Texture2D BarBackground;
        private Texture2D BarColor;
        private AnimatedTexture PlayerStatusUI;
        private Texture2D CandleBackground;
        private Texture2D CandleBar;
        private Texture2D CandleBarEmpty;
        private Vector2 Staminaposition;
        private Vector2 OilPosition;
        private Vector2 SpritePosition;
        private Rectangle BarIndicator;
        private Rectangle OilBarIndicator;

        private float MaxStamina;
        private float MaxOil;

        private int currentrow = 1;
        //private bool showbar;
        public Staminabar(float MaxStaminaValue, float MaxOilValue)
        {
            MaxStamina = MaxStaminaValue;
            MaxOil = MaxOilValue;
            Console.WriteLine(MaxStamina);
            Console.WriteLine(MaxOil);
            PlayerStatusUI = new AnimatedTexture(Vector2.Zero, 0, 1, 0.5f);

        }

        public void LoadSprite(ContentManager Content)
        {
            KeynCandlebg = Content.Load<Texture2D>("Bg_Candle_keys");
            BarBackground = Content.Load<Texture2D>("Mana empty ");
            BarColor = Content.Load<Texture2D>("Mana");
            CandleBackground = Content.Load<Texture2D>("FullCandle_Bg");
            CandleBar = Content.Load<Texture2D>("FullCandle");
            CandleBarEmpty = Content.Load<Texture2D>("EmptyCandle");
            PlayerStatusUI.Load(Content, "UI_Status_Sprite", 8, 2, 4);
            Map = Content.Load<Texture2D>("Large_Map");
            mapButton = Content.Load<Texture2D>("Small_Map_button");
            Lantern = Content.Load<Texture2D>("Big_candle");
            lanternButton = Content.Load<Texture2D>("Small_Flash_button");
            bg = Content.Load<Texture2D>("Bg_Map_n_Lanter");
        }

        public void UpdateBar(float StaminaValue, float OilValue, float elapsed)
        {
            SpritePosition = new Vector2(15, 10);
            OilPosition = new Vector2(110 + 35, 0);
            Staminaposition = new Vector2(5, 120);

            int StaminaIndicator = (int)MathF.Round(StaminaValue * (BarBackground.Width / MaxStamina));
            //int OilIndicator = (int)MathF.Round(OilValue * (CandleBackground.Height / MaxOil));

            if (StaminaIndicator > BarBackground.Width)
            {
                StaminaIndicator = BarBackground.Width;
            }
            BarIndicator = new Rectangle(0, 0, StaminaIndicator, BarBackground.Height);
            //OilBarIndicator = new Rectangle(0, 0, CandleBackground.Width, OilIndicator);

            if (StaminaValue <= MaxStamina * 0.3)
            {
                currentrow = 2;
            }
            else
            {
                currentrow = 1;
            }


            PlayerStatusUI.UpdateFrame(elapsed);

        }

        public void Drawbar(SpriteBatch SB, Player player, LanternLight light)
        {
            SB.Draw(KeynCandlebg, new Vector2(130 + 12, 7 + 5), Color.White);
            SB.Draw(BarBackground, Staminaposition, Color.White);
            SB.Draw(BarColor, Staminaposition, BarIndicator, Color.White);
            //SB.Draw(CandleBackground, new Vector2(OilPosition.X + 64, OilPosition.Y), Color.White);
            if (light.lightStart == true)
            {
                if (player.CurrentFuel > 200)
                {
                    SB.Draw(CandleBar, new Vector2(OilPosition.X + 128, OilPosition.Y), Color.White);
                    SB.Draw(CandleBar, new Vector2(OilPosition.X + 64, OilPosition.Y), Color.White);
                    SB.Draw(CandleBar, OilPosition, Color.White);
                }
                else if (player.CurrentFuel > 100 && player.CurrentFuel <= 200)
                {
                    SB.Draw(CandleBarEmpty, new Vector2(OilPosition.X + 128 - 10 + 5, OilPosition.Y + 50 + 3), Color.White);
                    SB.Draw(CandleBar, new Vector2(OilPosition.X + 64, OilPosition.Y), Color.White);
                    SB.Draw(CandleBar, OilPosition, Color.White);
                }
                else if (player.CurrentFuel > 0 && player.CurrentFuel <= 100)
                {
                    SB.Draw(CandleBarEmpty, new Vector2(OilPosition.X + 128 - 10 + 5, OilPosition.Y + 50 + 3), Color.White);
                    SB.Draw(CandleBarEmpty, new Vector2(OilPosition.X + 64 - 7 + 5, OilPosition.Y + 50 + 3), Color.White);
                    SB.Draw(CandleBar, OilPosition, Color.White);
                }
                else if (player.CurrentFuel <= 0)
                {
                    SB.Draw(CandleBarEmpty, new Vector2(OilPosition.X + 128 - 10 + 5, OilPosition.Y + 50 + 3), Color.White);
                    SB.Draw(CandleBarEmpty, new Vector2(OilPosition.X + 64 - 7 + 5, OilPosition.Y + 50 + 3), Color.White);
                    SB.Draw(CandleBarEmpty, new Vector2(OilPosition.X + 5, OilPosition.Y + 50 + 3), Color.White);
                }
            }
            else if (light.lightStart == false)
            {
                SB.Draw(CandleBarEmpty, new Vector2(OilPosition.X + 128 - 10 + 5, OilPosition.Y + 50 + 3), Color.White);
                SB.Draw(CandleBarEmpty, new Vector2(OilPosition.X + 64 - 7 + 5, OilPosition.Y + 50 + 3), Color.White);
                SB.Draw(CandleBarEmpty, new Vector2(OilPosition.X + 5, OilPosition.Y + 50 + 3), Color.White);
            
        
    }
                SB.Draw(bg, new Vector2(1280-135, 640-70), Color.White);
            SB.Draw(Map, new Vector2(1280 - 60, 640 - 65), Color.White);
            SB.Draw(Lantern, new Vector2(1280 - 120, 640 - 65), Color.White);
            SB.Draw(lanternButton, new Vector2(1280 - 135 + 10, 640 - 70 + 37), Color.White);
            SB.Draw(mapButton, new Vector2(1280 - 135 + 70, 640 - 70 + 37), Color.White);
            PlayerStatusUI.DrawFrame(SB, SpritePosition, currentrow);
        }
    }
}
