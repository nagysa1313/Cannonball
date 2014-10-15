using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cannonball.Engine.GameObjects.Managers
{
    public class NewtonianSystem
    {
        private World world;

        public NewtonianSystem(World world)
        {
            this.world = world;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var newtonian in world.Newtonians)
            {
                var transform = world.Transformations[newtonian.Key];
                if (transform != null)
                {
                    transform.Position += newtonian.Value.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    // TODO: angular velocity
                }
            }

            // TODO: mass, gravitational forces, et cetera
        }
    }
}