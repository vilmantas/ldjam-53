using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        None,
        Ammo,
        Health,
        Shell
    }
    public ItemType PickupType;

    public ItemType GetItem()
    {
        return PickupType;
    }
}
