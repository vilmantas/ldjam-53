using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaController : MonoBehaviour
{
    public static GameplayManager Manager;

    private void OnCollisionEnter(Collision collision)
    {
        Manager.DoBomba(transform.position);
        
        Destroy(gameObject);
    }
}
