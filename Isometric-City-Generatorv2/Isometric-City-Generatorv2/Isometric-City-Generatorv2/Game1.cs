using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Isometric_City_Generatorv2
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const int MAXHEIGHT = 17;
        const int MAXWIDTH = 17;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle[,] griddata = new Rectangle[MAXWIDTH, MAXHEIGHT];
        BuildingFactory bf;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {


            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Assets.LoadContent(this);
            InitData();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();           

            base.Update(gameTime);
        }
 
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            //Draw Grid
            foreach (Rectangle r in griddata)
            {
                spriteBatch.Draw(Assets.Grid, r, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }

            //Draw Buildings            
            for (int y = 0; y < bf.Buildings.GetUpperBound(1); y++)
            {
                for (int x = 0; x < bf.Buildings.GetUpperBound(0); x++)
                {
                    for (int z = 0; z < bf.Buildings.GetUpperBound(2); z++)
                    {
                        bf.Buildings[x, y, z].Draw(spriteBatch);
                    }
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void InitData()
        {
            //Square generation algorithm
            for (int y = 0; y < griddata.GetUpperBound(1); y++)
            {
                for (int x = 0; x < griddata.GetUpperBound(0); x++)
                {
                    griddata[x, y] = new Rectangle((x * (Assets.Grid.Width / 2) - x - y) + (y * (Assets.Grid.Width / 2) + Assets.SpacingX), (y * (Assets.Grid.Height / 2)) - (x * (Assets.Grid.Height / 2) + y - x) + Assets.SpacingY, Assets.Grid.Width, Assets.Grid.Height);
                }
            }

            bf = new BuildingFactory(griddata);
        }
    }
}
