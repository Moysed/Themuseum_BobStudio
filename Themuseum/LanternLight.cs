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

namespace Themuseum
{
    class LanternLight 
    {
        public bool lightStart = false;
        public Vector2 SelfPosition;
        public Rectangle Collision;
        private Texture2D LightSprite;
        public bool IsActive = false;

        public LanternLight()
        {
            SelfPosition = new Vector2(10000, 10000);
        }

        public void LoadSprite(ContentManager Content)
        {
            LightSprite = Content.Load<Texture2D>("LanternLight");
        }

        public void LightActivate(Player player)
        {
            SelfPosition = new Vector2(player.SelfPosition.X - 107 ,player.SelfPosition.Y - 82);
            Collision = new Rectangle((int)SelfPosition.X,(int)SelfPosition.Y,256,256);
        }

        public void LightDeactivate()
        {
            SelfPosition = new Vector2(10000000, 10000000);
            Collision = new Rectangle((int)SelfPosition.X, (int)SelfPosition.Y, 256, 256);
        }

        public void Drawlight(SpriteBatch SB)
        {
            SB.Draw(LightSprite, SelfPosition, Color.White * 0.3f);
        }
    }
}
