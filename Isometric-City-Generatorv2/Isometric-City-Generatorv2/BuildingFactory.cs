using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace Isometric_City_Generatorv2
{
    public class BuildingFactory
    {
        public Building[, ,] Buildings;
        private Random rand = new Random();
        private List<Color> Colors = new List<Color>();

        public BuildingFactory(Tile[,] tiledata)
        {
            Buildings = new Building[tiledata.GetUpperBound(0), tiledata.GetUpperBound(1), Assets.BUILDINGHEIGHT];

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
            Colors.Add(Color.DarkGray);

            AddBuildings(tiledata);
            AddPlants(tiledata);
            AddRoofs();
            AddStructures(tiledata);
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
                        if (tiledata[x, y].Texture == Assets.GRASS)
                        {
                            //Checks to see if it's the bottom one for door placement
                            if (z == 0)
                            {
                                //Makes it empty or not
                                if (Assets.RandomBool())
                                    Buildings[x, y, z].Texture = Assets.EMPTY;
                                else
                                    Buildings[x, y, z].Texture = Assets.BOTTOMBLANKBLOCK;
                                
                                //Door placement
                                if (Buildings[x, y, z].Texture == Assets.BOTTOMBLANKBLOCK)
                                {
                                    if (Assets.RandomBool())
                                        Buildings[x, y, z].FeatureTexture = Assets.EMPTY;
                                    else
                                    {
                                        Buildings[x, y, z].FeatureTexture = Assets.DOOR;

                                        //Checks to see if there is an adjacent road it can flip the door to. If not, then delete the door,
                                        if (x != 0 && tiledata[x - 1, y].Texture == Assets.ROAD)
                                            Buildings[x, y, z].FeatureFlip = true;
                                        else if (y >= tiledata.GetUpperBound(1) && tiledata[x, y + 1].Texture == Assets.ROAD)
                                            Buildings[x, y, z].FeatureFlip = false;
                                        else //If there isn't a road to align to, try making windows instead.
                                        {
                                            if (Assets.RandomBool())
                                                Buildings[x, y, z].FeatureTexture = Assets.Windows[Assets.Random.Next(0, Assets.Windows.Count)];
                                            else
                                                Buildings[x, y, z].FeatureTexture = Assets.EMPTY;
                                        }
                                                                                
                                    }
                                }
                            }

                            if (z != 0 && Buildings[x, y, z - 1].Texture != Assets.EMPTY)
                            {
                                if (Assets.RandomBool())
                                    Buildings[x, y, z].Texture = Assets.EMPTY;
                                else
                                    Buildings[x, y, z].Texture = Assets.BLANKBLOCK;

                                //Generate windows.
                                if (Assets.RandomBool())
                                    Buildings[x, y, z].FeatureTexture = Assets.Windows[Assets.Random.Next(0, Assets.Windows.Count)];
                                else
                                    Buildings[x, y, z].FeatureTexture = Assets.EMPTY;

                            }

                            //If there is no bottom block to build upon, continue through the z loop.
                            if (z != 0 && Buildings[x, y, z].Texture == Assets.EMPTY)
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
                    if (tiledata[x, y].Texture == Assets.GRASS && Buildings[x, y, 0].Texture == Assets.EMPTY)
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
                        if (((z != 0 && z != Buildings.GetUpperBound(2) && Buildings[x, y, z + 1].Texture == Assets.EMPTY) || (z == Buildings.GetUpperBound(2))) && Buildings[x, y, z].Texture == Assets.BLANKBLOCK)
                        {
                            //Get a one
                            if (Assets.Random.Next(0, 5) == 4)
                            {
                                Building b = new Building();
                                b.DrawRect = Buildings[x, y, z].DrawRect;
                                b.Tint = Buildings[x, y, z].Tint;

                                //Assign roof texture
                                //Check to see if the building can be a skyscraper
                                if (z > 3)
                                    b.Texture = Assets.Roofs[Assets.Random.Next(0, Assets.Roofs.Count)];
                                else //IF not, make it a normal roof.
                                    if (Assets.RandomBool())
                                        b.Texture = Assets.ROOF1;
                                    else
                                        b.Texture = Assets.ROOF4;

                                if(b.Texture == Assets.ROOF1)
                                    b.Flip = Assets.RandomBool();                                

                                //Assign Features based on tile used
                                if (b.Texture == Assets.ROOF1)
                                    b.FeatureTexture = Assets.Billboards[Assets.Random.Next(0, Assets.Billboards.Count)];
                                if(b.Texture == Assets.ROOF2)
                                    b.FeatureTexture = Assets.ROOF2LIGHT;
                                if (b.Texture == Assets.ROOF3)
                                    b.FeatureTexture = Assets.EMPTY;
                                if (b.Texture == Assets.ROOF4)
                                    b.FeatureTexture = Assets.WATERTOWER;

                                b.FeatureFlip = b.Flip;

                                Buildings[x, y, z] = b;
                            }

                        }
                    }
                }
            }

            //Sanity Check
            for (int y = 0; y <= Buildings.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= Buildings.GetUpperBound(0); x++)
                {
                    for (int z = 0; z <= Buildings.GetUpperBound(2); z++)
                    {
                        if (Buildings[x, y, z].Texture == Assets.ROOF1)
                        {
                            List<Point> check = CheckFrontNeighbors(new Point(x, y));

                            //Check if the bilboard is facing a wall if it is, flip it the other direction.
                            foreach (Point c in check)
                            {
                                if (Buildings[c.X, c.Y, z].Texture == Assets.BLANKBLOCK)
                                {
                                    if (c.X - 1 == x && Buildings[x, y, z].Flip)
                                    {
                                        Buildings[x, y, z].Flip = false;
                                        Buildings[x, y, z].FeatureFlip = false;
                                    }

                                    if (c.Y + 1 == x && !Buildings[x, y, z].Flip)
                                    {
                                        Buildings[x, y, z].Flip = true;
                                        Buildings[x, y, z].FeatureFlip = true;
                                    }
                                }
                            }

                            //Check if the billboard is next to another bilboard
                            check = CheckNeighbors(new Point(x, y));
                            foreach (Point c in check)
                            {
                                if (Buildings[c.X, c.Y, z].Texture == Assets.ROOF1)
                                {
                                    Buildings[c.X, c.Y, z].Texture = Assets.EMPTY;
                                    Buildings[c.X, c.Y, z].FeatureTexture = Assets.EMPTY;

                                    Buildings[c.X, c.Y, z].Flip = false;
                                    Buildings[c.X, c.Y, z].FeatureFlip = false;
                                }
                            }
                        }
                    }
                }
            }
            
        }

        private void AddStructures(Tile[,] tiledata)
        {
            for (int y = 0; y <= Buildings.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= Buildings.GetUpperBound(0); x++)
                {
                    //If the tile below us is buildable, and there isn't already a building there
                    if (tiledata[x, y].Texture == Assets.GRASS && Buildings[x, y, 0].Texture == Assets.EMPTY)
                    {
                        //One in 400 chance
                        if (Assets.Random.Next(0, 400) == 0)
                        {
                            Structure s = new Structure();
                            //Change the top drawrect to match the structure
                            

                            //Prevent other stuff from thinking it's ok to build here. 
                            for (int z = 0; z < Buildings.GetUpperBound(2); z++)
                            {
                                Buildings[x, y, z].Texture = Assets.EMPTY;
                            }

                            //Search through the structures to find one that is only one block width and height
                            //Randomly select it.
                            do
                            {
                                for (int i = 0; i <= Assets.StructureText.GetUpperBound(0); i++)
                                {
                                    if (Assets.StructureText[i].Width == Assets.Tilesize.X && Assets.RandomBool())
                                    {
                                        s.Texture = i;
                                        s.DrawRect = new Rectangle(Buildings[x, y, 6].DrawRect.X, Buildings[x, y, 6].DrawRect.Y, Assets.StructureText[i].Width, Assets.StructureText[i].Height);
                                    }

                                }
                            } while (s.Texture == Assets.EMPTY);

                            s.Tint = Color.White;

                            Buildings[x, y, 6] = s;
                        }
                    }
                }
        }
            }

        private List<Point> CheckNeighbors(Point check)
        {
            List<Point> answer = new List<Point>();

            if (check.Y != 0)
            {
                answer.Add(new Point(check.X, check.Y - 1));
            }
            if (check.Y != Buildings.GetUpperBound(0))
            {
                answer.Add(new Point(check.X, check.Y + 1));
            }

            if (check.X != 0)
            {
                answer.Add(new Point(check.X - 1, check.Y));
            }
            if (check.X != Buildings.GetUpperBound(1))
            {
                answer.Add(new Point(check.X + 1, check.Y));
            }

            return answer;
        }

        private List<Point> CheckFrontNeighbors(Point check)
        {
            List<Point> answer = new List<Point>();

            if (check.Y != Buildings.GetUpperBound(1))
            {
                answer.Add(new Point(check.X, check.Y + 1));
            }

            if (check.X != 0)
            {
                answer.Add(new Point(check.X - 1, check.Y));
            }

            return answer;
        }
       
    }
}