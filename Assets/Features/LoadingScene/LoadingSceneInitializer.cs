using UnityEngine;

namespace Features.LoadingScene
{
    public static class LoadingSceneInitializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void CreateSingleton()
        {
            var o = new GameObject("loading_scene_manager");

            o.AddComponent<LoadingManager>();
        }
    }
}