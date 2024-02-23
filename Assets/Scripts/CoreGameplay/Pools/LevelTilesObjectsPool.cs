using System;
using System.Linq;
using Configs;
using DLib.Pools;
using UnityEngine;

namespace CoreGameplay.Pools
{
    public sealed class LevelTilesObjectsPool : BaseObjectsPool<LevelTile>
    {
        #region fields

        public event Action<Transform[]> OnTilesSpawnedEvent;

        private readonly int _maxTilesOnScreen;
        private readonly int _tileLenght;
        private readonly LevelTile _startTile;
        private int _currentTilePosition;

        #endregion

        #region constructor

        public LevelTilesObjectsPool(LevelConfig levelConfig, Transform container, int poolSize) : base(
            levelConfig.TilePrefab, container, poolSize)
        {
            _maxTilesOnScreen = levelConfig.MaxTilesOnScreen;
            _tileLenght = levelConfig.TileLenght;

            Init(() => Preload(levelConfig.TilePrefab, container), poolSize, GetCallback, ReturnCallback);

            _startTile = Preload(levelConfig.StartTilePrefab, container);
        }

        #endregion

        #region public methods

        public void SpawnStartTiles()
        {
            _currentTilePosition = 0;
            SpawnLevelTile(_currentTilePosition - 1);
            SpawnLevelTile(_currentTilePosition);
            SpawnLevelTile(_currentTilePosition + 1);
        }

        public void CheckHeroPosition(Vector3 position)
        {
            if (!(position.x > _currentTilePosition * _tileLenght + _tileLenght / 2f)) return;

            SpawnLevelTile(_currentTilePosition + 2);
            _currentTilePosition++;
        }

        #endregion

        #region private methods

        private void CheckTiles()
        {
            if (ActiveList.Count < _maxTilesOnScreen) return;

            var tilesForRemove = ActiveList.Count - _maxTilesOnScreen;

            for (var i = 0; i < tilesForRemove; i++)
            {
                var tile = ActiveList.First();
                Return(tile);
            }
        }

        private void SpawnLevelTile(int tileIndex)
        {
            var position = new Vector3(tileIndex * _tileLenght, 0f, 0f);
            var tile = tileIndex <= 0 ? _startTile : Get(position);
            OnTilesSpawnedEvent.SafeInvoke(tile.CollectiblesSpawnPositions);

            CheckTiles();
        }

        #endregion
    }
}