using Cannonball.Engine.Inputs;
using Cannonball.Engine.Utils.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cannonball.GameObjects.Managers
{
    public class InputManager
    {
        private World world;
        private InputSystem inputSystem;

        public InputManager(World world)
        {
            this.world = world;
            
            inputSystem = new InputSystem(world.Game);
            inputSystem.RegisterKeyReleasedAction(Keys.Escape, () => world.Game.Exit());
            inputSystem.RegisterKeyReleasedAction(Keys.Tab, () =>
                {
                    DiagnosticsManager.Instance.UI.Show();
                });
            inputSystem.RegisterMouseWheelAction(change =>
                {
                    if (change < 0)
                    {
                        // closing to focus point
                        distance /= 2f;
                    }
                    else if (change > 0)
                    {
                        distance *= 2f;
                    }
                });
            inputSystem.RegisterMouseMoveAction((x, y) =>
                {
                    var horizontalAngle = MathHelper.ToRadians((float)-x / 10);
                    var verticalAngle = MathHelper.ToRadians((float)y / 10);
                    this.horizontalAngle += horizontalAngle;
                    this.verticalAngle += verticalAngle;
                });
        }

        private Vector3 prevPosToBeReached;
        private int updateCount = 1;
        private float ticksToCatchUp = 200;
        private float distance = 1.0f;
        private float horizontalAngle = 0;
        private float verticalAngle = 0;

        public void Handle(GameTime gameTime)
        {
            inputSystem.Update(gameTime);

            foreach (var player in world.PlayerControls)
            {
                if (player.Value.Controllable)
                {
                    var trans = world.Transformations[player.Key];

                    // TODO: movement controls



                    // TODO: update camera

                    world.Renderer.Camera.Up = trans.Up;
                    world.Renderer.Camera.Target = trans.Position + trans.Forward * trans.Scale.Z;

                    var posToBeReached = trans.Position + trans.Up * trans.Scale.Y - trans.Forward * trans.Scale.Z * distance;
                    posToBeReached = Vector3.Transform(posToBeReached, Quaternion.CreateFromAxisAngle(trans.Up, horizontalAngle));
                    posToBeReached = Vector3.Transform(posToBeReached, Quaternion.CreateFromAxisAngle(trans.Forward, verticalAngle));

                    //if (posToBeReached != prevPosToBeReached)
                    //{
                    //    updateCount = (int)target.Velocity.LengthSquared() + 1;
                    //}
                    //else 
                    updateCount += (int)(Math.Sqrt(Math.Sqrt(Math.Sqrt(updateCount)))); // power of 1/8

                    var t = Math.Min(Math.Max(((float)updateCount / ticksToCatchUp), 0), 1);
                    world.Renderer.Camera.Position += (posToBeReached - world.Renderer.Camera.Position) * t;

                    prevPosToBeReached = posToBeReached;
                }
            }
        }
    }
}