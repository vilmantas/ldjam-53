using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ItemDropOffUIController : MonoBehaviour
{
    [HideInInspector]
    public TextMeshProUGUI Title;

    [HideInInspector]
    public ItemDropOff[] m_items;

    public StatusBarDisplayScript StatusBarPrefab;
    public RectTransform Container;
    public void Initialize(DropOffZone zone)
    {
        m_items = zone.DropOffs;
        
        Title.text = zone.Name;
        
        Title.color = zone.color;

        foreach (var itemDrop in m_items)
        {
            var ins = Instantiate(StatusBarPrefab, Container);
            
            ins.Initialize(itemDrop);
        }
    }
}
