using DLib.EventBus;

namespace Events
{
    public struct UpdateDistanceEvent : IEvent
    {
        public readonly int Distance;

        public UpdateDistanceEvent(int distance)
        {
            Distance = distance;
        }
    }
}