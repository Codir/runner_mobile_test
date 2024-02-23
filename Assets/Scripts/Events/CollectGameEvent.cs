using CoreGameplay;
using DLib.EventBus;

namespace Events
{
    public struct CollectGameEvent : IEvent
    {
        public readonly CollectibleItem Item;

        public CollectGameEvent(CollectibleItem item)
        {
            Item = item;
        }
    }
}