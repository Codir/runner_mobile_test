using DLib.AttributesSystem;

namespace CoreGameplay.Hero.HeroProperties
{
    [System.Serializable]
    public class JumpSpeedAttribute : BaseAttribute
    {
        public JumpSpeedAttribute(string value) : base(value)
        {
        }
    }
}