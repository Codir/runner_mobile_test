using CoreGameplay.Hero.HeroProperties;

namespace CoreGameplay.Hero.HeroStrategies
{
    public class HeroPhysicsStrategy
    {
        #region fields

        private readonly IHeroMoveStrategy _heroRunStrategy;
        private readonly IHeroMoveStrategy _heroFlyStrategy;
        private readonly HeroAnimationController _heroAnimationController;

        #endregion

        #region constructor

        public HeroPhysicsStrategy(HeroAnimationController heroAnimationController)
        {
            _heroAnimationController = heroAnimationController;
            _heroRunStrategy = new HeroRunStrategy();
            _heroFlyStrategy = new HeroFlyStrategy();
        }

        #endregion

        #region public methods

        public void FixedUpdate(HeroPhysicsModel model, float fixedDeltaTime)
        {
            var movementType = model.AttributesState.GetValue<MovementTypes>(typeof(MovementTypeAttribute));
            _heroAnimationController.Fly(movementType == MovementTypes.Fly);

            model.Rigidbody2D.velocity = movementType switch
            {
                MovementTypes.Run => _heroRunStrategy.FixedUpdate(model, fixedDeltaTime),
                MovementTypes.Fly => _heroFlyStrategy.FixedUpdate(model, fixedDeltaTime),
                _ => model.Rigidbody2D.velocity
            };

            _heroAnimationController.SetSpeed(model.Velocity.x);
        }

        public void OnTap(HeroPhysicsModel model)
        {
            var movementType = model.AttributesState.GetValue<MovementTypes>(typeof(MovementTypeAttribute));
            var jumpsCount = model.AttributesState.GetValue<int>(typeof(JumpsCountAttribute));

            if (movementType == MovementTypes.Run && model.UsedJumps >= jumpsCount) return;

            if (movementType == MovementTypes.Run)
            {
                _heroAnimationController.OnJump();
                model.UsedJumps += 1;
            }

            model.Rigidbody2D.velocity = movementType switch
            {
                MovementTypes.Run => _heroRunStrategy.Jump(model),
                MovementTypes.Fly => _heroFlyStrategy.Jump(model),
                _ => model.Rigidbody2D.velocity
            };
        }

        #endregion
    }
}