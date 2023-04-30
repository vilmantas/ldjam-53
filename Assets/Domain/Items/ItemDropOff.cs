using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemDropOff : MonoBehaviour
{
    public string Name;
    
    public ItemPickup.ItemType NeededItem;

    [Range(0, 5000)]
    public int Available;
    
    [Range(0, 5000)]
    public int Max;

    public Action ResourceExpended;

    private void Start()
    {
        StartCoroutine(BurstFire());
    }

    public void ReceiveResource(int amount)
    {
        Available += amount;
    }

    private IEnumerator BurstFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

            if (Random.value >= 0.5f)
            {
                if (Random.value > 0.5f)
                {
                    var amount = Random.Range(50, 100);
                    for (var i = 0; i < amount; i++)
                    {
                        ExpendResource();
                        yield return new WaitForSeconds(Random.Range(0.01f, 0.05f));
                    }
                }
                else
                {
                    var amount = Random.Range(200, 400);
                    for (var i = 0; i < amount; i++)
                    {
                        ExpendResource();
                        yield return new WaitForSeconds(Random.Range(0.01f, 0.05f));
                    }
                }
            }    
        }
    }

    private void ExpendResource()
    {
        if (Available <= 0) return;
        
        Available--;
        ResourceExpended.Invoke();
    }
}
