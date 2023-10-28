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
        public float speed = 3f;
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
                    speed = 3f;
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
            collision = new Rectangle((int)SelfPosition.X, (int)SelfPosition.Y , 82, 100);
        }
        public void Prechase(Player player)
        {
            if(player.IsHaunted == false)
            {
                if (player.KeyControls.IsKeyDown(Keys.A))
                {
                    Changestartingposition(new Vector2(-80, 640 / 2));

                }
                else if (player.KeyControls.IsKeyDown(Keys.D))
                {
                    Changestartingposition(new Vector2(1300, 640 / 2));
                }
                else if (player.KeyControls.IsKeyDown(Keys.W))
                {
                    Changestartingposition(new Vector2(1300 / 2, -300));
                }
                else if (player.KeyControls.IsKeyDown(Keys.S))
                {
                    Changestartingposition(new Vector2(1300 / 2, 900));

                }
            }
            if(player.IsHaunted == true){

                if (player.KeyControls.IsKeyDown(Keys.A))
                {
                    Changestartingposition(new Vector2(1300, 640 / 2));

                }
                else if (player.KeyControls.IsKeyDown(Keys.D))
                {
                    Changestartingposition(new Vector2(-80, 640 / 2));
                    
                }
                else if (player.KeyControls.IsKeyDown(Keys.W))
                {
                    Changestartingposition(new Vector2(1300 / 2, 900));
                }
                else if (player.KeyControls.IsKeyDown(Keys.S))
                {
                    Changestartingposition(new Vector2(1300 / 2, -300));
                    

                }
            }
            
        }
        public void ChaseSeq(Player player)
        {

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
