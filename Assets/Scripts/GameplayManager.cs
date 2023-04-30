using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Features.Camera;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public GameObject Truck;

    public Action OnDropOffAvailable;
    
    public Action OnPickupAvailable;

    public Action OnLeavingZone;

    public List<ItemDropOff> ActiveZones;

    public ItemDropOff ActiveDropOff;

    public ItemPickup ActivePickup;

    public LevelConfiguration LevelConfiguration;

    public float TimePassed = 0f;

    public Action OnLevelDone;

    public bool IsLevelCompleted;

    public bool IsPaused;

    public bool InGracePeriod;

    public float GracePeriodStart;

    public bool IsGameOver;

    public Inventory inv;
    
    void Awake()
    {
        Truck = GameObject.Find("Truck");

        LevelConfiguration = GameObject.Find("Configuration")?.GetComponent<LevelConfiguration>();

        inv = Truck.GetComponentInChildren<Inventory>();

        inv.OnZoneLeft += LeavingTriggerZone;
        
        inv.OnInRangeOfPickup += OnInRangeOfPickup;
        inv.OnInRangeOfDropOff += OnInRangeOfDropOff;
        
        inv.OnPickupAction += OnPickupAction;
        inv.OnDropOffAction += OnDropOffAction;
        
        inv.OnActionAttempt += OnActionAttempt;
        
        CameraManager.Instance.ChangeTarget(Truck.gameObject.transform, Truck.gameObject.transform);

        ActiveZones = FindObjectsOfType<ItemDropOff>().ToList();

        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    private void OnActionAttempt()
    {
        if (ActivePickup)
        {
            var item = ActivePickup.GetItem();
            
            inv.DoPickup(item, LevelConfiguration.ItemDurations[item].LoadDuration);
        } 
        else if (ActiveDropOff)
        {
            var item = ActiveDropOff.NeededItem;
            
            inv.DoDropOff(item, LevelConfiguration.ItemDurations[item].OffloadDuration);
        }
    }

    private void Update()
    {
        if (IsLevelCompleted && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // LOAD NEXT LEVEL LMAOOOO
        }

        if (InGracePeriod)
        {
            CheckIfGameOver();
        } 
        
        if (IsLevelCompleted) return;
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (IsPaused) Resume();
            else Pause();
        }
        
        CheckDropOffs();
        
        if (!LevelConfiguration) return;

        if (TimePassed >= LevelConfiguration.TimeToSurviveMinutes * 60)
        {
            LevelCompleted();
        }
        
        TimePassed += Time.deltaTime;
    }

    public void CheckIfGameOver()
    {
        if (LevelConfiguration == null) return;
        
        var g = TimePassed - GracePeriodStart > LevelConfiguration.LoseGracePeriod;
        
        if (g) GameOver();
    }
    
    public void GameOver()
    {
        if (IsGameOver) return;
        
        Time.timeScale = 0f;
        IsGameOver = true;
        SceneManager.LoadScene("Lose", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("UI");
        OnLevelDone?.Invoke();
    }
    
    public void CheckDropOffs()
    {
        if (ActiveZones.Any(x => x.IsDepleted)) StartGracePeriod();
        else StopGracePeriod();
    }
    
    public void StopGracePeriod()
    {
        if (!InGracePeriod) return;
        
        InGracePeriod = false;

        GracePeriodStart = TimePassed;
    }

    public void StartGracePeriod()
    {
        if (InGracePeriod) return;
        
        InGracePeriod = true;

        GracePeriodStart = TimePassed;
    }

    public void LevelCompleted()
    {
        Time.timeScale = 0f;
        IsLevelCompleted = true;
        SceneManager.LoadScene("Victory", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("UI");
        OnLevelDone?.Invoke();
    }

    public void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    }

    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("Pause");

    }

    private void OnDropOffAction()
    {
        ActiveDropOff.ReceiveResource(1000);
    }

    private void OnPickupAction()
    {
        // IDK
    }

    private void OnInRangeOfDropOff(ItemDropOff obj)
    {
        ActiveDropOff = obj;
        OnDropOffAvailable.Invoke();
    }

    private void OnInRangeOfPickup(ItemPickup obj)
    {
        ActivePickup = obj;
        OnPickupAvailable.Invoke();
    }

    public void LeavingTriggerZone()
    {
        ActivePickup = null;
        ActiveDropOff = null;
        OnLeavingZone.Invoke();
    }

    private void OnGameLoaded()
    {
        
    }
}
