﻿using CoreGameplay.Models;
using UnityEngine;

namespace CoreGameplay.Views
{
    public class FastPowerUp : BaseCollectibleView
    {
        [SerializeField] private float TimeLeft;
        [SerializeField] private float MoveSpeed;
        
        public override CollectibleEntityModel GetModel()
        {
            return new CollectibleEntityModel()
            {
                TimeLeft = TimeLeft,
                MoveSpeed = MoveSpeed
            };
        }
    }
}