using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Content.GameContent
{
    public class EvolutionTriggers
    {
        public string itemUsed;
        public Enums.PokemonGender gender;
        public string heldItemName;
        public string knownMoveName;
        public Enums.Type knownMoveType;
        public int minLevel;
        public int minHappiness;
        public int minBeauty;
        public int minAffection;
        public bool mustBeRaining;
        public string partyPokemonRequired;
        public Enums.Type partyPokemonWithTypeRequired;
        public int relativePhysicalStats;
        /// <summary>
        /// Either:
        /// "night" or "day"
        /// </summary>
        public string timeOfDay;
        public bool mustBeTraded;

        public EvolutionTriggers()
        {

        }
        public EvolutionTriggers(PokeAPI.EvolutionDetail Trigger)
        {
            this.itemUsed =PokeDatabase.PokemonDatabase.SanitizeString(Trigger.HeldItem.Name);
            this.gender = Trigger.Gender!=null ? (Enums.PokemonGender)Trigger.Gender.Value : (Enums.PokemonGender)3;
            this.heldItemName = PokeDatabase.PokemonDatabase.SanitizeString(Trigger.HeldItem.Name);
            this.knownMoveName = PokeDatabase.PokemonDatabase.SanitizeString(Trigger.KnownMove.Name);
            this.knownMoveType =Enums.ParseEnum<Enums.Type>(PokeDatabase.PokemonDatabase.SanitizeString(Trigger.KnownMoveType.Name));
            this.minLevel = Trigger.MinLevel!=null ? Trigger.MinLevel.Value : 0;
            this.minHappiness = Trigger.MinHappiness != null ? Trigger.MinHappiness.Value : 0;
            this.minBeauty = Trigger.MinBeauty != null ? Trigger.MinBeauty.Value : 0;
            this.minAffection = Trigger.MinAffection != null ? Trigger.MinAffection.Value : 0;
            this.mustBeRaining = Trigger.NeedsOverworldRain;
            this.partyPokemonRequired = PokeDatabase.PokemonDatabase.SanitizeString(Trigger.PartySpecies.Name);
            this.partyPokemonWithTypeRequired=Enums.ParseEnum<Enums.Type>(PokeDatabase.PokemonDatabase.SanitizeString(Trigger.PartyType.Name));
            this.relativePhysicalStats = Trigger.RelativePhysicalStats!=null ? Trigger.RelativePhysicalStats.Value:999;

        }

        /*
        public bool evolutionCanBeTriggeredByLevel()
        {
            return false;
        }
        public bool evolutionCanBeTriggeredByGender()
        {
            return false;
        }
        ///etc.....
        */
    }
}
