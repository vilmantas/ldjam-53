using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "Levels/Level Configuration", order = 1)]
public class LevelConfiguration_SO : ScriptableObject
{
    public string LevelName;

    public int RespawnsAllowed;
    
    [Min(0.1f)]
    public float TimeToSurviveMinutes;
    
    [Min(0f)]
    public float LoseGracePeriod;

    public List<ItemLoadDurations> ItemDurations;

    public string LightingScene = "_Lighting";
    
    public SceneList_SO AdditionalScenes;
}


[Serializable]
public class ItemLoadDurations
{
    public ItemPickup.ItemType ItemType;

    [Min(0.1f)]
    public float LoadDuration;

    [Min(0.1f)]
    public float OffloadDuration;
}