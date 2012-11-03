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
            Colors.Add(Color.Beige);
            Colors.Add(Color.Bisque);
            Colors.Add(Color.BlanchedAlmond);
            Colors.Add(Color.Khaki);
            Colors.Add(Color.Moccasin);
            Colors.Add(Color.NavajoWhite);
            Colors.Add(Color.Sienna);
            Colors.Add(Color.Tan);
            Colors.Add(Color.Gray);
            Colors.Add(Color.LightGray);

            AddBuildings(tiledata);
            AddPlants(tiledata);
            AddRoofs();
        }

        public void Draw(SpriteBatch sb)
        {
            for (int y = 0; y <= Buildings.GetUpperBound(1); y++)
            {
                for (int x = Buildings.GetUpperBound(0); x >= 0; x--)
                {
                    for (int z = 0; z < Buildings.GetUpperBound(2); z++)
                    {
                        Buildings[x, y, z].Draw(sb);
                    }
                }
            }
        }

        private void AddBuildings(Tile[,] tiledata)
        {
            //Make all the buildings first.
            for (int y = 0; y <= Buildings.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= Buildings.GetUpperBound(0); x++)
                {
                    for (int z = 0; z <= Buildings.GetUpperBound(2); z++)
                    {
                        Buildings[x, y, z] = new Building();
                    }
                }
            }

            //Begin processing and placing the buildings
            for (int y = 0; y <= Buildings.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= Buildings.GetUpperBound(0); x++)
                {
                    //Pulls a random tint from our acceptable colors list to tint the building.
                    Color tint = Colors[Assets.Random.Next(0, Colors.Count)];

                    //This is where the building is erected
                    for (int z = 0; z < Buildings.GetUpperBound(2); z++)
                    {
                        //Make the rectangles;
                        Buildings[x, y, z].DrawRect = new Rectangle(tiledata[x, y].DrawRect.X, tiledata[x, y].DrawRect.Y - ((z + 1) * Assets.BUILDINGHEIGHT - z) + 1, Assets.Tilesize.X, Assets.Tilesize.Y);

                        //Assign a Texture

                        //First check to see if the floor tile below it can be built upon.
                        if (tiledata[x, y].Texture == 0)
                        {
                            //Checks to see if it's the bottom one for door placement
                            if (z == 0)
                            {
                                Buildings[x, y, z].Texture = rand.Next(-1, 1);
                                if (Buildings[x, y, z].Texture == 0)
                                {
                                    Buildings[x, y, z].Door = Assets.RandomBool();
                                    if (Buildings[x, y, z].Door)
                                    {
                                        if (x != 0 && tiledata[x - 1, y].Texture == 1)
                                            Buildings[x, y, z].Flip = true;
                                        else if (y >= tiledata.GetUpperBound(1) && tiledata[x, y + 1].Texture == 1)
                                            Buildings[x, y, z].Flip = false;
                                        else
                                            Buildings[x, y, z].Door = false;
                                    }
                                }
                            }

                            if (z != 0 && Buildings[x, y, z - 1].Texture >= 0)
                            {
                                Buildings[x, y, z].Texture = rand.Next(-1, 1);
                                Buildings[x, y, z].Windows = Assets.RandomBool();
                            }

                            //If there is no bottom block to build upon, continue through the z loop.
                            if (z != 0 && Buildings[x, y, z - 1].Texture == -1)
                            {
                                continue;
                            }
                        }
                        else //Filters out all other tile types that could be below it. According to this, only 0 is considered a buildable block.
                            Buildings[x, y, z].Texture = -1;

                        Buildings[x, y, z].Tint = tint;
                    }
                }
            }
        }

        private void AddPlants(Tile[,] tiledata)
        {
            for (int y = 0; y <= Buildings.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= Buildings.GetUpperBound(0); x++)
                {
                    //If the tile below us is buildable, and there isn't already a building there
                    if (tiledata[x, y].Texture == 0 && Buildings[x, y, 0].Texture == -1)
                    {
                        if (Assets.Random.Next(0, 5) == 0)
                        {
                            Plant tree = new Plant();
                            tree.Texture = rand.Next(0, Assets.Plants.GetUpperBound(0) + 1);
                            tree.Tint = Color.White;
                            tree.DrawRect = Buildings[x, y, 0].DrawRect;
                            Buildings[x, y, 0] = tree;
                        }
                    }
                }
            }
        }

        private void AddRoofs()
        {
            for (int y = 0; y <= Buildings.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= Buildings.GetUpperBound(0); x++)
                {
                    for (int z = 0; z <= Buildings.GetUpperBound(2); z++)
                    {
                        int zdebug = Buildings.GetUpperBound(2);
                        if (((z != 0 && z != zdebug && Buildings[x, y, z + 1].Texture == -1) || (z == zdebug)) && Buildings[x, y, z].Texture == 0)
                        {
                            if (Assets.Random.Next(0, 5) == 4)
                            {
                                Building b = new Building();
                                b.DrawRect = Buildings[x, y, z].DrawRect;
                                b.Tint = Buildings[x, y, z].Tint;
                                b.Flip = Assets.RandomBool();

                                //Assign roof texture
                                if (z > 5)
                                {
                                    b.Texture = Assets.Random.Next(Assets.MINROOFRANGE, Assets.MAXROOFRANGE);
                                }
                                else
                                {
                                    b.Texture = 1;
                                }
                                Buildings[x, y, z] = b;
                            }

                        }
                    }
                }
            }
        }
       
    }
}