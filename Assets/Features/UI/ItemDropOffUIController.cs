using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDropOffUIController : MonoBehaviour
{
    public TextMeshProUGUI Title;
    
    public TextMeshProUGUI Resource;
    
    public TextMeshProUGUI Left;
    
    public TextMeshProUGUI Max;

    public ItemDropOff m_item;

    public RectTransform BarMeter;

    public float BarWidth;

    public void Initialize(ItemDropOff item)
    {
        m_item = item;
        
        Title.text = item.Name;

        Resource.text = item.NeededItem.ToString();

        Left.text = item.Available.ToString();

        Max.text = item.Max.ToString();
        
        item.ResourceExpended += OnResourceExpended;

        BarWidth = BarMeter.rect.width;
        
        SetBarWidth();
    }

    private void OnResourceExpended()
    {
        Left.text = m_item.Available.ToString();
        
        SetBarWidth();
    }

    private void SetBarWidth()
    {
        float percentLeft = (float)m_item.Available / m_item.Max;

        var expectedSize = Mathf.Lerp(0, BarWidth, percentLeft );

        BarMeter.offsetMax = new Vector2(expectedSize - BarWidth, 0);
    }
}
