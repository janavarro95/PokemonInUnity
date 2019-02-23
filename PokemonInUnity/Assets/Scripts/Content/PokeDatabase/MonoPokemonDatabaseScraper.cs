using System;
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
        Text text;

        public void Awake()
        {
            PokemonDatabase.ScrapePokemonSpecies();
            PokemonDatabase.ScrapePokemon();
            try
            {
                text.text = PokemonDatabase.PokemonSpeciesByDex[1].FlavorTexts[1].FlavorText;
            }
            catch(Exception err)
            {

            }
        }

    }
}
