using CoreGameplay.Hero.HeroProperties;
using DLib.AttributesSystem;
using UnityEngine;

namespace CoreGameplay
{
    public class CollectibleItem : MonoBehaviour
    {
        #region serialize fields

        [SerializeField] private Vector3 Rotation;
        public HeroPropertyAttribute[] PropertyItems;
        public float TimeLeft;

        #endregion

        #region engine methods

        private void Update()
        {
            transform.Rotate(Rotation * Time.deltaTime);
        }

        #endregion
    }
}