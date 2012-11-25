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
        public override void Draw(SpriteBatch sb, Camera camera)
        {
            if (Texture >= 0)
            {
                Rectangle camerarect = new Rectangle(
                    DrawRect.X - camera.Position.X,
                    DrawRect.Y - camera.Position.Y,
                    DrawRect.Width,
                    DrawRect.Height);

                sb.Draw(Assets.StructureText[Texture], camerarect, Tint);
            }
        }
    }
}
