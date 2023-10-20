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
    class Ghost 
    {
        private float speed = 2f;
        private Vector2 SelfPosition;
        public Rectangle collision;
        private AnimatedTexture Sprite;
        public bool gameOver = false;
        
        public Ghost(Vector2 SpawningPosition)
        {
            SelfPosition = SpawningPosition;
            Sprite = new AnimatedTexture(Vector2.Zero,0,1,0.5f);
            collision = new Rectangle((int)SelfPosition.X, (int)SelfPosition.Y, 82, 200);
            
        }

        public void Draw(SpriteBatch SB)
        {
            Sprite.DrawFrame(SB, SelfPosition, 1);
        }
        public void Behavior(Player player,LanternLight Lantern , GameTime gameTime)
        {
            if(player.IsHaunted == true)
            {
                collision = new Rectangle((int)SelfPosition.X, (int)SelfPosition.Y, 82, 200);

                Vector2 Dir = Vector2.Normalize(player.SelfPosition - SelfPosition);


                if (Math.Abs(Dir.X) > Math.Abs(Dir.Y))
                {
                    Dir.Y = 0;
                }
                else
                {
                    Dir.X = 0;
                }
                SelfPosition += new Vector2(Dir.X * speed, Dir.Y * speed);
                Dir.Normalize();
                if (collision.Intersects(Lantern.Collision))
                {
                    speed = 1;
                }
                else
                {
                    speed = 2;
                }
                
                
                
                SelfPosition += Dir * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                
                Prechase(player);
            }

            if(collision.Intersects(player.collision) == true)
            {
                gameOver = true;
            }
           
        }
        public void Changestartingposition(Vector2 newPos)
        {
            SelfPosition = newPos;
            collision = new Rectangle((int)SelfPosition.X, (int)SelfPosition.Y, 82, 200);
        }
        public void Prechase(Player player)
        {
            if (player.KeyControls.IsKeyDown(Keys.A))
            {
                Changestartingposition(new Vector2(1300, 640/2));
            }
            else if (player.KeyControls.IsKeyDown(Keys.D))
            {
                Changestartingposition(new Vector2(-400, 640 / 2));
            }
            else if (player.KeyControls.IsKeyDown(Keys.W))
            {
                Changestartingposition(new Vector2(1300/2, 800));
            }
            else if (player.KeyControls.IsKeyDown(Keys.S))
            {
                Changestartingposition(new Vector2(1300 / 2, -400));
            }
            else
            {
                Changestartingposition(new Vector2(1300, 1300));
            }
        }
        public void UpdateAnimation(float elasped)
        {
            Sprite.UpdateFrame(elasped);
        }
        public void LoadSprite(ContentManager Content)
        {
            Sprite.Load(Content, "Ghost", 1, 1, 1);
        }    
    }
}
