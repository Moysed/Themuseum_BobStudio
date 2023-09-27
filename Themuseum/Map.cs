using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace Themuseum
{
    public class Map
    {
        private Vector2 mapPosition;
        private Texture2D MapSprite;
        public bool IsActive = false;

        public Map()
        {
            mapPosition = new Vector2(10000, 10000);
        }

        public void LoadSprite(ContentManager Content)
        {
            MapSprite = Content.Load<Texture2D>("MapSheet");
        }

        public void MapActivate()
        {
            mapPosition = new Vector2(320, 0);
        }

        public void MapDeactivate()
        {
            mapPosition = new Vector2(10000, 10000);
        }

        public void DrawMap(SpriteBatch SB)
        {
            SB.Draw(MapSprite, mapPosition, Color.White);
        }
    }
}
