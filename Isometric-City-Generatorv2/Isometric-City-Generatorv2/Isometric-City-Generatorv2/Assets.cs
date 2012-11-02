using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Isometric_City_Generatorv2
{
    public struct Assets
    {
        public const int BUILDINGHEIGHT = 10;
        public const int SpacingX = 60;
        public const int SpacingY = 250;

        public static Texture2D Grid;
        public static Texture2D[] BuildingText;
        public static Texture2D[] BuildingShadowsText;
        public static Point Tilesize;

        public static void LoadContent(Game game)
        {
            BuildingText = new Texture2D[3];
            BuildingShadowsText = new Texture2D[3];

            Grid = game.Content.Load<Texture2D>("grid");
            BuildingText[0] = game.Content.Load<Texture2D>("building");
            BuildingShadowsText[0] = game.Content.Load<Texture2D>("buildingshadow");

            Tilesize = new Point(BuildingText[0].Width, BuildingText[0].Height);
        }
    }
}
