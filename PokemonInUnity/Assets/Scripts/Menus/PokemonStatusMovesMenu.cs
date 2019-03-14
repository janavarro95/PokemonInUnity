using Assets.Scripts.Content.GameContent;
using Assets.Scripts.GameInput;
using Assets.Scripts.Menus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{

    public class PokemonStatusMovesMenu : Menu
    {
        Image pokemonSprite; //
        Text pokedexNo; //
        Text pokemonName; //
        Text pokemonLvlUP; //
        Text pokemonEXP;//


        Text pokemonMove1Text;//
        Text pokemonMove2Text;//
        Text pokemonMove3Text;//
        Text pokemonMove4Text;//

        Text pokemonMove1PP;//
        Text pokemonMove2PP;//
        Text pokemonMove3PP;//
        Text pokemonMove4PP;//

        public Pokemon pokemon;

        bool hasSetUp;
        bool eatFirstInput;

        public override void Start()
        {
            this.canvas = this.gameObject.transform.Find("Canvas").gameObject;
            GameObject background = this.canvas.transform.Find("Background").gameObject;
            layerMenuOnTop();


            pokemonSprite = background.transform.Find("Sprite").gameObject.GetComponent<Image>();
            pokedexNo = background.transform.Find("PokedexNumber").gameObject.GetComponent<Text>();
            pokemonName = background.transform.Find("PokemonName").gameObject.GetComponent<Text>();
            pokemonEXP = background.transform.Find("EXP").gameObject.GetComponent<Text>();
            pokemonLvlUP = background.transform.Find("ToLVLUp").gameObject.GetComponent<Text>();

            GameObject statusBackground = background.transform.Find("MovesBackground").gameObject;


            pokemonMove1Text = statusBackground.transform.Find("Move1").gameObject.GetComponent<Text>();
            pokemonMove2Text = statusBackground.transform.Find("Move2").gameObject.GetComponent<Text>();
            pokemonMove3Text = statusBackground.transform.Find("Move3").gameObject.GetComponent<Text>();
            pokemonMove4Text = statusBackground.transform.Find("Move4").gameObject.GetComponent<Text>();

            pokemonMove1PP = statusBackground.transform.Find("Move1").Find("Value").gameObject.GetComponent<Text>();
            pokemonMove2PP = statusBackground.transform.Find("Move2").Find("Value").gameObject.GetComponent<Text>();
            pokemonMove3PP = statusBackground.transform.Find("Move3").Find("Value").gameObject.GetComponent<Text>();
            pokemonMove4PP = statusBackground.transform.Find("Move4").Find("Value").gameObject.GetComponent<Text>();

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

            pokemonSprite.sprite = pokemon.frontSprite;
            pokedexNo.text = "No." + pokemon.info.pokedexNumber;
            this.pokemonName.text = pokemon.info.pokemonName;
            this.pokemonEXP.text = "Exp Points:" + Environment.NewLine + "     " + this.pokemon.currentEXP;
            this.pokemonLvlUP.text = "LVL Up:" + Environment.NewLine + (this.pokemon.EXPToLVLUp - this.pokemon.currentEXP) + " to " + "LVL: " + (this.pokemon.currentLevel + 1);

            pokemonMove1Text.text = this.pokemon.moves[0] != null ? this.pokemon.moves[0].moveInfo.moveName : "";
            pokemonMove2Text.text = this.pokemon.moves[1] != null ? this.pokemon.moves[1].moveInfo.moveName : "";
            pokemonMove3Text.text = this.pokemon.moves[2] != null ? this.pokemon.moves[2].moveInfo.moveName : "";
            pokemonMove4Text.text = this.pokemon.moves[3] != null ? this.pokemon.moves[3].moveInfo.moveName : "";

            pokemonMove1PP.text = this.pokemon.moves[0] != null ? this.pokemon.moves[0].currentPP + "/" + this.pokemon.moves[0].moveInfo.pp : "";
            pokemonMove2PP.text = this.pokemon.moves[1] != null ? this.pokemon.moves[1].currentPP + "/" + this.pokemon.moves[1].moveInfo.pp : "";
            pokemonMove3PP.text = this.pokemon.moves[2] != null ? this.pokemon.moves[2].currentPP + "/" + this.pokemon.moves[2].moveInfo.pp : "";
            pokemonMove4PP.text = this.pokemon.moves[3] != null ? this.pokemon.moves[3].currentPP + "/" + this.pokemon.moves[3].moveInfo.pp : "";

            //throw new System.Exception("MAKE THIS MENU");
        }

        private void checkForInput()
        {
            if (InputControls.StartPressed || InputControls.APressed)
            {
                exitMenu();
                //Go to next menu;
            }
        }

        public override void exitMenu()
        {
            base.exitMenu();
            ActiveMenu = null;
        }

        public override bool snapCompatible()
        {
            return false;
        }

        public override void setUpForSnapping()
        {

        }
    }
}