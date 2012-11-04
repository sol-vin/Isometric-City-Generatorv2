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

            for (int y = 0; y < MaxY; y++)
            {
                for (int x = 0; x < MaxX; x++)
                {
                    TileData[x, y] = new Tile();

                    TileData[x, y].DrawRect = new Rectangle((x * (Assets.Grid.Width / 2) - x - y) + (y * (Assets.Grid.Width / 2) + Assets.SpacingX), (y * (Assets.Grid.Height / 2)) - (x * (Assets.Grid.Height / 2) + y - x) + Assets.SpacingY, Assets.Grid.Width, Assets.Grid.Height);

                    TileData[x, y].Texture = 0;                    

                    TileData[x, y].Tint = Color.White;
                }
            }

            GenerateRoads();
        }

        public void Draw(SpriteBatch sb)
        {
            //Draw grass
            for (int y = 0; y <MaxY; y++)
            {
                for (int x = 0; x < MaxX; x++)
                {
                    if(TileData[x, y].Texture == 0)
                        TileData[x, y].Draw(sb);
                }
            }

            //Draw roads
            for (int y = 0; y <MaxY; y++)
            {
                for (int x = 0; x < MaxX; x++)
                {
                    if(TileData[x, y].Texture == 1)
                        TileData[x, y].Draw(sb);
                }
            }

            //Draw intersections
            for (int y = 0; y <MaxY; y++)
            {
                for (int x = 0; x < MaxX; x++)
                {
                    if(TileData[x, y].Texture == 2)
                        TileData[x, y].Draw(sb);
                }
            }
        }

        private void GenerateRoads()
        {
            Point roadfreq = new Point(MaxX / 5, MaxY / 5);

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
                    if (TileData[randomx, y].Texture == 0)
                    {
                        TileData[randomx, y].Texture = 1;
                        TileData[randomx, y].Flip = true;
                    }
                    else if (TileData[randomx, y].Texture == 1)
                    {
                        TileData[randomx, y].Texture = 2;
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
                    if (TileData[x, randomy].Texture == 0)
                    {
                        TileData[x, randomy].Texture = 1;
                    }
                    else if (TileData[x, randomy].Texture == 1)
                    {
                        TileData[x, randomy].Texture = 2;
                    }
                }
            }
        }       
    }
}
