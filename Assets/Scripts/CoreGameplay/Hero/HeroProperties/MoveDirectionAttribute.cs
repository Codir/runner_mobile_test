using DLib.AttributesSystem;
using UnityEngine;

namespace CoreGameplay.Hero.HeroProperties
{
    [System.Serializable]
    public class MoveDirectionAttribute : BaseAttribute
    {
        public MoveDirectionAttribute(string value) : base(value)
        {
        }
    }
}