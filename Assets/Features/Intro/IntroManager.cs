using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    private GameManager manager;
    
    void Start()
    {
        manager = GameObject.Find("game_manager").GetComponent<GameManager>();
    }

    public void OnStartClicked()
    {
        manager.StartGame();
    }
}
