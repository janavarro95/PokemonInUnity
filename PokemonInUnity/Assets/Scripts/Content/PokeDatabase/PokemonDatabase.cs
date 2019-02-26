using Assets.Scripts.Content.GameContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Content.PokeDatabase
{
    public class PokemonDatabase
    {
        public static Dictionary<string, MoveInfo> MovesByName = new Dictionary<string, MoveInfo>();
        public static Dictionary<int, MoveInfo> MovesByIndex = new Dictionary<int, MoveInfo>();

        public static Dictionary<int, EvolutionInfo> EvolutionByIndex = new Dictionary<int, EvolutionInfo>();
        public static Dictionary<string, EvolutionInfo> EvolutionByPokemon = new Dictionary<string, EvolutionInfo>();

        public static Dictionary<int, PokemonInfo> PokemonInfoByIndex = new Dictionary<int, PokemonInfo>();
        public static Dictionary<string, PokemonInfo> PokemonInfoByName = new Dictionary<string, PokemonInfo>();
        

        public static void AddMove(MoveInfo Move)
        {
            string name = SanitizeString(Move.moveName);
            if (!MovesByName.ContainsKey(name))
            {
                MovesByName.Add(name, Move);
            }
            if (!MovesByIndex.ContainsKey(Move.id))
            {
                MovesByIndex.Add(Move.id, Move);
            }
        }

        public static void AddEvolutionInfoPokemon(EvolutionInfo Info)
        {
            string name = SanitizeString(Info.speciesName);
            if (!EvolutionByPokemon.ContainsKey(name))
            {
                EvolutionByPokemon.Add(name,Info);
            }
            if (!EvolutionByIndex.ContainsKey(Info.id))
            {
                EvolutionByIndex.Add(Info.id, Info);
            }
        }

        public static void AddPokemonInfo(PokemonInfo Poke)
        {
            string name = SanitizeString(Poke.pokemonName);
            if (!PokemonInfoByIndex.ContainsKey(Poke.pokedexNumber))
            {
                PokemonInfoByIndex.Add(Poke.pokedexNumber, Poke);
            }
            if (!PokemonInfoByName.ContainsKey(name))
            {
                PokemonInfoByName.Add(name, Poke);
            }
        }

        /// <summary>
        /// Sanitizes a string like rock-smash to Rock Smash
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string SanitizeString(string input)
        {
            if (String.IsNullOrEmpty(input)) return "";
            string[] strs = input.Split('-');
            if (strs.Length == 0)
            {
                return input.First().ToString().ToUpper() + input.Substring(1);
            }
            else
            {
                string name = "";
                for (int i = 0; i < strs.Length; i++)
                {
                    strs[i] = strs[i].First().ToString().ToUpper() + strs[i].Substring(1);
                    name += strs[i];
                    if (i < strs.Length - 1)
                    {
                        name += " ";
                    }
                }
                return name;
            }
        }

        public static string SanitizeStringNoSpaces(string input)
        {
            if (String.IsNullOrEmpty(input)) return "";
            string[] strs = input.Split('-');
            if (strs.Length == 0)
            {
                return input.First().ToString().ToUpper() + input.Substring(1);
            }
            else
            {
                string name = "";
                for (int i = 0; i < strs.Length; i++)
                {
                    strs[i] = strs[i].First().ToString().ToUpper() + strs[i].Substring(1);
                    name += strs[i];
                }
                return name;
            }
        }

        public static int GetURIID(string URI)
        {
            if (String.IsNullOrEmpty(URI)) return 0;
            string[] splitter=URI.Split('/');
            return Convert.ToInt32(splitter[splitter.Length - 2]);
        }

        public static string GetProperFlavorText(PokeAPI.VersionGroupFlavorText[] Texts)
        {
            foreach (var flavorInfo in Texts)
            {
                if (flavorInfo.Language.Name == "en" && flavorInfo.VersionGroup.Name == "ultra-sun-ultra-moon")
                {
                    return flavorInfo.FlavorText;
                }
            }
            return "";
        }

        public static string GetProperFlavorText(PokeAPI.PokemonSpeciesFlavorText[] Texts)
        {
            foreach (PokeAPI.PokemonSpeciesFlavorText flavorInfo in Texts)
            {
                if (flavorInfo.Language.Name == "en" && flavorInfo.Version.Name == "omega-ruby")
                {
                    return flavorInfo.FlavorText;
                }
            }
            return "";
        }

        public static KeyValuePair<Enums.MoveLearnedType,int> GetProperMoveLearnedInfo(PokeAPI.MoveVersionGroupDetails[] Texts)
        {
            foreach (var obj in Texts)
            {
                if ( obj.VersionGroup.Name == "ultra-sun-ultra-moon")
                {
                    return new KeyValuePair<Enums.MoveLearnedType, int>(Enums.ParseEnum<Enums.MoveLearnedType>(SanitizeStringNoSpaces(obj.LearnMethod.Name)),obj.LearnedAt);
                }
            }
            return new KeyValuePair<Enums.MoveLearnedType, int>();
        }
    }
}
