using Assets.Scripts.Content.GameContent;
using Assets.Scripts.GameInformation;
using Assets.Scripts.Menus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus {
    public class PokemonBattleMenu : Menu
    {
        public Text allyName;
        public Text allyLvl;
        public Text allyHP;
        public Image allyHPBar;
        public Image allyPokemonSprite;

        //add in pokemon colors?
        public Text enemyName;
        public Text enemyLvl;
        public Text enemyHP;
        public Image enemyHPBar;
        public Image enemyPokemonSprite;

        public Image background;

        public GameObject yourPokemon;
        public GameObject enemyPokemon;

        public Image enemyTrainerImage;

        /// <summary>
        /// Your pokemon you are using
        /// </summary>
        public Pokemon self;
        /// <summary>
        /// The other pokemon.
        /// </summary>
        public Pokemon other;

        public void Awake()
        {
            this.canvas = this.gameObject.transform.Find("Canvas").gameObject;
            background = this.canvas.transform.Find("Background").gameObject.GetComponent<Image>();

            yourPokemon = background.gameObject.transform.Find("YourPokemon").gameObject;
            allyPokemonSprite = yourPokemon.GetComponent<Image>();
            allyName = yourPokemon.transform.Find("Name").gameObject.GetComponent<Text>();
            allyLvl = yourPokemon.transform.Find("Lvl").gameObject.GetComponent<Text>();
            allyHPBar = yourPokemon.transform.Find("HealthUnderlay").gameObject.GetComponent<Image>();
            allyHP = allyHPBar.transform.Find("Health").gameObject.GetComponent<Text>();

            yourPokemon.SetActive(false);


            enemyPokemon = background.gameObject.transform.Find("EnemyPokemon").gameObject;
            enemyPokemonSprite = enemyPokemon.GetComponent<Image>();
            enemyName = enemyPokemon.transform.Find("Name").gameObject.GetComponent<Text>();
            enemyLvl = enemyPokemon.transform.Find("Lvl").gameObject.GetComponent<Text>();
            enemyHPBar = enemyPokemon.transform.Find("HealthUnderlay").gameObject.GetComponent<Image>();
            enemyHP = enemyHPBar.transform.Find("Health").gameObject.GetComponent<Text>();

            enemyPokemon.SetActive(false);

            enemyTrainerImage= background.gameObject.transform.Find("EnemyTrainer").gameObject.GetComponent<Image>();
        }

        public override void Start()
        {

        }

        public override void exitMenu()
        {

        }

        public override void setUpForSnapping()
        {

        }

        public override bool snapCompatible()
        {
            return false;
        }

        public override void Update()
        {

        }

        public void setUpEnemyTrainer(PokemonTrainer Trainer)
        {
            enemyTrainerImage.enabled = true;
            enemyTrainerImage.sprite = Trainer.trainerSprite;
        }




        public void setUpBattlers()
        {
            setUpSelf();
            setUpOther();
        }


        /// <summary>
        /// Sets up all the battlers.
        /// </summary>
        /// <param name="Self"></param>
        /// <param name="Other"></param>
        public void setUpBattlers(Pokemon Self = null, Pokemon Other = null)
        {
            if (Self != null)
            {
                self = Self;
            }
            if (Other != null)
            {
                other = Other;
            }
            setUpSelf();
            setUpOther();
        }

        public void setUpSelf(Pokemon Self = null)
        {
            if (Self != null)
            {
                self = Self;
                setUpSelf();
            }
        }

        public void setUpOther(Pokemon Other = null)
        {
            if (Other != null)
            {
                other = Other;
                setUpOther();
            }
        }


        /// <summary>
        /// Sets up your pokemon.
        /// </summary>
        private void setUpSelf()
        {
            if (self == null) return;
            if (allyPokemonSprite == null)
            {
                Debug.Log("ALLY SPRITE NULL");
            }
            else if (self.backSprite == null)
            {
                Debug.Log("SELF BACK SPRITE NULL");
            }
            yourPokemon.SetActive(true);
            allyPokemonSprite.sprite = self.backSprite;
            allyName.text = self.Name;
            allyLvl.text = "Lvl:" + self.currentLevel;
            allyHPBar.rectTransform.localScale = new Vector3(calculatePokemonHPRemaining(self), 1, 1);
            allyHP.text = self.currentHP + " / " + self.MaxHP;
            

            GameManager.Manager.soundManager.playSound(self.cry);
        }


        /// <summary>
        /// Sets up the enemy pokemon.
        /// </summary>
        private void setUpOther()
        {
            if (other == null) return;
            enemyPokemon.SetActive(true);
            enemyTrainerImage.enabled = false;

            enemyPokemonSprite.sprite = other.frontSprite;
            enemyName.text = other.Name;
            enemyLvl.text = "Lvl:" + other.currentLevel;
            enemyHPBar.rectTransform.localScale = new Vector3(calculatePokemonHPRemaining(other), 1, 1);
            enemyHP.text = other.currentHP + " / " + other.MaxHP;
            GameManager.Manager.soundManager.playSound(other.cry);
        }


        /// <summary>
        /// Used to update pokemon I guess??? Not sure why I wrote this.
        /// </summary>
        private void updatePokemon()
        {

        }

        private float calculatePokemonHPRemaining(Pokemon p)
        {
            return p.currentHP / p.MaxHP;
        }
    }
}
