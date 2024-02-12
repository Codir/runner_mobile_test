using System.Collections.Generic;
using UnityEngine;

namespace CoreGameplay.Controllers
{
    //Class with base of pooling mechanism
    public abstract class BasePoolController<T> : IPoolController<T>
        where T : MonoBehaviour
    {
        private int _poolSize;
        private T[] _itemPrefabs;
        private Transform _container;

        private Queue<T> _pool;

        public virtual void InitPool(T[] itemPrefabs, Transform container, int poolSize)
        {
            _itemPrefabs = itemPrefabs;
            _container = container;
            _poolSize = poolSize;

            _pool = new Queue<T>();
            for (var i = 0; i < _poolSize; i++)
            {
                AddItemToPool();
            }
        }

        public virtual T GetFromPool()
        {
            var item = _pool.Dequeue();
            item.gameObject.SetActive(true);

            return item;
        }

        public virtual void ReturnToPool(T item)
        {
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
        }

        protected virtual void AddItemToPool()
        {
            var item = Object.Instantiate(_itemPrefabs.GetRandomElement(), Vector3.zero,
                Quaternion.identity);
            item.gameObject.transform.SetParent(_container);
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
        }
    }
}