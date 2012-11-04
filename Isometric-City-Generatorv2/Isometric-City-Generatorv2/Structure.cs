using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isometric_City_Generatorv2
{
    public class Structure : Building
    {
        public override void Draw(SpriteBatch sb)
        {
            if (Texture >= 0)
            {
                sb.Draw(Assets.StructureText[Texture], DrawRect, Tint);
            }
        }
    }
}
