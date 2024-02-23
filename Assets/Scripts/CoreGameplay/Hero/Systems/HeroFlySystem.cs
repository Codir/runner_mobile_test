using CoreGameplay.Hero.HeroProperties;
using UnityEngine;

namespace CoreGameplay.Hero.Systems
{
    public class HeroFlySystem : IHeroMoveSystem
    {
        #region public methods

        public Vector2 FixedUpdate(HeroPhysicsModel model, float fixedDeltaTime)
        {
            model.Velocity = model.Rigidbody2D.velocity;
            model.Velocity.x = model.AttributesState.GetValue<float>(typeof(MoveSpeedAttribute)) * fixedDeltaTime;
            model.Velocity.y = model.MouseMove != Vector3.zero
                ? model.MouseMove.y * model.AttributesState.GetValue<float>(typeof(MoveSpeedAttribute)) * fixedDeltaTime
                : 0f;

            return model.Velocity;
        }

        public Vector2 Jump(HeroPhysicsModel model)
        {
            return model.Velocity;
        }

        #endregion
    }
}