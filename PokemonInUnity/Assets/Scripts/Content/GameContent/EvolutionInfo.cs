using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Content.GameContent
{
    public class EvolutionInfo
    {

        public string speciesName;
        public Dictionary<string,EvolutionTriggers> evolvesTo;
        /// <summary>
        /// If true this pokemon can't breed.
        /// </summary>
        public bool isBaby;
        public EvolutionTriggers triggers;
        public int id;

        public EvolutionInfo()
        {

        }


        public EvolutionInfo(PokeAPI.ChainLink Link, int chainIndex)
        {
            this.speciesName =PokeDatabase.PokemonDatabase.SanitizeString(Link.Species.Name);
            this.evolvesTo = new Dictionary<string, EvolutionTriggers>();
            this.id = chainIndex;

            processEvolutionChains(Link,this.id);

            this.isBaby = Link.IsBaby;

            this.triggers = Link.Details.Length >= 1 ? new EvolutionTriggers(Link.Details[0]) : new EvolutionTriggers(false); //Probably would be fine....
            PokeDatabase.PokemonDatabaseScraper.SerializeEvolutionInfo(this);

        }

        private void processEvolutionChains(PokeAPI.ChainLink Link,int chainIndex)
        {
            string currentSpeciesName = Link.Species.Name;
            PokeDatabase.PokemonDatabase.AddEvolutionInfoPokemon(this);
            foreach (var v in Link.EvolvesTo)
            {
                EvolutionInfo info= new EvolutionInfo(v,chainIndex); //Seems bad but it calls a recursion level down this tree....
                PokeDatabase.PokemonDatabase.EvolutionByPokemon[PokeDatabase.PokemonDatabase.SanitizeString(currentSpeciesName)].evolvesTo.Add(PokeDatabase.PokemonDatabase.SanitizeString(v.Species.Name), PokeDatabase.PokemonDatabase.EvolutionByPokemon[v.Species.Name].triggers);
            }
            

        }

    }
}
