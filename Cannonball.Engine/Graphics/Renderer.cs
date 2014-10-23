using Cannonball.Engine.GameObjects;
using Cannonball.Engine.Graphics.Camera;
using Cannonball.Engine.Procedural.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cannonball.Engine.Graphics
{
    public class Renderer
    {
        public ICamera Camera { get; set; }

        private readonly Dictionary<GeometricPrimitive, List<Matrix>> primitives = new Dictionary<GeometricPrimitive, List<Matrix>>();
        private readonly Dictionary<Model, List<Matrix>> models = new Dictionary<Model, List<Matrix>>();
        private readonly Dictionary<SpriteFont, List<Tuple<Matrix, string>>> texts = new Dictionary<SpriteFont, List<Tuple<Matrix, string>>>();

        public void AddPrimitive(GeometricPrimitive prim, Matrix world)
        {
            List<Matrix> matrices;
            if (!primitives.TryGetValue(prim, out matrices))
            {
                matrices = new List<Matrix>();
                primitives.Add(prim, matrices);
            }

            matrices.Add(world);
        }

        public void AddModel(Model model, Matrix world)
        {
            List<Matrix> matrices;
            if (!models.TryGetValue(model, out matrices))
            {
                matrices = new List<Matrix>();
                models.Add(model, matrices);
            }

            matrices.Add(world);
        }

        public void AddText(SpriteFont font, string text, Matrix world)
        {
            List<Tuple<Matrix, string>> tuples;
            if (!texts.TryGetValue(font, out tuples))
            {
                tuples = new List<Tuple<Matrix, string>>();
                texts.Add(font, tuples);
            }

            tuples.Add(new Tuple<Matrix, string>(world, text));
        }

        public void Clear()
        {
            foreach (var matrices in primitives.Values)
            {
                matrices.Clear();
            }
            foreach (var matrices in models.Values)
            {
                matrices.Clear();
            }
            foreach (var tuples in texts.Values)
            {
                tuples.Clear();
            }
        }

        public void Draw(GraphicsDevice device, GameTime gameTime)
        {
            device.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            device.Clear(Color.Black);

            foreach (var primitive in primitives.Keys)
            {
                foreach (var matrix in primitives[primitive])
                {
                    // TODO: instancing
                    primitive.Draw(matrix, Camera.ViewMatrix, Camera.ProjectionMatrix, Color.White);
                }
            }

            foreach (var model in models.Keys)
            {
                foreach (var matrix in models[model])
                {
                    // TODO: instancing
                    model.Draw(matrix, Camera.ViewMatrix, Camera.ProjectionMatrix);
                }
            }

            // TODO: texts (billboarding player names to world)
        }
    }
}