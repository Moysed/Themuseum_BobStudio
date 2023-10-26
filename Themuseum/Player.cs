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

    class Player 
    {
        Room1 room1;
        public float speed = 1f;
        public Vector2 SelfPosition;
        public Rectangle collision;
        private AnimatedTexture Sprite;
        public float MaxStamina = 150;
        public float CurrentStamina = 150;
        public float MaxFuel = 300;
        public float CurrentFuel = 300;
        public KeyboardState KeyControls;
        private SpriteFont Font;
        private string StatusText = "";
        private float texttime = 60;
        public bool IsHaunted = false;
        
      
        private int framerow = 1;
        

        public Player(Vector2 SpawningPosition)
        {
            room1 = new Room1();
            SelfPosition = SpawningPosition;
            Sprite = new AnimatedTexture(Vector2.Zero, 0, 1, 0.5f);
            collision = new Rectangle((int)SelfPosition.X, (int)SelfPosition.Y, 42, 84);
        }

        public void LoadSprite(ContentManager Content)
        {
            Sprite.Load(Content, "Mini player", 4, 4, 4);
            Font = Content.Load<SpriteFont>("Keycollect");
        }

        public void Draw(SpriteBatch SB, KeyboardState Keystate)
        {
            SB.DrawString(Font, StatusText, new Vector2(SelfPosition.X, SelfPosition.Y - 32), Color.White);

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

        public void Controls(KeyboardState Keystate, LanternLight Light, KeyManagement keyManagement)
        {
            KeyControls = Keystate;
            collision = new Rectangle((int)SelfPosition.X, (int)SelfPosition.Y, 42, 84);
            

            if(texttime > 0)
            {
                texttime--;
            }
            else
            {
                StatusText = "";
            }
            
            

            if (KeyControls.IsKeyDown(Keys.A))
            {
                SelfPosition.X -= speed;

                if (KeyControls.IsKeyDown(Keys.A) && KeyControls.IsKeyDown(Keys.LeftShift) && CurrentStamina > 0 && KeyControls.IsKeyUp(Keys.F))
                {
                    speed = 2;
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
                    speed = 2;
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
                    speed = 2;
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
                    speed = 2;
                    CurrentStamina--;
                    //currentSpeed = speed;
                    SelfPosition.Y += speed;

                }
            }

            if (KeyControls.IsKeyUp(Keys.LeftShift))
            {
                CurrentStamina += 0.10f;
                speed = 2;
                //currentSpeed = speed;
            }

            if (CurrentStamina > 151)
            {
                
                CurrentStamina = 150;
            }

            if (KeyControls.IsKeyDown(Keys.F) && CurrentFuel > 0 && Light.IsActive == true)
            {
                CurrentFuel -= 0.5f;
                Light.LightActivate(this);
               // Console.WriteLine(CurrentFuel);
            }
            else if (KeyControls.IsKeyUp(Keys.F) || CurrentFuel <= 0 && Light.IsActive == false)
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
            return CurrentStamina;
        }

        public float UpdateOil()
        {
            return CurrentFuel;
        }

        public float UpdateSpeed()
        {
            return speed;
        }
        public void StatusTextDisplay(string DisplayText)
        {
            texttime = 60;
            StatusText = DisplayText;
        }
        public void Reset()
        {
            CurrentFuel = 300;
            CurrentStamina = 150;
            IsHaunted = false;
            SelfPosition = new Vector2(1280/2,640/2);
        }
    }
}
