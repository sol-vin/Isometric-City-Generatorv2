using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InputEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Isometric_City_Generatorv2
{
    public class Camera
    {
        public Point Position;
        public int Speed;
        public Rectangle ViewPort;

        private KeyBoardInput up, down, left, right;

        public Camera(Rectangle ScreenSize)
        {
            up = new KeyBoardInput(Keys.Up);
            down = new KeyBoardInput(Keys.Down);
            left = new KeyBoardInput(Keys.Left);
            right = new KeyBoardInput(Keys.Right);

            Position = Point.Zero;
            ViewPort = ScreenSize;
            Speed = 2;
        }

        public void Update()
        {
            if (up.Down())
                Position.Y -= Speed;
            if (down.Down())
                Position.Y += Speed;
            if (left.Down())
                Position.X -= Speed;
            if (right.Down())
                Position.X += Speed;

            //Update our viewport
            ViewPort.X = Position.X;
            ViewPort.Y = Position.Y;
        }
    }
}
