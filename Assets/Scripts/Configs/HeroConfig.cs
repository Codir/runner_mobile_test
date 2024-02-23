using CoreGameplay.Hero.HeroProperties;
using DLib.AttributesSystem;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "RunnerGame/HeroConfig")]
    public sealed class HeroConfig : ScriptableObject
    {
        public Vector3 HeroSpawnPosition;
        public float HeroAnimationSpeed;
        public LayerMask GroundLayer;
        public HeroPropertyAttribute[] PropertyItems;
    }
}