using Cinemachine;
using UnityEngine;

namespace Features.Camera
{
    public class CameraManager : MonoBehaviour
    {
        public CinemachineVirtualCamera CinemachineCamera;

        public UnityEngine.Camera Camera;
        
        public void ChangeTarget(Transform follow, Transform lookAt)
        {
            CinemachineCamera.Follow = follow;
            CinemachineCamera.LookAt = lookAt;
        }
    }
}