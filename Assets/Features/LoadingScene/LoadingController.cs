using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Features.LoadingScene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LoadingController : MonoBehaviour
    {
        public LoadingItemController ItemPrefab;

        public Camera SceneCamera;

        public Transform ItemContainer;

        public List<(string scene, AsyncOperation op)> Operations = new();

        [HideInInspector] public string SetActiveScene = "";

        private void Awake()
        {
            if (Camera.allCamerasCount > 1)
            {
                SceneCamera.gameObject.SetActive(false);
            }
            else
            {
                Camera.SetupCurrent(SceneCamera);

                SceneCamera.gameObject.SetActive(true);
            }
        }

        public void Initialize(IEnumerable<string> scenes, string activeScene)
        {
            SetActiveScene = activeScene;
            
            foreach (var scene in scenes)
            {
                var o = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

                o.allowSceneActivation = false;

                var loader = Instantiate(ItemPrefab, ItemContainer);

                loader.Initialize(scene, o.progress.ToString("F"));

                StartCoroutine(TrackProgress(o, loader));

                Operations.Add((scene, o));
            }

            StartCoroutine(Watcher());
        }

        private IEnumerator TrackProgress(AsyncOperation op, LoadingItemController loader)
        {
            while (!op.isDone)
            {
                var progress = Mathf.Clamp01(op.progress / .9f);

                loader.SetStatus(progress.ToString("F"));

                yield return null;
            }

            loader.SetStatus("DONE");
        }

        private IEnumerator Watcher()
        {
            while (!Operations.All(x => IsPreloadComplete(x.op)))
            {
                yield return null;
            }

            foreach (var asyncOperation in Operations)
            {
                asyncOperation.op.allowSceneActivation = true;
            }

            while (!Operations.All(x => IsLoadComplete(x.op)))
            {
                yield return null;
            }

            if (!string.IsNullOrEmpty(SetActiveScene))
            {
                var scene = SceneManager.GetSceneByName(SetActiveScene);

                if (scene.isLoaded)
                {
                    SceneManager.SetActiveScene(scene);
                }
            }
            else if (SceneManager.GetActiveScene().name == "Loading")
            {
                var scene = SceneManager.GetSceneByName(Operations.First().scene);

                SceneManager.SetActiveScene(scene);
            }

            SceneManager.UnloadSceneAsync("Loading");
        }

        private bool IsPreloadComplete(AsyncOperation op)
        {
            return Mathf.Clamp01(op.progress / .9f) == 1;
        }

        private bool IsLoadComplete(AsyncOperation op)
        {
            return Mathf.Clamp01(op.progress / 1f) == 1;
        }
    }
}