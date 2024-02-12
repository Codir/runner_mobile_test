using CoreGameplay.Controllers;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "RunnerGame/HeroConfig", order = 0)]
    public sealed class HeroConfig : BaseConfig
    {
        public MovementTypes BaseMovementType;
        public LayerMask GroundLayer;
        public float MoveSpeed;
        public float JumpSpeed;
        public float Gravity;
        public float CollectRadius;
        public Vector3 GroundColliderBox;
        public Vector3 GroundColliderOffsetPosition;
    }
}