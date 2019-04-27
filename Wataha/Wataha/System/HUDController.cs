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
            
            spriteBatch.Draw(pictures[0], new Vector2(10,5));
            spriteBatch.Draw(pictures[1], new Vector2(18,35));   // meat picture
            spriteBatch.Draw(pictures[2], new Vector2(740, 28));     //goldfangs picture
            spriteBatch.Draw(pictures[3], new Vector2(360, 28));     //whitefangs picture
            spriteBatch.End();

            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;
            device.SamplerStates[0] = SamplerState.LinearWrap;
        }



    }
}
