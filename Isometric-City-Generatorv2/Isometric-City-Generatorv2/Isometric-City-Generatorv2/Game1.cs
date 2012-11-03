using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Isometric_City_Generatorv2
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private const int MAXHEIGHT = 35;
        private const int MAXWIDTH = 35;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Rectangle[,] griddata = new Rectangle[MAXWIDTH, MAXHEIGHT];

        private BuildingFactory bf;
        private TileFactory tf;

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
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                bf = new BuildingFactory(tf.TileData);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

            tf.Draw(spriteBatch);
            bf.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void InitData()
        {
            tf = new TileFactory(MAXWIDTH, MAXHEIGHT);
            bf = new BuildingFactory(tf.TileData);
        }
    }
}