using UnityEngine;

namespace CoreGameplay
{
    public class LevelTile : MonoBehaviour
    {
        #region serialize fields

        public Transform[] CollectiblesSpawnPositions;

        #endregion

        #region engine methods

        private void Awake()
        {
            enabled = false;
        }

        #endregion
    }
}