using System;
using System.Collections;
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

    public Action OnZoneLeft;

    public Action<ItemPickup> OnInRangeOfPickup;
    public Action<ItemDropOff> OnInRangeOfDropOff;

    public Action OnPickupAction;
    public Action OnDropOffAction;

    public Action OnActionAttempt;

    public Action<float> OnStartAction;
    public Action OnStopAction;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnActionAttempt.Invoke();
        }
    }

    private bool HasItem(ItemType type) => _inventory.Any(x => x == type);
    
    public void DoDropOff(ItemType type, float delay)
    {
        if (!HasItem(type)) return;
        
        StartCoroutine(DelayedAction(delay, () => DropOff(type)));
    }

    private void DropOff(ItemType type)
    {
        if (TryTakeItemOut(type))
        {
            OnDropOffAction.Invoke();
        }

        RefreshDisplay();
    }
    
    public void DoPickup(ItemType type, float delay)
    {
        if (!HasEmptySlot()) return;
        
        StartCoroutine(DelayedAction(delay, () => Pickup(type)));
    }

    private void Pickup(ItemType type)
    {
        if (TryAddItem(type))
        {
            OnPickupAction.Invoke();
        }
        RefreshDisplay();
    }

    public IEnumerator DelayedAction(float delay, Action action)
    {
        OnStartAction.Invoke(delay);
        
        yield return new WaitForSeconds(delay);
        
        action.Invoke();
        
        OnStopAction.Invoke();
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        var pickup = other.GetComponent<ItemPickup>();

        if (pickup is not null) OnInRangeOfPickup.Invoke(pickup);

        var dropoff = other.GetComponent<ItemDropOff>();

        if (dropoff is not null) OnInRangeOfDropOff.Invoke(dropoff);
    }

    public void OnTriggerExit(Collider other)
    {
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
