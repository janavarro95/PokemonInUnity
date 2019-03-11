using Assets.Scripts.Content.GameContent;
using Assets.Scripts.GameInformation;
using Assets.Scripts.Menus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonTrainer : MonoBehaviour
{
    public PokemonFactoryInfo pokemon1Info;
    public PokemonFactoryInfo pokemon2Info;
    public PokemonFactoryInfo pokemon3Info;
    public PokemonFactoryInfo pokemon4Info;
    public PokemonFactoryInfo pokemon5Info;
    public PokemonFactoryInfo pokemon6Info;


    public Pokemon pokemon1;
    public Pokemon pokemon2;
    public Pokemon pokemon3;
    public Pokemon pokemon4;
    public Pokemon pokemon5;
    public Pokemon pokemon6;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void startPokemonTrainerBattle()
    {
        Debug.Log("Start the battle!");
        if (pokemon1Info != null)
        {
            pokemon1 = pokemon1Info.generatePokemon();
        }
        if (pokemon2Info != null)
        {
            pokemon2 = pokemon2Info.generatePokemon();
        }
        if (pokemon3Info != null)
        {
            pokemon3 = pokemon3Info.generatePokemon();
        }
        if (pokemon4Info != null)
        {
            pokemon4 = pokemon4Info.generatePokemon();
        }
        if (pokemon5Info != null)
        {
            pokemon5 = pokemon5Info.generatePokemon();
        }
        if (pokemon6Info != null)
        {
            pokemon6 = pokemon6Info.generatePokemon();
        }

        Menu.Instantiate<PokemonBattleMenu>();
        (GameMenu.ActiveMenu as PokemonBattleMenu).setUpBattlers(GameManager.Player.pokemon.getPokemonAtIndex(0), pokemon1);
    }
}
