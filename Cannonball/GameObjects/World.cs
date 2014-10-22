using Cannonball.GameObjects.Components;
using Cannonball.GameObjects.Managers;
using Cannonball.Engine.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cannonball.Engine.Graphics;

namespace Cannonball.GameObjects
{
    public class World
    {
        public SparseArray<Entity> Entities;
        public SparseArray<Transformation> Transformations;
        public SparseArray<Primitive> Primitives;
        public SparseArray<Newtonian> Newtonians;

        private NewtonianSystem newtonianSystem;
        private RenderingSystem renderingSystem;
        private NetworkSystem networkSystem;
        private InputSystem inputSystem;

        public World(Renderer renderer)
        {
            newtonianSystem = new NewtonianSystem(this);
            renderingSystem = new RenderingSystem(this, renderer);
            networkSystem = new NetworkSystem(this);
            inputSystem = new InputSystem(this);
        }

        public void Update(GameTime gameTime)
        {
            inputSystem.Handle(gameTime);

            newtonianSystem.Update(gameTime);

            renderingSystem.Draw(gameTime);

            networkSystem.Update(gameTime);
        }

        public int Spawn()
        {
            int last = 0;
            foreach (var entity in Entities)
            {
                if (!entity.Value.Active)
                {
                    entity.Value.Active = true;
                    return entity.Key;
                }
                else last = entity.Key;
            }

            var nextKey = last + 1;
            Entities[nextKey] = new Entity();
            return nextKey;
        }

        public void Destroy(int index)
        {
            Entities[index].Active = false;
            Transformations[index] = null;
            Primitives[index] = null;
            Newtonians[index] = null;
        }

        public int GetIndex(string searchCriteria)
        {
            throw new NotImplementedException();
        }

        public int[] GetIndices(string searchCriteria)
        {
            throw new NotImplementedException();
        }
    }
}