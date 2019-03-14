using Assets.Scripts.Content;
using Assets.Scripts.Content.GameContent;
using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Menus;
using Assets.Scripts.Menus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{

    public class PokemonPartyMenu : Menu
    {

        public Image background;

        GameObject pokemon1;
        GameObject pokemon2;
        GameObject pokemon3;
        GameObject pokemon4;
        GameObject pokemon5;
        GameObject pokemon6;


        /// <summary>
        /// The actual pokemon data
        /// </summary>
        Pokemon poke1Info;
        Pokemon poke2Info;
        Pokemon poke3Info;
        Pokemon poke4Info;
        Pokemon poke5Info;
        Pokemon poke6Info;

        MenuComponent snap1;
        MenuComponent snap2;
        MenuComponent snap3;
        MenuComponent snap4;
        MenuComponent snap5;
        MenuComponent snap6;

        MenuComponent closeSnap;

        PartyMemberSelectMenu selectMenu;

        public UnityEvent onPokemonSelected;
        public UnityEvent onMenuClose;
        public Pokemon selectedPokemon;


        // Start is called before the first frame update
        public override void Start()
        {
            ActiveMenu = this;
            this.canvas = this.transform.Find("Canvas").gameObject;
            layerMenuOnTop();
            background = this.transform.Find("Canvas").Find("Background").gameObject.GetComponent<Image>();
            background.rectTransform.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

            pokemon1 = background.gameObject.transform.Find("Pokemon1").gameObject;
            pokemon2 = background.gameObject.transform.Find("Pokemon2").gameObject;
            pokemon3 = background.gameObject.transform.Find("Pokemon3").gameObject;
            pokemon4 = background.gameObject.transform.Find("Pokemon4").gameObject;
            pokemon5 = background.gameObject.transform.Find("Pokemon5").gameObject;
            pokemon6 = background.gameObject.transform.Find("Pokemon6").gameObject;

            closeSnap = new MenuComponent(background.gameObject.transform.Find("CloseText").gameObject.transform.Find("SnapComponent").GetComponent<Image>());

            this.menuCursor = canvas.transform.Find("GameCursor").gameObject.GetComponent<Assets.Scripts.GameInput.GameCursor>();

            setPokemon();

            scaleMenuToSceen();
            setUpForSnapping();

            if (this.onPokemonSelected == null)
            {
                this.onPokemonSelected = new UnityEvent();
            }
        }

        public override void Update()
        {
            if (Menu.ActiveMenu != this)
            {
                Debug.Log(Menu.ActiveMenu.GetType());
                if (selectMenu == null)
                {
                    return;
                }
                else if (ActiveMenu!=selectMenu){
                    return;
                }
            }
            checkForInput();
        }

        private void checkForInput()
        {
            if (selectMenu == null)
            {
                if (this.onPokemonSelected.GetPersistentEventCount() == 0 && GameInput.InputControls.APressed==true)
                {
                    this.onPokemonSelected.AddListener(openStatsMenu);
                }

                if(this.onMenuClose.GetPersistentEventCount()==0 && GameInput.InputControls.APressed == true)
                {
                    
                }

                if (snap1 != null)
                {
                    if (this.menuCursor.simulateMousePress(snap1))
                    {
                        Menu.Instantiate<PartyMemberSelectMenu>();
                        this.selectMenu = (PartyMemberSelectMenu)Menu.ActiveMenu;
                        GameManager.Manager.soundEffects.playSelectSound();
                    }
                }
                if (snap2 != null)
                {
                    if (this.menuCursor.simulateMousePress(snap2))
                    {
                        Menu.Instantiate<PartyMemberSelectMenu>();
                        this.selectMenu = (PartyMemberSelectMenu)Menu.ActiveMenu;
                        GameManager.Manager.soundEffects.playSelectSound();
                    }
                }
                if (snap3 != null)
                {
                    if (this.menuCursor.simulateMousePress(snap3))
                    {
                        Menu.Instantiate<PartyMemberSelectMenu>();
                        this.selectMenu = (PartyMemberSelectMenu)Menu.ActiveMenu;
                        GameManager.Manager.soundEffects.playSelectSound();
                    }
                }
                if (snap4 != null)
                {
                    if (this.menuCursor.simulateMousePress(snap4))
                    {
                        Menu.Instantiate<PartyMemberSelectMenu>();
                        this.selectMenu = (PartyMemberSelectMenu)Menu.ActiveMenu;
                        GameManager.Manager.soundEffects.playSelectSound();
                    }
                }
                if (snap5 != null)
                {
                    if (this.menuCursor.simulateMousePress(snap5))
                    {
                        Menu.Instantiate<PartyMemberSelectMenu>();
                        this.selectMenu = (PartyMemberSelectMenu)Menu.ActiveMenu;
                        GameManager.Manager.soundEffects.playSelectSound();
                    }
                }
                if (snap6 != null)
                {
                    if (this.menuCursor.simulateMousePress(snap6))
                    {
                        Menu.Instantiate<PartyMemberSelectMenu>();
                        this.selectMenu = (PartyMemberSelectMenu)Menu.ActiveMenu;
                        GameManager.Manager.soundEffects.playSelectSound();
                    }
                }
                if (this.menuCursor.simulateMousePress(closeSnap))
                {
                    GameManager.Manager.soundEffects.playSelectSound();
                    this.exitMenu();
                    if (onMenuClose != null) onMenuClose.Invoke();
                }
            }
            else
            {
                if (snap1 != null)
                {
                    if (selectMenu.menuCursor == null) Debug.Log("WTF???");
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.selectSnap) && this.selectedComponent == snap1)
                    {
                        Debug.Log("Select Pokemon 1");
                        selectedPokemon = poke1Info;
                        Debug.Log("Select: " + selectedPokemon.Name);
                        if (onPokemonSelected != null)
                        {
                            onPokemonSelected.Invoke();
                        }
                    }
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.statsSnap) && this.selectedComponent == snap1)
                    {
                        Debug.Log("Stats Pokemon 1");
                        selectedPokemon = poke1Info;
                        Debug.Log("Stats: " + selectedPokemon.Name);

                        Menu.Instantiate<PokemonStatusMenu>();
                        GameManager.Manager.soundEffects.playSelectSound();
                        (Menu.ActiveMenu as PokemonStatusMenu).pokemon = selectedPokemon;

                    }
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.switchSnap) && this.selectedComponent == snap1)
                    {
                        Debug.Log("Switch Pokemon 1");
                        selectedPokemon = poke1Info;
                        Debug.Log("Switch: " + selectedPokemon.Name);
                        GameManager.Manager.soundEffects.playSelectSound();
                    }
                }
                if (snap2 != null)
                {
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.selectSnap) && this.selectedComponent == snap2)
                    {
                        Debug.Log("Select Pokemon 2");
                        selectedPokemon = poke2Info;
                        if (onPokemonSelected != null)
                        {
                            onPokemonSelected.Invoke();
                        }
                    }
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.statsSnap) && this.selectedComponent == snap2)
                    {
                        Debug.Log("Stats Pokemon 2");
                        selectedPokemon = poke2Info;
                        GameManager.Manager.soundEffects.playSelectSound();
                        Menu.Instantiate<PokemonStatusMenu>();
                        (Menu.ActiveMenu as PokemonStatusMenu).pokemon = selectedPokemon;
                    }
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.switchSnap) && this.selectedComponent == snap2)
                    {
                        GameManager.Manager.soundEffects.playSelectSound();
                        Debug.Log("Switch Pokemon 2");
                        selectedPokemon = poke2Info;
                    }
                }
                if (snap3 != null)
                {
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.selectSnap) && this.selectedComponent == snap3)
                    {
                        Debug.Log("Select Pokemon 3");
                        selectedPokemon = poke3Info;
                        if (onPokemonSelected != null)
                        {
                            onPokemonSelected.Invoke();
                        }
                    }
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.statsSnap) && this.selectedComponent == snap3)
                    {
                        Debug.Log("Stats Pokemon 3");
                        selectedPokemon = poke3Info;
                        GameManager.Manager.soundEffects.playSelectSound();
                        Menu.Instantiate<PokemonStatusMenu>();
                        (Menu.ActiveMenu as PokemonStatusMenu).pokemon = selectedPokemon;
                    }
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.switchSnap) && this.selectedComponent == snap3)
                    {
                        GameManager.Manager.soundEffects.playSelectSound();
                        Debug.Log("Switch Pokemon 3");
                        selectedPokemon = poke3Info;
                    }
                }

                if (snap4 != null)
                {
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.selectSnap) && this.selectedComponent == snap4)
                    {
                        Debug.Log("Select Pokemon 4");
                        selectedPokemon = poke4Info;
                        if (onPokemonSelected != null)
                        {
                            onPokemonSelected.Invoke();
                        }
                    }
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.statsSnap) && this.selectedComponent == snap4)
                    {
                        Debug.Log("Stats Pokemon 4");
                        selectedPokemon = poke4Info;
                        GameManager.Manager.soundEffects.playSelectSound();
                        Menu.Instantiate<PokemonStatusMenu>();
                        (Menu.ActiveMenu as PokemonStatusMenu).pokemon = selectedPokemon;
                    }
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.switchSnap) && this.selectedComponent == snap4)
                    {
                        GameManager.Manager.soundEffects.playSelectSound();
                        Debug.Log("Switch Pokemon 4");
                        selectedPokemon = poke4Info;
                    }
                }

                if (snap5 != null)
                {

                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.selectSnap) && this.selectedComponent == snap5)
                    {
                        Debug.Log("Select Pokemon 5");
                        selectedPokemon = poke5Info;
                        if (onPokemonSelected != null)
                        {
                            onPokemonSelected.Invoke();
                        }
                    }
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.statsSnap) && this.selectedComponent == snap5)
                    {
                        Debug.Log("Stats Pokemon 5");
                        selectedPokemon = poke5Info;
                        GameManager.Manager.soundEffects.playSelectSound();
                        Menu.Instantiate<PokemonStatusMenu>();
                        (Menu.ActiveMenu as PokemonStatusMenu).pokemon = selectedPokemon;
                    }
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.switchSnap) && this.selectedComponent == snap5)
                    {
                        GameManager.Manager.soundEffects.playSelectSound();
                        Debug.Log("Switch Pokemon 5");
                        selectedPokemon = poke5Info;
                    }
                }

                if (snap6 != null)
                {
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.selectSnap) && this.selectedComponent == snap6)
                    {
                        Debug.Log("Select Pokemon 6");
                        selectedPokemon = poke6Info;

                        if (onPokemonSelected != null)
                        {
                            onPokemonSelected.Invoke();
                        }
                    }
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.statsSnap) && this.selectedComponent == snap6)
                    {
                        Debug.Log("Stats Pokemon 6");
                        selectedPokemon = poke6Info;
                        GameManager.Manager.soundEffects.playSelectSound();
                        Menu.Instantiate<PokemonStatusMenu>();
                        (Menu.ActiveMenu as PokemonStatusMenu).pokemon = selectedPokemon;
                    }
                    if (selectMenu.menuCursor.simulateMousePress(selectMenu.switchSnap) && this.selectedComponent == snap6)
                    {
                        GameManager.Manager.soundEffects.playSelectSound();
                        Debug.Log("Switch Pokemon 6");
                        selectedPokemon = poke6Info;
                    }
                }
                if (this.menuCursor.simulateMousePress(selectMenu.closeSnap))
                {
                    GameManager.Manager.soundEffects.playSelectSound();
                    selectMenu.exitMenu();
                }
            }
        }

        public override void exitMenu()
        {
            GameManager.ActiveMenu = null;
            base.exitMenu();
        }

        public override void setUpForSnapping()
        {
            if (snap1 != null)
            {
                snap1.setNeighbors(null, null, null, snap2 != null ? snap2 : setUpCloseSnap(snap1));
                this.selectedComponent = snap1;
            }
            else
            {
                this.selectedComponent = closeSnap;
            }
            selectedComponent.snapToThisComponent();
            if (snap2 != null) snap2.setNeighbors(null, null, snap1, snap3 != null ? snap3 : setUpCloseSnap(snap2));
            if (snap3 != null) snap3.setNeighbors(null, null, snap2, snap4 != null ? snap4 : setUpCloseSnap(snap3));
            if (snap4 != null) snap4.setNeighbors(null, null, snap3, snap5 != null ? snap5 : setUpCloseSnap(snap4));
            if (snap5 != null) snap5.setNeighbors(null, null, snap4, snap6 != null ? snap6 : setUpCloseSnap(snap5));
            if (snap6 != null) snap6.setNeighbors(null, null, snap5, setUpCloseSnap(snap6));
        }

        private MenuComponent setUpCloseSnap(MenuComponent top)
        {
            closeSnap.setNeighbors(null, null, top, null);
            return closeSnap;
        }

        public override bool snapCompatible()
        {
            return true;
        }


        public void setPokemon()
        {
            if (GameManager.Manager.player.pokemon.Count >= 1)
            {
                poke1Info = GameManager.Manager.player.pokemon.getPokemonAtIndex(0);
                pokemon1.transform.Find("PokemonName").GetComponent<Text>().text = poke1Info.Name;
                pokemon1.transform.Find("LVL").GetComponent<Text>().text = "LV:" + poke1Info.currentLevel;
                pokemon1.transform.Find("HP").GetComponent<Text>().text = "HP:" + poke1Info.currentHP + "/" + poke1Info.MaxHP;
                pokemon1.transform.Find("Image").GetComponent<Image>().sprite = poke1Info.menuSprite;
                snap1 = new MenuComponent(pokemon1.transform.Find("SnapComponent").GetComponent<Image>());
            }
            else
            {
                pokemon1.SetActive(false);
            }

            if (GameManager.Manager.player.pokemon.Count >= 2)
            {
                poke2Info = GameManager.Manager.player.pokemon.getPokemonAtIndex(1);
                pokemon2.transform.Find("PokemonName").GetComponent<Text>().text = poke2Info.Name;
                pokemon2.transform.Find("LVL").GetComponent<Text>().text = "LV:" + poke2Info.currentLevel;
                pokemon2.transform.Find("HP").GetComponent<Text>().text = "HP:" + poke2Info.currentHP + "/" + poke2Info.MaxHP;
                pokemon2.transform.Find("Image").GetComponent<Image>().sprite = poke2Info.menuSprite;
                snap2 = new MenuComponent(pokemon2.transform.Find("SnapComponent").GetComponent<Image>());
            }
            else
            {
                pokemon2.SetActive(false);
            }

            if (GameManager.Manager.player.pokemon.Count >= 3)
            {
                poke3Info = GameManager.Manager.player.pokemon.getPokemonAtIndex(2);
                pokemon3.transform.Find("PokemonName").GetComponent<Text>().text = poke3Info.Name;
                pokemon3.transform.Find("LVL").GetComponent<Text>().text = "LV:" + poke3Info.currentLevel;
                pokemon3.transform.Find("HP").GetComponent<Text>().text = "HP:" + poke3Info.currentHP + "/" + poke3Info.MaxHP;
                pokemon3.transform.Find("Image").GetComponent<Image>().sprite = poke3Info.menuSprite;
                snap3 = new MenuComponent(pokemon3.transform.Find("SnapComponent").GetComponent<Image>());
            }
            else
            {
                pokemon3.SetActive(false);
            }

            if (GameManager.Manager.player.pokemon.Count >= 4)
            {
                poke4Info = GameManager.Manager.player.pokemon.getPokemonAtIndex(3);
                pokemon4.transform.Find("PokemonName").GetComponent<Text>().text = poke4Info.Name;
                pokemon4.transform.Find("LVL").GetComponent<Text>().text = "LV:" + poke4Info.currentLevel;
                pokemon4.transform.Find("HP").GetComponent<Text>().text = "HP:" + poke4Info.currentHP + "/" + poke4Info.MaxHP;
                pokemon4.transform.Find("Image").GetComponent<Image>().sprite = poke4Info.menuSprite;
                snap4 = new MenuComponent(pokemon4.transform.Find("SnapComponent").GetComponent<Image>());
            }
            else
            {
                pokemon4.SetActive(false);
            }

            if (GameManager.Manager.player.pokemon.Count >= 5)
            {
                poke5Info = GameManager.Manager.player.pokemon.getPokemonAtIndex(4);
                pokemon5.transform.Find("PokemonName").GetComponent<Text>().text = poke5Info.Name;
                pokemon5.transform.Find("LVL").GetComponent<Text>().text = "LV:" + poke5Info.currentLevel;
                pokemon5.transform.Find("HP").GetComponent<Text>().text = "HP:" + poke5Info.currentHP + "/" + poke5Info.MaxHP;
                pokemon5.transform.Find("Image").GetComponent<Image>().sprite = poke5Info.menuSprite;
                snap5 = new MenuComponent(pokemon5.transform.Find("SnapComponent").GetComponent<Image>());
            }
            else
            {
                pokemon5.SetActive(false);
            }

            if (GameManager.Manager.player.pokemon.Count >= 6)
            {
                poke6Info = GameManager.Manager.player.pokemon.getPokemonAtIndex(5);
                pokemon6.transform.Find("PokemonName").GetComponent<Text>().text = poke6Info.Name;
                pokemon6.transform.Find("LVL").GetComponent<Text>().text = "LV:" + poke6Info.currentLevel;
                pokemon6.transform.Find("HP").GetComponent<Text>().text = "HP:" + poke6Info.currentHP + "/" + poke6Info.MaxHP;
                pokemon6.transform.Find("Image").GetComponent<Image>().sprite = poke6Info.menuSprite;
                snap6 = new MenuComponent(pokemon6.transform.Find("SnapComponent").GetComponent<Image>());
            }
            else
            {
                pokemon6.SetActive(false);
            }
        }

        private void openStatsMenu()
        {
            Debug.Log("Open stats for: " + this.selectedPokemon.Name);
        }
    }
}