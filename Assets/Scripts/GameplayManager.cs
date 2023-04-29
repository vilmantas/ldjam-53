using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameObject Truck;
    
    private void Awake()
    {
        print("Game is running!");

        Truck = GameObject.Find("Truck");
    }

    private void OnGameLoaded()
    {
        
    }
}
