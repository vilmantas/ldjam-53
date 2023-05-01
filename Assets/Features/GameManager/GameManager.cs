using Features.LoadingScene;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public int CurrentLevelIndex;
    
    public GameConfiguration_SO Configuration;

    public void StartGame(LevelConfiguration_SO config)
    {
        var level = config.LevelName;

        var additionalScenes = config.AdditionalScenes.Scenes;
        
        LoadingManager.Instance.LoadScenes(new List<string>() { level }.Concat(additionalScenes), "Lighting");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0)) 
        {
            StartGame(Configuration.Levels.Levels[0]);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            StartGame(Configuration.Levels.Levels[1]);
        }

        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            StartGame();
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            StartNextLevel();
        }
    }

    public void StartGame()
    {
        CurrentLevelIndex = 0;
        
        StartGame(Configuration.Levels.Levels[CurrentLevelIndex]);
    }

    public void StartNextLevel()
    {
        if (Configuration.Levels.Levels.Length > CurrentLevelIndex+1)

        StartGame(Configuration.Levels.Levels[++CurrentLevelIndex]);
    }

    public void RestartLevel()
    {
        StartGame(Configuration.Levels.Levels[CurrentLevelIndex]);
    }
}
