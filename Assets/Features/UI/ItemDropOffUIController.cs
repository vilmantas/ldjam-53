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

    public void Initialize(ItemDropOff item)
    {
        m_item = item;
        
        Title.text = item.Name;

        Resource.text = item.NeededItem.ToString();

        Left.text = item.Available.ToString();

        Max.text = item.Max.ToString();
        
        item.ResourceExpended += OnResourceExpended;
    }

    private void OnResourceExpended()
    {
        Left.text = m_item.Available.ToString();
    }
}
