using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameInformation {
    public class MapManager : MonoBehaviour
    {

        public AudioClip songToPlay;
        public Color mapColor = Color.white;

        private void Awake()
        {
            GameManager.Manager.currentMap = this;
        }

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
}