using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InputEngine.Input;
using System.Collections.Generic;

namespace Isometric_City_Generatorv2
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private Camera camera;
        private KeyBoardInput randomize;

        public Rectangle ActualScreen = new Rectangle(0, 0, 800, 500);

        private const int MAXHEIGHT = 500; 
        private const int MAXWIDTH = 500;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Rectangle[,] griddata = new Rectangle[MAXWIDTH, MAXHEIGHT];

        private BuildingFactory bf;
        private TileFactory tf;

        private Texture2D rendertexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Components.Add(new InputHandler(this));
            randomize = new KeyBoardInput(Keys.Space);
            camera = new Camera(ActualScreen);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            MakeWindow();
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
            if (randomize.Pressed())
            {
                tf = new TileFactory(MAXWIDTH, MAXHEIGHT);
                bf = new BuildingFactory(tf.TileData);
            }
            camera.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            tf.Draw(spriteBatch, camera);
            bf.Draw(spriteBatch, camera);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void InitData()
        {
            tf = new TileFactory(MAXWIDTH, MAXHEIGHT);
            bf = new BuildingFactory(tf.TileData);
        }

        private void MakeWindow()
        {
            if ((ActualScreen.Width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width) && (ActualScreen.Height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
            {
                graphics.PreferredBackBufferWidth = ActualScreen.Width;
                graphics.PreferredBackBufferHeight = ActualScreen.Height;
                graphics.IsFullScreen = false;
                graphics.ApplyChanges();
                return;
            }

            return;
        }
    }
}