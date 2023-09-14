using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
    
    class PuzzleBlock
    {
        private AnimatedTexture Sprite;
        private Vector2 SelfPosition;
        private Vector2 OriginalPosition;
        private Rectangle Collision;
        private string KeyDesignation = "";
        public PuzzleBlock(Vector2 startingposition, string DefineID)
        {
            SelfPosition = startingposition;
            OriginalPosition = SelfPosition;
            KeyDesignation = DefineID;
            Sprite = new AnimatedTexture(Vector2.Zero, 0, 1, 0.5f);
        }

        public void LoadSprite(ContentManager content)
        {
            Sprite.Load(content, "187-Lorry01", 4, 4, 15);
        }

        public void Draw(SpriteBatch SB) 
        {
            Sprite.DrawFrame(SB, SelfPosition, 1);
        }

        public void Behavior(Player player, float elapsed)
        {
            Collision = new Rectangle((int)SelfPosition.X-16,(int)SelfPosition.Y-16,64,64);

            if (player.collision.Intersects(Collision))
            {
                if (player.collision.Right >= Collision.Right)
                {
                    if (player.speed == 4)
                    {
                        player.SelfPosition.X += player.speed * 2;
                        SelfPosition.X -= player.speed * 2;
                    }
                    else
                        player.SelfPosition.X += player.speed;
                        SelfPosition.X -= player.speed;
                }
                if (player.collision.Left <= Collision.Left)
                {
                    if (player.speed == 4)
                    {
                        player.SelfPosition.X -= player.speed * 2;
                        SelfPosition.X += player.speed * 2;
                    }
                    else
                        player.SelfPosition.X -= player.speed;
                        SelfPosition.X += player.speed;
                }
                if (player.collision.Top >= Collision.Top)
                {
                    if (player.speed == 4)
                    {
                        player.SelfPosition.Y += player.speed * 2;
                        SelfPosition.Y -= player.speed * 2;
                    }
                    else
                        player.SelfPosition.Y += player.speed;
                        SelfPosition.Y -= player.speed;
                }
                if (player.collision.Bottom <= Collision.Bottom)
                {
                    if (player.speed == 4)
                    {

                        player.SelfPosition.Y -= player.speed * 2;
                        SelfPosition.Y += player.speed * 2;
                    }
                    else
                        player.SelfPosition.Y -= player.speed;
                        SelfPosition.Y += player.speed;

                }

            }
            Sprite.UpdateFrame(elapsed);
        }
        
        public void ResetPosition()
        {
            SelfPosition = OriginalPosition;
        }

        
    }
}
