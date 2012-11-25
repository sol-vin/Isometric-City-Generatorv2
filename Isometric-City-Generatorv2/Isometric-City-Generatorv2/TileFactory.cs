using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isometric_City_Generatorv2
{
    public class TileFactory
    {
        public Tile[,] TileData;

        private int MaxX
        {
            get
            {
                return TileData.GetUpperBound(0);
            }
        }
        private int MaxY
        {
            get
            {
                return TileData.GetUpperBound(1);
            }
        }

        public TileFactory(int maxx, int maxy)
        {
            TileData = new Tile[maxx, maxy];

            for (int y = 0; y <= MaxY; y++)
            {
                for (int x = 0; x <= MaxX; x++)
                {
                    TileData[x, y] = new Tile();

                    TileData[x, y].DrawRect = new Rectangle((x * (Assets.Grid.Width / 2) - x - y) + (y * (Assets.Grid.Width / 2) + Assets.SpacingX), (y * (Assets.Grid.Height / 2)) - (x * (Assets.Grid.Height / 2) + y - x) + Assets.SpacingY, Assets.Grid.Width, Assets.Grid.Height);

                    TileData[x, y].Texture = Assets.GRASS;                    

                    TileData[x, y].Tint = Color.White;
                }
            }

            GenerateRoads();
            GenerateInfluence();
        }

        public void Draw(SpriteBatch sb, Camera cam)
        {
            //Draw grass
            for (int y = 0; y <MaxY; y++)
            {
                for (int x = 0; x < MaxX; x++)
                {
                    TileData[x, y].Draw(sb, cam);
                }
            }

            //Draw roads
            for (int y = 0; y <MaxY; y++)
            {
                for (int x = 0; x < MaxX; x++)
                {
                    if (TileData[x, y].Texture == 1 && TileData[x, y].DrawRect.Intersects(cam.ViewPort))
                        TileData[x, y].Draw(sb, cam);
                }
            }

            //Draw intersections
            for (int y = 0; y <MaxY; y++)
            {
                for (int x = 0; x < MaxX; x++)
                {
                    if (TileData[x, y].Texture == 2 && TileData[x, y].DrawRect.Intersects(cam.ViewPort))
                        TileData[x, y].Draw(sb, cam);
                }
            }
        }

        /// <summary>
        /// Generates the roads.
        /// </summary>
        private void GenerateRoads()
        {
            Point roadfreq = new Point(MaxX / 10, MaxY / 10);

            //Used to ensure that the roads don't draw on top of, or next to eachother
            List<int> used = new List<int>();

            for (int x = 0; x < roadfreq.X; x++)
            {
                int randomx;

                do
                {
                    randomx = Assets.Random.Next(1, MaxX - 1);
                } while (used.Contains(randomx) || used.Contains(randomx + 1) || used.Contains(randomx - 1));

                used.Add(randomx);

                //Generate the road line
                for (int y = 0; y < MaxY; y++)
                {
                    if (TileData[randomx, y].Texture == Assets.GRASS)
                    {
                        TileData[randomx, y].Texture = Assets.ROAD;
                        TileData[randomx, y].Flip = true;
                    }
                    else if (TileData[randomx, y].Texture == Assets.ROAD)
                    {
                        TileData[randomx, y].Texture = Assets.ROAD4WAY;
                    }
                }

            }

            //Clear the list
            used = new List<int>();

            for (int y = 0; y < roadfreq.Y; y++)
            {
                int randomy;               

                do
                {
                    randomy = Assets.Random.Next(1, MaxY - 1);
                } while (used.Contains(randomy) || used.Contains(randomy + 1) || used.Contains(randomy - 1));

                used.Add(randomy);

                for (int x = 0; x < MaxX; x++)
                {
                    if (TileData[x, randomy].Texture == Assets.GRASS)
                    {
                        TileData[x, randomy].Texture = Assets.ROAD;
                    }
                    else if (TileData[x, randomy].Texture == Assets.ROAD)
                    {
                        TileData[x, randomy].Texture = Assets.ROAD4WAY;
                    }
                }
            }
        }

        /// <summary>
        /// Generates the tile influence.
        /// </summary>
        private void GenerateInfluence()
        {
            Point midpoint = new Point(MaxX / 2, MaxY / 2);
            Point start = new Point(0,0);
            int width = 0;
            int height = 0;

            double heavy = .4;
            double mediumheavy = .3;
            double medium = .2;
            double low = .1;

            double[] styles = new double[4] { heavy, mediumheavy, medium, low };

            for (int i = 0; i <= styles.GetUpperBound(0); i++)
            {
                double coveragepercent = styles[i];

                //Add all the other squares to the coverage percent.
                if (i != 0)
                {
                    int j = i;
                    do
                    {
                        coveragepercent += styles[j];
                        j--;
                    } while (j != 0);
                }

                //Figure out our squares width and height and the starting point
                              
                width = (int)(MaxX * coveragepercent);
                height = (int)(MaxY * coveragepercent);

                
                start = new Point(midpoint.X - (width / 2), midpoint.Y - (height / 2));

                //Now make all the tiles that are not already taken into the current influence
                for (int x = start.X; x < start.X + width; x++)
                    for (int y = start.Y; y < start.Y + height; y++)
                        if (TileData[x, y].Influence == -1)
                        {
                            TileData[x, y].Influence = i;
                        }                
            }

            for (int x = 0; x <= MaxX; x++)
                for (int y = 0; y <= MaxY; y++)
                    if (TileData[x, y].Influence == -1)
                        TileData[x, y].Influence = 3;
        }
    }
}
