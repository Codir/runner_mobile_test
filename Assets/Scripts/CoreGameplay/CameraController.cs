using UnityEngine;

namespace CoreGameplay
{
    public class CameraController : MonoBehaviour
    {
        #region serialize fields

        [SerializeField] private Vector3 Multiply;
        [SerializeField] private float Lerp;

        #endregion

        #region fields

        private Transform _target;
        private Vector3 _offsetToTarget;
        private Vector3 _cashedPosition;

        #endregion

        #region engine methods

        private void Awake()
        {
            _cashedPosition = transform.position;
        }

        public void Update()
        {
            if (_target == null) return;

            var targetPosition = (_target.position + _offsetToTarget).Multiply(Multiply);
            targetPosition.y += 2f;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Lerp);
        }

        #endregion

        #region public methods

        public void SetTarget(Transform target)
        {
            _target = target;

            _offsetToTarget = transform.position - _target.position;
        }

        public void Reset()
        {
            transform.position = _cashedPosition;
        }

        #endregion
    }
}