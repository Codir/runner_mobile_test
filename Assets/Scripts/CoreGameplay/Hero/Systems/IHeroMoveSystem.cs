using UnityEngine;

namespace CoreGameplay.Hero.Systems
{
    public interface IHeroMoveSystem
    {
        Vector2 FixedUpdate(HeroPhysicsModel model, float fixedDeltaTime);
        Vector2 Jump(HeroPhysicsModel model);
    }
}