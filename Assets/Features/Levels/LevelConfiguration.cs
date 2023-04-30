using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelConfiguration : MonoBehaviour
{
    [HideInInspector]
    public string LevelName;

    [HideInInspector]
    public float TimeToSurviveMinutes;
    
    [HideInInspector]
    public float LoseGracePeriod;
    
    [SerializeField]
    private LevelConfiguration_SO Configuration;

    [HideInInspector]
    public int RespawnsAllowed;
    
    [HideInInspector]
    public Dictionary<ItemPickup.ItemType, ItemLoadDurations> ItemDurations;

    private void Awake()
    {
        LevelName = Configuration.LevelName;
        TimeToSurviveMinutes = Configuration.TimeToSurviveMinutes;
        LoseGracePeriod = Configuration.LoseGracePeriod;
        ItemDurations = Configuration.ItemDurations.ToDictionary(x => x.ItemType, x => x);
        RespawnsAllowed = Configuration.RespawnsAllowed;
    }
}
