using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public AudioClip songToPlay;

    // Start is called before the first frame update
    void Start()
    {
        if (this.songToPlay != null)
        {
            Assets.Scripts.GameInformation.GameManager.SoundManager.playSong(songToPlay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
