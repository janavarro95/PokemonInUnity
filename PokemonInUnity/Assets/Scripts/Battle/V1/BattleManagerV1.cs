using Assets.Scripts.Content.GameContent;
using Assets.Scripts.GameInformation;
using Assets.Scripts.Interactables;
using Assets.Scripts.Menus;
using Assets.Scripts.Utilities.Delegates;
using Assets.Scripts.Utilities.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Battle.V1
{
    public class BattleManagerV1 : Menu
    {

        public BattleDialogueManager battleDialogue;
        public PokemonBattleMenu pokemonBattleScreen;



        public DeltaTimer delayTimer;
        public Pokemon currentSelf;
        public Pokemon currentOther;


        public PokemonTrainer enemyTrainer;

        public bool isWildPokemon
        {
            get
            {
                return enemyTrainer == null;
            }
        }

        public void Awake()
        {
            battleDialogue = this.gameObject.transform.Find("BattleDialogueManager").gameObject.GetComponent<BattleDialogueManager>();
            pokemonBattleScreen = this.gameObject.transform.Find("PokemonBattleMenu").gameObject.GetComponent<PokemonBattleMenu>();
        }

        public override void Start()
        {

        }

        public override void setUpForSnapping()
        {

        }

        public override bool snapCompatible()
        {
            return false;
        }

        public override void exitMenu()
        {
            base.exitMenu();
            Menu.ActiveMenu = null;

            Menu.ExitAllMenus();
        }



        //~~~~~~~Pokemon Trainer Set-up~~~~~~~//
        #region
        /// <summary>
        /// Sets up the battle as a trainer battle.
        /// </summary>
        /// <param name="Trainer"></param>
        public virtual void setUpTrainerBattle(PokemonTrainer Trainer)
        {
            currentSelf = GameManager.Player.pokemon.getFirstNonFaintedPokemon();
            if (currentSelf == null)
            {
                Debug.Log("WHY SELF IS POKEMON NULLLL");
            }

            currentOther = Trainer.pokemon.getFirstNonFaintedPokemon(); //Which should be the first.

            if (currentOther == null)
            {
                Debug.Log("WHY IS OTHER NULL???");
            }
            enemyTrainer = Trainer;


            showTrainerIntro();


        }

        /// <summary>
        /// Shows the enemy trainer's sprite.
        /// </summary>
        public virtual void showTrainerIntro()
        {

            UnityEvent selectPokeEvent = new UnityEvent();
            selectPokeEvent.AddListener(showTrainerSelectedPokemon);

            pokemonBattleScreen.setUpEnemyTrainer(enemyTrainer);

            this.battleDialogue.initializeDialogues(enemyTrainer.trainerName, new List<string>() {
                enemyTrainer.trainerTitle+" "+enemyTrainer.trainerName+" wants to battle!"
            }, selectPokeEvent);

            /*
            delayTimer = new DeltaTimer(0.5f, Enums.TimerType.CountDown, false, new VoidDelegate(setUpSelfPokemon));
            delayTimer.start();
            */
        }

        /// <summary>
        /// Shows the enemy trainer selecting a pokemon and you sending out yours.
        /// </summary>
        public virtual void showTrainerSelectedPokemon()
        {
            UnityEvent finishSetUp = new UnityEvent();
            finishSetUp.AddListener(beginBattle);

            List<DialogueEvent> events = new List<DialogueEvent>();

            DialogueEvent enemySetUp = new DialogueEvent(0);
            enemySetUp.function.AddListener(setUpOtherPokemonTrainerBattle);
            DialogueEvent selfSetUp = new DialogueEvent(1);
            selfSetUp.function.AddListener(setUpSelfPokemonTrainerBattle);
            events.Add(enemySetUp);
            events.Add(selfSetUp);


            this.battleDialogue.initializeDialogues(enemyTrainer.trainerName, new List<string>() {
                enemyTrainer.trainerTitle+" "+enemyTrainer.trainerName+" sent out "+enemyTrainer.pokemon.getFirstNonFaintedPokemon().Name,
                "Go "+GameManager.Player.pokemon.getFirstNonFaintedPokemon().Name+"! I choose you!"
            }, finishSetUp, null, events);
        }

        public virtual void setUpSelfPokemonTrainerBattle()
        {
            pokemonBattleScreen.setUpBattlers(currentSelf, null);
            delayTimer.start();
        }

        public virtual void setUpOtherPokemonTrainerBattle()
        {
            pokemonBattleScreen.setUpBattlers(null, currentOther);
        }

        #endregion
        //~~~~~~~End Pokemon Trainer Set-up~~~~~~~//


        //~~~~~~ Pokemon Wild Battle Set-Up~~~~~~//
        #region

        /// <summary>
        /// Initializes a wild pokemon battle.
        /// </summary>
        /// <param name="Other"></param>
        public virtual void setUpWildBattle(Pokemon Other)
        {
            currentSelf = GameManager.Player.pokemon.getFirstNonFaintedPokemon();
            currentOther = Other;
            enemyTrainer = null;
            delayTimer = new DeltaTimer(0.5f, Enums.TimerType.CountDown, false, new VoidDelegate(setUpOtherPokemonWildBattle));
            delayTimer.start();
        }

        /// <summary>
        /// Sets up the player's pokemon for the battle.
        /// </summary>
        public virtual void setUpSelfPokemonWildBattle()
        {
            pokemonBattleScreen.setUpBattlers(currentSelf, null);
            beginBattle();
        }

        /// <summary>
        /// Sets up the wild pokemon for the battle.
        /// </summary>
        public virtual void setUpOtherPokemonWildBattle()
        {
            pokemonBattleScreen.setUpBattlers(null, currentOther);

            UnityEvent selectPokeEvent = new UnityEvent();
            selectPokeEvent.AddListener(setUpSelfPokemonWildBattle);

            pokemonBattleScreen.setUpEnemyTrainer(enemyTrainer);

            this.battleDialogue.initializeDialogues(enemyTrainer.trainerName, new List<string>() {
                "A wild "+currentOther.Name+" has appeared!"
            }, selectPokeEvent);

        }
        #endregion
        //~~~~~~ End Pokemon Wild Battle Set-Up~~~~~~//
        /// <summary>
        /// Do post-intro battle goodies here.
        /// </summary>
        public virtual void beginBattle()
        {
            Debug.Log("Battle has begun!");
        }

        public override void Update()
        {
            if (Menu.ActiveMenu == this)
            {
                //IDK
                if (GameInput.InputControls.StartPressed)
                {
                    exitMenu();
                }
            }

            if (delayTimer != null) delayTimer.Update();

        }

    }


}
