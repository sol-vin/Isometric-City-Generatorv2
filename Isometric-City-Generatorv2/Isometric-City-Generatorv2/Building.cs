using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isometric_City_Generatorv2
{
    public class Building : IsometricObject
    {
        public int FeatureTexture;
        public bool FeatureFlip;


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

                SpriteEffects fs;
                if (FeatureFlip)
                    fs = SpriteEffects.FlipHorizontally;
                else
                    fs = SpriteEffects.None;

                sb.Draw(Assets.BuildingText[Texture], DrawRect, null, Tint, 0f, Vector2.Zero, s, 0f);               

                //Draw Features
                if (FeatureTexture != -1)
                {
                    sb.Draw(Assets.Features[FeatureTexture], DrawRect, null, Color.White, 0f, Vector2.Zero, fs, 0f);    
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