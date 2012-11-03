using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isometric_City_Generatorv2
{
    public class Building
    {
        public Rectangle DrawRect;
        public int Texture;
        public Color Tint;
        public bool FinalBlock, Flip, Bottom, Roof, Windows;

        public Building()
        {
            Texture = -1;
        }

        public void Draw(SpriteBatch sb)
        {
            //Draw both the shadows and buildings here.
            if (Texture >= 0)
            {
                SpriteEffects s;
                if (Flip)
                    s = SpriteEffects.FlipHorizontally;
                else
                    s = SpriteEffects.None;

                sb.Draw(Assets.BuildingText[Texture], DrawRect, Tint);                

                if (Bottom)
                {
                    sb.Draw(Assets.Features[0], DrawRect, null, Color.White, 0f, Vector2.Zero, s, 0f);
                }

                if (Windows)
                {
                    sb.Draw(Assets.Features[1], DrawRect, Color.White);
                }

                //Must be last!
                sb.Draw(Assets.BuildingShadowsText[0], DrawRect, Color.White);

            }
        }
    }
}