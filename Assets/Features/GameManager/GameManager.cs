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

    public float Volume;
    
    public int CurrentLevelIndex;
    
    public GameConfiguration_SO Configuration;

    public void StartGame(LevelConfiguration_SO config)
    {
        var level = config.LevelName;

        var additionalScenes = config.AdditionalScenes.Scenes;
        
        LoadingManager.Instance.LoadScenes(new List<string>() { level, config.LightingScene }.Concat(additionalScenes), config.LightingScene);
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

    public bool IsLastLevel => Configuration.Levels.Levels.Length == CurrentLevelIndex + 1;
}
