using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "Levels/Level Configuration", order = 1)]
public class LevelConfiguration_SO : ScriptableObject
{
    public string LevelName;

    [Min(0.1f)]
    public float TimeToSurviveMinutes;
    
    [Min(0f)]
    public float LoseGracePeriod;
    
    public SceneList_SO AdditionalScenes;
}
