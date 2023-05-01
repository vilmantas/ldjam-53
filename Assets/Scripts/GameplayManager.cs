using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Features.Camera;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameplayManager : MonoBehaviour
{
    public GameObject Truck;

    public Action OnDropOffAvailable;
    
    public Action OnPickupAvailable;

    public Action OnLeavingZone;

    public List<DropOffZone> ActiveZones;

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

    public int RespanwsLeft;

    public Transform Spawn;

    public GameObject BombaPrefab;

    public GameObject BombaParticles;

    public int BombaRadius;

    public int BombaStrength;

    public float BombaRateFrom;
    
    public float BombaRateTo;

    public GameManager m_GameManager;
    
    void Awake()
    {
        m_GameManager = FindFirstObjectByType<GameManager>();
        
        var audios = FindObjectsOfType<AudioSource>();

        foreach (var audio in audios)
        {
            audio.volume = m_GameManager.Volume;
        }
        
        var bombaConfigs = FindObjectsOfType<BombaConfiguration>();

        foreach (var bomba in bombaConfigs.ToList())
        {
            var renderer = bomba.GetComponent<MeshRenderer>();

            var bounds = renderer.bounds;
            
            StartCoroutine(SpawnBomba(bomba, bounds));
        }
        
        BombaController.Manager = this;
        
        Spawn = FindObjectOfType<SpawnPoint>()?.transform;
        
        Truck = FindObjectOfType<Driving>().transform.root.gameObject;

        LevelConfiguration = FindObjectOfType<LevelConfiguration>();

        RespanwsLeft = 3;
        
        if (LevelConfiguration)
        {
            RespanwsLeft = LevelConfiguration.RespawnsAllowed;
        }
        
        inv = Truck.GetComponentInChildren<Inventory>();

        inv.OnZoneLeft += LeavingTriggerZone;
        
        inv.OnInRangeOfPickup += OnInRangeOfPickup;
        inv.OnInRangeOfDropOff += OnInRangeOfDropOff;
        
        inv.OnInvalidLocationReached += OnInvalidLocationReached;
        
        inv.OnPickupAction += OnPickupAction;
        inv.OnDropOffAction += OnDropOffAction;
        
        inv.OnActionAttempt += OnActionAttempt;
        
        ActiveZones = FindObjectsOfType<DropOffZone>().ToList();

        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    private void OnInvalidLocationReached()
    {
        ResetPlayer();
    }

    public IEnumerator SpawnBomba(BombaConfiguration config, Bounds bounds)
    {
        while (!IsGameOver || !IsLevelCompleted)
        {
            yield return new WaitForSeconds(Random.Range(config.BombaRateFrom, config.BombaRateTo));

            var target = new Vector3(
                UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
                UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
            );
            
            var ins = Instantiate(BombaPrefab, target, Quaternion.identity);
            
            Destroy(ins, 10f);
        }
    }

    public void DoBomba(Vector3 position)
    {
        var instance = Instantiate(BombaParticles, position, Quaternion.identity);

        Vector3 explosionPos = position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, BombaRadius);

        var truck = colliders.FirstOrDefault(x => x.name == "Truck");

        if (truck != null)
        {
            Rigidbody rb = truck.GetComponentInParent<Rigidbody>();
            
            rb.AddExplosionForce(BombaStrength, explosionPos, 0f, 3.0F, ForceMode.Impulse);
        }
        
        Destroy(instance, 3f);
    }
    
    private void OnActionAttempt()
    {
        if (ActivePickup)
        {
            var item = ActivePickup.GetItem();

            var duration = 2f;

            if (LevelConfiguration.ItemDurations.ContainsKey(item))
                duration = LevelConfiguration.ItemDurations[item].LoadDuration;
            
            inv.DoPickup(item, duration);
        } 
        else if (ActiveDropOff)
        {
            var item = ActiveDropOff.NeededItem;

            var duration = 2f;

            if (LevelConfiguration.ItemDurations.ContainsKey(item))
                duration = LevelConfiguration.ItemDurations[item].LoadDuration;
            
            inv.DoDropOff(item, duration);
        }
    }

    private void ResetPlayer()
    {
        if (RespanwsLeft > 0)
        {
            var inv = Truck.GetComponentInChildren<Inventory>();

            if (Spawn == null) return;
                
            inv.ClearInventory();
                
            Truck.transform.position = Spawn.position;
            Truck.transform.rotation = Spawn.rotation;

            var rb = Truck.GetComponent<Rigidbody>();
                
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
                
            RespanwsLeft--;
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Intro");

            return;
        }
        
        if (IsLevelCompleted && Input.GetKeyDown(KeyCode.Return))
        {
            m_GameManager.StartNextLevel();
        }

        if (InGracePeriod)
        {
            CheckIfGameOver();
        } 
        
        if (IsLevelCompleted || IsGameOver)
        { 
            if (Input.GetKeyDown(KeyCode.R))
            {
                m_GameManager.RestartLevel();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayer();
        }
        
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
        SceneManager.UnloadSceneAsync("_UI");
        OnLevelDone?.Invoke();
    }
    
    public void CheckDropOffs()
    {
        if (ActiveZones.Any(x => x.HasDepletedResource)) StartGracePeriod();
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
        if (m_GameManager.IsLastLevel)
        {
            SceneManager.LoadScene("GameDone");

            return;
        } 
        Time.timeScale = 0f;
        IsLevelCompleted = true;
        SceneManager.LoadScene("Victory", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("_UI");
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

    private void OnDropOffAction(ItemPickup.ItemType type)
    {
        var amount = 1000;
        
        if (ActiveDropOff == null)
        {
            inv.Pickup(type);
            return;
        }
        
        ActiveDropOff.ReceiveResource(amount);
    }

    private void OnPickupAction(ItemPickup.ItemType type)
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
