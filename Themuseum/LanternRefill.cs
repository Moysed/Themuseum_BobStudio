
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
    class LanternRefill : Game1
    {
        private Vector2 DeployPosition;
        private Vector2 StorePosition;
        public Rectangle Collision;
        private AnimatedTexture Texture;

        public LanternRefill(Vector2 NewPosition)
        {
            DeployPosition = NewPosition;
            StorePosition = DeployPosition;
            Collision = new Rectangle((int)DeployPosition.X, (int) DeployPosition.Y, 64, 64);
            Texture = new AnimatedTexture(Vector2.Zero, 0, 1, 0.5f);

        }

        public void LoadSprite(ContentManager Content)
        {
            Texture.Load(Content, "187-Lorry01", 4, 4, 15);
        }

        public void DrawSprite(SpriteBatch SB)
        {
            Texture.DrawFrame(SB, DeployPosition, 1);
        }

        public void ChangePosition(Vector2 NewPosition)
        {
            DeployPosition = NewPosition;
            Collision = new Rectangle((int)DeployPosition.X, (int)DeployPosition.Y, 64, 64);
        }

        public void RestorePosition()
        {
            DeployPosition = StorePosition;
            Collision = new Rectangle((int)DeployPosition.X, (int)DeployPosition.Y, 64, 64);
        }

        public Rectangle UpdateCollision()
        {
            return Collision;
        }
    }
}
