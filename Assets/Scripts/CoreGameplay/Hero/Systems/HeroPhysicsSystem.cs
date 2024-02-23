using CoreGameplay.Hero.HeroProperties;

namespace CoreGameplay.Hero.Systems
{
    public class HeroPhysicsSystem
    {
        #region fields

        private readonly IHeroMoveSystem _heroRunSystem;
        private readonly IHeroMoveSystem _heroFlySystem;
        private readonly HeroAnimationSystem _heroAnimationSystem;

        #endregion

        #region constructor

        public HeroPhysicsSystem(HeroAnimationSystem heroAnimationSystem)
        {
            _heroAnimationSystem = heroAnimationSystem;
            _heroRunSystem = new HeroRunSystem();
            _heroFlySystem = new HeroFlySystem();
        }

        #endregion

        #region public methods

        public void FixedUpdate(HeroPhysicsModel model, float fixedDeltaTime)
        {
            var movementType = model.AttributesState.GetValue<MovementTypes>(typeof(MovementTypeAttribute));
            _heroAnimationSystem.Fly(movementType == MovementTypes.Fly);

            model.Rigidbody2D.velocity = movementType switch
            {
                MovementTypes.Run => _heroRunSystem.FixedUpdate(model, fixedDeltaTime),
                MovementTypes.Fly => _heroFlySystem.FixedUpdate(model, fixedDeltaTime),
                _ => model.Rigidbody2D.velocity
            };

            _heroAnimationSystem.SetSpeed(model.Velocity.x);
        }

        public void OnTap(HeroPhysicsModel model)
        {
            var movementType = model.AttributesState.GetValue<MovementTypes>(typeof(MovementTypeAttribute));
            var jumpsCount = model.AttributesState.GetValue<int>(typeof(JumpsCountAttribute));

            if (movementType == MovementTypes.Run && model.UsedJumps >= jumpsCount) return;

            if (movementType == MovementTypes.Run)
            {
                _heroAnimationSystem.OnJump();
                model.UsedJumps += 1;
            }

            model.Rigidbody2D.velocity = movementType switch
            {
                MovementTypes.Run => _heroRunSystem.Jump(model),
                MovementTypes.Fly => _heroFlySystem.Jump(model),
                _ => model.Rigidbody2D.velocity
            };
        }

        #endregion
    }
}