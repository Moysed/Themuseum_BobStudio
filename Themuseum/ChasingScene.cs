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
    class ChasingScene
    {
        Room1 room1;
        private Texture2D Map_Sprite;
        private Texture2D TileStatic;
        private Texture2D Wallpaper;
        private Texture2D Door;
        private Texture2D Tree;
        private Vector2 DoorPos_Room3;
        private Vector2 DoorPos_MRC;
        private Rectangle DoorCollision_Room3;
        private Rectangle DoorCollision_MRC;
        private Rectangle Slowdownpath;
        private KeyboardState KeyControls;
        private KeyboardState OldKey;
        private Texture2D WallArea_Tex;
        Shire shire;
        private List<Rectangle> WallArea_Col = new List<Rectangle>();
        private List<Rectangle> Obstacles = new List<Rectangle>();
        public ChasingScene()
        {
            room1 = new Room1();
            shire = new Shire(new Vector2(600,250));
            WallArea_Col.Add(new Rectangle(0, 0, 1280, 150));
            WallArea_Col.Add(new Rectangle(0, 0, 15, 640));
            WallArea_Col.Add(new Rectangle(0, 485, 1280, 640));
            WallArea_Col.Add(new Rectangle(1280 - 15, 0, 64, 640));
            //Tree
            WallArea_Col.Add(new Rectangle(600, 240, 50, 50));
            //Obstacles for chasing event
            Obstacles.Add(new Rectangle(200, 252, 30, 100));
            Obstacles.Add(new Rectangle(305, 390, 30, 95));
            Obstacles.Add(new Rectangle(430, 252, 30, 130));
            Obstacles.Add(new Rectangle(560, 390, 30, 95));
            Obstacles.Add(new Rectangle(680, 252, 30, 95));
            Obstacles.Add(new Rectangle(820, 350, 30, 135));
            Obstacles.Add(new Rectangle(950, 400, 30, 85));
            Obstacles.Add(new Rectangle(950, 252, 30, 60)); 
        }

        public void LoadSprite(ContentManager content)
        {
            Tree = content.Load<Texture2D>("Tree");
            Wallpaper = content.Load<Texture2D>("HallwayBG");
            Map_Sprite = content.Load<Texture2D>("MapSheet");
            WallArea_Tex = content.Load<Texture2D>("wallplaceholder");
            Door = content.Load<Texture2D>("placeholderdoor");
            shire.LoadSprite(content);
        }

        public void Draw(SpriteBatch SB, Color roomcolor)
        {
            /*for (int i = 0; i < WallArea_Col.Count; i++)
            {
                SB.Draw(WallArea_Tex, WallArea_Col[i], Color.White);
            }*/
            SB.Draw(Wallpaper, Vector2.Zero, roomcolor);
            //SB.Draw(Tree, new Vector2(447, 0), Color.White);
            for (int i = 0; i < Obstacles.Count; i++)
            {
                SB.Draw(WallArea_Tex, Obstacles[i], Color.White);
            }
            //SB.Draw(Door, DoorPos_Room3, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
            //SB.Draw(Door, DoorPos_MRC, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
            //shire.Draw(SB);
        }

        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed, DialogueBox dialogue, LanternLight light,SoundSystem sound, Ghost ghost, Staminabar UI)
        {
            KeyControls = Keyboard.GetState();
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
            for (int i = 0; i < Obstacles.Count; i++)
            {
                if (Obstacles[i].Intersects(player.collision))
                {
                    if (player.collision.Right >= Obstacles[i].Right)
                    {
                        if (player.speed == 3)
                        {
                            player.SelfPosition.X += player.speed * 2;
                        }
                        else
                            player.SelfPosition.X += player.speed;
                    }
                    if (player.collision.Left <= Obstacles[i].Left)
                    {
                        if (player.speed == 3)
                        {
                            player.SelfPosition.X -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.X -= player.speed;
                    }
                    if (player.collision.Top >= Obstacles[i].Top)
                    {
                        if (player.speed == 3)
                        {
                            player.SelfPosition.Y += player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y += player.speed;
                    }
                    if (player.collision.Bottom <= Obstacles[i].Bottom)
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
            DoorPos_Room3 = new Vector2(32,0);
                DoorCollision_Room3 = new Rectangle((int)DoorPos_Room3.X, (int)DoorPos_Room3.Y, 32, 640);
                DoorPos_MRC = new Vector2(1280 - 64, 0);
                DoorCollision_MRC = new Rectangle((int)DoorPos_MRC.X, (int)DoorPos_MRC.Y, 32, 640);
                Slowdownpath = new Rectangle(0, 0, 100, 2000);

            //Player Interaction
            if (player.collision.Intersects(DoorCollision_Room3) == true)
                {
                    player.StatusTextDisplay("Press K to Interact");
                    if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K))
                    {
                        ghost.Prechase(player);
                        sound.PlaySfx(1);
                        player.ChangeStartingPosition(new Vector2(1180-65, 8*32));
                        roomManager.Roomchange(3);
                    }
                }
                if (player.collision.Intersects(DoorCollision_MRC) == true)
                {
                    player.StatusTextDisplay("Press K to Interact");

                    if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K))
                    {
                        ghost.Prechase(player);
                        sound.PlaySfx(1);
                        UI.ChangeObjectiveText("Find clues and complete the puzzle", "Hint: A magic circle can reset object position");
                        player.ChangeStartingPosition(new Vector2(64, player.SelfPosition.Y));
                        roomManager.Roomchange(5);
                    }
                }
            if (ghost.collision.Intersects(Slowdownpath) == true || player.collision.Intersects(Slowdownpath) == true) { }
            {

                if (KeyControls.IsKeyDown(Keys.K) && OldKey.IsKeyUp(Keys.K))
                {
                    ghost.Prechase(player);
                }
                ghost.speed = 1.5f;
            }
            //shire.Behavior(player, elapsed, sound,roomManager);
            OldKey = KeyControls;
            }
        }
    }

