using CoreGameplay;
using CoreGameplay.Views;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "RunnerGame/LevelConfig", order = 1)]
    public sealed class LevelConfig : BaseConfig
    {
        public int TileLenght;
        public int MaxTilesOnScreen;
        public LevelTile[] TilePrefab;
        public BaseCollectibleView[] CollectiblePrefabs;
        public float FailHeight;
    }
}