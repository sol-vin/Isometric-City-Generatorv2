using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace Isometric_City_Generatorv2
{
    public class BuildingFactory
    {
        public Building[, ,] Buildings;
        public const int MAXBUILDINGHEIGHT = 8;
        private Random rand = new Random();
        private List<Color> Colors = new List<Color>();

        public BuildingFactory(Tile[,] tiledata)
        {
            Buildings = new Building[tiledata.GetUpperBound(0), tiledata.GetUpperBound(1), MAXBUILDINGHEIGHT];

            //Colors
            Colors.Add(Color.Brown);
            Colors.Add(Color.Beige);
            Colors.Add(Color.Bisque);
            Colors.Add(Color.BlanchedAlmond);
            Colors.Add(Color.Khaki);
            Colors.Add(Color.Moccasin);
            Colors.Add(Color.NavajoWhite);
            Colors.Add(Color.Sienna);
            Colors.Add(Color.Tan);


            for (int y = 0; y < Buildings.GetUpperBound(1); y++)
            {
                for (int x = 0; x < Buildings.GetUpperBound(0); x++)
                {
                    Color tint = Colors[Assets.Random.Next(0, Colors.Count)];

                    for (int z = 0; z < Buildings.GetUpperBound(2); z++)
                    {
                        Buildings[x, y, z] = new Building();

                        //Make the rectangles;
                        Buildings[x, y, z].DrawRect = new Rectangle(tiledata[x, y].DrawRect.X, tiledata[x, y].DrawRect.Y - ((z + 1) * Assets.BUILDINGHEIGHT - z) + 1, Assets.Tilesize.X, Assets.Tilesize.Y);

                        //Assign a Texture

                        //First check to see if the floor tile below it can be built upon.
                        if (tiledata[x, y].Texture == 0)
                        {
                            if (z == 0)
                            {
                                Buildings[x, y, z].Texture = rand.Next(-1, 1);
                                if (Buildings[x, y, z].Texture == 0)
                                {
                                    Buildings[x, y, z].Flip = Assets.RandomBool();
                                    Buildings[x, y, z].Bottom = true;
                                }
                            }

                            if (z != 0 && Buildings[x, y, z - 1].Texture >= 0 && !Buildings[x, y, z - 1].FinalBlock)
                            {
                                Buildings[x, y, z].Texture = rand.Next(-1, 1);
                                Buildings[x, y, z].Windows = Assets.RandomBool();
                            }

                            if (z != 0 && Buildings[x, y, z - 1].Texture == -1)
                            {
                                continue;
                            }
                        }
                        else
                            Buildings[x, y, z].Texture = -1;

                        Buildings[x, y, z].Tint = tint;
                    }
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int y = 0; y < Buildings.GetUpperBound(1); y++)
            {
                for (int x = Buildings.GetUpperBound(0) - 1; x >= 0; x--)
                {
                    for (int z = 0; z < Buildings.GetUpperBound(2); z++)
                    {
                        Buildings[x, y, z].Draw(sb);
                    }
                }
            }
        }
       
    }
}