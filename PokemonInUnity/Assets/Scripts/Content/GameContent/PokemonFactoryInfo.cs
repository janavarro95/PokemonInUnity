using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Content.GameContent
{
    [Serializable]
    public class PokemonFactoryInfo
    {
        public string PokemonName;
        public int PokeDexNo;
        public int LVL;

        public EV_IVInfo IVS;



        /// <summary>
        /// Used to guarentee this pokemon has this move.
        /// </summary>
        public string move1Name;
        /// <summary>
        /// Used to guarentee this pokemon has this move.
        /// </summary>
        public string move2Name;
        /// <summary>
        /// Used to guarentee this pokemon has this move.
        /// </summary>
        public string move3Name;
        /// <summary>
        /// Used to guarentee this pokemon has this move.
        /// </summary>
        public string move4Name;
        /// <summary>
        /// Used to guarentee this pokemon has this move.
        /// </summary>


        public Pokemon generatePokemon()
        {
            if (!String.IsNullOrEmpty(PokemonName))
            {
                List<Move> moves = new List<Move>();
                PokemonInfo info = PokeDatabase.PokemonDatabase.PokemonInfoByName[PokemonName];
                if (!String.IsNullOrEmpty(move1Name))
                {
                    moves.Add(PokeDatabase.PokemonDatabase.GetMove(move1Name));
                }
                if (!String.IsNullOrEmpty(move2Name))
                {
                    moves.Add(PokeDatabase.PokemonDatabase.GetMove(move2Name));
                }
                if (!String.IsNullOrEmpty(move3Name))
                {
                    moves.Add(PokeDatabase.PokemonDatabase.GetMove(move3Name));
                }
                if (!String.IsNullOrEmpty(move4Name))
                {
                    moves.Add(PokeDatabase.PokemonDatabase.GetMove(move4Name));
                }
                Pokemon poke = new Pokemon(info, LVL, moves, IVS);
                return poke;
            }
            else
            {
                if ((PokeDexNo != 0))
                {
                    List<Move> moves = new List<Move>();
                    PokemonInfo info = PokeDatabase.PokemonDatabase.PokemonInfoByIndex[PokeDexNo];
                    if (!String.IsNullOrEmpty(move1Name))
                    {
                        moves.Add(PokeDatabase.PokemonDatabase.GetMove(move1Name));
                    }
                    if (!String.IsNullOrEmpty(move2Name))
                    {
                        moves.Add(PokeDatabase.PokemonDatabase.GetMove(move2Name));
                    }
                    if (!String.IsNullOrEmpty(move3Name))
                    {
                        moves.Add(PokeDatabase.PokemonDatabase.GetMove(move3Name));
                    }
                    if (!String.IsNullOrEmpty(move4Name))
                    {
                        moves.Add(PokeDatabase.PokemonDatabase.GetMove(move4Name));
                    }
                    Pokemon poke = new Pokemon(info, LVL, moves, IVS);
                    return poke;
                }
                return null;
            }
        }

    }



}
