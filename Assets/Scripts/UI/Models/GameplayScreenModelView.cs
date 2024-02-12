using System;

namespace UI.Models
{
    public class GameplayScreenModelView : IScreenModelView
    {
        public event Action<float> DistanceChangedEvent;

        public float Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                if (DistanceChangedEvent is { Target: { } })
                {
                    DistanceChangedEvent?.Invoke(_distance);
                }
            }
        }

        private float _distance;
    }
}