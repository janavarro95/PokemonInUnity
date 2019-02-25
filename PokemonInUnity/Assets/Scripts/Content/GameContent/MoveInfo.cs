using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Content.GameContent
{
    /// <summary>
    /// Deals with all of the necessary info for a move. Unfortunately contests are not supported at this moment. =(
    /// 
    /// Todo: Dream, add in contests.
    /// </summary>
    public class MoveInfo
    {
        public float accuracy;
        public int pp;
        public int priority;
        public int power;

        /// <summary>
        /// Descriptions of the effect on what it may do.
        /// </summary>
        public EffectInfo effects;

        /// <summary>
        /// The actual status effect this move inflicts.
        /// </summary>
        public string ailment;
        /// <summary>
        /// pysical/special
        /// </summary>
        public string category;

        public int minHits;
        public int maxHits;
        public int minTurns;
        public int maxTurns;

        /// <summary>
        /// How much this pokemon heals from dealing damage as a percent.
        /// </summary>
        public int drainRecoil;

        /// <summary>
        /// Abs value for how much is healed.
        /// </summary>
        public int healing;
        public int critRate;
        public float ailmentChance;
        public float flinchChance;

        /// <summary>
        /// Chance to change stats
        /// </summary>
        public float statChance;

        /// <summary>
        /// All stat changes that occur.
        /// </summary>
        public Dictionary<string,StatChangeInfo> statChanges;

        /// <summary>
        /// What this move targets.
        /// </summary>
        public Enums.TargetType targets;

        /// <summary>
        /// The elemental move type.
        /// </summary>
        public Enums.Type moveType;

        public string moveDescription;

        public string moveName;

        public int id;

        public MoveInfo()
        {

        }

        public MoveInfo(PokeAPI.Move Move)
        {

            this.accuracy = Move.Accuracy !=null ? Move.Accuracy.Value: default(float);
            this.pp = Move.PP !=null ? Move.PP.Value : default(int);
            this.priority = Move.Priority;
            this.power = Move.Power !=null ? Move.Power.Value : default(int);
            this.effects = new EffectInfo(Move.Effects[0].Effect, Move.Effects[0].ShortEffect);

            this.ailment = Move.Meta.Value.Ailment.Name;
            this.category = Move.Meta.Value.Category.Name;

            this.minHits = Move.Meta.Value.MinHits != null ? Move.Meta.Value.MinHits.Value : default(int);
            this.maxHits = Move.Meta.Value.MaxHits != null ? Move.Meta.Value.MaxHits.Value : default(int);
            this.minTurns = Move.Meta.Value.MinTurns != null ? Move.Meta.Value.MinTurns.Value : default(int);
            this.maxTurns = Move.Meta.Value.MaxTurns != null ? Move.Meta.Value.MaxTurns.Value : default(int);

            this.drainRecoil = Move.Meta.Value.DrainRecoil;
            this.healing = Move.Meta.Value.Healing;
            this.critRate = Move.Meta.Value.CritRate;
            this.ailmentChance = Move.Meta.Value.AilmentChance;
            this.flinchChance = Move.Meta.Value.FlinchChance;
            this.statChance = Move.Meta.Value.StatChance;

            this.statChanges = new Dictionary<string, StatChangeInfo>();
            foreach(var v in Move.StatChanges)
            {
                if (this.statChanges.ContainsKey(v.Stat.Name)) continue;
                else
                {
                    this.statChanges.Add(v.Stat.Name, new StatChangeInfo(v.Stat.Name, v.Change));
                }
            }

            string[] splits = Move.Target.Url.AbsolutePath.Split('/');
            this.targets =Enums.TargetTypeNameToEnum(Convert.ToInt32(splits[splits.Length - 2]));

            this.moveType = Enums.ParseEnum<Enums.Type>(Move.Type.Name);

            

            foreach(var flavorInfo in Move.FlavorTextEntries)
            {
                if(flavorInfo.Language.Name=="en" && flavorInfo.VersionGroup.Name== "ultra-sun-ultra-moon")
                {
                    this.moveDescription = flavorInfo.FlavorText;
                }
            }

            this.moveName = PokeDatabase.PokemonDatabase.SanitizeString(Move.Name);
            this.id = Move.ID;
        }
    }
}
