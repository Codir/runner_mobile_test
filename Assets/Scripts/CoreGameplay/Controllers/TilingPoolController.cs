using System;
using System.Collections.Generic;
using UnityEngine;

namespace CoreGameplay.Controllers
{
    //Class with logic of tiling level segments
    public class TilingPoolController : BasePoolController<LevelTile>
    {
        public event Action<Transform[]> OnTilesSpawnedEvent;

        public int CurrentTilePosition => _currentTilePosition;

        private readonly int _maxTilesOnScreen;
        private readonly int _tileLenght;
        private readonly Queue<LevelTile> _tiles;
        private int _currentTilePosition;

        public TilingPoolController(int maxTilesOnScreen, int tileLenght)
        {
            _maxTilesOnScreen = maxTilesOnScreen;
            _tileLenght = tileLenght;

            _tiles = new Queue<LevelTile>();
        }

        public void SpawnTiles()
        {
            SpawnTile(_currentTilePosition - 1);
            SpawnTile(_currentTilePosition);
            SpawnTile(_currentTilePosition + 1);
        }

        public void NextTile()
        {
            SpawnTile(_currentTilePosition + 2);
            _currentTilePosition++;
        }

        private LevelTile GetTile(Vector3 position)
        {
            var tile = GetFromPool();
            tile.gameObject.transform.position = position;
            _tiles.Enqueue(tile);

            return tile;
        }

        private void CheckTiles()
        {
            if (_tiles.Count < _maxTilesOnScreen) return;

            var tilesForRemove = _tiles.Count - _maxTilesOnScreen;

            for (var i = 0; i < tilesForRemove; i++)
            {
                var tile = _tiles.Dequeue();
                ReturnToPool(tile);
            }
        }

        private void SpawnTile(int tileIndex)
        {
            var position = new Vector3(tileIndex * _tileLenght, 0f, 0f);
            var tile = GetTile(position);
            OnTilesSpawnedEvent.SafeInvoke(tile.CollectiblesSpawnPositions);
            CheckTiles();
        }
    }
}