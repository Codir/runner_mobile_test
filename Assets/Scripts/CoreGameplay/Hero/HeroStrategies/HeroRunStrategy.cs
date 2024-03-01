using CoreGameplay.Hero.HeroProperties;
using UnityEngine;

namespace CoreGameplay.Hero.HeroStrategies
{
    public class HeroRunStrategy : IHeroMoveStrategy
    {
        #region public methods

        public Vector2 FixedUpdate(HeroPhysicsModel model, float fixedDeltaTime)
        {
            model.Velocity = model.Rigidbody2D.velocity;
            model.Velocity.x = model.AttributesState.GetValue<float>(typeof(MoveSpeedAttribute)) * fixedDeltaTime;

            return model.Velocity;
        }

        public Vector2 Jump(HeroPhysicsModel model)
        {
            model.Velocity = model.Rigidbody2D.velocity;
            model.Velocity.y += model.AttributesState.GetValue<float>(typeof(JumpSpeedAttribute));

            return model.Velocity;
        }

        #endregion
    }
}