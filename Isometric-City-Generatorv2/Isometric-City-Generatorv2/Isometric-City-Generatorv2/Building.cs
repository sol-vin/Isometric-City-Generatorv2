using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isometric_City_Generatorv2
{
    public class Building : IsometricObject
    {
        public bool Door, Roof, Windows;

        public Building()
        {
            Texture = -1;
        }

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

                sb.Draw(Assets.BuildingText[Texture], DrawRect, Tint);                

                //Draw Features
                if (Door)
                {
                    sb.Draw(Assets.Features[0], DrawRect, null, Color.White, 0f, Vector2.Zero, s, 0f);
                }

                if (Windows)
                {
                    sb.Draw(Assets.Features[1], DrawRect, Color.White);
                }

                if (Texture == 1)
                {
                    sb.Draw(Assets.Features[2], DrawRect, Color.White);
                }

                if (Texture == 2)
                {
                    sb.Draw(Assets.Features[3], DrawRect, Color.White);
                }

                //Must be last!
                if (Texture == 0)
                {
                    sb.Draw(Assets.BuildingShadowsText[0], DrawRect, Color.White);
                }

            }
        }
    }
}