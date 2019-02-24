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
        Text loadingText;

        public void Awake()
        {
            StartCoroutine(PokemonDatabaseScraper.InitializeDataBase());

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
            this.loadingText.text = "Pokemon Species Loaded: " + PokemonDatabaseScraper.PokemonSpeciesByDex.Count + "/" + PokemonDatabaseScraper.NumberOfPokemon +
            Environment.NewLine + "Pokemon Loaded: " + PokemonDatabaseScraper.PokemonByDex.Count + "/" + PokemonDatabaseScraper.NumberOfPokemon +
            Environment.NewLine + "Moves Loaded:" + PokemonDatabase.MovesByIndex.Count + "/" + PokemonDatabaseScraper.NumberOfMoves;
            
        }

    }
}
