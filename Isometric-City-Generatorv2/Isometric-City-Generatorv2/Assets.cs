using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isometric_City_Generatorv2
{
    public class Assets
    {
        public const int BUILDINGHEIGHT = 10;
        public const int SpacingX = 20;
        public const int SpacingY = 250;

        public const int EMPTY = -1;

        public static Texture2D Grid;

        public static Texture2D[] BuildingText = new Texture2D[6];
        public const int BOTTOMBLANKBLOCK = 0;
        public const int BLANKBLOCK = 1;
        public const int ROOF1 = 2;
        public const int ROOF2 = 3;
        public const int ROOF3 = 4;
        public const int ROOF4 = 5;

        public static List<int> Roofs;

        public static Texture2D[] Features = new Texture2D[10];
        public const int DOOR = 0;
        public const int WINDOWS1 = 1;
        public const int WINDOWS2 = 2;
        public const int WINDOWS3 = 3;
        public const int WINDOWS4 = 4;
        public const int BILLBOARD1 = 5;
        public const int BILLBOARD2 = 6;
        public const int BILLBOARD3 = 7;
        public const int ROOF2LIGHT = 8;
        public const int WATERTOWER = 9;

        public static List<int> Windows;
        public static List<int> Billboards;

        public static Texture2D[] FloorTiles = new Texture2D[3];
        public const int GRASS = 0;
        public const int ROAD = 1;
        public const int ROAD4WAY = 2;
        
        public static Texture2D[] Plants = new Texture2D[2];
        public const int OAKTREE = 0;
        public const int FIRTREE = 1;

        public static Texture2D[] StructureText = new Texture2D[2];
        public const int CLOCKTOWER = 0;
        public const int RADIOTOWER = 1;

        public static Point Tilesize;

        public static Random Random = new Random();

        public static void LoadContent(Game game)
        {
            Grid = game.Content.Load<Texture2D>(@"floor/grid");

            BuildingText[BLANKBLOCK] = game.Content.Load<Texture2D>(@"building/blankblock");
            BuildingText[BOTTOMBLANKBLOCK] = game.Content.Load<Texture2D>(@"building/bottomblankblock");
            BuildingText[ROOF1] = game.Content.Load<Texture2D>(@"building/roof1");
            BuildingText[ROOF2] = game.Content.Load<Texture2D>(@"building/roof2");
            BuildingText[ROOF3] = game.Content.Load<Texture2D>(@"building/roof3");
            BuildingText[ROOF4] = game.Content.Load<Texture2D>(@"building/roof4");

            Features[DOOR] = game.Content.Load<Texture2D>(@"features/door");
            Features[WINDOWS1] = game.Content.Load<Texture2D>(@"features/windows1");
            Features[WINDOWS2] = game.Content.Load<Texture2D>(@"features/windows2");
            Features[WINDOWS3] = game.Content.Load<Texture2D>(@"features/windows3");
            Features[WINDOWS4] = game.Content.Load<Texture2D>(@"features/windows4");
            Features[BILLBOARD1] = game.Content.Load<Texture2D>(@"features/roof1billboard1");
            Features[BILLBOARD2] = game.Content.Load<Texture2D>(@"features/roof1billboard2");
            Features[BILLBOARD3] = game.Content.Load<Texture2D>(@"features/roof1billboard3");
            Features[ROOF2LIGHT] = game.Content.Load<Texture2D>(@"features/roof2light");
            Features[WATERTOWER] = game.Content.Load<Texture2D>(@"features/watertower");

            FloorTiles[GRASS] = game.Content.Load<Texture2D>(@"floor/grass");
            FloorTiles[ROAD] = game.Content.Load<Texture2D>(@"floor/road");
            FloorTiles[ROAD4WAY] = game.Content.Load<Texture2D>(@"floor/road4way");

            Plants[OAKTREE] = game.Content.Load<Texture2D>(@"plants/tree");
            Plants[FIRTREE] = game.Content.Load<Texture2D>(@"plants/tree2");

            StructureText[CLOCKTOWER] = game.Content.Load<Texture2D>(@"structures/clocktower");
            StructureText[RADIOTOWER] = game.Content.Load<Texture2D>(@"structures/radiotower");

            //Add to our type collectios
            Roofs = new List<int>();
            Windows = new List<int>();
            Billboards = new List<int>();

            //Add roofs
            Roofs.Add(ROOF1);
            Roofs.Add(ROOF2);
            Roofs.Add(ROOF3);
            Roofs.Add(ROOF4);

            //Add windows
            Windows.Add(WINDOWS1);
            Windows.Add(WINDOWS2);
            Windows.Add(WINDOWS3);
            Windows.Add(WINDOWS4);

            //Add billboards
            Billboards.Add(BILLBOARD1);
            Billboards.Add(BILLBOARD2);
            Billboards.Add(BILLBOARD3);

            Tilesize = new Point(BuildingText[BLANKBLOCK].Width, BuildingText[BLANKBLOCK].Height);
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