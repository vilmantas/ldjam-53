using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropOffZone : MonoBehaviour
{
    public string Name;

    [HideInInspector]
    public ItemDropOff[] DropOffs;

    public bool HasDepletedResource => DropOffs.Any(x => x.IsDepleted);
    
    private void Awake()
    {
        DropOffs = GetComponentsInChildren<ItemDropOff>();
    }
}
