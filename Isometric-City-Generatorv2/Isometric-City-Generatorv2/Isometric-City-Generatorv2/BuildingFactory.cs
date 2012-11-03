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
            Colors.Add(Color.Red);
            Colors.Add(Color.Orange);
            Colors.Add(Color.Yellow);
            Colors.Add(Color.Green);
            Colors.Add(Color.LightBlue);
            Colors.Add(Color.Blue);
            Colors.Add(Color.Purple);
            Colors.Add(Color.Pink);
            Colors.Add(Color.Crimson);


            for (int y = 0; y < Buildings.GetUpperBound(1); y++)
            {
                for (int x = 0; x < Buildings.GetUpperBound(0); x++)
                {
                    for (int z = 0; z < Buildings.GetUpperBound(2); z++)
                    {
                        Buildings[x, y, z] = new Building();

                        //Make the rectangles;
                        Buildings[x, y, z].DrawRect = new Rectangle(tiledata[x, y].DrawRect.X, tiledata[x, y].DrawRect.Y - ((z + 1) * Assets.BUILDINGHEIGHT - z) + 1, Assets.Tilesize.X, Assets.Tilesize.Y);

                        //Assign a Texturek
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


                        //Assign a Color
                        Buildings[x, y, z].Tint = Color.White;
                        //Debug Z Colors
                        //Buildings[x, y, z].Tint = Colors[z];
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