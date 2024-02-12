using CoreGameplay.Views;
using UnityEngine;

namespace CoreGameplay.Controllers
{
    //Class with logic of collectibles
    public class CollectiblesPoolController : BasePoolController<BaseCollectibleView>
    {
        public void SpawnCollectibles(Transform[] spawnPositions)
        {
            foreach (var spawnPosition in spawnPositions)
            {
                if (Random.value > 0.5f)
                {
                    var collectible = GetFromPool();
                    collectible.transform.position = spawnPosition.position;
                }
            }
        }
    }
}