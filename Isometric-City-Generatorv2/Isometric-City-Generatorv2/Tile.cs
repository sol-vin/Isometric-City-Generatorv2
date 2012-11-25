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
        public const int HEAVY = 0;
        public const int MEDIUMHEAVY = 1;
        public const int MEDIUM = 2;
        public const int LOW = 3;
        /// <summary>
        /// Determines what kind of buildings should be made
        /// </summary>
        public int Influence = -1;

        /// <summary>
        /// Draws the tile.
        /// </summary>
        /// <param name="sb">The SpriteBatch.</param>
        /// <param name="camera">The camera.</param>
        public override void Draw(SpriteBatch sb, Camera camera)
        {
            //Draw both the shadows and buildings here.
            if (Texture >= 0)
            {
                SpriteEffects s;
                if (Flip)
                    s = SpriteEffects.FlipHorizontally;
                else
                    s = SpriteEffects.None;

                Rectangle camerarect = new Rectangle(
                    DrawRect.X - camera.Position.X,
                    DrawRect.Y - camera.Position.Y,
                    DrawRect.Width,
                    DrawRect.Height);
                
                sb.Draw(Assets.FloorTiles[Texture], camerarect, null, Tint, 0f, Vector2.Zero, s, 0f);
            }
        }
    }
}
