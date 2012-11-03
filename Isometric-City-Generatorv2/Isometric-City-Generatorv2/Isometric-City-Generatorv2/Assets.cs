using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isometric_City_Generatorv2
{
    public struct Assets
    {
        public const int BUILDINGHEIGHT = 10;
        public const int SpacingX = 60;
        public const int SpacingY = 250;

        public static Texture2D Grid;

        public static Texture2D[] BuildingText;
        /// 0 = Blank block

        public static Texture2D[] BuildingShadowsText;
        /// 0 = Cube Shading

        public static Texture2D[] Features;
        /// 0 = Door
        /// 1 = Windows

        public static Texture2D[] FloorTiles;
        /// 0 = Grass
        /// 1 = Road
        /// 2 = Road 4-way

        public static Point Tilesize;

        public static Random Random = new Random();

        public static void LoadContent(Game game)
        {
            BuildingShadowsText = new Texture2D[3];
            BuildingText = new Texture2D[3];
            Features = new Texture2D[3];
            FloorTiles = new Texture2D[3];

            Grid = game.Content.Load<Texture2D>(@"floor/grid");

            BuildingText[0] = game.Content.Load<Texture2D>(@"building/building1");

            BuildingShadowsText[0] = game.Content.Load<Texture2D>(@"shading/cubeshadow");

            Features[0] = game.Content.Load<Texture2D>(@"features/door");
            Features[1] = game.Content.Load<Texture2D>(@"features/windows");

            FloorTiles[0] = game.Content.Load<Texture2D>(@"floor/grass");
            FloorTiles[1] = game.Content.Load<Texture2D>(@"floor/road");
            FloorTiles[2] = game.Content.Load<Texture2D>(@"floor/road4way");

            Tilesize = new Point(BuildingText[0].Width, BuildingText[0].Height);
        }

        public static Color RandomColor()
        {
            return new Color(Random.Next(0, 256), Random.Next(0, 256), Random.Next(0, 256));
        }

        public static bool RandomBool()
        {
            int var = Random.Next(0, 2);
            if (var == 0) { return true; }
            else { return false; }
        }
    }
}