using Assets.Scripts.Content.GameContent;
using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Menus;
using Assets.Scripts.Menus.Components;
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
    public class BattleActionSelectionMenu:Menu
    {
        public MenuComponent fight;
        public MenuComponent pokemon;
        public MenuComponent item;
        public MenuComponent run;

        public Pokemon currentPokemon;
        public bool isTrainerBattle;

        public override void Start()
        {
            ActiveMenu = this;
            
        }


        public virtual void initialize(Pokemon Pokemon,bool IsTrainer)
        {
            currentPokemon = Pokemon;
            setUpForSnapping();
            this.isTrainerBattle = IsTrainer;
        }

        public override void setUpForSnapping()
        {
            this.canvas = this.gameObject.transform.Find("Canvas").gameObject;

            this.menuCursor = canvas.transform.Find("GameCursor").gameObject.GetComponent<GameCursor>();

            GameObject background = canvas.transform.Find("Background").gameObject;
            fight = new MenuComponent(background.transform.Find("Fight").Find("SnapComponent").gameObject.GetComponent<Image>());
            pokemon = new MenuComponent(background.transform.Find("Pokemon").Find("SnapComponent").gameObject.GetComponent<Image>());
            item = new MenuComponent(background.transform.Find("Items").Find("SnapComponent").gameObject.GetComponent<Image>());
            run = new MenuComponent(background.transform.Find("Run").Find("SnapComponent").gameObject.GetComponent<Image>());

            this.selectedComponent = fight;
            fight.snapToThisComponent();

            fight.setNeighbors(null, pokemon, null, item);
            pokemon.setNeighbors(fight, null, null, run);
            item.setNeighbors(null, run, fight, null);
            run.setNeighbors(item, null, pokemon, null);

        }

        public void OnGUI()
        {
            if (Menu.ActiveMenu == this)
            {
                this.selectedComponent.snapToThisComponent();
            }
        }

        public override bool snapCompatible()
        {
            return true;
        }

        public override void exitMenu()
        {
            base.exitMenu();
            ActiveMenu = null;
        }

        public override void Update()
        {
            if (Menu.ActiveMenu != this) return;
            if (this.menuCursor.simulateMousePress(fight))
            {
                GameManager.Manager.soundEffects.playSelectSound();
            }
            else if (this.menuCursor.simulateMousePress(pokemon))
            {
                exitMenu();
                BattleManagerV1 battle = (BattleManagerV1)MenuStack.Find(menu => menu.GetType() == typeof(BattleManagerV1));
                Menu.Instantiate<PokemonPartyMenu>();
                (Menu.ActiveMenu as PokemonPartyMenu).onMenuClose.AddListener(battle.beginSelfTurn);
                (Menu.ActiveMenu as PokemonPartyMenu).onPokemonSelected.AddListener(battle.swapPokemonCallback);

                GameManager.Manager.soundEffects.playSelectSound();
            }
            else if (this.menuCursor.simulateMousePress(item))
            {
                GameManager.Manager.soundEffects.playSelectSound();
            }
            else if (this.menuCursor.simulateMousePress(run))
            {
                Debug.Log("Run");

                if (isTrainerBattle) {
                    this.exitMenu();
                    Debug.Log("RUN AWAY!");
                    BattleManagerV1 battle =(BattleManagerV1)MenuStack.Find(menu => menu.GetType() == typeof(BattleManagerV1));
                    UnityEvent after = new UnityEvent();
                    after.AddListener(battle.initializeBattleSelectionMenu);
                    battle.battleDialogue.initializeDialogues("", new List<string>()

                    {
                        "You can't run away from a trainer battle!"
                    },after);
                    return;
                }
                else
                {
                    this.exitMenu();
                    BattleManagerV1 battle = (BattleManagerV1)MenuStack.Find(menu => menu.GetType() == typeof(BattleManagerV1));
                    UnityEvent after = new UnityEvent();
                    after.AddListener(battle.runFromBattle);
                    battle.battleDialogue.initializeDialogues("", new List<string>()

                    {
                        "You ran away."
                    }, after);
                    return;
                }
            }

        }

    }
}
