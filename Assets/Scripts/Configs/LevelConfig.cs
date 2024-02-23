using CoreGameplay;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "RunnerGame/LevelConfig")]
    public sealed class LevelConfig : ScriptableObject
    {
        public int TileLenght;
        public int MaxTilesOnScreen;
        public LevelTile StartTilePrefab;
        public LevelTile[] TilePrefab;
        public CollectibleItem[] CollectiblePrefabs;
        public float FailHeight;
    }
}