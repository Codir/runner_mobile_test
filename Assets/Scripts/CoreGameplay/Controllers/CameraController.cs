using Configs;
using UnityEngine;

namespace CoreGameplay.Controllers
{
    //Camera moving controller
    public class CameraController : BaseControllerWithConfig<CameraConfig>
    {
        private Transform _target;

        private Vector3 _offsetToTarget;

        public void Update(float deltaTime)
        {
            if (_target == null) return;

            var targetPosition = (_target.position + _offsetToTarget).Multiply(Config.Multiply);
            targetPosition.y += 2f;
            View.transform.position = Vector3.Lerp(View.transform.position, targetPosition, Config.Lerp);
        }

        public void SetTarget(Transform target)
        {
            _target = target;

            _offsetToTarget = View.transform.position - _target.position;
        }

        public void Reset()
        {
            var cameraPosition = Vector3.zero;
            cameraPosition.z = -10;
            View.transform.position = cameraPosition;
        }
    }
}