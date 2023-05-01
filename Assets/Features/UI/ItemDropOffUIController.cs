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

    public void Initialize(ItemDropOff[] items)
    {
        m_items = items;
        
        Title.text = items.First().Name;

        foreach (var itemDrop in m_items)
        {
            var ins = Instantiate(StatusBarPrefab, transform);
            
            ins.Initialize(itemDrop);
        }
    }
}
