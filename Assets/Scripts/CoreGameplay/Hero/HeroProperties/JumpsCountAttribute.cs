using DLib.AttributesSystem;

namespace CoreGameplay.Hero.HeroProperties
{
    [System.Serializable]
    public class JumpsCountAttribute : BaseAttribute
    {
        public JumpsCountAttribute(string value) : base(value)
        {
        }
    }
}