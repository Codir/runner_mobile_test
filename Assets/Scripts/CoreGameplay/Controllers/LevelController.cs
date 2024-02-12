using Configs;
using CoreGameplay.Views;
using UI;
using UnityEngine;

namespace CoreGameplay.Controllers
{
    //Class with logic of construction level
    public class LevelController : BaseControllerWithConfig<LevelConfig>
    {
        private const int TILES_POOL = 10;
        private const int COLLECTIBLES_POOL = 10;

        private Transform _heroView;

        private CollectiblesPoolController _collectiblesPoolController;
        private TilingPoolController _tilingPoolController;

        private Transform _levelContainer;

        public void Init(Transform heroView, Transform levelContainer)
        {
            _levelContainer = levelContainer;

            _heroView = heroView;

            _tilingPoolController = new TilingPoolController(Config.MaxTilesOnScreen, Config.TileLenght);
            _tilingPoolController.InitPool(Config.TilePrefab, _levelContainer, TILES_POOL);
            _tilingPoolController.OnTilesSpawnedEvent += OnTilesSpawned;

            _collectiblesPoolController = new CollectiblesPoolController();
            _collectiblesPoolController.InitPool(Config.CollectiblePrefabs, _levelContainer, COLLECTIBLES_POOL);

            _tilingPoolController.SpawnTiles();
        }

        public void Update(float deltaTime)
        {
            if (_heroView.position.y <= Config.FailHeight)
            {
                ScreensManager.ChangeScreen(ScreenTypes.FailScreen);
            }

            if (!(_heroView.position.x >
                  _tilingPoolController.CurrentTilePosition * Config.TileLenght + Config.TileLenght / 2f)) return;

            _tilingPoolController.NextTile();
        }

        public void OnCollect(BaseCollectibleView collectable)
        {
            _collectiblesPoolController.ReturnToPool(collectable);
        }

        private void OnTilesSpawned(Transform[] spawnPositions)
        {
            _collectiblesPoolController.SpawnCollectibles(spawnPositions);
        }
    }
}