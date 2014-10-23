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
            renderer.Clear();

            foreach (var primitive in world.Primitives)
            {
                //if (world.Transformations.IndexExists(primitive.Key))
                //{
                    var trans = world.Transformations[primitive.Key];
                    renderer.AddPrimitive(primitive.Value.Geometry, Matrix.CreateWorld(trans.Position, trans.Forward, trans.Up));
                //}
            }

            foreach (var model in world.Models)
            {
                //if (world.Transformations.IndexExists(model.Key))
                //{
                    var trans = world.Transformations[model.Key];
                    renderer.AddModel(model.Value, Matrix.CreateWorld(trans.Position, trans.Forward, trans.Up));
                //}
            }

            foreach (var player in world.Players)
            {
                //if (world.Transformations.IndexExists(player.Key))
                //{
                    var trans = world.Transformations[player.Key];
                    renderer.AddText(player.Value.Font, player.Value.Name, Matrix.CreateWorld(trans.Position, trans.Forward, trans.Up));
                //}
            }
        }
    }
}