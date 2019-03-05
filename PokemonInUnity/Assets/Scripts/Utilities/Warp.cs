using Assets.Scripts.GameInformation;
using Assets.Scripts.Utilities.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Utilities
{
    public class Warp:MonoBehaviour
    {
        [SerializeField]
        private bool pressAToWarp;
        [SerializeField]
        private string sceneToWarpTo;
        [SerializeField]
        private Vector2 warpLocation;

        [SerializeField]
        private float transitionTime = .5f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (pressAToWarp)
                {
                    if (Assets.Scripts.GameInput.InputControls.APressed)
                    {
                        ScreenTransitions.StartSceneTransition(transitionTime, sceneToWarpTo, Color.black, ScreenTransitions.TransitionState.FadeOut, new VoidDelegate(finishedTransition));
                    }
                }
                else
                {
                    ScreenTransitions.StartSceneTransition(transitionTime, sceneToWarpTo, Color.black, ScreenTransitions.TransitionState.FadeOut, new VoidDelegate(finishedTransition));
                }
            }
        }

        private void finishedTransition()
        {
            GameManager.Player.gameObject.transform.position = warpLocation;
            SceneManager.LoadScene(sceneToWarpTo);
            //ScreenTransitions.StartSceneTransition(transitionTime, "", Color.black, ScreenTransitions.TransitionState.FadeIn);
            ScreenTransitions.PrepareForSceneFadeIn(.5f, Color.black);
        }
    }
}
