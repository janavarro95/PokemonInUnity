using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Content.GameContent
{
    public class EvolutionInfo
    {

        public string speciesName;
        public List<string> evolvesTo;
        public bool isBaby;
        public EvolutionTriggers triggers;

        public EvolutionInfo()
        {

        }

        public EvolutionInfo(string SpeciesName, List<string> evolvesTo,bool IsBaby,EvolutionTriggers Triggers)
        {
            this.speciesName = SpeciesName;
            this.evolvesTo = evolvesTo;
            this.isBaby = IsBaby;
            this.triggers = Triggers;
        }

        public EvolutionInfo(PokeAPI.ChainLink Link)
        {
            this.speciesName =PokeDatabase.PokemonDatabase.SanitizeString(Link.Species.Name);
            this.evolvesTo = new List<string>();

            processEvolutionChains(Link);

            this.isBaby = Link.IsBaby;
            this.triggers = new EvolutionTriggers(Link.Details[0]); //Probably would be fine....
        }

        private void processEvolutionChains(PokeAPI.ChainLink Link)
        {
            string currentSpeciesName = Link.Species.Name;
            PokeDatabase.PokemonDatabase.AddEvolutionInfoPokemon(this);
            foreach (var v in Link.EvolvesTo)
            {
                new EvolutionInfo(v); //Seems bad but it calls a recursion level down this tree....
                PokeDatabase.PokemonDatabase.EvolutionByPokemon[PokeDatabase.PokemonDatabase.SanitizeString(currentSpeciesName)].evolvesTo.Add(PokeDatabase.PokemonDatabase.SanitizeString(v.Species.Name));
            }

        }

    }
}
