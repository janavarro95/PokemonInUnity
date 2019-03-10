using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Menus.Components;
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
        Image background;

        /// <summary>
        /// Instantiate all menu logic here.
        /// </summary>
        public override void Start()
        {
            GameObject canvas=this.transform.Find("Canvas").gameObject;
            background = canvas.transform.Find("Background").gameObject.GetComponent<Image>();
            background.rectTransform.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
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
                SceneManager.LoadScene("LoadingScene");
            }
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
