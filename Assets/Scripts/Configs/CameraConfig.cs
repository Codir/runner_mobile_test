using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "RunnerGame/CameraConfig", order = 0)]
    public sealed class CameraConfig : BaseConfig
    {
        public Vector3 Multiply;
        public float Lerp;
    }
}