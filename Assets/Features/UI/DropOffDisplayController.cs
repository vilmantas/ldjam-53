using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropOffDisplayController : MonoBehaviour
{
    public TextMeshProUGUI DropOffText;

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
        
        gg.OnDropOffAvailable += OnDropOffAvailable;
        
        gg.OnLeavingZone += OnLeavingZone;
    }

    private void OnLeavingZone()
    {
        DropOffText.enabled = false;
    }

    private void OnDropOffAvailable()
    {
        DropOffText.enabled = true;
        DropOffText.text = $"PRESS E TO DROP OFF {p_GameplayManager.DropOffAvailable}";
    }
}
