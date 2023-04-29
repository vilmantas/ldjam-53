using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        Ammo,
        Health
    }
    public ItemType PickupType;

    public ItemType GetItem()
    {
        return PickupType;
    }
}
