using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isometric_City_Generatorv2
{
    public class IsometricObject
    {
        public Rectangle DrawRect;
        public int Texture;
        public Color Tint;
        public bool Flip;

        public virtual void Draw(SpriteBatch sb, Camera camera)
        {

        }

    }
}
