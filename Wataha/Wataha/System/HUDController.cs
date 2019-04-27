using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.System
{
    public class HUDController
    {
        public int meat;
        public int white_fangs;
        public int gold_fangs;

        public SpriteFont font30;
        public List<Texture2D> pictures;

        public HUDController(int meat, int white_fangs, int gold_fangs)
        {
            this.meat = meat;
            this.white_fangs = white_fangs;
            this.gold_fangs = gold_fangs;
            pictures = new List<Texture2D>();
        }


        public void  Draw(SpriteBatch spriteBatch,GraphicsDevice device)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            
            spriteBatch.Draw(pictures[0], new Vector2(10,5));   // rectangle to display resources


            spriteBatch.Draw(pictures[1], new Vector2(15,35));   // meat picture
            spriteBatch.DrawString(font30, meat.ToString(), new Vector2(130, 33), Color.Red);

            spriteBatch.Draw(pictures[3], new Vector2(250, 28));     //whitefangs picture
            spriteBatch.DrawString(font30, white_fangs.ToString(), new Vector2(410, 33), Color.White);

            spriteBatch.Draw(pictures[2], new Vector2(530, 28));     //goldfangs picture
            spriteBatch.DrawString(font30, gold_fangs.ToString(), new Vector2(680, 33), Color.Gold);

            
            spriteBatch.End();

            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;
            device.SamplerStates[0] = SamplerState.LinearWrap;
        }



    }
}
