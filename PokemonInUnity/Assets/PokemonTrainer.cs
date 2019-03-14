using Assets.Scripts.Battle.V1;
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


    public PokemonInventory pokemon;

    public Sprite trainerSprite;
    public string trainerName;
    public string trainerTitle;

    public string FullName
    {
        get
        {
            return trainerTitle + " " + trainerName;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Initializes a pokemon trainer battle by generating all the pokemon info from the factory info.
    /// </summary>
    public void startPokemonTrainerBattle()
    {
        Debug.Log("Start the battle!");

        if (GameManager.Player.pokemon.getFirstNonFaintedPokemon() == null)
        {
            GameManager.Manager.dialogueManager.initializeDialogues("", new List<string>()
            {
                "You don't have any pokemon to fight with."

            });
            return;
        }

        pokemon = new PokemonInventory(6);

        if (pokemon1Info != null)
        {
            pokemon1 = pokemon1Info.generatePokemon();
            pokemon.addPokemon(pokemon1);
        }
        if (pokemon2Info != null)
        {
            pokemon2 = pokemon2Info.generatePokemon();
            pokemon.addPokemon(pokemon2);
        }
        if (pokemon3Info != null)
        {
            pokemon3 = pokemon3Info.generatePokemon();
            pokemon.addPokemon(pokemon3);
        }
        if (pokemon4Info != null)
        {
            pokemon4 = pokemon4Info.generatePokemon();
            pokemon.addPokemon(pokemon4);
        }
        if (pokemon5Info != null)
        {
            pokemon5 = pokemon5Info.generatePokemon();
            pokemon.addPokemon(pokemon5);
        }
        if (pokemon6Info != null)
        {
            pokemon6 = pokemon6Info.generatePokemon();
            pokemon.addPokemon(pokemon6);
        }

        Menu.Instantiate<BattleManagerV1>();
        (GameMenu.ActiveMenu as BattleManagerV1).setUpTrainerBattle(this);
    }
}
