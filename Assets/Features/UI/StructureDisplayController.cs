using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StructureDisplayController : MonoBehaviour
{
    public GameplayManager p_GameplayManager;

    public RectTransform Container;

    public ItemDropOffUIController Prefab;
    
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

        foreach (Transform child in Container.transform) {
            Destroy(child.gameObject);
        }
        
        foreach (var zone in gg.ActiveZones)
        {
            var z = Instantiate(Prefab, Container);
            
            z.Initialize(new []{zone} );
        }
    }
}
