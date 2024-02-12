using CoreGameplay.Models;
using UnityEngine;

namespace CoreGameplay.Views
{
    public class BaseCollectibleView : MonoBehaviour
    {
        public virtual CollectibleEntityModel GetModel()
        {
            return null;
        }
    }
}