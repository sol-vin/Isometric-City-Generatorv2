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
            AddFeatures(tiledata);
            AddPlants(tiledata);
            AddRoofs();
            AddStructures(tiledata);
        }

        public void Draw(SpriteBatch sb, Camera cam)
        {
            for (int y = 0; y <= Buildings.GetUpperBound(1); y++)
            {
                for (int x = Buildings.GetUpperBound(0); x >= 0; x--)
                {
                    for (int z = 0; z < Buildings.GetUpperBound(2); z++)
                    {
                        Buildings[x, y, z].Draw(sb, cam);
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
                        Buildings[x, y, z].DrawRect = new Rectangle(tiledata[x, y].DrawRect.X, tiledata[x, y].DrawRect.Y - ((z + 1) * Assets.BUILDINGHEIGHT - z) + 1, Assets.Tilesize.X, Assets.Tilesize.Y);
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
                int buildingheight = 0;

                    //Assign a Texture
                    //First check to see if the floor tile below it can be built upon.
                    if (tiledata[x, y].Texture == Assets.GRASS)
                    {               
                        //Check the tile influence and set the building height accoridingly.
                        if (tiledata[x, y].Influence == Tile.HEAVY)
                        {
                            //Spawn a building with an 80% chance.
                            if (Assets.Random.Next(0, 10) < 4)
                            {
                                //80% chance to make a 6-8 tall building
                                //20% chance to make a 1-5 tall building
                                if (Assets.Random.Next(0, 10) < 8)
                                    buildingheight = Assets.Random.Next(4, 6);
                                else
                                    buildingheight = Assets.Random.Next(1, 4);
                            }
                        }

                        else if (tiledata[x, y].Influence == Tile.MEDIUMHEAVY)
                        {
                            //Spawn a building with an 80% chance.
                            if (Assets.Random.Next(0, 10) < 3)
                            {
                                //80% chance to make a 6-8 tall building
                                //20% chance to make a 1-5 tall building
                                if (Assets.Random.Next(0, 10) < 6)
                                    buildingheight = Assets.Random.Next(3, 4);
                                else
                                    buildingheight = Assets.Random.Next(1, 3);
                            }
                        }

                        else if (tiledata[x, y].Influence == Tile.MEDIUM)
                        {
                            //Spawn a building with an 80% chance.
                            if (Assets.Random.Next(0, 10) < 2)
                            {
                                //80% chance to make a 6-8 tall building
                                //20% chance to make a 1-5 tall building
                                if (Assets.Random.Next(0, 10) < 5)
                                    buildingheight = Assets.Random.Next(2, 3);
                                else
                                    buildingheight = Assets.Random.Next(1, 2);
                            }
                        }

                        else if (tiledata[x, y].Influence == Tile.LOW)
                        {
                            //Spawn a building with an 80% chance.
                            if (Assets.Random.Next(0, 10) < 1)
                            {
                                //80% chance to make a 6-8 tall building
                                //20% chance to make a 1-5 tall building
                                if (Assets.Random.Next(0, 10) < 5)
                                    buildingheight = Assets.Random.Next(1, 2);
                                else
                                    buildingheight = 1;
                            }
                        }
                        else
                        {
                            for (int z = 0; z < Assets.BUILDINGHEIGHT; z++)
                            {
                                Buildings[x, y, z].Texture = Assets.BLANKBLOCK;
                                Buildings[x, y, z].Tint = Color.Red;
                            }
                        }

                        //Build our building
                        if (buildingheight != 0)
                            for (int z = 0; z < buildingheight; z++)
                            {
                                if (z == 0)
                                    Buildings[x, y, z].Texture = Assets.BOTTOMBLANKBLOCK;
                                else
                                    Buildings[x, y, z].Texture = Assets.BLANKBLOCK;
                                Buildings[x, y, z].Tint = tint;
                            }                        
                    }
                }
            }
        }

        private void AddFeatures(Tile[,] tiledata)
        {
            for (int y = 0; y <= Buildings.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= Buildings.GetUpperBound(0); x++)
                {
                    //Generate Doors
                    if (Buildings[x, y, 0].Texture == Assets.BOTTOMBLANKBLOCK)
                    {
                        if (Assets.RandomBool())
                            Buildings[x, y, 0].FeatureTexture = Assets.EMPTY;
                        else
                        {
                            Buildings[x, y, 0].FeatureTexture = Assets.DOOR;

                            //Checks to see if there is an adjacent road it can flip the door to. If not, then delete the door,
                            if (x != 0 && tiledata[x - 1, y].Texture == Assets.ROAD)
                                Buildings[x, y, 0].FeatureFlip = true;
                            else if (y >= tiledata.GetUpperBound(1) && tiledata[x, y + 1].Texture == Assets.ROAD)
                                Buildings[x, y, 0].FeatureFlip = false;
                            else //If there isn't a road to align to, try making windows instead.
                            {
                                if (Assets.RandomBool())
                                    Buildings[x, y, 0].FeatureTexture = Assets.Windows[Assets.Random.Next(0, Assets.Windows.Count)];
                                else
                                    Buildings[x, y, 0].FeatureTexture = Assets.EMPTY;
                            }

                        }
                    }
                    for (int z = 0; z < Assets.BUILDINGHEIGHT; z++ )
                    {
                        if (Buildings[x, y, z].Texture == Assets.BLANKBLOCK && Buildings[x, y, z].FeatureTexture == Assets.EMPTY)
                        {
                            //Generate Windows
                            if (Assets.RandomBool())
                                Buildings[x, y, z].FeatureTexture = Assets.EMPTY;
                            else
                            {
                                Buildings[x, y, z].FeatureTexture = Assets.Windows[Assets.Random.Next(0, Assets.Windows.Count)];
                            }
                        }
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
                        Point buildchance = Point.Zero;
                        int texturechance = 0;

                        if (tiledata[x, y].Influence == Tile.HEAVY)
                        {
                            buildchance = new Point(1,1000);
                            texturechance = 8;
                        }
                        else if (tiledata[x, y].Influence == Tile.MEDIUMHEAVY)
                        {
                            buildchance = new Point(1,1500);
                            texturechance = 5;
                        }
                        else if (tiledata[x, y].Influence == Tile.MEDIUM)
                        {
                            buildchance = new Point(1, 170);
                            texturechance = 3;
                        }
                        else if (tiledata[x, y].Influence == Tile.LOW)
                        {
                            buildchance = new Point(1,2000);
                            texturechance = 0;
                        }

                        //Roll the dice to see if a structure should be built.
                        if (Assets.Random.Next(0, buildchance.Y) == buildchance.X)
                        {
                            Structure s = new Structure();

                            //Prevent other stuff from thinking it's ok to build here. 
                            for (int z = 0; z < Buildings.GetUpperBound(2); z++)
                            {
                                Buildings[x, y, z].Texture = Assets.EMPTY;
                            }

                            if (Assets.Random.Next(1, 10) <= texturechance)
                                s.Texture = Assets.CLOCKTOWER;
                            else
                                s.Texture = Assets.RADIOTOWER;

                            s.DrawRect = new Rectangle(
                                Buildings[x, y, 6].DrawRect.X, 
                                Buildings[x, y, 6].DrawRect.Y, 
                                Assets.StructureText[s.Texture].Width, 
                                Assets.StructureText[s.Texture].Height);

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