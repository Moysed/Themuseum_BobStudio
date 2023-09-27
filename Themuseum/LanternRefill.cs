
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
    public class LanternRefill 
    {
        
        
        public bool IsCollected = false;
        private Vector2 DeployPosition;
        private Vector2 StorePosition;
        public Rectangle Collision;
        private Texture2D unCollected;
        private Texture2D Collected;

        public LanternRefill(Vector2 NewPosition)
        {
            
            DeployPosition = NewPosition;
            StorePosition = DeployPosition;
            Collision = new Rectangle((int)DeployPosition.X, (int) DeployPosition.Y, 44, 50);

        }

        public void LoadSprite(ContentManager Content)
        {
            unCollected = Content.Load<Texture2D>("LightUncollect");
            Collected = Content.Load<Texture2D>("LightCollected");
        }

        public void DrawSprite(SpriteBatch SB)
        {
            if(IsCollected == false)
            {
                SB.Draw(unCollected, DeployPosition, Color.White);
            }
            else if(IsCollected == true)
            {
                SB.Draw(Collected, StorePosition, Color.White);
            }
        }

        public void ChangePosition(Vector2 NewPosition)
        {
            DeployPosition = NewPosition;
            Collision = new Rectangle((int)DeployPosition.X, (int)DeployPosition.Y, 44, 50);
        }

        public void RestorePosition()
        {
            DeployPosition = StorePosition;
            Collision = new Rectangle((int)DeployPosition.X, (int)DeployPosition.Y, 44, 50);
        }

        public Rectangle UpdateCollision()
        {
            return Collision;
        }

        public void ResetState()
        {
            IsCollected = false;
        }
    }
}
