using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BridgeDestroyer : MonoBehaviour
{
    public int Ammount = 2;
    private int _ammountDestroyed = 0;

    public float TimeDelayInS = 180;
    public List<Bridge> Bridges = new List<Bridge>();

    void Start()
    {
        InvokeRepeating("DestroyBridge", TimeDelayInS, TimeDelayInS);
    }

    // Update is called once per frame
    void DestroyBridge()
    {
        if(_ammountDestroyed < Ammount)
        {
            bool destroyed = false;
            
                while (!destroyed)
            {
                var destroyIndex = Random.Range(0, Bridges.Count);
                if (!Bridges[destroyIndex].Destroyed)
                {
                    Bridges[destroyIndex].Destroyed = true;
                    Bridges[destroyIndex].GoodBridge.SetActive(false);
                    Bridges[destroyIndex].BrokenBridge.SetActive(true);
                    destroyed = true;
                    _ammountDestroyed++;
                }
            }
        }
    }
}
[System.Serializable]
public class Bridge
{
    public GameObject GoodBridge;
    public GameObject BrokenBridge;
    public bool Destroyed;
}
