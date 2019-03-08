using Assets.Scripts.Content.GameContent;
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

        public string nextScene = "PalletTown";
        public bool loadPokemon = true;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS4014:Await.Warning")]
        public void Awake()
        {
            if (loadPokemon)
            {
                PokemonDatabaseScraper.InitializeDataBase(new Action<bool>(loadNextScene));


            }
            

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

        public void Start()
        {
            if (loadPokemon == false)
            {
                loadNextScene(true);
            }
        }

        public void loadNextScene(bool ok)
        {
            Pokemon bulba = new Pokemon(Assets.Scripts.Content.PokeDatabase.PokemonDatabase.PokemonInfoByIndex[1],5);
            if (bulba == null)
            {
                Debug.Log("BULBA IS DEAD");
            }
            else
            {
                Debug.Log("BULBA LIVES");
            }
            bool added=GameInformation.GameManager.Manager.player.pokemon.addPokemon(bulba);
            GameInformation.GameManager.Manager.serializer.Serialize(Application.dataPath + "POkemonTest1.json", bulba);

            if (added == false)
            {
                Debug.Log("ERRROR ADDING BULBA");
            }
            else
            {
               Debug.Log(GameInformation.GameManager.Manager.player.pokemon.getPokemonAtIndex(0).Name);
            }

            SceneManager.LoadScene(nextScene);
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
