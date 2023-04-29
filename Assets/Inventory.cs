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

    ItemPickup.ItemType? _inRangeType;
    ItemType?[] _inventory = new ItemType?[4];

    public Transform[] InvenotrySlots = new Transform[4];
    public List<InventoryItem> InvenotryItemMap = new List<InventoryItem>();
    private List<GameObject> InvenotryDisplayItems = new List<GameObject>();


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _inRangeType is not null && HasEmptySlot())
        {
            AddItem(_inRangeType.Value);
            RefreshDisplay();
        }
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        _inRangeType = other.GetComponent<ItemPickup>().GetItem();
    }

    public void OnTriggerExit(Collider other)
    {
        _inRangeType = null;
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
