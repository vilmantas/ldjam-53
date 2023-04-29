using System;
using System.Collections;
using System.Collections.Generic;
using Features.Camera;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameObject Truck;

    public ItemPickup.ItemType DropOffAvailable;

    public ItemPickup.ItemType PickUpAvailable;

    public Action OnDropOffAvailable;
    
    public Action OnPickupAvailable;

    public Action OnLeavingZone;
    
    void Awake()
    {
        Truck = GameObject.Find("Truck");

        var inv = Truck.GetComponentInChildren<Inventory>();

        inv.OnDropOffAvailable += SetDropOff;
        inv.OnPickupAvailable += SetPickup;
        inv.OnZoneLeft += LeavingTriggerZone;
        
        CameraManager.Instance.ChangeTarget(Truck.gameObject.transform, Truck.gameObject.transform);
    }

    public void SetPickup(ItemPickup.ItemType type)
    {
        PickUpAvailable = type;
        OnPickupAvailable.Invoke();
    }

    public void SetDropOff(ItemPickup.ItemType type)
    {
        DropOffAvailable = type;
        OnDropOffAvailable.Invoke();
    }

    public void LeavingTriggerZone()
    {
        OnLeavingZone.Invoke();
    }

    private void OnGameLoaded()
    {
        
    }
}
