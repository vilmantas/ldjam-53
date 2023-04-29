using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.LoadingScene
{
    public class LoadingManager : SingletonManager<LoadingManager>
    {
        protected override void DoSetup()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScenes(IEnumerable<string> scenes, string activeScene = "")
        {
            StartCoroutine(ShowLoadingScreen(scenes, activeScene, LoadSceneMode.Single));
        }

        public void LoadAdditionalScenes(IEnumerable<string> scenes, string activeScene = "")
        {
            StartCoroutine(ShowLoadingScreen(scenes, activeScene, LoadSceneMode.Additive));
        }

        private static IEnumerator ShowLoadingScreen(IEnumerable<string> scenes, string activeScene, LoadSceneMode mode)
        {
            var scene = SceneManager.GetSceneByName("Loading");

            if (!scene.isLoaded)
            {
                var op = SceneManager.LoadSceneAsync("Loading", mode);

                while (!op.isDone)
                {
                    yield return null;
                }    
            }

            PrepareScene(scenes, activeScene);
        }

        private static void PrepareScene(IEnumerable<string> scenes, string activeScene)
        {
            var g = GameObject.Find("LoadingController").GetComponent<LoadingController>();

            g.Initialize(scenes, activeScene);
        }
    }
}