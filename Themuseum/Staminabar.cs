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
    class Staminabar : Game1
    {
        private Texture2D BarBackground;
        private Texture2D BarColor;
        private Vector2 selfposition;
        private Rectangle BarIndicator;
        
        private float MaxStamina;
        private bool showbar;
        public Staminabar(float MaxValue)
        {
            MaxStamina = MaxValue;
        }

        public void LoadSprite(ContentManager Content)
        {
            BarBackground = Content.Load<Texture2D>("EmptyBar");
            BarColor = Content.Load<Texture2D>("BlueBar");

        }

        public void UpdateBar(Vector2 playerposition, float StaminaValue)
        {
            selfposition = new Vector2(playerposition.X - 96, playerposition.Y + 64);
            //(int)StaminaValue * 2
            BarIndicator = new Rectangle(0, 0, (int)StaminaValue * (BarBackground.Width / (int)MaxStamina) , BarBackground.Height);
            
            if(StaminaValue < MaxStamina)
            {
                showbar = true;
            }
            else
            {
                showbar = false;
            }
        }

        public void Drawbar(SpriteBatch SB)
        {
            if(showbar == true)
            {
                SB.Draw(BarBackground, selfposition, Color.White);
                SB.Draw(BarColor, selfposition,BarIndicator, Color.White);
            }
        }
    }
}
