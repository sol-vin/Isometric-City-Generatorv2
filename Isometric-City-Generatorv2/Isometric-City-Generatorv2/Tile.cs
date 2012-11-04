using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isometric_City_Generatorv2
{
    public class Tile : IsometricObject
    {
        public bool Buildable;

        public override void Draw(SpriteBatch sb)
        {
            //Draw both the shadows and buildings here.
            if (Texture >= 0)
            {
                SpriteEffects s;
                if (Flip)
                    s = SpriteEffects.FlipHorizontally;
                else
                    s = SpriteEffects.None;
                   
                sb.Draw(Assets.FloorTiles[Texture], DrawRect, null, Tint, 0f, Vector2.Zero, s, 0f);
            }
        }
    }
}
