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
        KeyboardState Oldkey_;
        KeyboardState keycontrols;

        public MainRoomB()
        {
            DoorPos = new Vector2(610, 72 * 8);
        }

        public void LoadContent(ContentManager content)
        {
            TileStatic = content.Load<Texture2D>("room3_placeholder");
                Door = content.Load<Texture2D>("placeholderdoor");

        }

        public void Draw(SpriteBatch SB)
        {
            SB.Draw(TileStatic, Vector2.Zero, Color.White);
            SB.Draw(Door, DoorPos, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
           

        }
        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed)
        {
            keycontrols = Keyboard.GetState();
            //Object Hitbox
           
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
                }
        
            }
}
