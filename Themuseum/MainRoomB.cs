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
        private Texture2D piece3;
        private Vector2 piece3Pos;
        private Texture2D statue;
        private Vector2 statuePos;
        KeyboardState Oldkey_;
        KeyboardState keycontrols;
        private List<Rectangle> WallArea_Col = new List<Rectangle>();
        Random random;

        public MainRoomB()
        {
            LanternRefillPos = new Vector2(650, 250);
            lanternRefill = new LanternRefill(LanternRefillPos);
            room1 = new Room1();    
            random = new Random();
            DoorPos = new Vector2(610, 72 * 8);
            statuePos = new Vector2(500, 500);
            piece3Pos = new Vector2(random.Next(100,700), random.Next(250,400));
            hintPos = new Vector2(500, 200);
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
            lanternRefill.IsCollected = false;
        }

        public void LoadContent(ContentManager content)
        {
            Map_Sprite = content.Load<Texture2D>("MapSheet");
            hint = content.Load<Texture2D>("Hint");
            TileStatic = content.Load<Texture2D>("Room B bg");
            Door = content.Load<Texture2D>("placeholderdoor");
            statue = content.Load<Texture2D>("Statue");
            piece3 = content.Load<Texture2D>("Piece3");
            lanternRefill.LoadSprite(content);
        }

        public void Draw(SpriteBatch SB)
        {
           SB.Draw(TileStatic, Vector2.Zero, Color.White);
           //SB.Draw(Door, DoorPos, new Rectangle(6 * 32, 8 * 32, 32, 64), Color.White);
           SB.Draw(statue, statuePos, Color.White);
           SB.Draw(piece3, piece3Pos, Color.White);
           lanternRefill.DrawSprite(SB);
           SB.Draw(hint, hintPos, Color.White);
            

        }
        public void Function(GraphicsDeviceManager _graphics, Player player, RoomManager roomManager, KeyManagement Keymanager, float elapsed, DialogueBox dialogue, LanternLight light,SoundSystem sound, Ghost ghost)
        {
            keycontrols = Keyboard.GetState();
            //Object Hitbox
            Rectangle statueCollision = new Rectangle((int)statuePos.X, (int)statuePos.Y, 64, 64);
            Rectangle piece3Col = new Rectangle((int)piece3Pos.X, (int)piece3Pos.Y, 64, 64);
            Rectangle hint = new Rectangle((int)hintPos.X, (int)hintPos.Y, 64, 64);

            DoorCollision = new Rectangle((int)DoorPos.X , (int)DoorPos.Y, 32, 64);

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

                        if ((keycontrols.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E)))
                        {
                            sound.PlaySfx(1);
                            player.ChangeStartingPosition(new Vector2(638-21, 240));
                            roomManager.Roomchange(3);
                        }

                    }
                    if (player.collision.Top >= WallArea_Col[i].Bottom)
                    {
                        if (player.speed == 4)
                        {
                            player.SelfPosition.Y += player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y += player.speed;
                    }
                    if (player.collision.Top >= WallArea_Col[i].Bottom)
                    {
                        if (player.speed == 4)
                        {
                            player.SelfPosition.Y += player.speed * 2;
                        }
                        else
                            player.SelfPosition.Y += player.speed;
                    }
                }
            }
           
            
            if (player.collision.Intersects(DoorCollision))
            {
                player.StatusTextDisplay("Press E to Interact");
                //Console.WriteLine("Hit");
                if (keycontrols.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E))
                {
                    ghost.Prechase(player);
                    sound.PlaySfx(1);
                    player.ChangeStartingPosition(new Vector2(player.SelfPosition.X, 240));
                    roomManager.Roomchange(3);
                }
            }

            //Piece Collect
            if (player.collision.Intersects(piece3Col) == true)
            {
                player.StatusTextDisplay("Press E to Interact");
                if (keycontrols.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E))
                {
                    dialogue.SettingParameter("placeholderblock", 200, 200, "Statue piece collected", Color.Green);
                    dialogue.Activation(true);
                    sound.PlaySfx(0);
                    Keymanager.MRB_Pieces += 1;
                    Console.WriteLine(Keymanager.MRB_Pieces);
                    piece3Pos.X = 5000;
                }
            }

            //Statue Collision
            if (player.collision.Intersects(statueCollision) == true)
            {
                player.StatusTextDisplay("Press E to Interact");
                if (keycontrols.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E))
                {
                    Console.WriteLine(Keymanager.MRB_StatueActive);
                    if(Keymanager.MRB_Pieces == 3)
                    {
                        sound.PlaySfx(4);
                        dialogue.SettingParameter("Hint Block", 200, 200, "You assembled the statue!, But something is happening", Color.DarkRed);
                        dialogue.Activation(true);
                        Keymanager.KeyTrigger("MRB_Statue");
                        player.IsHaunted = true;
                        sound.StopBGM();
                        sound.PlayBGM(1);
                    }
                    else
                    {
                        sound.PlaySfx(5);
                        dialogue.SettingParameter("Hint Block", 200, 200, "Find pieces to complete the statue", Color.Purple);
                        dialogue.Activation(true);
                    }
                    Console.WriteLine(Keymanager.MRB_StatueActive); 
                }
            }

            //hint Collision
            if(player.collision.Intersects(hint) == true )
            {
                player.StatusTextDisplay("Press E to Interact");
                if (keycontrols.IsKeyDown(Keys.E) && Oldkey_.IsKeyUp(Keys.E))
                {
                    sound.PlaySfx(3);
                    dialogue.SettingParameter("Hint Block", 200, 200, "Full Statue", Color.Green);
                    dialogue.Activation(true);
                }
               
            }

            lanternRefill.Behavior(player, sound);

            Oldkey_ = keycontrols;
        }
        public void Reset()
        {
            piece3Pos = new Vector2(random.Next(64, 200), random.Next(100, 200));
            lanternRefill.ResetState();
        }
        
    }
}
