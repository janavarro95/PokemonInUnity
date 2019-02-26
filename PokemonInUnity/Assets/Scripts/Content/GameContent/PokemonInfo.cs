using Assets.Scripts.Content.PokeDatabase;
using PokeAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Content.GameContent
{
    public class PokemonInfo
    {
        public string pokemonName;

        public int pokedexNumber;
        public string pokedexDescription;

        public int baseHP;
        public int baseAttack;
        public int baseDefense;
        public int baseSpecialAttack;
        public int baseSpecialDefense;
        public int baseSpeed;

        public List<Enums.Type> types;


        public string evolvesFrom;
        public EvolutionInfo evolutionInfo;


        /// <summary>
        /// Should always be 0?
        /// </summary>
        public int baseExperience;

        public float genderRateRaw;
        [Newtonsoft.Json.JsonIgnore]
        public float GenderRate
        {
            get
            {
                return genderRateRaw / 8;
            }
        }

        /// <summary>
        /// The base capture rate; up to 255. The higher the number, the easier the catch.
        /// </summary>
        public float captureRateRaw;
        [Newtonsoft.Json.JsonIgnore]
        public float captureRatePercent
        {
            get
            {
                return captureRatePercent / 255;
            }
        }

        /// <summary>
        /// The happiness when caught by a normal Pokéball; up to 255. The higher the number, the happier the Pokémon.
        /// </summary>
        public int baseHappiness;

        /// <summary>
        /// Initial hatch counter: one must walk 255 × (hatch_counter + 1) steps before this Pokémon's egg hatches, unless utilizing bonuses like Flame Body's.
        /// </summary>
        public int hatchCounter;

        /// <summary>
        /// Whether or not this Pokémon has multiple forms and can switch between them.
        /// </summary>
        public bool hasMultipleForms;

        public Enums.ExperienceGrowthRate growthRate;

        public List<Enums.EggGroup> eggGroups;


        /// <summary>
        /// A dictionary of method types. The int in the nested dictionary is for what level the move is learned at.
        /// </summary>
        public Dictionary<Enums.MoveLearnedType, Dictionary<string, int>> learnableMoves;


        public PokemonInfo()
        {
            
        }

        public PokemonInfo(PokeAPI.Pokemon pokeInfo,int dexNumber)
        {

            this.pokemonName = PokeDatabase.PokemonDatabase.SanitizeString(pokeInfo.Name);

            foreach (var v in pokeInfo.Stats)
            {
                if (v.Stat.Name == "hp")
                {
                    this.baseHP = v.BaseValue;
                }
                else if (v.Stat.Name == "attack")
                {
                    this.baseAttack = v.BaseValue;
                }
                else if (v.Stat.Name == "defense")
                {
                    this.baseDefense = v.BaseValue;
                }
                else if (v.Stat.Name == "special-attack")
                {
                    this.baseSpecialAttack = v.BaseValue;
                }
                else if (v.Stat.Name == "special-defense")
                {
                    this.baseSpecialDefense = v.BaseValue;
                }
                else if (v.Stat.Name == "speed")
                {
                    this.baseSpeed = v.BaseValue;
                }
            }

            this.baseExperience = pokeInfo.BaseExperience;

            learnableMoves = new Dictionary<Enums.MoveLearnedType, Dictionary<string, int>>();
            foreach(var num in Enums.GetValues<Enums.MoveLearnedType>())
            {
                learnableMoves.Add(num, new Dictionary<string, int>());
            }
            int index = 0;
            foreach (var v in pokeInfo.Moves)
            {
                Debug.Log("Processing Move: " + v.Move.Name + " on Pokemon: " + this.pokemonName);
                Debug.Log("Processing Move: " + v.Move.Name + " Progress: " + index +pokeInfo.Moves.Length);
                index++;
                var pair = PokeDatabase.PokemonDatabase.GetProperMoveLearnedInfo(v.VersionGroupDetails);
                learnableMoves[pair.Key].Add(PokeDatabase.PokemonDatabase.SanitizeString(v.Move.Name), pair.Value);
            }
            Debug.Log("Process Types");

            types = new List<Enums.Type>();
            foreach (var type in pokeInfo.Types)
            {
                Debug.Log("processing type: " + type.Type.Name);
                types.Add(Enums.ParseEnum<Enums.Type>(type.Type.Name));
            }
            Debug.Log("Done with types");

            Debug.Log("Get species info");
            PokemonSpecies speciesInfo = PokemonDatabaseScraper.PokemonSpeciesByDex[dexNumber];

            Debug.Log("Got species info");
            this.genderRateRaw = speciesInfo.FemaleToMaleRate.Value;
            this.captureRateRaw = speciesInfo.CaptureRate;
            this.baseHappiness = speciesInfo.BaseHappiness;
            this.hatchCounter = speciesInfo.HatchCounter;
            this.hasMultipleForms = speciesInfo.FormsAreSwitchable;

            Debug.Log("Process exp rate");
            this.growthRate = Enums.ParseEnum<Enums.ExperienceGrowthRate>(PokeDatabase.PokemonDatabase.SanitizeStringNoSpaces(speciesInfo.GrowthRate.Name));
            Debug.Log("done with exp rate");

            this.pokedexNumber = dexNumber;

            Debug.Log("Process dex description");
            this.pokedexDescription = PokeDatabase.PokemonDatabase.GetProperFlavorText(speciesInfo.FlavorTexts);
            Debug.Log("Done with dex description");

            this.eggGroups = new List<Enums.EggGroup>();
            Debug.Log("Process egg groups");
            foreach(var group in speciesInfo.EggGroups)
            {
               eggGroups.Add( Enums.ParseEnum<Enums.EggGroup>(PokeDatabase.PokemonDatabase.SanitizeStringNoSpaces(group.Name)));
            }
            Debug.Log("done with egg groups");

            /*
            Debug.Log("Get evolves from");
            this.evolvesFrom =PokeDatabase.PokemonDatabase.SanitizeString(speciesInfo.EvolvesFromSpecies.Name);
            Debug.Log("Got evolves from");
            */

            Debug.Log("Get evolution info");
            //throw new Exception("Need to implement pokemon color for pokedex search?");
            try
            {
                this.evolutionInfo = PokeDatabase.PokemonDatabase.EvolutionByPokemon[this.pokemonName];
            }
            catch (Exception err)
            {
                Debug.Log(err);
            }
            Debug.Log("Got evolution info");
        }

    }
}
