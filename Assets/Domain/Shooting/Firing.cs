using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    public Transform PlayerTransform;
    public GameObject bullet;
    public float variance = 0.1f;
    public float gunSpeed = 0.2f;
    public float magazineSize = 10;
    public float reloadTime = 3;
    public float bulletSpeed = 5;
    public Transform BulletSpawn;
    public Transform BulletAudioSpawn;
    public GameObject BulletAudioPrefab;

    private void OnEnable()
    {
        InvokeRepeating("FireMagazine", reloadTime * Random.value * 3, reloadTime + Random.value * 3);
    }

    void FireMagazine()
    {
        for (int n = 0; n< magazineSize; n++)
        {
            Invoke("Fire", n* gunSpeed);
        }
    }

    // Update is called once per frame
    void Fire()
    {
        var audioInstance = Instantiate(BulletAudioPrefab, BulletAudioSpawn);
        Destroy(audioInstance, 1);
        var instance = Instantiate(bullet);
        instance.transform.position = BulletSpawn.position;
        var body = instance.GetComponent<Rigidbody>();
        var target = new Vector3( PlayerTransform.position.x + Random.Range(-1*variance,variance),
            PlayerTransform.position.y + Random.Range(-1 * variance/2, variance/2),
            PlayerTransform.position.z + Random.Range(-1 * variance, variance)
        );

        body.velocity =  (target - transform.position)* bulletSpeed;
        Destroy(instance, 3);
    }
}