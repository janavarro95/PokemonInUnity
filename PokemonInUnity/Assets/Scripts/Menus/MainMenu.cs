using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Menus.Components;
using Assets.Scripts.Utilities.Timers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    /// <summary>
    /// Deals with the main menu.
    /// </summary>
    public class MainMenu:Menu
    {

        public AudioClip titleMusic;
        Image background;

        RandomPokemonImageScript rando;

        DeltaTimer cryTimer;

        /// <summary>
        /// Instantiate all menu logic here.
        /// </summary>
        public override void Start()
        {
            this.canvas=this.transform.Find("Canvas").gameObject;
            background = canvas.transform.Find("Background").gameObject.GetComponent<Image>();
            background.rectTransform.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
            scaleMenuToSceen();

            if (titleMusic!=null){
                GameManager.SoundManager.playSong(titleMusic);
            }

            rando = canvas.transform.Find("PokemonImage").GetComponent<RandomPokemonImageScript>();
        }

        public override void setUpForSnapping()
        {

        }

        public override bool snapCompatible()
        {
            return false;
        }

        /// <summary>
        /// Runs ~60 times a second.
        /// </summary>
        public override void Update()
        {
            if (GameInput.InputControls.APressed)
            {
                GameManager.SoundManager.playSound(rando.cry);
                cryTimer = new DeltaTimer(3d, Enums.TimerType.CountDown,false,swapScenes);
                cryTimer.start();
            }
            if (cryTimer != null) cryTimer.Update();
        }

        private void swapScenes()
        {
            GameManager.SoundManager.stopSong();
            SceneManager.LoadScene("LoadingScene");
        }

        /// <summary>
        /// Close the active menu.
        /// </summary>
        public override void exitMenu()
        {
            Destroy(this.gameObject);
            Menu.ActiveMenu = null;
        }   
    }
}
