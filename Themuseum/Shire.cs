using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Themuseum
{
    class Shire
    {
        private AnimatedTexture Sprite;
        private Vector2 SelfPosition;
        private Rectangle Collision;
        private KeyboardState KeyInteract;
        private KeyboardState OldKey;
        public Shire(Vector2 startingposition){
            SelfPosition = startingposition;
            Sprite = new AnimatedTexture(Vector2.Zero, 0, 1, 0.5f);
            Collision = new Rectangle((int)SelfPosition.X, (int)SelfPosition.Y, 32, 64);
        }

        public void LoadSprite(ContentManager content)
        {
            Sprite.Load(content, "198-Support06", 4, 4, 15);
        }

        public void Draw(SpriteBatch SB)
        {
            Sprite.DrawFrame(SB, SelfPosition, 1);
        }

        public void Behavior(Player player,float elasped)
        {
            Collision = new Rectangle((int)SelfPosition.X, (int)SelfPosition.Y, 32, 64);
            Sprite.UpdateFrame(elasped);

            KeyInteract = Keyboard.GetState();

            if (player.collision.Intersects(Collision) == true)
            {
                if (KeyInteract.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    player.IsHaunted = false;
                    Console.WriteLine("Shire Used!");
                }
            }

            OldKey = KeyInteract;
        }

        public void NewPosition(Vector2 startingposition)
        {
            SelfPosition = startingposition;
        }
    }
}
