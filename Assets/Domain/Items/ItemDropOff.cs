using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemDropOff : MonoBehaviour
{
    public string Name;
    
    public ItemPickup.ItemType NeededItem;

    public int Available;
    
    public int Max;

    public Action ResourceExpended;

    public bool IsDepleted => Available <= 0;
    
    public float RateFromSeconds;
    
    public float RateToSeconds;
    
    public float SuccessRate;
    
    public float HighUsageChance;

    [Header("Usage Amounts")]
    public int LowMin;

    public int LowMax;
    
    public int HighMin;

    public int HighMax;
    
    public int ResourceExpenseRateMin;

    public int ResourceExpenseRateMax;
    
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
            yield return new WaitForSeconds(Random.Range(RateFromSeconds, RateToSeconds));

            if (Random.value >= 1-SuccessRate)
            {
                if (Random.value >= HighUsageChance)
                {
                    var amount = Random.Range(LowMin, LowMax);
                    for (var i = 0; i < amount; i++)
                    {
                        ExpendResource(Random.Range(ResourceExpenseRateMin, ResourceExpenseRateMax));
                        yield return new WaitForSeconds(Random.Range(0.01f, 0.05f));
                    }
                }
                else
                {
                    var amount = Random.Range(HighMin, HighMax);
                    for (var i = 0; i < amount; i++)
                    {
                        ExpendResource(Random.Range(ResourceExpenseRateMin, ResourceExpenseRateMax));
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
