using System;
using UnityEngine;

namespace CoreGameplay
{
    public class LevelTile : MonoBehaviour
    {
        public Transform[] CollectiblesSpawnPositions;

        private void Awake()
        {
            enabled = false;
        }
    }
}