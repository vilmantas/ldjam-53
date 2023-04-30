using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeLeftDisplayController : MonoBehaviour
{
    public TextMeshProUGUI TimeLeft;
    
    public GameplayManager p_GameplayManager;
    
    // Start is called before the first frame update
    void Start()
    {
        var gameplayManager = GameObject.Find("Manager");

        if (gameplayManager == null) return;

        var gg = gameplayManager.GetComponent<GameplayManager>();
        
        if (gg == null)
        {
            print(" UI FAILED TO INITIALIZE");
            return;
        }

        p_GameplayManager = gg;
    }

    private void Update()
    {
        if (p_GameplayManager.LevelConfiguration == null) return;
        
        var timeLeft = TimeSpan.FromSeconds((p_GameplayManager.LevelConfiguration.TimeToSurviveMinutes * 60) -
                                            p_GameplayManager.TimePassed);

        TimeLeft.text = timeLeft.ToString(@"mm\:ss");
    }
}
