using DLib.EventBus;
using DLib.Pools;
using Events;
using UnityEngine;

namespace CoreGameplay.Pools
{
    public sealed class CollectiblesObjectsPool : BaseObjectsPool<CollectibleItem>, IEventListener<CollectGameEvent>
    {
        #region constructor

        public CollectiblesObjectsPool(CollectibleItem[] prefab, Transform container, int poolSize) : base(prefab,
            container,
            poolSize)
        {
            AppController.EventBus.Subscribe(this);
        }

        ~CollectiblesObjectsPool()
        {
            AppController.EventBus.Unsubscribe(this);
        }

        #endregion

        #region public methods

        public void OnTileSpawned(Transform[] spawnPositions)
        {
            foreach (var spawnPosition in spawnPositions)
            {
                if (Random.value > 0.5f)
                {
                    var collectible = Get();
                    collectible.transform.position = spawnPosition.position;
                }
            }
        }

        public void OnEvent(CollectGameEvent gameEvent)
        {
            Return(gameEvent.Item);
        }

        #endregion
    }
}