using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupDisplayController : MonoBehaviour
{
    public TextMeshProUGUI PickUpText;
    
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
        
        gg.OnPickupAvailable += OnPickupAvailable;
        
        gg.OnLeavingZone += OnLeavingZone;
    }
    
    private void OnLeavingZone()
    {
        PickUpText.enabled = false;
    }

    private void OnPickupAvailable()
    {
        PickUpText.enabled = true;
        PickUpText.text = $"PRESS E TO PICK UP {p_GameplayManager.PickUpAvailable}";
    }
}
