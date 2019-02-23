﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Content.GameContent
{
    public class MoveInfo
    {
        public double accuracy;
        public int pp;
        public int priority;
        public int power;

        public EffectInfo effects;

        public string ailment;
        public string category;

        public int minHits;
        public int maxHits;
        public int minTurns;
        public int maxTurns;

        public int drainRecoil;
        public int healing;
        public int critRate;
        public int ailmentChance;
        public int flinchChance;
        public int statChance;

        public List<StatChangeInfo> statChanges;

        /// <summary>
        /// Look into this more:https://pokeapi.co/api/v2/move-target/10/
        /// </summary>
        public string target;

        public Enums.Type moveType;

        public string moveDescription;

        public string moveName;

        public int id;

        public MoveInfo()
        {

        }

        public MoveInfo(PokeAPI.Move Move)
        {
            throw new NotImplementedException("DO STUFF HERE");
        }
    }
}