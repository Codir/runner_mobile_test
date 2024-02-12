using System.Collections.Generic;
using CoreGameplay.Models;
using CoreGameplay.Views;
using UnityEngine;

namespace CoreGameplay.Controllers
{
    //Class with checking collision logic
    public class CollisionController
    {
        private const int MAX_COLLIDERS_OVERLAPPED = 10;

        private readonly Collider[] _collidersOverlapped = new Collider[MAX_COLLIDERS_OVERLAPPED];
        private readonly float _sphereRadius;
        private readonly Vector3 _boxSize;
        private readonly Quaternion _boxOrientation;
        private readonly LayerMask _mask;

        public CollisionController(float sphereRadius)
        {
            _sphereRadius = sphereRadius;
        }

        public CollisionController(Vector3 boxSize, Quaternion boxOrientation, LayerMask mask)
        {
            _boxSize = boxSize;
            _boxOrientation = boxOrientation;
            _mask = mask;
        }

        public List<T> GetCollisionsBySphere<T>(Vector3 position)
            where T : CollectibleEntityModel
        {
            return GetCollisionsBySphere<T>(position, _sphereRadius);
        }

        private List<T> GetCollisionsBySphere<T>(Vector3 position, float radius)
            where T : CollectibleEntityModel
        {
            var collectables = new List<T>();

            var count = Physics.OverlapSphereNonAlloc(position, radius, _collidersOverlapped);
            if (count <= 0) return collectables;

            for (var i = 0; i < count; i++)
            {
                if (_collidersOverlapped[i] == null) continue;
                var collectable = _collidersOverlapped[i].gameObject.GetComponent<BaseCollectibleView>();
                if (collectable == null) continue;

                collectables.Add(collectable.GetModel() as T);
                CoreGameplayController.Instance.LevelController.OnCollect(collectable);
            }

            return collectables;
        }

        public bool CheckBox(Vector3 position)
        {
            return CheckBox(position, _boxSize, _boxOrientation, _mask);
        }

        public bool CheckBox(Vector3 position, Vector3 boxSize, Quaternion boxOrientation, LayerMask mask)
        {
            return Physics.CheckBox(position, boxSize, boxOrientation, mask);
        }
    }
}