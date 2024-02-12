using CoreGameplay.Controllers;
using CoreGameplay.Models;
using UnityEngine;

namespace CoreGameplay.Views
{
    public class FlyPowerUp : BaseCollectibleView
    {
        [SerializeField] private MovementTypes MovementType;
        [SerializeField] private float TimeLeft;

        public override CollectibleEntityModel GetModel()
        {
            return new CollectibleEntityModel()
            {
                MovementType = MovementType,
                TimeLeft = TimeLeft,
            };
        }
    }
}