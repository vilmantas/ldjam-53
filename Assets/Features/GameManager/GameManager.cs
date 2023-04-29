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

        var m = manager.AddComponent<GameManager>();

        m.Configuration = Resources.Load<GameConfiguration_SO>("Configuration");

        DontDestroyOnLoad(manager);
    }

    public GameConfiguration_SO Configuration;

    public void StartGame(string level)
    {
        LoadingManager.Instance.LoadScenes(new List<string>() { level, "Gameplay", "Lighting" }, "Lighting");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0)) 
        {
            StartGame(Configuration.Levels[0].LevelName);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            StartGame(Configuration.Levels[1].LevelName);
        }
    }
}
