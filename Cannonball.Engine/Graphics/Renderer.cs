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

        public void Clear()
        {
            foreach (var matrices in primitives.Values)
            {
                matrices.Clear();
            }
        }

        public void Draw(GraphicsDevice device, GameTime gameTime)
        {
            foreach (var primitive in primitives.Keys)
            {
                foreach (var matrix in primitives[primitive])
                {
                    primitive.Draw(matrix, Camera.ViewMatrix, Camera.ProjectionMatrix, Color.Gray);
                }
            }
        }
    }
}