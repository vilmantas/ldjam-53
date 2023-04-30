using System;
using UnityEngine;
using UnityEngine.UI;

public class LoaderDisplayController : MonoBehaviour
{
    public Image img;
    
    public GameplayManager p_GameplayManager;

    public GameObject LoaderDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        var gameplayManager = GameObject.Find("Manager");

        if (gameplayManager == null) return;

        var gg = gameplayManager.GetComponent<GameplayManager>();
        
        if (gg == null)
        {
            print(" UI FAILED TO INITIALIZE");
            return;
        }

        p_GameplayManager = gg;
        
        p_GameplayManager.inv.OnStartAction += ShowLoader;
        p_GameplayManager.inv.OnStopAction += OnStopAction;
    }

    private void OnStopAction()
    {
        Loading = false;
        
        LoaderDisplay.SetActive(false);
    }

    private bool Loading = false;

    private float Max;

    private float Current;
    
    private void ShowLoader(float obj)
    {
        Max = obj;

        Current = 0f;

        LoaderDisplay.SetActive(true);
        
        Loading = true;
    }

    private void Update()
    {
        if (!Loading) return;

        Current += Time.deltaTime;

        img.fillAmount = Mathf.Lerp(0, 1, Current / Max);

    }
}
