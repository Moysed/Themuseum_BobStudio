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
    class MRB_To_MRC_Corridor
    {
        private Texture2D TileStatic;
        private Texture2D Door;
        private Vector2 DoorPos_Room3;
        private Vector2 DoorPos_MRC;
        private Rectangle DoorCollision_Room3;
        private Rectangle DoorCollision_MRC;
        private KeyboardState KeyControls;
        private KeyboardState OldKey;
        private Texture2D WallArea_Tex;
        private List<Rectangle> WallArea_Col = new List<Rectangle>();
        public MRB_To_MRC_Corridor()
        {
            WallArea_Col.Add(new Rectangle(0, 0, 1280, 64));
            WallArea_Col.Add(new Rectangle(0, 0, 64, 640));
            WallArea_Col.Add(new Rectangle(0, 400, 1280, 640));
            WallArea_Col.Add(new Rectangle(1280 - 64, 0, 64, 640));
        }

        public void LoadSprite(ContentManager content)
        {
            WallArea_Tex = content.Load<Texture2D>("wallplaceholder");
            Door = content.Load<Texture2D>("placeholderdoor");
        }

        public void Draw(SpriteBatch SB)
        {
            for (int i = 0; i < WallArea_Col.Count; i++)
            {
                SB.Draw(WallArea_Tex, WallArea_Col[i], Color.White);
            }
            SB.Draw(Door, DoorPos_Room3, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
            SB.Draw(Door, DoorPos_MRC, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
        }

        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed)
        {
            KeyControls = Keyboard.GetState();
            //Wall Collision
            for (int i = 0; i < WallArea_Col.Count; i++)
            {
                if (WallArea_Col[i].Intersects(player.collision))
                {
                    if (player.collision.Right >= WallArea_Col[i].Right)
                    {
                        if (player.speed == 4)
                        {
                            player.SelfPosition.X += player.speed * 2;
                        }
                        else
                            player.SelfPosition.X += player.speed;
                    }
                    if (player.collision.Left <= WallArea_Col[i].Left)
                    {
                        if (player.speed == 4)
                        {
                            player.SelfPosition.X -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.X -= player.speed;
                    }
                    if (player.collision.Top >= WallArea_Col[i].Top)
                    {
                        if (player.speed == 4)
                        {
                            player.SelfPosition.Y += player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y += player.speed;
                    }
                    if (player.collision.Bottom <= WallArea_Col[i].Bottom)
                    {
                        if (player.speed == 4)
                        {

                            player.SelfPosition.Y -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y -= player.speed;

                    }

                }
            }
                //Object Behavior
                DoorPos_Room3 = new Vector2(32,200);
                DoorCollision_Room3 = new Rectangle((int)DoorPos_Room3.X + 10, (int)DoorPos_Room3.Y, 32, 64);
                DoorPos_MRC = new Vector2(1280 - 64, 200);
                DoorCollision_MRC = new Rectangle((int)DoorPos_MRC.X - 10, (int)DoorPos_MRC.Y, 32, 64);

                //Player Interaction
                if (player.collision.Intersects(DoorCollision_Room3) == true)
                {

                    if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                    {
                        
                        player.ChangeStartingPosition(new Vector2(1180-65, player.SelfPosition.Y));
                        roomManager.Roomchange(3);
                    }
                }
                if (player.collision.Intersects(DoorCollision_MRC) == true)
                {

                    if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                    {

                        player.ChangeStartingPosition(new Vector2(64, player.SelfPosition.Y));
                        roomManager.Roomchange(6);
                    }
                }

                OldKey = KeyControls;
            }
        }
    }

