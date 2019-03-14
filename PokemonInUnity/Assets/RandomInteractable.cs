using Assets.Scripts.Content.GameContent;
using Assets.Scripts.GameInformation;
using Assets.Scripts.Interactables;
using Assets.Scripts.Utilities.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomInteractable : Interactable
{

    public UnityEvent onInteract;

    DeltaTimer randomTimer;

    public override void Update()
    {
        if (randomTimer != null) randomTimer.Update();
    }

    public override void interact()
    {
        this.onInteract.Invoke();
    }




    public void addRandomPokemon()
    {
        if (GameManager.Player.pokemon.canAddPokemon())
        {

            GameManager.Manager.soundEffects.playItemGetSound();
            Pokemon p = new Pokemon(Assets.Scripts.Content.PokeDatabase.PokemonDatabase.PokemonInfoByIndex[UnityEngine.Random.Range(1, 152)], 5);
            GameManager.Manager.dialogueManager.initializeDialogues("Pokeball", new List<string>()
        {
            "You got a "+p.Name+" !"
        });
            GameManager.Player.pokemon.addPokemon(p);
        }
        else
        {
            GameManager.Manager.dialogueManager.initializeDialogues("Pokeball", new List<string>()
        {
            "Your party is full"
        });
        }
        Destroy(this.gameObject);
    }

    public void flickerMapLights()
    {
        GameManager.Manager.currentMap.mapColor = Color.cyan;
        randomTimer = new DeltaTimer(1f, Assets.Scripts.Enums.TimerType.CountDown, false, resetMapColor);
        randomTimer.start();
    }

    public void resetMapColor()
    {
        GameManager.Manager.currentMap.mapColor = Color.white;
    }
}
