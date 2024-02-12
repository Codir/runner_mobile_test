using CoreGameplay.Controllers;

namespace CoreGameplay.Models
{
    public class BaseEntityModel
    {
        public MovementTypes MovementType;
        public float MoveSpeed;
        public float JumpSpeed;
        public float Gravity;

        public static BaseEntityModel operator +(BaseEntityModel model1, BaseEntityModel model2)
        {
            model1.MovementType = model2.MovementType != MovementTypes.Run ? model2.MovementType : model1.MovementType;
            model1.MoveSpeed *= model2.MoveSpeed != 0f ? model2.MoveSpeed : 1f;
            model1.JumpSpeed *= model2.JumpSpeed != 0f ? model2.JumpSpeed : 1f;
            model1.Gravity *= model2.Gravity != 0f ? model2.Gravity : 1f;

            return model1;
        }
    }
}