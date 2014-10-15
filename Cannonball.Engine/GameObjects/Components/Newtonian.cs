using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cannonball.Engine.GameObjects.Components
{
    public class Newtonian
    {
        public Vector3 Velocity;
        public Vector3 AngularVelocity;
        public int Mass;
        public Vector3 CenterOfMass;
    }
}