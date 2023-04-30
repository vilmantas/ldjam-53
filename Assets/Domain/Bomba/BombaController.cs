using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaController : MonoBehaviour
{
    public static GameplayManager Manager;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        Manager.DoBomba(transform.position);
        
        Destroy(gameObject);
    }
}
