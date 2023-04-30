using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ItemPickup;

[System.Serializable]
public class InventoryItem
{
    public ItemType TypeOfItem;
    public GameObject Prefab;
}
public class Inventory : MonoBehaviour
{

    ItemPickup.ItemType? _inRangePickupType;
    ItemPickup.ItemType? _inRangeDropOffType;
    ItemType?[] _inventory = new ItemType?[4];

    public Transform[] InvenotrySlots = new Transform[4];
    public List<InventoryItem> InvenotryItemMap = new List<InventoryItem>();
    private List<GameObject> InvenotryDisplayItems = new List<GameObject>();

    public Action<ItemType> OnPickupAvailable;
    public Action<ItemType> OnDropOffAvailable;
    public Action OnZoneLeft;

    public Action<ItemPickup> OnInRangeOfPickup;
    public Action<ItemDropOff> OnInRangeOfDropOff;

    public Action OnPickupAction;
    public Action OnDropOffAction;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _inRangePickupType is not null && HasEmptySlot())
        {
            if (TryAddItem(_inRangePickupType.Value))
            {
                OnPickupAction.Invoke();
            }
            RefreshDisplay();
        }

        if (Input.GetKeyDown(KeyCode.E) && _inRangeDropOffType is not null)
        {
            if (TryTakeItemOut(_inRangeDropOffType.Value))
            {
                OnDropOffAction.Invoke();
            }

            RefreshDisplay();
        }
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        var pickup = other.GetComponent<ItemPickup>();

        if (pickup is not null) 
        {
            _inRangePickupType = pickup.GetItem();
            
            OnPickupAvailable.Invoke(_inRangePickupType.Value);
            
            OnInRangeOfPickup.Invoke(pickup);
        }

        var dropoff = other.GetComponent<ItemDropOff>();

        if (dropoff is not null)
        {
            _inRangeDropOffType = dropoff.NeededItem;
            
            OnDropOffAvailable.Invoke(_inRangeDropOffType.Value);
            
            OnInRangeOfDropOff.Invoke(dropoff);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        _inRangePickupType = null;
        _inRangeDropOffType = null;

        OnZoneLeft.Invoke();
    }

    // returns true when item is in invenotry and was taken out
    public bool TryTakeItemOut(ItemType itemToTakeOut)
    {
        for (int n = 0; n < _inventory.Length; n++)
        {
            if (_inventory[n] == itemToTakeOut)
            {
                _inventory[n] = null;
                RefreshDisplay();
                return true;
            }
        }
        return false;
    }
    private bool HasEmptySlot()
    {
        return _inventory.Any(i => i == null);
    }
    private bool TryAddItem(ItemType type)
    {
        for (int n = 0; n < _inventory.Length; n++)
        {
            if (_inventory[n] == null)
            {
                _inventory[n] = type;
                return true;
            }
        }

        return false;
    }
    private void RefreshDisplay()
    {
        InvenotryDisplayItems.ForEach(i => Destroy(i));
        InvenotryDisplayItems.Clear();
        for (int n = 0; n < _inventory.Length; n++)
        {
            if (_inventory[n] != null)
            {
               var inventoryItem= InvenotryItemMap.Find(ii => ii.TypeOfItem == _inventory[n]);
               var itemInstance = Instantiate(inventoryItem.Prefab, InvenotrySlots[n]);
                InvenotryDisplayItems.Add(itemInstance);
            }
        }
    }
}
