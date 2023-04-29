using System;
using System.Collections;
using System.Collections.Generic;
using Features.Camera;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameObject Truck;
    
    private void Awake()
    {
        print("Game is running!");

        Truck = GameObject.Find("Truck");
        
        CameraManager.Instance.ChangeTarget(Truck.gameObject.transform, Truck.gameObject.transform);
    }

    private void OnGameLoaded()
    {
        
    }
}
