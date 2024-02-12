using UnityEngine;

namespace CoreGameplay.Controllers
{
    public interface IPoolController<T>
    {
        void InitPool(T[] item, Transform container, int poolSize);

        T GetFromPool();

        void ReturnToPool(T item);
    }
}