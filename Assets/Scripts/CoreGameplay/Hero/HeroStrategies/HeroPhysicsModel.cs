using DLib.AttributesSystem;
using UnityEngine;

namespace CoreGameplay.Hero.HeroStrategies
{
    public class HeroPhysicsModel
    {
        public Rigidbody2D Rigidbody2D;
        public AttributesStateModel AttributesState;
        public Vector3 MouseMove;
        public Vector2 Velocity;
        public int UsedJumps;
    }
}