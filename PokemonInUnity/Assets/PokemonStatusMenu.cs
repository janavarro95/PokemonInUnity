using Assets.Scripts;
using Assets.Scripts.Content;
using Assets.Scripts.Content.GameContent;
using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Menus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonStatusMenu : Menu
{


    Image pokemonSprite; //
    Text pokedexNo; //
    Text pokemonName; //
    Text pokemonLvl; //
    Text pokemonHP;//

    Text pokemonStatus;//

    Text pokemonType1;//
    Text pokemonType2;//

    Text attackValue;//
    Text defenseValue;//
    Text specialAttackValue;//
    Text specialDefenseValue;//
    Text speedValue;//

    public Pokemon pokemon;

    bool hasSetUp;
    bool eatFirstInput;

    public override void Start()
    {
        this.canvas = this.gameObject.transform.Find("Canvas").gameObject;

        GameObject background = canvas.transform.Find("Background").gameObject;

        pokemonSprite = background.transform.Find("Sprite").gameObject.GetComponent<Image>();
        pokedexNo = background.transform.Find("PokedexNumber").gameObject.GetComponent<Text>();
        pokemonName = background.transform.Find("PokemonName").gameObject.GetComponent<Text>();
        pokemonLvl = background.transform.Find("LVL").gameObject.GetComponent<Text>();
        pokemonHP = background.transform.Find("HP").gameObject.GetComponent<Text>();

        pokemonStatus = background.transform.Find("Status").gameObject.GetComponent<Text>();

        pokemonType1 = background.transform.Find("Type1").gameObject.GetComponent<Text>();
        pokemonType2 = background.transform.Find("Type2").gameObject.GetComponent<Text>();

        GameObject statusBackground = background.transform.Find("StatsBackground").gameObject;

        attackValue = statusBackground.transform.Find("Attack").Find("Value").gameObject.GetComponent<Text>();
        defenseValue = statusBackground.transform.Find("Defense").Find("Value").gameObject.GetComponent<Text>();
        specialAttackValue = statusBackground.transform.Find("SpecialAttack").Find("Value").gameObject.GetComponent<Text>();
        specialDefenseValue = statusBackground.transform.Find("SpecialDefense").Find("Value").gameObject.GetComponent<Text>();
        speedValue = statusBackground.transform.Find("Speed").Find("Value").gameObject.GetComponent<Text>();

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
    }

    public override void Update()
    {
        if (Menu.ActiveMenu != this) return;
        if (eatFirstInput == false)
        {
            eatFirstInput = true;
            return;
        }
        if (pokemon == null) return;
        else
        {
            if (hasSetUp == false)
            {
                setUpMenu();
                hasSetUp = true;
            }
        }
        checkForInput();
    }


    private void setUpMenu()
    {
        this.pokemonSprite.sprite = pokemon.frontSprite;

        pokedexNo.text = "No." + this.pokemon.info.pokedexNumber;
        pokemonName.text = this.pokemon.Name;
        this.pokemonLvl.text = "Lvl:" + this.pokemon.currentLevel;
        this.pokemonHP.text = "HP:" + this.pokemon.currentHP + "/" + this.pokemon.MaxHP;

        this.pokemonStatus.text = "Status: OK";

        if (this.pokemon.info.types.Count >= 2)
        {
            this.pokemonType1.text = "Type 1:" + (this.pokemon.info.types.Count >= 1 ? this.pokemon.info.types[1].ToString() : "BOOP");
            this.pokemonType2.text = "Type 2:" + (this.pokemon.info.types.Count >= 2 ? this.pokemon.info.types[0].ToString() : "SSSSS");
        }
        else
        {
            this.pokemonType1.text = "Type 1:" + (this.pokemon.info.types.Count >= 1 ? this.pokemon.info.types[0].ToString() : "BOOP");
        }
        attackValue.text = pokemon.Attack.ToString();
        defenseValue.text = pokemon.Defense.ToString();
        specialAttackValue.text = pokemon.SpecialAttack.ToString();
        specialDefenseValue.text = pokemon.SpecialDefense.ToString();
        speedValue.text = pokemon.Speed.ToString();
    }

    private void checkForInput()
    {
        if (Assets.Scripts.GameInput.InputControls.StartPressed)
        {
            exitMenu();
        }
        if (Assets.Scripts.GameInput.InputControls.APressed)
        {
            exitMenu();
            Menu.Instantiate<PokemonStatusMovesMenu>();
            (ActiveMenu as PokemonStatusMovesMenu).pokemon = this.pokemon;
            //Go to next menu;
        }
    }
}
