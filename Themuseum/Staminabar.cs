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
        private Texture2D BarBackground;
        private Texture2D BarColor;
        private AnimatedTexture PlayerStatusUI;
        private Texture2D CandleBackground;
        private Texture2D CandleBar;
        private Vector2 Staminaposition;
        private Vector2 OilPosition;
        private Vector2 SpritePosition;
        private Rectangle BarIndicator;
        private Rectangle OilBarIndicator;
        
        private float MaxStamina;
        private float MaxOil;

        private int currentrow = 1;
        //private bool showbar;
        public Staminabar(float MaxStaminaValue,float MaxOilValue)
        {
            MaxStamina = MaxStaminaValue;
            MaxOil = MaxOilValue;
            Console.WriteLine(MaxStamina);
            Console.WriteLine(MaxOil);
            PlayerStatusUI = new AnimatedTexture(Vector2.Zero, 0, 1, 0.5f);

        }

        public void LoadSprite(ContentManager Content)
        {
            BarBackground = Content.Load<Texture2D>("Mana empty ");
            BarColor = Content.Load<Texture2D>("Mana");
            CandleBackground = Content.Load<Texture2D>("FullCandle_Bg");
            CandleBar = Content.Load<Texture2D>("FullCandle");
            PlayerStatusUI.Load(Content, "UI_Status_Sprite", 8, 2, 4);
        }

        public void UpdateBar(float StaminaValue,float OilValue,float elapsed)
        {
            SpritePosition = new Vector2(32, 32);
            OilPosition = new Vector2(110 + 48, 32);
            Staminaposition = new Vector2(32, 110 + 32);
            
            BarIndicator = new Rectangle(0, 0, (int)StaminaValue * (BarBackground.Width / (int)MaxStamina) , BarBackground.Height);
            OilBarIndicator = new Rectangle(0, 0, CandleBackground.Width, (int)OilValue * (CandleBackground.Height / (int)MaxOil));

            if(StaminaValue <= MaxStamina * 0.3)
            {
                currentrow = 2;
            }
            else
            {
                currentrow = 1;
            }


            PlayerStatusUI.UpdateFrame(elapsed);
            
        }

        public void Drawbar(SpriteBatch SB)
        {
            
                SB.Draw(BarBackground, Staminaposition, Color.White);
                SB.Draw(BarColor, Staminaposition, BarIndicator, Color.White);
                SB.Draw(CandleBackground, OilPosition, Color.White);
                SB.Draw(CandleBar, OilPosition, OilBarIndicator, Color.White);
                PlayerStatusUI.DrawFrame(SB, SpritePosition, currentrow);
        }
    }
}
