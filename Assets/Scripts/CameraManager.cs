using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Hooker()
    {
        GameObject manager = new GameObject("camera_manager");

        manager.AddComponent<CameraManager>();
        
        DontDestroyOnLoad(manager);
    }
}
