using Configs;
using CoreGameplay.Pools;
using DLib;
using UnityEngine;

namespace CoreGameplay
{
    public class LevelController
    {
        #region const

        private const int TILES_POOL = 10;
        private const int MAX_COLLECTIBLES_IN_TILE = 5;

        #endregion

        #region fields

        private readonly LevelTilesObjectsPool _levelTilesPool;
        private readonly CollectiblesObjectsPool _collectiblesPool;
        private readonly float _failHeight;

        #endregion

        #region constructor

        public LevelController(Transform levelContainer)
        {
            var config = ConfigLoader.GetConfig<LevelConfig>();
            _failHeight = config.FailHeight;

            _levelTilesPool = new LevelTilesObjectsPool(config, levelContainer, TILES_POOL);

            var poolSize = config.MaxTilesOnScreen * MAX_COLLECTIBLES_IN_TILE;
            _collectiblesPool = new CollectiblesObjectsPool(config.CollectiblePrefabs, levelContainer, poolSize);
            _levelTilesPool.OnTilesSpawnedEvent += _collectiblesPool.OnTileSpawned;
        }

        #endregion

        #region public methods

        public void OnLoadLevel()
        {
            _levelTilesPool.SpawnStartTiles();
        }

        public void OnUnloadLevel()
        {
            _levelTilesPool.ReturnAll();
            _collectiblesPool.ReturnAll();
        }

        public bool CheckLevelFail(Vector3 heroPosition)
        {
            return heroPosition.y <= _failHeight;
        }

        public void CheckHeroPosition(Vector3 heroPosition)
        {
            _levelTilesPool.CheckHeroPosition(heroPosition);
        }

        #endregion
    }
}