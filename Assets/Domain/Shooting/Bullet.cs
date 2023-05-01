using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rigidbody;
    public float MinBulletSpeed = 30;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ( rigidbody.velocity.magnitude < MinBulletSpeed)
        {
            Destroy(gameObject);
        }
    }
}