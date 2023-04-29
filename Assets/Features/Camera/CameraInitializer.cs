using UnityEngine;

namespace Features.Camera
{
    public static class CameraInitializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitializeCameraScene()
        {
            var cameraResource = Resources.Load<GameObject>("Camera");

            var instance = Object.Instantiate(cameraResource);

            instance.name = "camera_manager";
        }
    }
}