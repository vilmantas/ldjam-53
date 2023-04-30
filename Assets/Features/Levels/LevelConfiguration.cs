using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfiguration : MonoBehaviour
{
    [HideInInspector]
    public string LevelName;

    [HideInInspector]
    public float TimeToSurviveMinutes;
    
    [SerializeField]
    private LevelConfiguration_SO Configuration;

    private void Start()
    {
        LevelName = Configuration.LevelName;
        TimeToSurviveMinutes = Configuration.TimeToSurviveMinutes;
    }
}
