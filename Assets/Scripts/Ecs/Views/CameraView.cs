using Cinemachine;
using UnityEngine;

namespace Ecs.Views
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        public void SetTarget(Transform target)
        {
            virtualCamera.Follow = target;
            virtualCamera.LookAt = target;
        }
    }
}