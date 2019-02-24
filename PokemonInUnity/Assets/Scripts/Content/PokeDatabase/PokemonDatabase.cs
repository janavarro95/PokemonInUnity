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


    }
}
