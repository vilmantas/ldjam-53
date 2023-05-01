using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBarDisplayScript : MonoBehaviour
{
    [HideInInspector]
    public ItemDropOff m_item;
    
    [HideInInspector]
    public float BarWidth;
    
    public RectTransform BarMeter;

    public void Initialize(ItemDropOff item)
    {
        m_item = item;

        BarWidth = BarMeter.rect.width;
        
        m_item.ResourceExpended += OnResourceExpended;

        SetBarWidth();
    }
    
    private void OnResourceExpended()
    {
        SetBarWidth();
    }

    private void SetBarWidth()
    {
        float percentLeft = (float)m_item.Available / m_item.Max;

        var expectedSize = Mathf.Lerp(0, BarWidth, percentLeft );

        BarMeter.offsetMax = new Vector2(expectedSize - BarWidth, 0);
    }
}
