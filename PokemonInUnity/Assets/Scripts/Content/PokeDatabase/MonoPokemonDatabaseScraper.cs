using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Content.PokeDatabase
{
    public class MonoPokemonDatabaseScraper:MonoBehaviour
    {
        [SerializeField]
        Text pokemonSpeciesLoadedText;
        [SerializeField]
        Text pokemonLoadedText;

        public void Awake()
        {
            StartCoroutine(PokemonDatabase.InitializeDataBase());

            //PokemonDatabase.ScrapePokemonSpecies();
            /*
            PokemonDatabase.ScrapePokemon();
            try
            {
                text.text = PokemonDatabase.PokemonSpeciesByDex[1].FlavorTexts[1].FlavorText;
            }
            catch(Exception err)
            {

            }
            */
        }

        public void StartPage()
        {
            print("in StartPage()");
            StartCoroutine(FinishFirst(5.0f, DoLast));
        }

        IEnumerator FinishFirst(float waitTime, Action doLast)
        {
            print("in FinishFirst");
            yield return new WaitForSeconds(waitTime);
            print("leave FinishFirst");
            doLast();
        }

        void DoLast()
        {
            print("do after everything is finished");
            print("done");
        }

        public void Update()
        {
            this.pokemonSpeciesLoadedText.text = "Pokemon Species Loaded: " + PokemonDatabase.PokemonSpeciesByDex.Count + "/" + PokemonDatabase.NumberOfPokemon;
            this.pokemonLoadedText.text = "Pokemon Loaded: " + PokemonDatabase.PokemonByDex.Count + "/" + PokemonDatabase.NumberOfPokemon;
        }

    }
}
