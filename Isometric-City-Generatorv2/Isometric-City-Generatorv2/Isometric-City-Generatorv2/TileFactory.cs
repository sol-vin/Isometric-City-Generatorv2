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

        public TileFactory(int maxx, int maxy)
        {
            TileData = new Tile[maxx, maxy];

            for (int y = 0; y < TileData.GetUpperBound(1); y++)
            {
                for (int x = 0; x < TileData.GetUpperBound(0); x++)
                {
                    TileData[x, y] = new Tile();

                    TileData[x, y].DrawRect = new Rectangle((x * (Assets.Grid.Width / 2) - x - y) + (y * (Assets.Grid.Width / 2) + Assets.SpacingX), (y * (Assets.Grid.Height / 2)) - (x * (Assets.Grid.Height / 2) + y - x) + Assets.SpacingY, Assets.Grid.Width, Assets.Grid.Height);

                    TileData[x, y].Texture = Assets.Random.Next(0, 2);

                    TileData[x, y].Tint = Color.White;
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int y = 0; y < TileData.GetUpperBound(1); y++)
            {
                for (int x = 0; x < TileData.GetUpperBound(0); x++)
                {
                    TileData[x, y].Draw(sb);
                }
            }
        }
    }
}
