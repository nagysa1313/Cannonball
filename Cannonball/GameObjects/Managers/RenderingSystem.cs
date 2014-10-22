using Cannonball.Engine.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cannonball.GameObjects.Managers
{
    class RenderingSystem
    {
        private World world;
        private Renderer renderer;

        public RenderingSystem(World world, Renderer renderer)
        {
            this.world = world;
            this.renderer = renderer;
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var primitive in world.Primitives)
            {
                if (world.Transformations.IndexExists(primitive.Key))
                {
                    var trans = world.Transformations[primitive.Key];
                    renderer.AddPrimitive(primitive.Value.Geometry, Matrix.CreateWorld(trans.Position, trans.Forward, trans.Up));
                }
            }
        }
    }
}