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
    
    class PuzzleBlock
    {
        private Texture2D Sprite;
        private bool isvisible = true;
        private Vector2 SelfPosition;
        private Vector2 OriginalPosition;
        public Rectangle Collision;
        public string KeyDesignation = "";
        private string spritestring = "";
        public PuzzleBlock(Vector2 startingposition, string DefineID,string Imagestring)
        {
            SelfPosition = startingposition;
            OriginalPosition = SelfPosition;
            KeyDesignation = DefineID;
            spritestring = Imagestring;
            
        }

        public void LoadSprite(ContentManager content)
        {
            Sprite = content.Load<Texture2D>(spritestring);
        }

        public void Draw(SpriteBatch SB) 
        {
            if(isvisible == true)
            {
                SB.Draw(Sprite, SelfPosition, Color.White);
            }
           
        }

        public void Behavior(Player player, float elapsed)
        {
            Collision = new Rectangle((int)SelfPosition.X,(int)SelfPosition.Y,Sprite.Width,Sprite.Height);

            if (player.collision.Intersects(Collision) == true && isvisible == true)
            {
                if (player.collision.Right >= Collision.Right)
                {
                    if (player.speed == 4)
                    {
                        player.SelfPosition.X += player.speed * 2;
                        SelfPosition.X -= player.speed * 2;
                    }
                    else
                        player.SelfPosition.X += player.speed;
                        SelfPosition.X -= player.speed;
                }
                if (player.collision.Left <= Collision.Left)
                {
                    if (player.speed == 4)
                    {
                        player.SelfPosition.X -= player.speed * 2;
                        SelfPosition.X += player.speed * 2;
                    }
                    else
                        player.SelfPosition.X -= player.speed;
                        SelfPosition.X += player.speed;
                }
                if(player.collision.Top >= Collision.Top)
                {
                    if (player.speed == 4)
                    {
                        player.SelfPosition.Y += player.speed * 2;
                        SelfPosition.Y -= player.speed * 2;
                    }
                    else
                        player.SelfPosition.Y += player.speed;
                        SelfPosition.Y -= player.speed;
                }
                if(player.collision.Bottom <= Collision.Bottom)
                {
                    if (player.speed == 4)
                    {

                        player.SelfPosition.Y -= player.speed * 2;
                        SelfPosition.Y += player.speed * 2;
                    }
                    else
                        player.SelfPosition.Y -= player.speed;
                        SelfPosition.Y += player.speed;

                }

            }
            
        }
        
        public void setvisible(bool status)
        {
            isvisible = status;
        }
        public void ResetPosition()
        {
            SelfPosition = OriginalPosition;
            isvisible = true;
        }

        
    }
}
