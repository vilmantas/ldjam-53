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

    public bool IsDepleted => Available <= 0;
    
    private void Start()
    {
        StartCoroutine(BurstFire());
    }

    public void ReceiveResource(int amount)
    {
        Available = Math.Min(Max, Available + amount);
        ResourceExpended.Invoke();
    }

    private IEnumerator BurstFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

            if (Random.value >= 0.7f)
            {
                if (Random.value > 0.5f)
                {
                    var amount = Random.Range(20, 40);
                    for (var i = 0; i < amount; i++)
                    {
                        ExpendResource(Random.Range(1, 5));
                        yield return new WaitForSeconds(Random.Range(0.01f, 0.05f));
                    }
                }
                else
                {
                    var amount = Random.Range(80, 120);
                    for (var i = 0; i < amount; i++)
                    {
                        ExpendResource(Random.Range(1, 5));
                        yield return new WaitForSeconds(Random.Range(0.01f, 0.05f));
                    }
                }
            }    
        }
    }

    private void ExpendResource(int amount)
    {
        if (Available <= 0) return;

        Available = Math.Max(0, Available - amount);

        ResourceExpended.Invoke();
    }
}
