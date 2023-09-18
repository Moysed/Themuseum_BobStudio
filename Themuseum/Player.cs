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

    class Player 
    {
        public float speed = 3f;
        public Vector2 SelfPosition;
        public Rectangle collision;
        private AnimatedTexture Sprite;
        public float MaxStamina = 100;
        public float CurrentStamina = 100;
        public float MaxFuel = 300;
        public float CurrentFuel = 300;
        private KeyboardState KeyControls;
        public bool IsHaunted = false;
        
      
        private int framerow = 1;
        

        public Player(Vector2 SpawningPosition)
        {
            SelfPosition = SpawningPosition;
            Sprite = new AnimatedTexture(Vector2.Zero, 0, 1, 0.5f);
            collision = new Rectangle((int)SelfPosition.X, (int)SelfPosition.Y, 42, 84);
        }

        public void LoadSprite(ContentManager Content)
        {
            Sprite.Load(Content, "Mini player", 4, 4, 15);
        }

        public void Draw(SpriteBatch SB, KeyboardState Keystate)
        {
            if (Keystate.IsKeyDown(Keys.A))
            {
                framerow = 3;
                Sprite.DrawFrame(SB, SelfPosition, framerow);
            }
            else if (Keystate.IsKeyDown(Keys.D))
            {
                framerow = 4;
                Sprite.DrawFrame(SB, SelfPosition, framerow);
            }
            else if (Keystate.IsKeyDown(Keys.W))
            {
                framerow = 2;
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

        public void Controls(KeyboardState Keystate, LanternLight Light)
        {
            KeyControls = Keystate;
            collision = new Rectangle((int)SelfPosition.X, (int)SelfPosition.Y, 42, 84);


            if (KeyControls.IsKeyDown(Keys.A))
            {
                SelfPosition.X -= speed;

                if (KeyControls.IsKeyDown(Keys.A) && KeyControls.IsKeyDown(Keys.LeftShift) && CurrentStamina > 0 && KeyControls.IsKeyUp(Keys.F))
                {
                    speed = 4;
                    CurrentStamina--;
                    //currentSpeed = speed;
                    SelfPosition.X -= speed;
                    
                }
            }
            else if (KeyControls.IsKeyDown(Keys.D))
            {
                SelfPosition.X += speed;

                if (KeyControls.IsKeyDown(Keys.D) && KeyControls.IsKeyDown(Keys.LeftShift) && CurrentStamina > 0 && KeyControls.IsKeyUp(Keys.F))
                {
                    speed = 4;
                    CurrentStamina--;
                    //currentSpeed = speed;
                    SelfPosition.X += speed;
                    
                }
            }
            else if (KeyControls.IsKeyDown(Keys.W))
            {
                SelfPosition.Y -= speed;

                if (KeyControls.IsKeyDown(Keys.W) && KeyControls.IsKeyDown(Keys.LeftShift) && CurrentStamina > 0 && KeyControls.IsKeyUp(Keys.F))
                {
                    speed = 4;
                    CurrentStamina--;
                    //currentSpeed = speed;
                    SelfPosition.Y -= speed;

                }
            }
            else if (KeyControls.IsKeyDown(Keys.S))
            {
                SelfPosition.Y += speed;

                if (KeyControls.IsKeyDown(Keys.S) && KeyControls.IsKeyDown(Keys.LeftShift) && CurrentStamina > 0 && KeyControls.IsKeyUp(Keys.F))
                {
                    speed = 4;
                    CurrentStamina--;
                    //currentSpeed = speed;
                    SelfPosition.Y += speed;

                }
            }

            if (KeyControls.IsKeyUp(Keys.LeftShift))
            {
                CurrentStamina += 0.10f;
                speed = 3;
                //currentSpeed = speed;
            }

            if (CurrentStamina > 100)
            {
                
                CurrentStamina = 100;
            }

            if(KeyControls.IsKeyDown(Keys.F) && CurrentFuel > 0)
            {
                CurrentFuel--;
                Light.LightActivate(this);
            }
            else if(KeyControls.IsKeyUp(Keys.F) || CurrentFuel <= 0)
            {
                Light.LightDeactivate();
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

        public float UpdateSpeed()
        {
            return speed;
        }
        
    }
}
