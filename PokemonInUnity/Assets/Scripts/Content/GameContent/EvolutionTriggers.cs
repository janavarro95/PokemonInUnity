using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Content.GameContent
{
    /// <summary>
    /// Deals with all of the requirements to evolve into said pokemon.
    /// </summary>
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


        public bool evolves;
        public EvolutionTriggers()
        {
            
        }
        public EvolutionTriggers(bool evolves = false)
        {
        }

        public EvolutionTriggers(PokeAPI.EvolutionDetail Trigger)
        {
            this.itemUsed =PokeDatabase.PokemonDatabase.SanitizeString(Trigger.Item!=null ? Trigger.Item.Name: "");
            this.gender = Trigger.Gender!=null ? (Enums.PokemonGender)Trigger.Gender.Value : (Enums.PokemonGender)3;
            this.heldItemName = PokeDatabase.PokemonDatabase.SanitizeString(Trigger.HeldItem!=null? Trigger.HeldItem.Name:"");
            this.knownMoveName = PokeDatabase.PokemonDatabase.SanitizeString(Trigger.KnownMove!=null? Trigger.KnownMove.Name:"");
            this.knownMoveType =Enums.ParseEnum<Enums.Type>(PokeDatabase.PokemonDatabase.SanitizeString(Trigger.KnownMoveType!=null?Trigger.KnownMoveType.Name:"NULL"));
            this.minLevel = Trigger.MinLevel!=null ? Trigger.MinLevel.Value : 0;
            this.minHappiness = Trigger.MinHappiness != null ? Trigger.MinHappiness.Value : 0;
            this.minBeauty = Trigger.MinBeauty != null ? Trigger.MinBeauty.Value : 0;
            this.minAffection = Trigger.MinAffection != null ? Trigger.MinAffection.Value : 0;
            this.mustBeRaining = Trigger.NeedsOverworldRain;
            this.partyPokemonRequired = PokeDatabase.PokemonDatabase.SanitizeString(Trigger.PartySpecies!=null?Trigger.PartySpecies.Name:"");
            this.partyPokemonWithTypeRequired=Enums.ParseEnum<Enums.Type>(PokeDatabase.PokemonDatabase.SanitizeString(Trigger.PartyType!=null?Trigger.PartyType.Name:"NULL"));
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
