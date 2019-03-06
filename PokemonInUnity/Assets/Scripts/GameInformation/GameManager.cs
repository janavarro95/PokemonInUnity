using Assets.Scripts.Characters.Player;
using Assets.Scripts.Utilities.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameInformation
{

    public class GameManager : MonoBehaviour
    {

        public static GameManager Manager;
        public static GameOptions Options
        {
            get
            {
                return Manager.options;
            }
        }
        public static GameSoundManager SoundManager
        {
            get
            {
                return Manager.soundManager;
            }
            set
            {
                Manager.soundManager = value;
            }
        }
        public static PlayerInfo Player
        {
            get
            {
                return Manager.player;
            }
        }


        public Serializer serializer;
        public GameOptions options;
        public GameSoundManager soundManager;
        public DialogueManager dialogueManager;
        public Interactables.Interactable currentInteractable;
        public MapManager currentMap;
        public SoundEffects soundEffects;


        public PlayerInfo player;
        /// <summary>
        /// Initializing the game manager.
        /// </summary>
        private void Awake()
        {
            if (Manager != null)
            {
                Destroy(this.gameObject);
                return;
            }

            Manager = this;
            DontDestroyOnLoad(this.gameObject);
            initializeGame();
            serializer = new Serializer();
            options = new GameOptions();
            this.gameObject.AddComponent<GameSoundManager>();
            this.dialogueManager=this.gameObject.AddComponent<DialogueManager>();
            this.soundEffects = this.gameObject.transform.Find("SoundEffects").GetComponent<SoundEffects>();

            player = new PlayerInfo();
        }

        // Start is called before the first frame update
        void Start()
        {

        }


        /// <summary>
        /// Initializes the game.
        /// </summary>
        private void initializeGame()
        {
            if (Serializer.JSONSerializer == null) Serializer.JSONSerializer = new Assets.Scripts.Utilities.Serialization.Serializer();

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnLoaded;


        }

        public bool isObjectActiveInteractable(GameObject o)
        {
            if (o.GetComponent<Interactables.Interactable>() == null) return false;
            if (currentInteractable == null) return false;
            return this.currentInteractable.gameObject == o;
        }

        /// <summary>
        /// When a scene is unloaded it calls this info here.
        /// </summary>
        /// <param name="arg0"></param>
        private void OnSceneUnLoaded(Scene arg0)
        {

        }

        /// <summary>
        /// When a scene is loaded it does some set up stuff here.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="arg1"></param>
        private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
        {

        }

        // Update is called once per frame
        void Update()
        {


        }
    }
}