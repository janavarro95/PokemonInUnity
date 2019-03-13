using Assets.Scripts.Content.PokeDatabase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Content.GameContent
{
    
    public class Pokemon
    {
        public string Name
        {
            get
            {
                if (String.IsNullOrEmpty(nickName))
                {
                    return info.pokemonName;
                }
                else return nickName;
            }
        }

        public string nickName;

        public Move[] moves;

        public int currentLevel;

        public int currentHP;
        public int MaxHP
        {
            get
            {
                return (((((info.baseHP + IV_HP) * 2) + (int)(Math.Sqrt(EV_HP) / 4) * this.currentLevel) / 100) + currentLevel + 10);
            }
        }
        public int Attack
        {
            get
            {
                return (((((info.baseAttack + IV_Attack) * 2) + (int)(Math.Sqrt(EV_Attack) / 4) * this.currentLevel) / 100) + 5);
            }
        }
        public int Defense
        {
            get
            {
                return (((((info.baseDefense + IV_Defense) * 2) + (int)(Math.Sqrt(EV_Defense) / 4) * this.currentLevel) / 100) + 5);
            }
        }

        public int SpecialAttack
        {
            get
            {
                return (((((info.baseSpecialAttack + IV_SpecialAttack) * 2) + (int)(Math.Sqrt(EV_SpecialAttack) / 4) * this.currentLevel) / 100) + 5);
            }
        }

        public int SpecialDefense
        {
            get
            {
                return (((((info.baseSpecialDefense + IV_SpecialDefense) * 2) + (int)(Math.Sqrt(EV_SpecialDefense) / 4) * this.currentLevel) / 100) + 5);
            }
        }
        public int Speed
        {
            get
            {
                return (((((info.baseSpeed + IV_Speed) * 2) + (int)(Math.Sqrt(EV_Speed) / 4) * this.currentLevel) / 100) + 5);
            }
        }

        public int EV_HP;
        public int EV_Attack;
        public int EV_Defense;
        public int EV_SpecialAttack;
        public int EV_SpecialDefense;
        public int EV_Speed;

        public int IV_HP;
        public int IV_Attack;
        public int IV_Defense;
        public int IV_SpecialAttack;
        public int IV_SpecialDefense;
        public int IV_Speed;


        public int currentEXP;
        public int EXPToLVLUp
        {
            get
            {
                return ExperienceCalculator.ExperienceToNextLevel(this.currentLevel + 1, info.growthRate);
            }
        }

        public PokemonInfo info;

        [JsonIgnore]
        public Sprite backSprite;
        [JsonIgnore]
        public Sprite frontSprite;
        [JsonIgnore]
        public Sprite menuSprite;

        [JsonIgnore]
        public AudioClip cry;


        /// <summary>
        /// Checks to see if a Pokemon already knows 4 moves.
        /// </summary>
        public bool KnowsFourMoves
        {
            get
            {
                foreach(Move m in this.moves)
                {
                    if (m == null) return false;
                }
                return true;
            }
        }

        public Pokemon()
        {

        }

        public Pokemon(PokemonInfo info, int StartingLevel)
        {
            this.info = info;
            this.moves = new Move[4];
            this.currentLevel = StartingLevel;

            learnInitialMoves();
            generateIVS(new EV_IVInfo());
            heal();
            loadSprites();
            loadCry();
        }


        public Pokemon(PokemonInfo info, int StartingLevel,List<Move> GuarenteedMovesToHave,EV_IVInfo IVS=null)
        {
            this.info = info;
            this.moves = new Move[4];
            this.currentLevel = StartingLevel;

            for(int i=0; i < GuarenteedMovesToHave.Count; i++)
            {
                learnMoveIntoEmptySlot(GuarenteedMovesToHave[i]);
            }

            learnInitialMoves();
            generateIVS(IVS != null ? IVS : new EV_IVInfo());
            heal();
            loadSprites();
            loadCry();
        }



        private void learnInitialMoves()
        {
            List<Move> learnable = new List<Move>();
            foreach(var v in this.info.learnableMoves[Enums.MoveLearnedType.LevelUp])
            {
                if (this.currentLevel >= v.Value && v.Value!=0) learnable.Add(PokemonDatabase.GetMove(v.Key));
            }

            for(int i = learnable.Count - 1; i >= 0; i--)
            {
                if (this.KnowsFourMoves == false)
                {
                    learnMoveIntoEmptySlot(learnable[i]);
                }
            }
        }

        private void learnMove(int index,Move m)
        {
            this.moves[index] = m;
        }
        private void learnMoveIntoEmptySlot(Move m)
        {
            for(int i = 0; i < 4; i++)
            {
                if (moves[i] == null)
                {
                    moves[i] = m;
                    return;
                }
            }
        }
        private void generateIVS()
        {
            this.IV_HP=UnityEngine.Random.Range(0, 32);
            this.IV_Attack=UnityEngine.Random.Range(0, 32);
            this.IV_Defense=UnityEngine.Random.Range(0, 32);
            this.IV_SpecialAttack=UnityEngine.Random.Range(0, 32);
            this.IV_SpecialDefense=UnityEngine.Random.Range(0, 32);
            this.IV_Speed=UnityEngine.Random.Range(0, 32);
        }

        private void generateIVS(EV_IVInfo IVS)
        {
            this.IV_HP = IVS.hp!=-1? IVS.hp : UnityEngine.Random.Range(0, 32);
            this.IV_Attack = IVS.attack!=-1? IVS.attack : UnityEngine.Random.Range(0, 32);
            this.IV_Defense = IVS.defense!=-1? IVS.defense : UnityEngine.Random.Range(0, 32);
            this.IV_SpecialAttack = IVS.specialAttack!=-1? IVS.specialAttack : UnityEngine.Random.Range(0, 32);
            this.IV_SpecialDefense = IVS.specialDefense!=-1? IVS.specialDefense : UnityEngine.Random.Range(0, 32);
            this.IV_Speed = IVS.speed!=-1? IVS.speed : UnityEngine.Random.Range(0, 32);
        }

        /// <summary>
        /// Heals a pokemon fully.
        /// </summary>
        private void heal()
        {
            this.currentHP = this.MaxHP;
        }

        public bool canLevelUp()
        {
            return this.currentEXP >= EXPToLVLUp;
        }


        public void gainEXP(int amount)
        {
            this.currentEXP += amount;
            levelUp();
        }

        /// <summary>
        /// Levels up a pokemon if they meet the exp requirement.
        /// </summary>
        public void levelUp()
        {
            if (canLevelUp())
            {
                this.currentEXP -= this.EXPToLVLUp;
                this.currentLevel++;
                checkForNewMovesToLearn();
            }
        }

        private void checkForNewMovesToLearn()
        {
            List<Move> learnable = new List<Move>();
            foreach (var v in this.info.learnableMoves[Enums.MoveLearnedType.LevelUp])
            {
                if (this.currentLevel == v.Value && v.Value != 0) learnable.Add(PokemonDatabase.GetMove(v.Key));
            }

            for (int i = learnable.Count - 1; i >= 0; i--)
            {
                if (this.KnowsFourMoves == false)
                {
                    learnMoveIntoEmptySlot(learnable[i]);
                }
            }
        }


        private void loadSprites()
        {
            try
            {
                Debug.Log("NUMBER: "+this.info.pokedexNumber);
                frontSprite = ContentManager.Instance.loadTextureFrom2DAtlas(Path.Combine("Graphics", "PokemonGen1"), "PokemonGen1_" + ((this.info.pokedexNumber - 1) * 2).ToString());
                backSprite = ContentManager.Instance.loadTextureFrom2DAtlas(Path.Combine("Graphics", "PokemonBacks"), "PokemonBacks_" + ((this.info.pokedexNumber - 1)).ToString());
                menuSprite = ContentManager.Instance.loadTextureFrom2DAtlas(Path.Combine("Graphics", "PokemonMenuSprites"), "PokemonMenuSprites_" + ((this.info.pokedexNumber - 1)).ToString());

                frontSprite.texture.filterMode = FilterMode.Point;
                backSprite.texture.filterMode = FilterMode.Point;
                menuSprite.texture.filterMode = FilterMode.Point;
            }
            catch(Exception err)
            {
                Debug.Log(err.ToString());
            }
        }

        private void loadCry()
        {
            string name = "";
            if (info.pokedexNumber < 10)
            {
                name = "00" + info.pokedexNumber.ToString() + "Cry";
            }
            else if (info.pokedexNumber < 100)
            {
                name="0" + info.pokedexNumber.ToString() + "Cry";
            }
            else
            {
                name= info.pokedexNumber.ToString() + "Cry";
            }

            AudioClip cry = Resources.Load<AudioClip>(Path.Combine("Audio", "PokemonCries", name));
            this.cry = cry;
        }
    }
}
