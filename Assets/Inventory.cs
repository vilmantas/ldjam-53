using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static ItemPickup;

public class Inventory : MonoBehaviour
{

    ItemPickup.ItemType? _inRangeType;
    List<ItemType> _inventory = new List<ItemType>();
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _inRangeType is not null && _inventory.Count < 4)
        {
            _inventory.Add(_inRangeType.Value);
            Debug.Log("test" + _inventory.Count);
        }
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("in range");
        _inRangeType = other.GetComponent<ItemPickup>().GetItem();
    }

    public void OnTriggerExit(Collider other)
    {
        _inRangeType = null;
        Debug.Log("out range");
    }

    // returns true when item is in invenotry and was taken out
    private bool TryTakeItemOut(ItemType itemToTakeOut)
    {
        return _inventory.Remove(itemToTakeOut);
    }
}
