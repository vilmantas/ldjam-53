using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemDropOff : MonoBehaviour
{ 
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
    private bool _firstRun = true;
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
            if (!_firstRun)
            {
                yield return new WaitForSeconds(Random.Range(RateFromSeconds, RateToSeconds));
            }
            else
            {
                yield return new WaitForSeconds(3);//eh
            }
            _firstRun = false;

            if (Random.value >= 1-SuccessRate)
            {
                if (Random.value >= HighUsageChance)
                {
                    var amount = Random.Range(LowMin, LowMax);
                    for (var i = 0; i < amount; i++)
                    {
                        ExpendResource(Random.Range(ResourceExpenseRateMin, ResourceExpenseRateMax));
                        yield return new WaitForSeconds(Random.Range(0.002f, 0.008f));
                    }
                }
                else
                {
                    var amount = Random.Range(HighMin, HighMax);
                    for (var i = 0; i < amount; i++)
                    {
                        ExpendResource(Random.Range(ResourceExpenseRateMin, ResourceExpenseRateMax));
                        yield return new WaitForSeconds(Random.Range(0.002f, 0.008f));
                    }
                }
            }    
        }
    }

    private void ExpendResource(int amount)
    {
        if (Available <= 0) return;

        Available = Math.Max(0, Available - amount);

        ResourceExpended?.Invoke();
    }
}
