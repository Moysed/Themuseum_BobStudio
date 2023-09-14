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
    class MRC
    {
        private Texture2D Door;
        private Vector2 DoorPos_MB_MC_C;
        private Rectangle DoorCollision_MB_MC_C;
        private KeyboardState KeyControls;
        private KeyboardState OldKey;
        private Texture2D WallArea_Tex;
        private List<Rectangle> WallArea_Col = new List<Rectangle>();
        private List<PuzzleBlock> puzzleBlocks = new List<PuzzleBlock>();
        public MRC()
        {
            WallArea_Col.Add(new Rectangle(0, 0, 1280, 64));
            WallArea_Col.Add(new Rectangle(0, 0, 64, 640));
            WallArea_Col.Add(new Rectangle(1280-64, 0, 64, 640));
            WallArea_Col.Add(new Rectangle(0, 640-64, 1280, 64));

            puzzleBlocks.Add(new PuzzleBlock(new Vector2(256,256),"Blue"));
        }

        public void LoadSprite(ContentManager content)
        {
            WallArea_Tex = content.Load<Texture2D>("wallplaceholder");
            Door = content.Load<Texture2D>("placeholderdoor");

            for(int i = 0; i < puzzleBlocks.Count; i++)
            {
                puzzleBlocks[i].LoadSprite(content);
            }
        }

        public void Draw(SpriteBatch SB)
        {
            for (int i = 0; i < WallArea_Col.Count; i++)
            {
                SB.Draw(WallArea_Tex, WallArea_Col[i], Color.White);
            }
            for (int i = 0; i < puzzleBlocks.Count; i++)
            {
                puzzleBlocks[i].Draw(SB);
            }
            SB.Draw(Door, DoorPos_MB_MC_C, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
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
                DoorPos_MB_MC_C = new Vector2(32, 200);
                DoorCollision_MB_MC_C = new Rectangle((int)DoorPos_MB_MC_C.X, (int)DoorPos_MB_MC_C.Y, 32, 64);

                for (int i = 0; i < puzzleBlocks.Count; i++)
                {
                    puzzleBlocks[i].Behavior(player,elapsed);
                }

            //Player Interaction
            if (player.collision.Intersects(DoorCollision_MB_MC_C) == true)
                {

                    if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                    {

                        player.ChangeStartingPosition(new Vector2(1180 - 32, 200));
                        roomManager.Roomchange(5);
                    }
                }

                OldKey = KeyControls;
            }
        }
    }
