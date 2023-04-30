using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GracePeriodDisplayController : MonoBehaviour
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

        if (p_GameplayManager.InGracePeriod)
        {
            var passed = p_GameplayManager.TimePassed - p_GameplayManager.GracePeriodStart;

            var left = Math.Max(0, p_GameplayManager.LevelConfiguration.LoseGracePeriod - passed);
        
            var timeLeft = TimeSpan.FromSeconds(left);

            TimeLeft.text = timeLeft.ToString(@"mm\:ss");    
        }
        else
        {
            TimeLeft.text = "";
        }
    }
}
