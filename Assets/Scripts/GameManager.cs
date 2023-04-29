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
        
    }
}
