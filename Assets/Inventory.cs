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


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _inRangePickupType is not null && HasEmptySlot())
        {
            AddItem(_inRangePickupType.Value);
            RefreshDisplay();
        }

        if (Input.GetKeyDown(KeyCode.E) && _inRangeDropOffType is not null)
        {
            TryTakeItemOut(_inRangeDropOffType.Value);
            RefreshDisplay();
        }
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        var pickup = other.GetComponent<ItemPickup>();

        if (pickup is not null) 
        {
            _inRangePickupType = other.GetComponent<ItemPickup>().GetItem();
        }

        var dropoff = other.GetComponent<ItemDropOff>();

        if (dropoff is not null)
        {
            _inRangeDropOffType = dropoff.NeededItem;
        }

    }

    public void OnTriggerExit(Collider other)
    {
        _inRangePickupType = null;
        _inRangeDropOffType = null;
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
    private void AddItem(ItemType type)
    {
        for (int n = 0; n < _inventory.Length; n++)
        {
            if (_inventory[n] == null)
            {
                _inventory[n] = type;
                return;
            }
        }
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
