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
        private LanternRefill lanternRefill;
        private Vector2 LanternRefillPos;
        private Texture2D Map_Sprite;
        private Texture2D hint;
        private Vector2 hintPos;
        private Texture2D TileStatic;
        private Texture2D Door;
        private Vector2 DoorPos;
        private Rectangle DoorCollision;
        private Texture2D WallArea_Tex;
       
        private Texture2D uncompletedStatue;
        private Texture2D completedStatue;
        private Vector2 uncomStatuePos;
        private Vector2 comStatuePos;
        KeyboardState Oldkey_;
        KeyboardState keycontrols;
        private List<Rectangle> WallArea_Col = new List<Rectangle>();
        private List<Rectangle> Hitbox_Showcase = new List<Rectangle>();
        private List<Texture2D> Showcase = new List<Texture2D> ();
        Random random;
        private KeyManagement keyManagement;

        public MainRoomB()
        {
            keyManagement = new KeyManagement();
            LanternRefillPos = new Vector2(900, 250);
            lanternRefill = new LanternRefill(LanternRefillPos);
            room1 = new Room1();    
            random = new Random();
            DoorPos = new Vector2(610, 72 * 8);
            uncomStatuePos = new Vector2(536, 0);
            comStatuePos = new Vector2(20000, 0);
            
            hintPos = new Vector2(400, 200);
            //top
            WallArea_Col.Add(new Rectangle(0, 0, 1280, 200));
            //left
            WallArea_Col.Add(new Rectangle(0, 0, 80, 640));
            //right
            WallArea_Col.Add(new Rectangle(1280-80, 0, 80, 640));
            //down(Door)
            WallArea_Col.Add(new Rectangle(430, 610, 413, 15));
            //down(left)
            WallArea_Col.Add(new Rectangle(68, 542-5, 375, 102));
            //down(right)
            WallArea_Col.Add(new Rectangle(843-10, 542-5, 365, 102));

            // Showcase Hitbox
            Hitbox_Showcase.Add(new Rectangle(152, 290, 74, 30));
            Hitbox_Showcase.Add(new Rectangle(262, 394, 74, 30));
            Hitbox_Showcase.Add(new Rectangle(368, 290, 74, 30));
            Hitbox_Showcase.Add(new Rectangle(803, 290, 74, 30));
            Hitbox_Showcase.Add(new Rectangle(914, 395, 74, 30));
            Hitbox_Showcase.Add(new Rectangle(1038, 290, 74, 30));
            //Statue hitbox
            Hitbox_Showcase.Add(new Rectangle(536, 0, 207, 270));
            lanternRefill.IsCollected = false;
        }

        public void LoadContent(ContentManager content)
        {
            Map_Sprite = content.Load<Texture2D>("MapSheet");
            hint = content.Load<Texture2D>("Note");
            TileStatic = content.Load<Texture2D>("MainRoomBSeperate BG");
            Door = content.Load<Texture2D>("placeholderdoor");
            uncompletedStatue = content.Load<Texture2D>("empthyStatue");
            completedStatue = content.Load<Texture2D>("fullStatue");
            
            lanternRefill.LoadSprite(content);
            //Showcase
            for(int i = 1; i < 7; i++)
            { 
                Showcase.Add(content.Load<Texture2D>("Showcase " +  i));
            }  
        }

        public void Draw(SpriteBatch SB, Color roomcolor)
        {
           SB.Draw(TileStatic, Vector2.Zero, roomcolor);
            //SB.Draw(Door, DoorPos, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);

                SB.Draw(uncompletedStatue, uncomStatuePos, Color.White);
                SB.Draw(completedStatue, comStatuePos, Color.White);

            
           lanternRefill.DrawSprite(SB);
           SB.Draw(hint, hintPos, Color.White);
            SB.Draw(Showcase[0], new Vector2(152, 271), Color.White);
            SB.Draw(Showcase[1], new Vector2(262, 394), Color.White);
            SB.Draw(Showcase[2], new Vector2(368, 278), Color.White);
            SB.Draw(Showcase[3], new Vector2(803, 271), Color.White);
            SB.Draw(Showcase[4], new Vector2(914, 395), Color.White);
            SB.Draw(Showcase[5], new Vector2(1038, 272), Color.White);



        }
        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed, DialogueBox dialogue, LanternLight light,SoundSystem sound, Ghost ghost, Staminabar UI)
        {
            keycontrols = Keyboard.GetState();
            //Object Hitbox
            Rectangle statueCollision = new Rectangle(536, 0, 207, 329);
            
            Rectangle hint = new Rectangle((int)hintPos.X, (int)hintPos.Y, 64, 64);

            DoorCollision = new Rectangle((int)DoorPos.X , (int)DoorPos.Y, 200, 64);

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

            for (int i = 0; i < Hitbox_Showcase.Count; i++)
            {
                if (Hitbox_Showcase[i].Intersects(player.collision))
                {
                    if (player.collision.Right >= Hitbox_Showcase[i].Right)
                    {
                        if (player.speed == 2)
                        {
                            player.SelfPosition.X += player.speed * 2;
                        }
                        else
                            player.SelfPosition.X += player.speed;
                    }
                    if (player.collision.Left <= Hitbox_Showcase[i].Left)
                    {
                        if (player.speed == 2)
                        {
                            player.SelfPosition.X -= player.speed * 2;
                        }
                        else
                            player.SelfPosition.X -= player.speed;
                    }
                    if (player.collision.Top >= Hitbox_Showcase[i].Top)
                    {
                        if (player.speed == 2)
                        {
                            player.SelfPosition.Y += player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y += player.speed;
                    }
                    if (player.collision.Bottom <= Hitbox_Showcase[i].Bottom)
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


            if (player.collision.Intersects(DoorCollision))
            {
                //player.StatusTextDisplay("Press K to Interact");
                //Console.WriteLine("Hit");
                
                    ghost.Prechase(player);
                    sound.PlaySfx(1);
                    player.ChangeStartingPosition(new Vector2(player.SelfPosition.X, 240));
                    roomManager.Roomchange(3);
                
            }

            //Piece Collect
            



            //Statue Collision
            if (player.collision.Intersects(statueCollision) == true)
            {
                player.StatusTextDisplay("Press K to Interact");
                if (keycontrols.IsKeyDown(Keys.K) && Oldkey_.IsKeyUp(Keys.K))
                {
                    Console.WriteLine(Keymanager.MRB_StatueActive);
                    if(Keymanager.MRB_Pieces == 3 && keycontrols.IsKeyDown(Keys.K) && Oldkey_.IsKeyUp(Keys.K))
                    {
                        sound.PlaySfx(4);
                        dialogue.SettingParameter("Hint Block", 200, 200, "You assembled the statue!, But something is happening", Color.DarkRed);
                        UI.ChangeObjectiveText("Evade evil spirit", "Hint: Candlelight can slows down the spirit, Find shire to dispel it");
                        dialogue.Activation(true);
                        Keymanager.KeyTrigger("MRB_Statue");
                        player.IsHaunted = true;
                        roomManager.mapcolor = Color.Red;
                        sound.StopBGM();
                        sound.PlayBGM(1);
                    }
                    else
                    {
                        sound.PlaySfx(5);
                        UI.ChangeObjectiveText($"Find pieces for statue {Keymanager.MRB_Pieces}/3", "Hint: Pieces are hidden items");
                        dialogue.SettingParameter("Hint Block", 200, 200, "Find pieces to complete the statue", Color.Purple);
                        dialogue.Activation(true);
                    }
                    Console.WriteLine(Keymanager.MRB_StatueActive); 
                }
            }

            //hint Collision
            if (player.collision.Intersects(hint) == true )
            {
                player.StatusTextDisplay("Press K to Interact");
                if (keycontrols.IsKeyDown(Keys.K) && Oldkey_.IsKeyUp(Keys.K))
                {
                    sound.PlaySfx(3);
                    dialogue.SettingParameter("Hint Block", 200, 200, "Pieces of past scattered among the path you've walked. \n One in the previous room, One in the hall of terror, One at the beginning.", Color.Brown);
                    UI.ChangeObjectiveText($"Find pieces for statue {Keymanager.MRB_Pieces}/3", "Hint: Pieces are hidden items");
                    dialogue.Activation(true);
                }
               
            }

            if(Keymanager.MRB_StatueActive == false)
            {
                uncomStatuePos = new Vector2(536, 0);
                comStatuePos = new Vector2(20000, 0);
            }
            if (Keymanager.MRB_StatueActive == true && player.collision.Intersects(statueCollision) == true && keycontrols.IsKeyDown(Keys.K) && Oldkey_.IsKeyUp(Keys.K))
            {
                uncomStatuePos = new Vector2(20000, 0);
                comStatuePos = new Vector2(536, 0);
            }

            

            lanternRefill.Behavior(player, sound);

            Oldkey_ = keycontrols;
        }
        public void Reset()
        {
            
            lanternRefill.ResetState();
        }
        
    }
}
