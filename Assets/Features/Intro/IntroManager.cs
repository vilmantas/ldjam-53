using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    private GameManager manager;

    public AudioSource Audio;

    public Slider Slider;
    
    void Start()
    {
        manager = GameObject.Find("game_manager").GetComponent<GameManager>();
        Audio.Play();
    }

    public void OnStartClicked()
    {
        manager.StartGame();
    }

    public void OnVolumeChange()
    {
        manager.Volume = Slider.value;
        Audio.volume = Slider.value;
      //  Audio.Play();
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
