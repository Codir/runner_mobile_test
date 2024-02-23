using DLib.AttributesSystem;

namespace CoreGameplay.Hero.HeroProperties
{
    public enum MovementTypes
    {
        Run,
        Fly
    }

    [System.Serializable]
    public class MovementTypeAttribute : BaseAttribute
    {
        public MovementTypeAttribute(string value) : base(value)
        {
        }

        public override object GetValue<T>()
        {
            return Value switch
            {
                "Run" => MovementTypes.Run,
                "Fly" => MovementTypes.Fly,
                _ => null
            };
        }
    }
}