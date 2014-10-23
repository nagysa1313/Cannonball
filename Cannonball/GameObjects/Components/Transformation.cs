using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cannonball.GameObjects.Components
{
    public class Transformation
    {
        public Vector3 Position;
        public Vector3 Scale;
        public Vector3 Up;
        public Vector3 Forward;

        public Transformation()
        {
            Position = Vector3.Zero;
            Scale = Vector3.One;
            Up = Vector3.Up;
            Forward = Vector3.Forward;
        }
    }
}