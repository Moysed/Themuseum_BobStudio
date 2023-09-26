using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
namespace Themuseum
{

    class MainRoomB
    {
        Room1 room1;
        private Texture2D Map_Sprite;
        private Texture2D hint;
        private Vector2 hintPos;
        private Texture2D TileStatic;
        private Texture2D Door;
        private Vector2 DoorPos;
        private Rectangle DoorCollision;
        private Texture2D WallArea_Tex;
        private Texture2D piece3;
        private Vector2 piece3Pos;
        private Texture2D statue;
        private Vector2 statuePos;
        KeyboardState Oldkey_;
        KeyboardState keycontrols;
        Random random;

        public MainRoomB()
        {
            room1 = new Room1();    
            random = new Random();
            DoorPos = new Vector2(610, 72 * 8);
            statuePos = new Vector2(500, 500);
            piece3Pos = new Vector2(random.Next(64,200), random.Next(100,200));
            hintPos = new Vector2(500, 200);
        }

        public void LoadContent(ContentManager content)
        {
            Map_Sprite = content.Load<Texture2D>("MapSheet");
            hint = content.Load<Texture2D>("Hint");
            TileStatic = content.Load<Texture2D>("room3_placeholder");
            Door = content.Load<Texture2D>("placeholderdoor");
            statue = content.Load<Texture2D>("Statue");
            piece3 = content.Load<Texture2D>("Piece3");
        }

        public void Draw(SpriteBatch SB)
        {
           SB.Draw(TileStatic, Vector2.Zero, Color.White);
           SB.Draw(Door, DoorPos, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
           SB.Draw(statue, statuePos, Color.White);
           SB.Draw(piece3, piece3Pos, Color.White);
           SB.Draw(hint, hintPos, Color.White);
            if (room1.showMap == true)
            {
                SB.Draw(Map_Sprite, new Vector2(320, 0), Color.White);
            }
        }
        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed, DialogueBox dialogue)
        {
            keycontrols = Keyboard.GetState();
            //Object Hitbox
            Rectangle statueCollision = new Rectangle((int)statuePos.X, (int)statuePos.Y, 64, 64);
            Rectangle piece5Col = new Rectangle((int)piece3Pos.X, (int)piece3Pos.Y, 64, 64);
            Rectangle hint = new Rectangle((int)hintPos.X, (int)hintPos.Y, 64, 64);

            DoorCollision = new Rectangle((int)DoorPos.X , (int)DoorPos.Y - 5, 32, 30);

            //Wall Collision
            if (player.SelfPosition.X >= _graphics.GraphicsDevice.Viewport.Bounds.Right - 116)
            {
                if (player.speed == 4)
                {
                    player.SelfPosition.X -= player.speed * 2;
                }
                else
                    player.SelfPosition.X -= player.speed;
            }

            else if (player.SelfPosition.X <= _graphics.GraphicsDevice.Viewport.Bounds.Left + 52)
            {
                if (player.speed == 4)
                {
                    player.SelfPosition.X += player.speed * 2;
                }
                else
                    player.SelfPosition.X += player.speed;
            }

            else if (player.SelfPosition.Y <= _graphics.GraphicsDevice.Viewport.Bounds.Top + 64)
            {
                if (player.speed == 4)
                {
                    player.SelfPosition.Y += player.speed * 2;
                }
                else
                    player.SelfPosition.Y += player.speed;
            }
            else if(player.SelfPosition.Y >= _graphics.GraphicsDevice.Viewport.Bounds.Bottom - 145)
            {
                if (player.speed == 4)
                {
                    player.SelfPosition.Y -= player.speed * 2;
                }
                else
                    player.SelfPosition.Y -= player.speed;
            }
            
            if (player.collision.Intersects(DoorCollision))
            {
                Console.WriteLine("Hit");
                if (keycontrols.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E))
                {
                    player.ChangeStartingPosition(new Vector2(player.SelfPosition.X, 110));
                    roomManager.Roomchange(3);
                }
            }

            //Piece Collect
            if (player.collision.Intersects(piece5Col) == true)
            {
                if (keycontrols.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E))
                {
                    Keymanager.MRB_Pieces += 1;
                    Console.WriteLine(Keymanager.MRB_Pieces);
                    piece3Pos.X = 5000;
                }
            }

            //Statue Collision
            if (player.collision.Intersects(statueCollision) == true)
            {
                if (keycontrols.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E))
                {
                    Console.WriteLine(Keymanager.MRB_StatueActive);
                    if(Keymanager.MRB_Pieces == 3)
                    {
                        Keymanager.KeyTrigger("MRB_Statue");
                    }
                    Console.WriteLine(Keymanager.MRB_StatueActive); 
                }
            }

            //hint Collision
            if(player.collision.Intersects(hint) == true && keycontrols.IsKeyDown(Keys.E))
            {
                dialogue.SettingParameter("Hint Block", 200, 200, "Full Statue", Color.Green);
                dialogue.Activation(true);
            }
            else 
            {
                dialogue.Activation(false);
            }

            if (room1.mapActive = true && Keyboard.GetState().IsKeyDown(Keys.G))
            {
                room1.showMap = true;
            }
            else
            {
                room1.showMap = false;
            }

            Oldkey_ = keycontrols;
        }

        
    }
}
