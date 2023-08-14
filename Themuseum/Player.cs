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

    class Player : Game1
    {
        public float speed = 3f;
        public Vector2 SelfPosition;
        public Rectangle collision;
        private AnimatedTexture Sprite;
        public float MaxStamina = 100;
        private float CurrentStamina = 100;
       
        private int framerow = 1;
        
        public Player(Vector2 SpawningPosition)
        {
            SelfPosition = SpawningPosition;
            Sprite = new AnimatedTexture(Vector2.Zero, 0, 1, 0.5f);
            collision = new Rectangle((int)SelfPosition.X, (int)SelfPosition.Y, 32, 48);
            
        }

        public void LoadSprite(ContentManager Content)
        {
            Sprite.Load(Content, "placeholdersprite", 4, 4, 15);
        }

        public void Draw(SpriteBatch SB, KeyboardState Keystate)
        {
            if (Keystate.IsKeyDown(Keys.A))
            {
                framerow = 2;
                Sprite.DrawFrame(SB, SelfPosition, framerow);
            }
            else if (Keystate.IsKeyDown(Keys.D))
            {
                framerow = 3;
                Sprite.DrawFrame(SB, SelfPosition, framerow);
            }
            else if (Keystate.IsKeyDown(Keys.W))
            {
                framerow = 4;
                Sprite.DrawFrame(SB, SelfPosition, framerow);
            }
            else if (Keystate.IsKeyDown(Keys.S))
            {
                framerow = 1;
                Sprite.DrawFrame(SB, SelfPosition, framerow);
            }
            else
            {
                Sprite.DrawFrame(SB, 2, SelfPosition, framerow);
            }
        }

        public void Controls(KeyboardState Keystate)
        {
            
            collision = new Rectangle((int)SelfPosition.X, (int)SelfPosition.Y, 32, 48);

            if (Keystate.IsKeyDown(Keys.A))
            {
                SelfPosition.X -= speed;

                if (Keystate.IsKeyDown(Keys.A) && Keystate.IsKeyDown(Keys.LeftShift))
                {
                    speed = 4;
                    CurrentStamina--;
                    SelfPosition.X -= speed;
                    
                }
            }
            else if (Keystate.IsKeyDown(Keys.D))
            {
                SelfPosition.X += speed;

                if (Keystate.IsKeyDown(Keys.D) && Keystate.IsKeyDown(Keys.LeftShift) && CurrentStamina > 0)
                {
                    speed = 4;
                    CurrentStamina--;
                    SelfPosition.X += speed;
                    
                }
            }
            else if (Keystate.IsKeyDown(Keys.W))
            {
                SelfPosition.Y -= speed;

                if (Keystate.IsKeyDown(Keys.W) && Keystate.IsKeyDown(Keys.LeftShift) && CurrentStamina > 0)
                {
                    speed = 4;
                    CurrentStamina--;
                    SelfPosition.Y -= speed;

                }
            }
            else if (Keystate.IsKeyDown(Keys.S))
            {
                SelfPosition.Y += speed;

                if (Keystate.IsKeyDown(Keys.S) && Keystate.IsKeyDown(Keys.LeftShift) && CurrentStamina > 0)
                {
                    speed = 4;
                    CurrentStamina--;
                    SelfPosition.Y += speed;

                }
            }

            if (Keystate.IsKeyUp(Keys.LeftShift))
            {
                CurrentStamina += 0.10f;
                speed = 3;
                currentSpeed = speed;
            }

           

            if (CurrentStamina > 100)
            {
                
                CurrentStamina = 100;
            }
           
        }


        public void UpdateAnimation(float elasped)
        {
            Sprite.UpdateFrame(elasped);
        }

        public void ChangeStartingPosition(Vector2 spawnposition)
        {
            SelfPosition = spawnposition;
        }

        public float UpdateStamina()
        {
            return (CurrentStamina);
        }
    }
}
