using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Themuseum
{
    public class Screen : Game1
    {
        protected EventHandler ScreenEvent; public Screen(EventHandler theScreenEvent)
        {
            ScreenEvent = theScreenEvent;
        }
        public virtual void Update(GameTime theTime)
        {
        }
        public virtual void Draw(SpriteBatch theBatch)
        {
        }
    }
}
