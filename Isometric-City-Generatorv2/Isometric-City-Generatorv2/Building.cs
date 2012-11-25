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
            Texture = Assets.EMPTY;
            FeatureTexture = Assets.EMPTY;
        }

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

                SpriteEffects fs;
                if (FeatureFlip)
                    fs = SpriteEffects.FlipHorizontally;
                else
                    fs = SpriteEffects.None;

                Rectangle camerarect = new Rectangle(
                    DrawRect.X - camera.Position.X,
                    DrawRect.Y - camera.Position.Y,
                    DrawRect.Width,
                    DrawRect.Height);

                sb.Draw(Assets.BuildingText[Texture], camerarect, null, Tint, 0f, Vector2.Zero, s, 0f);               

                //Draw Features
                if (FeatureTexture != Assets.EMPTY)
                {
                    sb.Draw(Assets.Features[FeatureTexture], camerarect, null, Color.White, 0f, Vector2.Zero, fs, 0f);    
                }
            }
        }
    }
}