using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        Ammo
    }
    public ItemType PickupType;

    public ItemType GetItem()
    {
        return PickupType;
    }
}
