using Features.LoadingScene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Hooker()
    {
        GameObject manager = new GameObject("game_manager");

        manager.AddComponent<GameManager>();
        
        DontDestroyOnLoad(manager);
    }

    public void StartGame()
    {
        LoadingManager.Instance.LoadScenes(new List<string>() { "Gereuses_Levelis", "Gameplay", "Lighting" }, "Lighting");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            StartGame();
        }
    }
}
