using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public List<ItemDropOff> ActiveZones;

    public ItemDropOff ActiveDropOff;

    public ItemPickup ActivePickup;
    
    void Awake()
    {
        Truck = GameObject.Find("Truck");

        var inv = Truck.GetComponentInChildren<Inventory>();

        inv.OnDropOffAvailable += SetDropOff;
        inv.OnPickupAvailable += SetPickup;
        inv.OnZoneLeft += LeavingTriggerZone;
        
        inv.OnInRangeOfPickup += OnInRangeOfPickup;
        inv.OnInRangeOfDropOff += OnInRangeOfDropOff;
        
        inv.OnPickupAction += OnPickupAction;
        inv.OnDropOffAction += OnDropOffAction;
        
        CameraManager.Instance.ChangeTarget(Truck.gameObject.transform, Truck.gameObject.transform);

        ActiveZones = FindObjectsOfType<ItemDropOff>().ToList();
    }

    private void OnDropOffAction()
    {
        ActiveDropOff.ReceiveResource(1000);
    }

    private void OnPickupAction()
    {
        // IDK
    }

    private void OnInRangeOfDropOff(ItemDropOff obj)
    {
        ActiveDropOff = obj;
    }

    private void OnInRangeOfPickup(ItemPickup obj)
    {
        ActivePickup = obj;
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
