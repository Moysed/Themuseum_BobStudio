using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        Room1 room1;
        private Texture2D Map_Sprite;
        private Texture2D Door;
        private Vector2 DoorPos_MB_MC_C;
        private Rectangle DoorCollision_MB_MC_C;
        private Rectangle DoorCollision_End;
        private Vector2 DoorPos_End;
        private KeyboardState KeyControls;
        private KeyboardState OldKey;
        private Texture2D WallArea_Tex;
        private List<Rectangle> WallArea_Col = new List<Rectangle>();
        private List<PuzzleBlock> puzzleBlocks = new List<PuzzleBlock>();
        private Texture2D BlockArea;
        private Vector2 BlockAreaPos_R;
        private Vector2 BlockAreaPos_B;
        private Vector2 BlockAreaPos_G;
        private Vector2 BlockAreaPos_Y;
        private Rectangle BlockAreaCol_R;
        private Rectangle BlockAreaCol_B;
        private Rectangle BlockAreaCol_G;
        private Rectangle BlockAreaCol_Y;

        private Texture2D BlockReset_Tex;
        private Rectangle BlockReset_Col;
        private Vector2 BlockReset_Pos;

        private Texture2D keyC;
        private Vector2 keyCPos;
        Rectangle keyChitbox;

        
        
        public MRC()
        {
            keyCPos = new Vector2(5000,0);
            room1 = new Room1();    
            WallArea_Col.Add(new Rectangle(0, 0, 1280, 64));
            WallArea_Col.Add(new Rectangle(0, 0, 64, 640));
            WallArea_Col.Add(new Rectangle(1280 - 64, 0, 64, 640));
            WallArea_Col.Add(new Rectangle(0, 640 - 64, 1280, 64));

            puzzleBlocks.Add(new PuzzleBlock(new Vector2(1280 - 256, 640 - 256), "Red"));
            puzzleBlocks.Add(new PuzzleBlock(new Vector2(256, 256), "Blue"));
            puzzleBlocks.Add(new PuzzleBlock(new Vector2(500, 400), "Green"));
            puzzleBlocks.Add(new PuzzleBlock(new Vector2(900, 400), "Yellow"));
        }

        public void LoadSprite(ContentManager content)
        {
            Map_Sprite = content.Load<Texture2D>("MapSheet");
            WallArea_Tex = content.Load<Texture2D>("wallplaceholder");
            Door = content.Load<Texture2D>("placeholderdoor");
            BlockArea = content.Load<Texture2D>("placeholderblock");
            BlockReset_Tex = content.Load<Texture2D>("199-Support07");
            keyC = content.Load<Texture2D>("smallKey");

            for (int i = 0; i < puzzleBlocks.Count; i++)
            {
                puzzleBlocks[i].LoadSprite(content);
            }
        }

        public void Draw(SpriteBatch SB, Color roomcolor , KeyManagement key)
        {
            SB.Draw(BlockArea, BlockAreaPos_R, new Rectangle(0, 0, 64, 64), Color.Red);
            SB.Draw(BlockArea, BlockAreaPos_B, new Rectangle(0, 0, 64, 64), Color.Blue);
            SB.Draw(BlockArea, BlockAreaPos_G, new Rectangle(0, 0, 64, 64), Color.Green);
            SB.Draw(BlockArea, BlockAreaPos_Y, new Rectangle(0, 0, 64, 64), Color.Yellow);

            for (int i = 0; i < WallArea_Col.Count; i++)
            {
                SB.Draw(WallArea_Tex, WallArea_Col[i], Color.White);
            }
            for (int i = 0; i < puzzleBlocks.Count; i++)
            {
                puzzleBlocks[i].Draw(SB);
            }
            SB.Draw(Door, DoorPos_MB_MC_C, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
            SB.Draw(Door, DoorPos_End, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
            SB.Draw(BlockReset_Tex, BlockReset_Pos, new Rectangle(0, 128, 64, 64), Color.White);
            if(key.MRC_Unlock == true)
            {
                SB.Draw(keyC, keyCPos, Color.White);
            }
            
        }

        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed, DialogueBox dialogue, LanternLight light,SoundSystem sound, Ghost ghost)
        {
            KeyControls = Keyboard.GetState();
            //keyC hitbox
            keyChitbox = new Rectangle((int)keyCPos.X, (int)keyCPos.Y, 26, 10);
            //Wall Collision
            for (int i = 0; i < WallArea_Col.Count; i++)
            {
                if (WallArea_Col[i].Intersects(player.collision))
                {
                    if (player.collision.Right >= WallArea_Col[i].Right)
                    {
                        if (player.speed == 3)
                        {
                            player.SelfPosition.X += player.speed * 2;
                        }
                        else
                            player.SelfPosition.X += player.speed;
                    }
                    if (player.collision.Left <= WallArea_Col[i].Left)
                    {
                        if (player.speed == 3)
                        {
                            player.SelfPosition.X -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.X -= player.speed;
                    }
                    if (player.collision.Top >= WallArea_Col[i].Top)
                    {
                        if (player.speed == 3)
                        {
                            player.SelfPosition.Y += player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y += player.speed;
                    }
                    if (player.collision.Bottom <= WallArea_Col[i].Bottom)
                    {
                        if (player.speed == 3)
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
            DoorCollision_MB_MC_C = new Rectangle((int)DoorPos_MB_MC_C.X + 10, (int)DoorPos_MB_MC_C.Y, 32, 64);
            DoorPos_End = new Vector2(640, 0);
            DoorCollision_End = new Rectangle((int)DoorPos_End.X, (int)DoorPos_End.Y + 10, 32, 64);
            BlockAreaPos_R = new Vector2(500, 500);
            BlockAreaPos_B = new Vector2(700, 200);
            BlockAreaPos_G = new Vector2(128, 400);
            BlockAreaPos_Y = new Vector2(1000, 500);
            BlockAreaCol_R = new Rectangle((int)BlockAreaPos_R.X, (int)BlockAreaPos_R.Y, 64, 64);
            BlockAreaCol_B = new Rectangle((int)BlockAreaPos_B.X, (int)BlockAreaPos_B.Y, 64, 64);
            BlockAreaCol_G = new Rectangle((int)BlockAreaPos_G.X, (int)BlockAreaPos_G.Y, 64, 64);
            BlockAreaCol_Y = new Rectangle((int)BlockAreaPos_Y.X, (int)BlockAreaPos_Y.Y, 64, 64);
            BlockReset_Pos = new Vector2(1100, 128);
            BlockReset_Col = new Rectangle((int)BlockReset_Pos.X, (int)BlockReset_Pos.Y, 64, 64);

            for (int i = 0; i < puzzleBlocks.Count; i++)
            {
                puzzleBlocks[i].Behavior(player, elapsed);

                if (BlockAreaCol_R.Intersects(puzzleBlocks[i].Collision) == true && puzzleBlocks[i].KeyDesignation == "Red")
                {
                    if (Keymanager.MRC_R_B == false)
                    {
                        Keymanager.MRC_R_B = true;
                        Console.WriteLine("Block_Red in position");
                    }
                }
                else if (BlockAreaCol_R.Intersects(puzzleBlocks[i].Collision) == false && puzzleBlocks[i].KeyDesignation == "Red")
                {
                    Keymanager.MRC_R_B = false;
                }

                if (BlockAreaCol_B.Intersects(puzzleBlocks[i].Collision) == true && puzzleBlocks[i].KeyDesignation == "Blue")
                {
                    if (Keymanager.MRC_B_B == false)
                    {
                        Keymanager.MRC_B_B = true;
                        Console.WriteLine("Block_Blue in position");
                    }
                }
                else if (BlockAreaCol_B.Intersects(puzzleBlocks[i].Collision) == false && puzzleBlocks[i].KeyDesignation == "Blue")
                {
                    Keymanager.MRC_B_B = false;
                }

                if (BlockAreaCol_G.Intersects(puzzleBlocks[i].Collision) == true && puzzleBlocks[i].KeyDesignation == "Green")
                {
                    if (Keymanager.MRC_G_B == false)
                    {
                        Keymanager.MRC_G_B = true;
                        Console.WriteLine("Block_Green in position");
                    }
                }
                else if (BlockAreaCol_G.Intersects(puzzleBlocks[i].Collision) == false && puzzleBlocks[i].KeyDesignation == "Green")
                {
                    Keymanager.MRC_G_B = false;
                }

                if (BlockAreaCol_Y.Intersects(puzzleBlocks[i].Collision) == true && puzzleBlocks[i].KeyDesignation == "Yellow")
                {
                    if (Keymanager.MRC_Y_B == false)
                    {
                        Keymanager.MRC_Y_B = true;
                        Console.WriteLine("Block_Yellow in position");
                    }
                }
                else if (BlockAreaCol_Y.Intersects(puzzleBlocks[i].Collision) == false && puzzleBlocks[i].KeyDesignation == "Yellow")
                {
                    Keymanager.MRC_Y_B = false;
                }

            }

            if (Keymanager.MRC_R_B == true && Keymanager.MRC_B_B == true && Keymanager.MRC_G_B == true && Keymanager.MRC_Y_B == true && Keymanager.MRC_Unlock == false)
            {
                Console.WriteLine("All Combination in place");
                Keymanager.MRC_Unlock = true;
                dialogue.SettingParameter("Hint Block", 0, 0, "I heard something dropped", Color.Red);
                dialogue.Activation(true);
                keyCPos = new Vector2(400, 200);
            }

            if(player.collision.Intersects(keyChitbox))
            {
                player.StatusTextDisplay("Press E to Interact");
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    dialogue.SettingParameter("placeholderblock", 200, 200, "Key collected", Color.Green);
                    dialogue.Activation(true);
                    sound.PlaySfx(0);
                    Keymanager.KeyCollectC = true;
                    keyCPos.X = 5000;
                   
                }
            }

            

            
            //Player Interaction
            if (player.collision.Intersects(DoorCollision_MB_MC_C) == true)
            {
                player.StatusTextDisplay("Press E to Interact");
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    ghost.Prechase(player);
                    sound.PlaySfx(1);
                    player.ChangeStartingPosition(new Vector2(1180 - 32, player.SelfPosition.Y));
                    roomManager.Roomchange(5);
                }
            }
            if (player.collision.Intersects(DoorCollision_End) == true)
            {
                player.StatusTextDisplay("Press E to Interact");

                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    if (Keymanager.KeyCollectC == false)
                    {
                        sound.PlaySfx(2);
                        dialogue.SettingParameter("Hint Block", 0, 0, "The door is locked, Find a key to open it", Color.Red);
                        dialogue.Activation(true);
                        Console.WriteLine("KeyC didn't collected");
                    }
                    else if (Keymanager.KeyCollectC == true)
                    {
                        sound.PlaySfx(1);
                        Console.WriteLine("Door unlocked");
                        Keymanager.GameEnded = true;
                    }

                }
            }
            if (player.collision.Intersects(BlockReset_Col) == true)
            {
                player.StatusTextDisplay("Press E to Interact");
                if (KeyControls.IsKeyDown(Keys.E) && OldKey.IsKeyUp(Keys.E))
                {
                    sound.PlaySfx(4);
                    Console.WriteLine("Puzzle Blocks Reset");
                    for (int i = 0; i < puzzleBlocks.Count; i++)
                    {
                        puzzleBlocks[i].ResetPosition();
                    }
                }
            }
            OldKey = KeyControls;
        }
        public void Reset()
        {
            keyCPos = new Vector2(400000, 200);
            for (int i = 0; i < puzzleBlocks.Count; i++)
            {
                puzzleBlocks[i].ResetPosition();
            }


        }
       
    }
   

}

