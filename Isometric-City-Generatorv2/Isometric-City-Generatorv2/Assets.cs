using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isometric_City_Generatorv2
{
    public struct Assets
    {
        public const int BUILDINGHEIGHT = 10;
        public const int SpacingX = 20;
        public const int SpacingY = 250;
        public const int MINROOFRANGE = 1;
        public const int MAXROOFRANGE = 4;
        public const int MINBILLBOARDRANGE = 2;
        public const int MAXBILLBOARDRANGE = 5;

        public static Texture2D Grid;

        public static Texture2D[] BuildingText;
        /// 0 = Blank block
        /// 1 = Roof1
        /// 2 = Roof2
        /// 3 = Roof3

        public static Texture2D[] BuildingShadowsText;
        /// 0 = Cube Shading

        public static Texture2D[] Features;
        /// 0 = Door
        /// 1 = Windows
        /// 2 = Roof 1 - Billboard 1
        /// 3 = Roof 1 - Billboard 2
        /// 4 = Roof 1 - Billboard 3
        /// 3 = Roof 2 - Light

        public static Texture2D[] FloorTiles;
        /// 0 = Grass
        /// 1 = Road
        /// 2 = Road 4-way
        
        public static Texture2D[] Plants;
        /// 0 = Tree

        public static Texture2D[] StructureText;
        /// 1 = Clocktower
        
        public static Texture2D[,] StructureShadingText;

        public static Point Tilesize;

        public static Random Random = new Random();

        public static void LoadContent(Game game)
        {
            BuildingShadowsText = new Texture2D[1];
            BuildingText = new Texture2D[4];
            Features = new Texture2D[6];
            FloorTiles = new Texture2D[3];
            Plants = new Texture2D[2];

            StructureText = new Texture2D[1];

            Grid = game.Content.Load<Texture2D>(@"floor/grid");

            BuildingText[0] = game.Content.Load<Texture2D>(@"building/building");
            BuildingText[1] = game.Content.Load<Texture2D>(@"building/roof1");
            BuildingText[2] = game.Content.Load<Texture2D>(@"building/roof2");
            BuildingText[3] = game.Content.Load<Texture2D>(@"building/roof3");

            BuildingShadowsText[0] = game.Content.Load<Texture2D>(@"shading/cubeshadow");

            Features[0] = game.Content.Load<Texture2D>(@"features/door");
            Features[1] = game.Content.Load<Texture2D>(@"features/windows");
            Features[2] = game.Content.Load<Texture2D>(@"features/roof1billboard1");
            Features[3] = game.Content.Load<Texture2D>(@"features/roof1billboard2");
            Features[4] = game.Content.Load<Texture2D>(@"features/roof1billboard3");
            Features[5] = game.Content.Load<Texture2D>(@"features/roof2light");

            FloorTiles[0] = game.Content.Load<Texture2D>(@"floor/grass");
            FloorTiles[1] = game.Content.Load<Texture2D>(@"floor/road");
            FloorTiles[2] = game.Content.Load<Texture2D>(@"floor/road4way");

            Plants[0] = game.Content.Load<Texture2D>(@"plants/tree");
            Plants[1] = game.Content.Load<Texture2D>(@"plants/tree2");

            StructureText[0] = game.Content.Load<Texture2D>(@"structures/clocktower/clocktower");

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