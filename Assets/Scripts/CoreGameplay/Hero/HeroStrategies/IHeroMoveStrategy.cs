using UnityEngine;

namespace CoreGameplay.Hero.HeroStrategies
{
    public interface IHeroMoveStrategy
    {
        Vector2 FixedUpdate(HeroPhysicsModel model, float fixedDeltaTime);
        Vector2 Jump(HeroPhysicsModel model);
    }
}