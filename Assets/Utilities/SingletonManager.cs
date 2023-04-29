using System;
using UnityEngine;

public class SingletonManager<T> : MonoBehaviour where T : class
{
    [NonSerialized] public static T Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this as T;

        DoSetup();
    }

    protected virtual void DoSetup()
    {
    }
}
