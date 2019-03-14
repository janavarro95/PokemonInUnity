using Assets.Scripts.GameInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{


    public AudioClip selectSound;
    public AudioClip bumpSound;
    public AudioClip enterDoor;
    public AudioClip exitDoor;
    public AudioClip menuOpen;
    public AudioClip itemGet;


    public AudioClip wildBattleMusic;
    public AudioClip trainerBattleMusic;
    public AudioClip wildVictoryMusic;
    public AudioClip trainerVictoryMusic;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void playBumpSound()
    {
        GameManager.SoundManager.playSound(bumpSound);
    }

    public void playSelectSound()
    {
        GameManager.SoundManager.playSound(selectSound);
    }

    public void playMenuSound()
    {
        GameManager.SoundManager.playSound(menuOpen);
    }

    public void playItemGetSound()
    {
        GameManager.SoundManager.playSound(itemGet);
    }


    public void playWildBattleSong()
    {
        GameManager.SoundManager.playSong(wildBattleMusic);
    }

    public void playTrainerBattleSong()
    {
        GameManager.SoundManager.playSong(trainerBattleMusic);
    }

    public void playWildVictorySong()
    {
        GameManager.SoundManager.playSong(wildVictoryMusic);
    }

    public void playTrainerVictorySong()
    {
        GameManager.SoundManager.playSong(trainerVictoryMusic);
    }
}
