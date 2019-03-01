using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Content.PokeDatabase
{
    public class MonoPokemonDatabaseScraper:MonoBehaviour
    {
        [SerializeField]
        Text loadingText;

        public string nextScene = "SampleScene";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS4014:Await.Warning")]
        public void Awake()
        {
            
            PokemonDatabaseScraper.InitializeDataBase(new Action<bool>(loadNextScene));

            

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

        public void loadNextScene(bool ok)
        {
            SceneManager.LoadScene(nextScene);
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
            this.loadingText.text = "Pokemon Species Loaded: " + Mathf.Max(PokemonDatabaseScraper.PokemonSpeciesByDex.Count, PokemonDatabase.PokemonInfoByIndex.Count) + "/" + PokemonDatabaseScraper.NumberOfPokemon +
            Environment.NewLine + "Pokemon Loaded: " + Mathf.Max(PokemonDatabaseScraper.PokemonByDex.Count, PokemonDatabase.PokemonInfoByIndex.Count) + "/" + PokemonDatabaseScraper.NumberOfPokemon +
            Environment.NewLine + "Moves Loaded:" + PokemonDatabase.MovesByIndex.Count + "/" + PokemonDatabaseScraper.NumberOfMoves +
            Environment.NewLine + "Evolution Chains Loaded: " + (PokemonDatabase.EvolutionByIndex.Count) + "/" + (PokemonDatabaseScraper.NumberOfEvolutionChains-8)+
            Environment.NewLine + "Pokemon Info Loaded: " + (PokemonDatabase.PokemonInfoByIndex.Count) + "/" + PokemonDatabaseScraper.NumberOfPokemon;
            
        }

    }
}
