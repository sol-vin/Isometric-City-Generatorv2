using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isometric_City_Generatorv2
{
    public class Tile
    {
        public Rectangle DrawRect;
        public int Texture;
        public Color Tint;

        public void Draw(SpriteBatch sb)
        {
            //Draw both the shadows and buildings here.
            if (Texture >= 0)
            {
                sb.Draw(Assets.FloorTiles[Texture], DrawRect, Tint);
            }
        }
    }
}
