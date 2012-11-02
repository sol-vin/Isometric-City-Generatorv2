using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Isometric_City_Generatorv2
{
    public class Building
    {
        public Rectangle DrawRect;
        public int Texture;
        Color Tint;
        public float Layer = 1f;

        public Building(Rectangle drawrect, int texture, Color tint)
        {
            DrawRect = drawrect;
            Texture = texture;
            Tint = tint;
        }

        public void Draw(SpriteBatch sb)
        {
            //Draw both the shadows and buildings here.
            if (Texture != 0)
            {
                sb.Draw(Assets.BuildingText[0], DrawRect, null, Tint, 0f, Vector2.Zero, SpriteEffects.None, Layer);
                sb.Draw(Assets.BuildingShadowsText[0], DrawRect, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, Layer + 0.001f);
            }
        }
    }
}
