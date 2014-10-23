using Cannonball.GameObjects.Components;
using Cannonball.GameObjects.Managers;
using Cannonball.Engine.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cannonball.Engine.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Cannonball.GameObjects
{
    public class World
    {
        public SparseArray<Entity> Entities;
        public SparseArray<Transformation> Transformations;
        public SparseArray<Primitive> Primitives;
        public SparseArray<Newtonian> Newtonians;
        public SparseArray<Microsoft.Xna.Framework.Graphics.Model> Models;
        public SparseArray<Player> Players;
        public SparseArray<PlayerControl> PlayerControls;

        private NewtonianSystem newtonianSystem;
        private RenderingSystem renderingSystem;
        private NetworkSystem networkSystem;
        private InputManager inputSystem;
        public Game Game { get; private set; }
        public Renderer Renderer { get; private set; }

        public World(Game game, Renderer renderer)
        {
            this.Game = game;
            this.Renderer = renderer;

            Entities = new SparseArray<Entity>();
            Transformations = new SparseArray<Transformation>();
            Primitives = new SparseArray<Primitive>();
            Newtonians = new SparseArray<Newtonian>();
            Models = new SparseArray<Microsoft.Xna.Framework.Graphics.Model>();
            Players = new SparseArray<Player>();
            PlayerControls = new SparseArray<PlayerControl>();

            newtonianSystem = new NewtonianSystem(this);
            renderingSystem = new RenderingSystem(this, renderer);
            networkSystem = new NetworkSystem(this);
            inputSystem = new InputManager(this);
        }

        public void Update(GameTime gameTime)
        {
            inputSystem.Handle(gameTime);

            newtonianSystem.Update(gameTime);

            renderingSystem.Draw(gameTime);

            networkSystem.Update(gameTime);
        }

        // TODO: spawn without new keyword!
        // TODO: cache a few thousand entity record beforehand

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

        // TODO: asset ids instead of SpriteFont and Model references

        public int SpawnPlayer(string name)
        {
            var player = Spawn();
            Transformations[player] = new Transformation();
            Primitives[player] = new Primitive() { Geometry = Cannonball.Engine.Procedural.Objects.Primitives.Cube };
            Players[player] = new Player() { Name = name, Font = Game.Content.Load<SpriteFont>("Fonts/DebugFont") };
            PlayerControls[player] = new PlayerControl() { Controllable = true };
            return player;
        }

        public int SpawnEnemy()
        {
            var enemy = Spawn();
            Transformations[enemy] = new Transformation();
            Primitives[enemy] = new Primitive() { Geometry = Cannonball.Engine.Procedural.Objects.Primitives.Cube };
            return enemy;
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