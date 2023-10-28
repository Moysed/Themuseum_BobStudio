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
        private List<Texture2D> Wood = new List<Texture2D>();
        private Texture2D wood1;
        private Texture2D wood2;
        private Texture2D wood3;
        Random r = new Random();

        private Vector2 ChaseTriggerPos;
        private Rectangle ChaseTriggerCol;
        public ChasingScene()
        {
            room1 = new Room1();
            shire = new Shire(new Vector2(600,250));
            WallArea_Col.Add(new Rectangle(0, 0, 1280, 200));
            WallArea_Col.Add(new Rectangle(0, 0, 15, 640));
            WallArea_Col.Add(new Rectangle(0, 485, 1280, 640));
            WallArea_Col.Add(new Rectangle(1280 - 15, 0, 64, 640));
            //Tree
            WallArea_Col.Add(new Rectangle(600, 240, 50, 50));
            //Obstacles for chasing event
            Obstacles.Add(new Rectangle(200, 252-20, 54, 110));
            Obstacles.Add(new Rectangle(305, 390+20, 54, 70));
            Obstacles.Add(new Rectangle(430, 252, 30, 130));
            Obstacles.Add(new Rectangle(560, 390, 30, 95));
            Obstacles.Add(new Rectangle(680, 252, 30, 95));
            Obstacles.Add(new Rectangle(820, 350, 30, 135));
            Obstacles.Add(new Rectangle(950, 400, 30, 85));
            Obstacles.Add(new Rectangle(950, 252, 30, 60));

            ChaseTriggerPos = new Vector2(200,0);
        }

        public void LoadSprite(ContentManager content)
        {
            Tree = content.Load<Texture2D>("Tree");
            Wallpaper = content.Load<Texture2D>("HallwayBG");
            Map_Sprite = content.Load<Texture2D>("MapSheet");
            WallArea_Tex = content.Load<Texture2D>("wallplaceholder");
            Door = content.Load<Texture2D>("placeholderdoor");
            shire.LoadSprite(content);
            Wood.Add(content.Load<Texture2D>("Wood"));
            Wood.Add(content.Load<Texture2D>("Wood2"));
            Wood.Add(content.Load<Texture2D>("Wood3"));
        }

        public void Draw(SpriteBatch SB, Color roomcolor)
        {
            /*for (int i = 0; i < WallArea_Col.Count; i++)
            {
                SB.Draw(WallArea_Tex, WallArea_Col[i], Color.White);
            }*/
            SB.Draw(Wallpaper, Vector2.Zero, roomcolor);
            //SB.Draw(Tree, new Vector2(447, 0), Color.White);
            
            

            for (int i = 0; i < 2; i++)
            {
                
                    SB.Draw(Wood[0], Obstacles[i], Color.White);
                
                
            }

            for (int i = 2; i < 5; i++)
            {

                SB.Draw(Wood[2], Obstacles[i], Color.White);


            }
            for (int i = 5; i < 8; i++)
            {

                SB.Draw(Wood[1], Obstacles[i], Color.White);


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
                        if (player.speed == 2)
                        {
                            player.SelfPosition.X += player.speed * 2;
                        }
                        else
                            player.SelfPosition.X += player.speed;
                    }
                    if (player.collision.Left <= Obstacles[i].Left)
                    {
                        if (player.speed == 2)
                        {
                            player.SelfPosition.X -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.X -= player.speed;
                    }
                    if (player.collision.Top >= Obstacles[i].Top)
                    {
                        if (player.speed == 2)
                        {
                            player.SelfPosition.Y += player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y += player.speed;
                    }
                    if (player.collision.Bottom <= Obstacles[i].Bottom)
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
            DoorPos_Room3 = new Vector2(32,0);
                DoorCollision_Room3 = new Rectangle((int)DoorPos_Room3.X, (int)DoorPos_Room3.Y, 32, 640);
                DoorPos_MRC = new Vector2(1280 - 64, 0);
                DoorCollision_MRC = new Rectangle((int)DoorPos_MRC.X, (int)DoorPos_MRC.Y, 32, 640);
                Slowdownpath = new Rectangle(0, 0, 1280, 640);
            ChaseTriggerCol = new Rectangle((int)ChaseTriggerPos.X, (int)ChaseTriggerPos.Y, 32, 640);

            //Player Interaction
            if (player.collision.Intersects(DoorCollision_Room3) == true)
                {
                   
                        ghost.Prechase(player,Keymanager);
                        sound.PlaySfx(1);
                        player.ChangeStartingPosition(new Vector2(1180-65, 8*32));
                        roomManager.Roomchange(3);
                    
                }
                if (player.collision.Intersects(DoorCollision_MRC) == true)
                {



                        ghost.Prechase(player, Keymanager);
                //sound.PlaySfx(1);
                        UI.ChangeObjectiveText("Continue through the courtyard", "");
                        player.ChangeStartingPosition(new Vector2(64, player.SelfPosition.Y));
                        roomManager.Roomchange(5);
                    
                }
            if(player.collision.Intersects(ChaseTriggerCol) == true && Keymanager.chasescenetrigger == false)
            {
                dialogue.SettingParameter("Hint Block", 0, 0, "Run", Color.DarkRed);
                dialogue.Activation(true);
                UI.ChangeObjectiveText("Evade evil spirit", "Hint: Candlelight can slows down the spirit, Find shire to dispel it");
                Keymanager.chasescenetrigger = true;
                player.IsHaunted = true;
                ghost.Prechase(player, Keymanager);
                
                roomManager.mapcolor = Color.Red;
                sound.PlayBGM(1);

            }
            if (ghost.collision.Intersects(Slowdownpath) == true || player.collision.Intersects(Slowdownpath) == true)
            {
                ghost.speed = 1.5f;
            }
            //shire.Behavior(player, elapsed, sound,roomManager);
            OldKey = KeyControls;
            }
        }
    }

