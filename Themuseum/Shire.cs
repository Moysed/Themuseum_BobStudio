﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DXGI;

namespace Themuseum
{
    class Shire
    {
        bool animateActive = false;
        private Texture2D _Sprite;
        private AnimatedTexture Sprite;
        private Vector2 SelfPosition;
        private Rectangle Collision;
        private KeyboardState KeyInteract;
        private KeyboardState OldKey;
        Timer timer;
        public Shire(Vector2 startingposition){
            SelfPosition = startingposition;
            Sprite = new AnimatedTexture(Vector2.Zero, 0, 1, 0.5f);
            timer = new Timer();
            timer.Interval = 1000;
            Collision = new Rectangle((int)SelfPosition.X, (int)SelfPosition.Y, 50, 109);
        }

        public void LoadSprite(ContentManager content)
        {
            Sprite.Load(content, "spritesheet (3)", 6, 1, 4);
            _Sprite = content.Load<Texture2D>("Shire");
        }

        public void Draw(SpriteBatch SB)
        {
            SB.Draw(_Sprite, SelfPosition, Color.White);
            if(animateActive == true)
            {
                Sprite.DrawFrame(SB, new Vector2(465, 0), 1);
            }
        }

        public void Behavior(Player player, float elasped,SoundSystem sound,RoomManager roommanager)
        {
            Collision = new Rectangle((int)SelfPosition.X, (int)SelfPosition.Y, 50, 109);
            Sprite.UpdateFrame(elasped);

            KeyInteract = Keyboard.GetState();

            if (player.collision.Intersects(Collision) == true)
            {
                player.StatusTextDisplay("Press K to Interact");

                if (KeyInteract.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K))
                {

                    timer.Start();
                    timer.Interval--;
                    player.IsHaunted = false;
                    sound.PlaySfx(4);
                    sound.StopBGM();
                    sound.PlayBGM(0);
                    roommanager.mapcolor = Color.White;
                    Console.WriteLine("Shire Used!");
                    animateActive = true;
                }
            }
            else
            {
                animateActive = false;
            }

            OldKey = KeyInteract;
        }

        public void NewPosition(Vector2 startingposition)
        {
            SelfPosition = startingposition;
        }
    }
}
