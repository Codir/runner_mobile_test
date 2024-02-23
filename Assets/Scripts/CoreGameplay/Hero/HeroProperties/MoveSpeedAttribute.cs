using DLib.AttributesSystem;

namespace CoreGameplay.Hero.HeroProperties
{
    [System.Serializable]
    public class MoveSpeedAttribute : BaseAttribute
    {
        public MoveSpeedAttribute(string value) : base(value)
        {
        }
    }
}