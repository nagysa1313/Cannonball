using Cannonball.Engine.Procedural.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cannonball.Engine.GameObjects.Components
{
    public class Primitive
    {
        public GeometricPrimitive Geometry;
        public BoundingSphere BoundingSphere;
        public BoundingBox BoundingBox;
    }
}