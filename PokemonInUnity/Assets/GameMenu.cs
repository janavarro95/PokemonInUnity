using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Menus;
using Assets.Scripts.Menus.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : Menu
{

    public Text trainerText;


    public MenuComponent pokeDexSnap;
    public MenuComponent pokemonSnap; //Lol
    public MenuComponent itemsSnap;
    public MenuComponent trainerSnap;
    public MenuComponent saveSnap;
    public MenuComponent optionsSnap;
    public MenuComponent closeSnap;

    public string TrainerName
    {
        get
        {
            if (String.IsNullOrEmpty(GameManager.Player.playerName))
            {
                return "Trainer";
            }
            else
            {
                return GameManager.Player.playerName;
            }
        }
    }


    public override void Start()
    {
        GameManager.ActiveMenu = this;
        GameObject canvas = this.transform.Find("Canvas").gameObject;
        GameObject img = this.transform.Find("Canvas").Find("Image").gameObject;

        Text pokedex = img.transform.Find("Pokedex").gameObject.GetComponent<Text>();
        pokeDexSnap = new MenuComponent(pokedex.transform.GetChild(0).gameObject.GetComponent<Image>());

        Text pokemon = img.transform.Find("Pokemon").gameObject.GetComponent<Text>();
        pokemonSnap = new MenuComponent(pokemon.transform.GetChild(0).gameObject.GetComponent<Image>());

        Text items = img.transform.Find("Items").gameObject.GetComponent<Text>();
        itemsSnap = new MenuComponent(items.transform.GetChild(0).gameObject.GetComponent<Image>());

        Text save = img.transform.Find("Save").gameObject.GetComponent<Text>();
        saveSnap = new MenuComponent(save.transform.GetChild(0).gameObject.GetComponent<Image>());

        trainerText = img.transform.Find("Trainer").gameObject.GetComponent<Text>();
        trainerSnap = new MenuComponent(trainerText.transform.GetChild(0).gameObject.GetComponent<Image>());

        Text options = img.transform.Find("Options").gameObject.GetComponent<Text>();
        optionsSnap = new MenuComponent(options.transform.GetChild(0).gameObject.GetComponent<Image>());

        Text close = img.transform.Find("Close").gameObject.GetComponent<Text>();
        closeSnap = new MenuComponent(close.transform.GetChild(0).gameObject.GetComponent<Image>());

        this.menuCursor = canvas.transform.Find("GameCursor").gameObject.GetComponent<Assets.Scripts.GameInput.GameCursor>();
        setUpForSnapping();

        

    }

    public override void setUpForSnapping()
    {
        pokeDexSnap.setNeighbors(null, null, null, pokemonSnap);
        pokemonSnap.setNeighbors(null, null, pokeDexSnap, itemsSnap);
        itemsSnap.setNeighbors(null, null, pokemonSnap, trainerSnap);
        trainerSnap.setNeighbors(null, null, itemsSnap, saveSnap);
        saveSnap.setNeighbors(null, null, trainerSnap, optionsSnap);
        optionsSnap.setNeighbors(null, null, saveSnap, closeSnap);
        closeSnap.setNeighbors(null, null, optionsSnap, null);
        this.selectedComponent = pokeDexSnap;
        this.selectedComponent.snapToThisComponent();
    }

    public override bool snapCompatible()
    {
        return true;
    }

    public override void exitMenu()
    {
        base.exitMenu();
        GameMenu.ActiveMenu = null;
    }

    public override void Update()
    {

        if (trainerText != null)
        {
            trainerText.text = TrainerName;
        }
        checkForInput();
        //this.selectedComponent.snapToThisComponent();
    }

    private void checkForInput()
    {
        if (GameCursor.SimulateMousePress(pokeDexSnap))
        {
            Debug.Log("Add in pokedex!");
        }
        else if (GameCursor.SimulateMousePress(pokemonSnap))
        {
            Debug.Log("Add in pokemon!");
        }
        else if (GameCursor.SimulateMousePress(itemsSnap))
        {
            Debug.Log("Add in items!");
        }
        else if (GameCursor.SimulateMousePress(trainerSnap))
        {
            Debug.Log("Add in trainer!");
        }
        else if (GameCursor.SimulateMousePress(saveSnap))
        {
            Debug.Log("Add in saving!");
        }
        else if (GameCursor.SimulateMousePress(optionsSnap))
        {
            Debug.Log("Add in options!");
        }
        else if (GameCursor.SimulateMousePress(closeSnap))
        {
            exitMenu();
        }
    }
}
