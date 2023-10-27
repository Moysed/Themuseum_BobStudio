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
        private Texture2D bg;
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
        private List<Texture2D> BlockArea = new List<Texture2D>();
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

        private Texture2D hint;
        private Vector2 hintPos;




        public MRC()
        {
            keyCPos = new Vector2(5000,0);
            room1 = new Room1();
            hintPos = new Vector2(400, 250);
            WallArea_Col.Add(new Rectangle(0, 0, 1280, 200));
            WallArea_Col.Add(new Rectangle(0, 0, 79, 245));
            WallArea_Col.Add(new Rectangle(1280 - 75, 0, 64, 640));
            WallArea_Col.Add(new Rectangle(0, 640 - 43, 1280, 44));
            WallArea_Col.Add(new Rectangle(0, 471, 79, 500));
            WallArea_Col.Add(new Rectangle(0, 242, 32, 233));

            puzzleBlocks.Add(new PuzzleBlock(new Vector2(256, 300), "Pillow","pillow_obj"));
            puzzleBlocks.Add(new PuzzleBlock(new Vector2(1280 - 256, 300), "Tusk", "tusk_obj"));
            puzzleBlocks.Add(new PuzzleBlock(new Vector2(500, 300), "Vase", "vase_obj"));
            puzzleBlocks.Add(new PuzzleBlock(new Vector2(900, 300), "Chest", "chest_obj"));
        }

        public void LoadSprite(ContentManager content)
        {
            bg = content.Load<Texture2D>("RoomCbg");
            hint = content.Load<Texture2D>("Note");
            Map_Sprite = content.Load<Texture2D>("MapSheet");
            WallArea_Tex = content.Load<Texture2D>("wallplaceholder");
            Door = content.Load<Texture2D>("placeholderdoor");
            BlockArea.Add(content.Load<Texture2D>("emptycase"));
            BlockArea.Add(content.Load<Texture2D>("chest_case"));
            BlockArea.Add(content.Load<Texture2D>("vase_case"));
            BlockArea.Add(content.Load<Texture2D>("tusk_case"));
            BlockArea.Add(content.Load<Texture2D>("pillow_case"));
            BlockReset_Tex = content.Load<Texture2D>("199-Support07");
            keyC = content.Load<Texture2D>("smallKey");

            for (int i = 0; i < puzzleBlocks.Count; i++)
            {
                puzzleBlocks[i].LoadSprite(content);
            }
        }

        public void Draw(SpriteBatch SB, Color roomcolor , KeyManagement key)
        {
            for (int i = 0; i < WallArea_Col.Count; i++)
            {
                SB.Draw(WallArea_Tex, WallArea_Col[i], Color.White);
            }

            

            SB.Draw(bg, Vector2.Zero, Color.White);

            if(key.MRC_R_B == true)
            {
                SB.Draw(BlockArea[1], BlockAreaPos_R, new Rectangle(0, 0, 92, 165), Color.White);
            }
            else if(key.MRC_R_B == false)
            {
                SB.Draw(BlockArea[0], BlockAreaPos_R, new Rectangle(0, 0, 92, 165), Color.White);
            }

            if(key.MRC_B_B == true) 
            {
                SB.Draw(BlockArea[2], BlockAreaPos_B, new Rectangle(0, 0, 92, 165), Color.White);
            }
            else if(key.MRC_B_B == false)
            {
                SB.Draw(BlockArea[0], BlockAreaPos_B, new Rectangle(0, 0, 92, 165), Color.White);
            }

            if(key.MRC_G_B == true)
            {
                SB.Draw(BlockArea[3], BlockAreaPos_G, new Rectangle(0, 0, 92, 165), Color.White);
            }
            else if(key.MRC_G_B == false)
            {
                SB.Draw(BlockArea[0], BlockAreaPos_G, new Rectangle(0, 0, 92, 165), Color.White);
            }
            
            if(key.MRC_Y_B == true)
            {
                SB.Draw(BlockArea[4], BlockAreaPos_Y, new Rectangle(0, 0, 92, 165), Color.White);
            }
            else if(key.MRC_Y_B== false)
            {
                SB.Draw(BlockArea[0], BlockAreaPos_Y, new Rectangle(0, 0, 92, 165), Color.White);
            }
           
            
            
            for (int i = 0; i < puzzleBlocks.Count; i++)
            {
                puzzleBlocks[i].Draw(SB);
            }
            //SB.Draw(Door, DoorPos_MB_MC_C, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
            //SB.Draw(Door, DoorPos_End, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
            SB.Draw(BlockReset_Tex, BlockReset_Pos, new Rectangle(0, 128, 64, 64), Color.White);
            if(key.MRC_Unlock == true)
            {
                SB.Draw(keyC, keyCPos, Color.White);
            }

            SB.Draw(hint, hintPos, Color.White);
        }

        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed, DialogueBox dialogue, LanternLight light,SoundSystem sound, Ghost ghost, Staminabar UI)
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
                        if (player.speed == 2)
                        {
                            player.SelfPosition.X += player.speed * 2;
                        }
                        else
                            player.SelfPosition.X += player.speed;
                    }
                    if (player.collision.Left <= WallArea_Col[i].Left)
                    {
                        if (player.speed == 2)
                        {
                            player.SelfPosition.X -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.X -= player.speed;
                    }
                    if (player.collision.Top >= WallArea_Col[i].Top)
                    {
                        if (player.speed == 2)
                        {
                            player.SelfPosition.Y += player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y += player.speed;
                    }
                    if (player.collision.Bottom <= WallArea_Col[i].Bottom)
                    {
                        if (player.speed == 2)
                        {

                            player.SelfPosition.Y -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y -= player.speed;

                    }

                }
            }
            //Object Behavior
            //DoorPos_MB_MC_C = new Vector2(32, 200);
            DoorCollision_MB_MC_C = new Rectangle(0, 242, 32, 233);
            DoorPos_End = new Vector2(640, 0);
            DoorCollision_End = new Rectangle(570, 57, 138, 182);
            BlockAreaPos_R = new Vector2(149, 400);
            BlockAreaPos_B = new Vector2(700, 400);
            BlockAreaPos_G = new Vector2(450, 400);
            BlockAreaPos_Y = new Vector2(1000, 400);
            BlockAreaCol_R = new Rectangle((int)BlockAreaPos_R.X, (int)BlockAreaPos_R.Y, 92, 165);
            BlockAreaCol_B = new Rectangle((int)BlockAreaPos_B.X, (int)BlockAreaPos_B.Y, 92, 165);
            BlockAreaCol_G = new Rectangle((int)BlockAreaPos_G.X, (int)BlockAreaPos_G.Y, 92, 165);
            BlockAreaCol_Y = new Rectangle((int)BlockAreaPos_Y.X, (int)BlockAreaPos_Y.Y, 92, 165);
            BlockReset_Pos = new Vector2(1100, 240);
            BlockReset_Col = new Rectangle((int)BlockReset_Pos.X, (int)BlockReset_Pos.Y, 64, 64);
            Rectangle hint = new Rectangle((int)hintPos.X, (int)hintPos.Y, 64, 64);


            for (int i = 0; i < puzzleBlocks.Count; i++)
            {
                puzzleBlocks[i].Behavior(player, elapsed);

                if (BlockAreaCol_R.Intersects(puzzleBlocks[i].Collision) == true && puzzleBlocks[i].KeyDesignation == "Chest")
                {
                    if (Keymanager.MRC_R_B == false)
                    {
                        Keymanager.MRC_R_B = true;
                        puzzleBlocks[i].setvisible(false);
                        Console.WriteLine("Block_Red in position");
                    }
                }
                else if (BlockAreaCol_R.Intersects(puzzleBlocks[i].Collision) == false && puzzleBlocks[i].KeyDesignation == "Chest")
                {
                    Keymanager.MRC_R_B = false;
                }

                if (BlockAreaCol_B.Intersects(puzzleBlocks[i].Collision) == true && puzzleBlocks[i].KeyDesignation == "Vase")
                {
                    if (Keymanager.MRC_B_B == false)
                    {
                        puzzleBlocks[i].setvisible(false);
                        Keymanager.MRC_B_B = true;
                        Console.WriteLine("Block_Blue in position");
                    }
                }
                else if (BlockAreaCol_B.Intersects(puzzleBlocks[i].Collision) == false && puzzleBlocks[i].KeyDesignation == "Vase")
                {
                    Keymanager.MRC_B_B = false;
                }

                if (BlockAreaCol_G.Intersects(puzzleBlocks[i].Collision) == true && puzzleBlocks[i].KeyDesignation == "Tusk")
                {
                    if (Keymanager.MRC_G_B == false)
                    {

                        puzzleBlocks[i].setvisible(false);
                        Keymanager.MRC_G_B = true;
                        Console.WriteLine("Block_Green in position");
                    }
                }
                else if (BlockAreaCol_G.Intersects(puzzleBlocks[i].Collision) == false && puzzleBlocks[i].KeyDesignation == "Tusk")
                {
                    Keymanager.MRC_G_B = false;
                }

                if (BlockAreaCol_Y.Intersects(puzzleBlocks[i].Collision) == true && puzzleBlocks[i].KeyDesignation == "Pillow")
                {
                    if (Keymanager.MRC_Y_B == false)
                    {
                        puzzleBlocks[i].setvisible(false);
                        Keymanager.MRC_Y_B = true;
                        Console.WriteLine("Block_Yellow in position");
                    }
                }
                else if (BlockAreaCol_Y.Intersects(puzzleBlocks[i].Collision) == false && puzzleBlocks[i].KeyDesignation == "Pillow")
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
                keyCPos = new Vector2(400, 240);
            }

            if(player.collision.Intersects(keyChitbox))
            {
                player.StatusTextDisplay("Press K to Interact");
                if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K))
                {
                    dialogue.SettingParameter("placeholderblock", 200, 200, "Key collected", Color.Green);
                    UI.ChangeObjectiveText("Process through the next room", "");
                    dialogue.Activation(true);
                    sound.PlaySfx(0);
                    Keymanager.KeyCollectC = true;
                    keyCPos.X = 5000;
                   
                }
            }

            

            
            //Player Interaction
            if (player.collision.Intersects(DoorCollision_MB_MC_C) == true)
            {
                player.StatusTextDisplay("Press K to Interact");
                if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K))
                {
                    ghost.Prechase(player);
                    sound.PlaySfx(1);
                    player.ChangeStartingPosition(new Vector2(1180 - 32, player.SelfPosition.Y));
                    roomManager.Roomchange(5);
                }
            }
            if (player.collision.Intersects(DoorCollision_End) == true)
            {
                player.StatusTextDisplay("Press K to Interact");

                if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K))
                {
                    if (Keymanager.KeyCollectC == false)
                    {
                        sound.PlaySfx(2);
                        dialogue.SettingParameter("Hint Block", 0, 0, "The door is locked, Find a key to open it", Color.Red);
                        UI.ChangeObjectiveText("Find clues and complete the puzzle", "Hint: A magic circle can reset object position");
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
                player.StatusTextDisplay("Press K to Interact");
                if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K))
                {
                    sound.PlaySfx(4);
                    Console.WriteLine("Puzzle Blocks Reset");
                    for (int i = 0; i < puzzleBlocks.Count; i++)
                    {
                        puzzleBlocks[i].ResetPosition();
                    }
                }
            }

            if (player.collision.Intersects(hint) == true)
            {
                player.StatusTextDisplay("Press K to Interact");
                if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K))
                {
                    sound.PlaySfx(3);
                    dialogue.SettingParameter("Hint Block", 200, 200, "Each object has its place and time. First,ornated and used for keeping things.\nSecond, once belonged to a mighty beast. Third, the display for olden time delicacy,\nLast, a thing your head laid on at the end of day", Color.Brown);
                    dialogue.Activation(true);
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

