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
    public class BuildingFactory
    {
        public Building[,,] Buildings;
        public const int MAXBUILDINGHEIGHT = 8;
        Random rand = new Random();

        public BuildingFactory(Rectangle[,] positiondata)
        {
            Buildings = new Building[positiondata.GetUpperBound(0),positiondata.GetUpperBound(1),MAXBUILDINGHEIGHT];

            for (int z = 0; z < Buildings.GetUpperBound(2); z++)
            {
                for (int y = 0; y < Buildings.GetUpperBound(1); y++)
                {
                    for (int x = 0; x < Buildings.GetUpperBound(0); x++)
                    {
                        Rectangle drawrect;
                        int texture = 0;

                        //Make the rectangles;
                        drawrect = new Rectangle(positiondata[x, y].X, positiondata[x, y].Y - ((z + 1) * Assets.BUILDINGHEIGHT - z) + 1, Assets.Tilesize.X, Assets.Tilesize.Y);

                        //Assign a Texture
                        if (z == 0)
                        {
                            texture = rand.Next(0, 2);
                        }
                        if (z != 0 && Buildings[x, y, z - 1].Texture != 0)
                        {
                            texture = rand.Next(0, 2);
                        }

                        Buildings[x, y, z] = new Building(drawrect, texture, RandomColor());
                    }
                }
            }
        }

        private Color RandomColor()
        {
            return new Color(rand.Next(0,256),rand.Next(0,256),rand.Next(0,256));
        }
    }
}
