using Cinemachine;
using UnityEngine;

namespace Features.Camera
{
    public class CameraManager : SingletonManager<CameraManager>
    {
        public CinemachineVirtualCamera CinemachineCamera;

        public UnityEngine.Camera Camera;
        
        protected override void DoSetup()
        {
            if (UnityEngine.Camera.allCameras.Length > 1)
            {
                Destroy(gameObject);

                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void ChangeTarget(Transform follow, Transform lookAt)
        {
            CinemachineCamera.Follow = follow;
            CinemachineCamera.LookAt = lookAt;
        }
    }
}