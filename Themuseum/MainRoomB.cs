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

        private Texture2D TileStatic;
        private Texture2D Door;
        private Vector2 DoorPos;
        private Rectangle DoorCollision;
        private Texture2D WallArea_Tex;
        private Texture2D piece5;
        private Vector2 piece5Pos;
        private Texture2D piece6;
        private Vector2 piece6Pos;
        private Texture2D statue;
        private Vector2 statuePos;
        KeyboardState Oldkey_;
        KeyboardState keycontrols;


        public MainRoomB()
        {
            DoorPos = new Vector2(610, 72 * 8);

            statuePos = new Vector2(500, 500);
            piece5Pos = new Vector2(200, 200);
            piece6Pos = new Vector2(800, 200);
        }

        public void LoadContent(ContentManager content)
        {
            TileStatic = content.Load<Texture2D>("room3_placeholder");
            Door = content.Load<Texture2D>("placeholderdoor");
            statue = content.Load<Texture2D>("Statue");
            piece5 = content.Load<Texture2D>("Piece5");
            piece6 = content.Load<Texture2D>("Piece6");
        }

        public void Draw(SpriteBatch SB)
        {
           SB.Draw(TileStatic, Vector2.Zero, Color.White);
           SB.Draw(Door, DoorPos, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
           SB.Draw(statue, statuePos, Color.White);
           SB.Draw(piece5, piece5Pos, Color.White);
           SB.Draw(piece6, piece6Pos, Color.White);
        }
        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed)
        {
            keycontrols = Keyboard.GetState();
            //Object Hitbox
            Rectangle statueCollision = new Rectangle((int)statuePos.X, (int)statuePos.Y, 64, 64);
            Rectangle piece5Col = new Rectangle((int)piece5Pos.X, (int)piece5Pos.Y, 64, 64);
            Rectangle piece6Col = new Rectangle((int)piece6Pos.X, (int)piece6Pos.Y, 64, 64);

            DoorCollision = new Rectangle((int)DoorPos.X, (int)DoorPos.Y , 32, 30);

            //Wall Collision
            if (player.SelfPosition.X >= _graphics.GraphicsDevice.Viewport.Bounds.Right - 64)
            {
                if (player.speed == 4)
                {
                    player.SelfPosition.X -= player.speed * 2;
                }
                else
                    player.SelfPosition.X -= player.speed;
            }

            else if (player.SelfPosition.X <= _graphics.GraphicsDevice.Viewport.Bounds.Left + 64)
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
            else if(player.SelfPosition.Y >= _graphics.GraphicsDevice.Viewport.Bounds.Bottom - 64)
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
                    player.ChangeStartingPosition(new Vector2(600, 110));
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
                    piece5Pos.X = 5000;
                }
            }
            if (player.collision.Intersects(piece6Col) == true)
            {
                if (keycontrols.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E))
                {
                    Keymanager.MRB_Pieces += 1;
                    Console.WriteLine(Keymanager.MRB_Pieces);
                    piece6Pos.X = 5000;
                }
            }

            //Statue Collision
            if (player.collision.Intersects(statueCollision) == true)
            {
                if (keycontrols.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E))
                {
                    Console.WriteLine(Keymanager.MRB_StatueActive);
                    if(Keymanager.MRB_Pieces == 6)
                    {
                        Keymanager.KeyTrigger("MRB_Statue");
                    }
                    Console.WriteLine(Keymanager.MRB_StatueActive); 
                }
            }

            Oldkey_ = keycontrols;
        }
        
    }
}
