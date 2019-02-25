using PokeAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Content.GameContent
{
    public class PokemonInfo
    {
        string pokemonName;

        public List<string> learnableMoves;
        /*
        Move[] Moves;
        public bool knowsFourMoves
        {
            get
            {
                for(int i = 0; i <= 3; i++)
                {
                    if (Moves[i] == null) return false;
                }
                return true;
            }
        }
        */

        public int baseHP;
        public int baseAttack;
        public int baseDefense;
        public int baseSpecialAttack;
        public int baseSpecialDefense;
        public int baseSpeed;

        public List<Enums.Type> types;

        public int baseExperience;

        public float genderRateRaw;
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
        public int pokedexNumber;

        public List<Enums.EggGroup> eggGroups;

        public PokemonInfo()
        {
            
        }

        public PokemonInfo(PokeAPI.Pokemon pokeInfo, PokemonSpecies speciesInfo,int dexNumber)
        {
            this.genderRateRaw = speciesInfo.FemaleToMaleRate.Value;
            this.captureRateRaw = speciesInfo.CaptureRate;
            this.baseHappiness = speciesInfo.BaseHappiness;
            this.hatchCounter = speciesInfo.HatchCounter;
            this.hasMultipleForms = speciesInfo.FormsAreSwitchable;

            this.growthRate = Enums.ParseEnum<Enums.ExperienceGrowthRate>(PokeDatabase.PokemonDatabase.SanitizeStringNoSpaces(speciesInfo.GrowthRate.Name));
            this.pokedexNumber = dexNumber;

            this.eggGroups = new List<Enums.EggGroup>();
            foreach(var group in speciesInfo.EggGroups)
            {
               eggGroups.Add( Enums.ParseEnum<Enums.EggGroup>(PokeDatabase.PokemonDatabase.SanitizeStringNoSpaces(group.Name)));
            }


            //throw new Exception("Need to implement pokemon color for pokedex search?");

            throw new Exception("Need to implement evolves from pokemon.");
            throw new Exception("Need to implement evolution chain!");
            throw new Exception("Need to implement dex description!");

            this.pokemonName =PokeDatabase.PokemonDatabase.SanitizeString(pokeInfo.Name);

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

            learnableMoves = new List<string>();
            foreach(var v in pokeInfo.Moves)
            {
                learnableMoves.Add(PokeDatabase.PokemonDatabase.SanitizeString(v.Move.Name));
            }
            foreach(var type in pokeInfo.Types)
            {
                types.Add(Enums.ParseEnum<Enums.Type>(type.Type.Name));
            }
             
        }

        public bool canLearnMove(MoveInfo move)
        {
            if (this.learnableMoves.Contains(move.moveName)) return true;
            return false;
        }

    }
}
