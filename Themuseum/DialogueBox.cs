using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Themuseum
{
    class DialogueBox
    {
        private Texture2D Dialoguebox_Sprite;
        private Texture2D PortraitSprite;
        private string PortraitInput;
        private string DialogueText = "";
        private int PortraitWidth;
        private int PortraitHeight;
        private Vector2 DialogueBoxPos;
        private Vector2 PortraitBoxPos;
        private Color Textcolor;
        private SpriteFont Font;
        private KeyboardState KeyControls;
        private KeyboardState OldKey;
        public bool IsActive = false;

        public DialogueBox(string Input,int Width,int Height)
        {
            PortraitInput = Input;
            PortraitWidth = Width;
            PortraitHeight = Height;
            Textcolor = Color.Black;
            
        }

        public void LoadResource(ContentManager content)
        {
            Dialoguebox_Sprite = content.Load<Texture2D>("placeholderblock");
            Font = content.Load<SpriteFont>("Keycollect");
            PortraitSprite = content.Load<Texture2D>(PortraitInput);
        }

        public void Draw(SpriteBatch SB)
        {
            SB.Draw(Dialoguebox_Sprite,DialogueBoxPos,new Rectangle(0,0,1280,200),Color.White);
            SB.Draw(PortraitSprite,PortraitBoxPos , new Rectangle(0, 0, PortraitWidth, PortraitHeight), Color.White);
            SB.DrawString(Font, DialogueText, new Vector2(DialogueBoxPos.X + 32, DialogueBoxPos.Y + 32),Textcolor);
        }

        public void SettingParameter(string Input,int Width,int height, string displaytext, Color color)
        {
            Textcolor = color;
            if(height > 400)
            {
                height = 400;
            }
            PortraitHeight = height;
            PortraitWidth = Width;
            PortraitInput = Input;
            DialogueText = displaytext;
        }

        public void behavior()
        {
            KeyControls = Keyboard.GetState();
            if(IsActive == true)
            {
                DialogueBoxPos = new Vector2(0, 440);
                PortraitBoxPos = new Vector2(640-(PortraitWidth/2), 40);

                
                if (KeyControls.IsKeyDown(Keys.Escape) && OldKey.IsKeyUp(Keys.Escape))
                {
                    IsActive = false;
                }
                
            }
            else
            {
                DialogueBoxPos = new Vector2(10000, 10000);
                PortraitBoxPos = new Vector2(10000, 10000);
            }

            OldKey = KeyControls;
        }

        public void Activation(bool isShow)
        {
            IsActive = isShow;
        }
    }
}
