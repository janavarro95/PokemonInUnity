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

        

        public static void AddMove(MoveInfo Move)
        {
            if (!MovesByName.ContainsKey(Move.moveName))
            {
                MovesByName.Add(Move.moveName, Move);
            }
            if (!MovesByIndex.ContainsKey(Move.id))
            {
                MovesByIndex.Add(Move.id, Move);
            }
        }

        /// <summary>
        /// Sanitizes a string like rock-smash to Rock Smash
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string SanitizeString(string input)
        {
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


    }
}
